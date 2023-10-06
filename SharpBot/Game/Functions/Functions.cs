using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SharpBot.Properties;
using Binarysharp.MemoryManagement;
using System.Runtime.InteropServices;
using SharpBot.SharpGUI;
using System.Windows.Forms;
using Binarysharp.MemoryManagement.Memory;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.ConstrainedExecution;

namespace SharpBot.Game.Functions
{
    internal class Functions
    {
        //Hook Function to Our
        public void Hook(string[] asm)
        {
            try
            {
                //MemorySharp Object
                MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

                //Resolve Addresses
                IntPtr HOOK_ADDRESS = new IntPtr(0x00658B2A);
                IntPtr ORIGINAL_FUNCTION = new IntPtr(0x006587D0);

                //memory allocation
                var hook_mem = sharp.Memory.Allocate(1); //assembly code 
                var unhook_flag = sharp.Memory.Allocate(1); //unhook flag

                //create our function
                string[] hook_function = new[] {
                    "pushad", //save registers
                    "pushfd" //save flags
                };
                hook_function = hook_function.Concat(asm).ToArray(); //insert arbitrary code
                hook_function = hook_function.Concat(new[] {
                    "popfd", //restore flags
                    "popad", //restore registers
                    "call " + (uint)ORIGINAL_FUNCTION, //call original function
                    "mov byte [" + (uint)unhook_flag.BaseAddress + "], 0x1", //set unhook flag to 1
                    "jmp " + ((uint)HOOK_ADDRESS + 5), //jmp back to original return ebp
                }).ToArray();


                //inject full function into allocated memory
                sharp.Assembly.Inject(hook_function, hook_mem.BaseAddress);

                //set hook in memory
                sharp.Assembly.Inject(
                    new[]
                    {
                    "jmp " + (uint)hook_mem.BaseAddress, //set hook to our func address
                    },
                    HOOK_ADDRESS);

                //wait for unhook flag is set to 1
                while (sharp[unhook_flag.BaseAddress, false].Read<byte>() == 0)
                {
                }

                //restore original instruction
                sharp.Assembly.Inject(
                    new[]
                    {
                    "call " + (uint)ORIGINAL_FUNCTION,
                    },
                    HOOK_ADDRESS);

                Thread.Sleep(20); //make sure EBP left injected code

                sharp.Memory.Deallocate(hook_mem); //memory cleaning
                sharp.Memory.Deallocate(unhook_flag); //memory cleaning
            }
            catch { }

            return;
        }
        public void ResetInstances()
        {
            //memsharp instance
            MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //Hook ResetInstances()
            for (int i = 1; i < 5; i++)
            {
                Lua("ResetInstances();");
                Thread.Sleep(100);
            }
        }

        public bool vanishIfSpotted()
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //vanish if mob resisted pickpocket or Sap spell
            var vanished = false;
            var isStealthAddress = new IntPtr(0xBC6C80); //4C dont work anymore, 80 keeps good
            if (sharp[isStealthAddress, false].Read<int>() != 1)
            {
                Thread.Sleep(3200);
                while (sharp[isStealthAddress, false].Read<int>() == 0)
                {
                    Lua("CastSpellByName(\"Vanish\")");
                    Thread.Sleep(100);
                }
                vanished = true;
            }
            if (vanished == true)
            {
                Thread.Sleep(210000);
                return true;
            }
            return false;
        }


        //sap and pickpocket only works in turtle wow due to improved sap modification
        public void PickPocket(ulong guid)
        {
            //memsharp instance
            MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //wait energy for sap
            while (getPlayerEnergy() < 65)
            {
                Thread.Sleep(100);
            }

            //Select Target
            Target(guid);

            /*
            Sap only works in turtle wow due to improved sap modification
            If you not playing in turtle wow, switch Sap to Pick Pocket
            and also comment out the next two lines of code
            */
            Lua("CastSpellByName(\"Sap\")");
            Thread.Sleep(1000);
            Lua("CastSpellByName(\"Pick Pocket\")");
            Thread.Sleep(1000);

            //if not vanished, pickpocket mob
            if (vanishIfSpotted() == false)
            {
                AutoLoot();
            }
}

        public void Lua(string str)
        {
            //memsharp object
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //allocate memory
            var mem_str = sharp.Memory.Allocate(1);
            mem_str.WriteString(str);

            //write lua func
            IntPtr Lua = new IntPtr(0x00704CD0);
            string[] asm = {
                "mov edx, " + (uint)mem_str.BaseAddress,
                "mov ecx, " + (uint)mem_str.BaseAddress,
                "call " + (uint)Lua
            };

            //call func
            Hook(asm);

            //free memory
            sharp.Memory.Deallocate(mem_str);
        }
        public void AutoLoot()
        {
            string[] asm = {
                "mov ecx, 0",
                "call " + (uint)0x004C1FA0,
            };
            Hook(asm); 
        }

        public int getPlayerEnergy()
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //get current player guid
            IntPtr pInstance = GetPlayerPtr();
            pInstance = pInstance - 0x8;//fix for offset calc

            //read player energy
            var energy = sharp[pInstance + 0x1DE0, false].Read<int>();
            return energy;
        }

        public IntPtr GetPlayerPtr()
        {
            try
            {
                //MemorySharp Object
                MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

                //Resolve Offsets For PlayerGUID
                var ObjMgrPointer = new IntPtr(0x00B41414);
                var ObjMgr = sharp[ObjMgrPointer, false].Read<IntPtr>();
                var PlayerGuid = sharp[ObjMgr + 0xC0, false].Read<UInt64>();

                //Resolve Parameters
                var RetObjFromPtr = 0x00464870;
                var lastBytes = (PlayerGuid >> 32);
                var firstBytes = (PlayerGuid & 0xFFFFFFFF);

                //Calling assembly
                using (var memory = sharp.Memory.Allocate(1))
                {
                    var ret = sharp.Assembly.InjectAndExecute<IntPtr>(new[]
                    {
                    "mov eax, " + lastBytes,
                    "mov ecx, " + firstBytes,
                    "push eax",
                    "push ecx",
                    "call " + RetObjFromPtr,
                    "retn"
                    }, memory.BaseAddress);
                    return ret;
                }
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        public void Move(uint z, uint x, uint y)
        {
            //MemorySharp Object
            MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //Resolve Offsets
            IntPtr MoveFunc = new IntPtr(0x00611130);
            var object_guid = (ulong)0;
            var destination = new uint[3] { z, x, y };

            //Allocate vars in memory
            var mem_obj = sharp.Memory.Allocate(1); //stores the obj guid to interact with
            mem_obj.Write(object_guid);
            var mem_destination = sharp.Memory.Allocate(1);
            mem_destination.Write(destination);

            //vars
            uint zOld = 0;
            uint xOld = 0;
            bool near = false;
            IntPtr pInstance = GetPlayerPtr();
            IntPtr zPosAddr = new IntPtr(0x00BC831C);
            IntPtr xPosAddr = new IntPtr(0x00BC8320);
            IntPtr fixStutter = new IntPtr(0x00860A90);

            //fix movement stutter
            if (sharp[fixStutter, false].Read<int>() != 0)
            {
                sharp[fixStutter, false].Write<int>(0);
            }

            //Inject and Execute Asm
            string[] asm = {
            "mov ecx, " + (uint)pInstance,
            "push 0",
            "push " + (uint)mem_destination.BaseAddress,
            "push " + (uint)mem_obj.BaseAddress,
            "push 4",
            "call " + (uint)MoveFunc
            };
            Hook(asm);

            //Check if near
            while (near == false)
            {
                //get current positions
                Thread.Sleep(50);
                uint zPos = sharp[zPosAddr, false].Read<uint>();
                uint xPos = sharp[xPosAddr, false].Read<uint>();

                //check if in same position
                if (zOld == zPos && xOld == xPos)
                {
                    near = true;
                }

                //set current position as old for checking if moved on next step
                zOld = zPos;
                xOld = xPos;

                //Verify if not stealth
                vanishIfSpotted();
            }
            
            //avoid memory leak
            sharp.Memory.Deallocate(mem_obj);
            sharp.Memory.Deallocate(mem_destination);
        }

        public void MoveAndJump(uint z, uint x, uint y)
        {
            //MemorySharp Object
            MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //Resolve Offsets
            IntPtr MoveFunc = new IntPtr(0x00611130);
            var object_guid = (ulong)0;
            var destination = new uint[3] { z, x, y };

            //Allocate vars in memory
            var mem_obj = sharp.Memory.Allocate(1); //stores the obj guid to interact with
            mem_obj.Write(object_guid);
            var mem_destination = sharp.Memory.Allocate(1);
            mem_destination.Write(destination);

            //vars
            uint zOld = 0;
            uint xOld = 0;
            bool jump = false;
            bool near = false;
            IntPtr pInstance = GetPlayerPtr();
            IntPtr zPosAddr = new IntPtr(0x00BC831C);
            IntPtr xPosAddr = new IntPtr(0x00BC8320);
            IntPtr fixStutter = new IntPtr(0x00860A90);

            //fix movement stutter
            if (sharp[fixStutter, false].Read<int>() != 0)
            {
                sharp[fixStutter, false].Write<int>(0);
            }


            //Inject and Execute Asm
            string[] asm = {
            "mov ecx, " + (uint)pInstance,
            "push 0",
            "push " + (uint)mem_destination.BaseAddress,
            "push " + (uint)mem_obj.BaseAddress,
            "push 4",
            "call " + (uint)MoveFunc
            };
            Hook(asm);

            //tick for jump while walking
            Thread.Sleep(50);

            while (near == false)
            {
                //get current positions
                uint zPos = sharp[zPosAddr, false].Read<uint>();
                uint xPos = sharp[xPosAddr, false].Read<uint>();

                //check if jumped and in same position
                if (zOld == zPos && xOld == xPos && jump == true)
                {
                    near = true;
                }
                if (jump == false)
                {
                    Jump();
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep(1000);
                }
                jump = !jump;

                //set current position as old for checking if moved on next step
                zOld = zPos;
                xOld = xPos;

                //hook again just to make sure it is where we want
                Hook(asm);
            }
           //avoid conflict with Move Func
            Thread.Sleep(1000); 

            //avoid memory leak
            sharp.Memory.Deallocate(mem_obj);
            sharp.Memory.Deallocate(mem_destination);
        }

        public void avoidAFK()
        {
            //memsharp obj
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //vars
            IntPtr afkAddr = new IntPtr(0x00CF0BC8);

            //get main window
            var window = sharp.Windows.MainWindow;
            window.Keyboard.PressRelease(Binarysharp.MemoryManagement.Native.Keys.Space);

            //write tick count
            var curTick = sharp[afkAddr, false].Read<int>();
            sharp[afkAddr, false].Write<int>(curTick + 1);

            //wait char's fall
            Thread.Sleep(1000);
        }

        public IntPtr GetEntityByGuid(ulong guid)
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //vars
            IntPtr entityBase = new IntPtr(0xB41414);
            var firstEntityOffset = 0xAC;
            var nextEntityOffset = 0x3C;
            var entityIdOffset = 0x30;

            var entityList = sharp[entityBase, false].Read<IntPtr>();
            var curEntity = sharp[entityList + firstEntityOffset, false].Read<IntPtr>();

            try
            {
                while (true)
                {
                    ulong currentEntityId = sharp[curEntity + entityIdOffset, false].Read<ulong>();
                    if (currentEntityId == guid)
                        return curEntity;
                    curEntity = sharp[curEntity + nextEntityOffset, false].Read<IntPtr>();
                }
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        public void InteractWithObject(ulong guid)
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //addresses
            IntPtr OnRightClick = new IntPtr(0x005F8660);
            IntPtr objPointer = GetEntityByGuid(guid);

            //write func
            string[] asm =
            {
                "push 0",
                "mov ecx, " + objPointer,
                "call " + OnRightClick,
            };

            //execute asm
            Hook(asm);

            //casting time
            Thread.Sleep(6000);
        }

        public void Target(ulong guid)
        {
            //MemorySharp Object
            MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //Resolve Offsets
            IntPtr SelectedTarget = new IntPtr(0x00B4E2D8);

            //Write to Selected Target Address
            sharp[SelectedTarget, false].Write<ulong>(guid);
        }

        public void Jump()
        {
            //memsharp obj
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //get main window
            var window = sharp.Windows.MainWindow;

            //send space key
            window.Keyboard.PressRelease(Binarysharp.MemoryManagement.Native.Keys.Space);
        }


        public void MoveOut()
        {
            //memsharp obj
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //get main window
            var window = sharp.Windows.MainWindow;

            //get current player state
            var state = GetPlayerPtr();

            //wait for change player state
            while (state == GetPlayerPtr())
            {
                Thread.Sleep(100);
                //send space key
                window.Keyboard.Press(Binarysharp.MemoryManagement.Native.Keys.W);
            }

            //release key
            window.Keyboard.Release(Binarysharp.MemoryManagement.Native.Keys.W);

            //wait for game to load
            while (GetPlayerPtr() == IntPtr.Zero)
            {
                Thread.Sleep(100);
            }
            Thread.Sleep(5000);
        }

        public void MoveIn()
        {
            //memsharp obj
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //get main window
            var window = sharp.Windows.MainWindow;

            //get current player state
            var state = GetPlayerPtr();

            //wait for change player state
            while (state == GetPlayerPtr())
            {
                Thread.Sleep(100);
                //send space key
                window.Keyboard.Press(Binarysharp.MemoryManagement.Native.Keys.S);
            }

            //release key
            window.Keyboard.Release(Binarysharp.MemoryManagement.Native.Keys.S);

            //wait for game to load
            while (GetPlayerPtr() == IntPtr.Zero)
            {
                Thread.Sleep(100);
            }
            Thread.Sleep(5000);
        }
    }
}