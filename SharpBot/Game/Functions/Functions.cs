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
                    "mov word [" + (uint)unhook_flag.BaseAddress + "], 1", //set unhook flag to 1
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
                while (sharp[unhook_flag.BaseAddress, false].Read<int>() == 0)
                {
                }

                //restore original instruction
                sharp.Assembly.Inject(
                    new[]
                    {
                    "call " + (uint)ORIGINAL_FUNCTION,
                    },
                    HOOK_ADDRESS);

                Thread.Sleep(50); //make sure EBP left injected code

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

        public void VanishIfSpotted()
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //vanish if mob resisted pickpocket or Sap spell
            var isStealth = new IntPtr(0x00BC6CA0); //this one is perfect

            //check if not in stealth
            var isSpotted = sharp[isStealth, false].Read<int>();
            if (isSpotted != 1)
            {
                Thread.Sleep(2500);
                while (isSpotted != 1)
                {
                    isSpotted = sharp[isStealth, false].Read<int>();
                    Lua("CastSpellByName(\"Vanish\")");
                    Thread.Sleep(10);
                }
                Thread.Sleep(180000);
            }
        }


        //sap and pickpocket only works in turtle wow due to improved sap modification
        //remember that pickpocket spell is nerfed in turtle wow, mobs resist almost all the time 
        public void PickPocket(ulong guid)
        {
            //memsharp instance
            MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //Select Target
            Target(guid);

            Lua("CastSpellByName(\"Pick Pocket\")");
            Thread.Sleep(500);
            AutoLoot();

            //if not vanished, pickpocket mob
            VanishIfSpotted();
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
                "call " + Lua,
            };

            //call func
            Hook(asm);

            //free memory
            sharp.Memory.Deallocate(mem_str);
        }
        public void AutoLoot()
        {
            //var
            IntPtr AutoLoot = new IntPtr(0x004C1FA0);
            string[] asm = {
                "mov ecx, 0",
                "call " + AutoLoot,
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
                    "mov eax, " + (uint)lastBytes,
                    "mov ecx, " + (uint)firstBytes,
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
            var mem_func = sharp.Memory.Allocate(1);

            //vars
            uint zOld = 0;
            uint xOld = 0;
            bool near = false;
            IntPtr pInstance = GetPlayerPtr();
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
            "call " + MoveFunc,
            };
            Hook(asm);

            //Check if near
            while (near == false)
            {
                //get current positions
                Thread.Sleep(50);
                uint zPos = sharp[pInstance + 0x9B8, false].Read<uint>();
                uint xPos = sharp[pInstance + 0x9BC, false].Read<uint>();

                //check if in same position or near
                if (zOld == zPos && xOld == xPos)
                {
                    near = true;
                }
                else if (Math.Abs((int)zPos - (int)z) < 1000 && Math.Abs((int)xPos - (int)x) < 1000)
                {
                    break;
                }

                //set current position as old for checking if moved on next step
                zOld = zPos;
                xOld = xPos;

                //Verify if not stealth
                VanishIfSpotted();
            }
            
            //avoid memory leak
            sharp.Memory.Deallocate(mem_obj);
            sharp.Memory.Deallocate(mem_destination);
            sharp.Memory.Deallocate(mem_func);
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
            var mem_func = sharp.Memory.Allocate(1);

            //vars
            uint zOld = 0;
            uint xOld = 0;
            bool near = false;
            IntPtr pInstance = GetPlayerPtr();
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
            "call " + (uint)MoveFunc,
            };
            Hook(asm);

            //tick for jump while walking
            Thread.Sleep(100);

            //jump
            Jump();

            //check if near loop
            while (near == false)
            {
                //get current positions
                uint zPos = sharp[pInstance + 0x9B8, false].Read<uint>();
                uint xPos = sharp[pInstance + 0x9BC, false].Read<uint>();

                //check if in same position
                if (zOld == zPos && xOld == xPos)
                {
                    near = true;
                }
                else if (Math.Abs((int)zPos - (int)z) < 1000 && Math.Abs((int)xPos - (int)x) < 1000)
                {
                    break;
                }

                //set current position as old for checking if moved on next step
                zOld = zPos;
                xOld = xPos;

                //Verify if not stealth
                VanishIfSpotted();
            }

            //avoid memory leak
            sharp.Memory.Deallocate(mem_obj);
            sharp.Memory.Deallocate(mem_destination);
            sharp.Memory.Deallocate(mem_func);
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
            var firstEntity = true;
            var firstEntityOffset = 0xAC;
            var nextEntityOffset = 0x3C;
            var entityIdOffset = 0x30;
            var entityBase = new IntPtr(0x00B41414);

            var curEntity = sharp[entityBase, false].Read<IntPtr>();

            try
            {
                while (true)
                {
                    ulong currentEntityId = sharp[curEntity + entityIdOffset, false].Read<ulong>();
                    if (currentEntityId == guid)
                        return curEntity;
                    if (firstEntity == true)
                    {
                        curEntity = sharp[curEntity + firstEntityOffset, false].Read<IntPtr>();
                        firstEntity = false;
                    }
                    else
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
                "mov ecx, " + (uint)objPointer,
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

        public void waitLoadscreen()
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //read isIngame var
            IntPtr isIngame = new IntPtr(0x00B4B424);
            while (sharp[isIngame, false].Read<int>() == 1)
            {
                Thread.Sleep(100);
            }
            while (sharp[isIngame, false].Read<int>() == 0)
            {
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
        }

        public void MoveOut()
        {
            //memsharp obj
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //get main window
            var window = sharp.Windows.MainWindow;

            //press key
            window.Keyboard.Press(Binarysharp.MemoryManagement.Native.Keys.W);

            //wait for loadscreen end
            waitLoadscreen();

            //release key
            window.Keyboard.Release(Binarysharp.MemoryManagement.Native.Keys.W);
        }

        public void MoveIn()
        {
            //memsharp obj
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //get main window
            var window = sharp.Windows.MainWindow;

            //press key
            window.Keyboard.Press(Binarysharp.MemoryManagement.Native.Keys.S);

            //wait for loadscreen end
            waitLoadscreen();

            //release key
            window.Keyboard.Release(Binarysharp.MemoryManagement.Native.Keys.S);
        }

        public void Teleport(uint z, uint x, uint y)
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //vars
            var lPlayer = GetPlayerPtr();
            var jmpBackAddr = new IntPtr(0x005F1F27);
            var updPosition = new IntPtr(0x007C4930);
            var hookAddress = new IntPtr(0x005F1F22);
            var unhook_flag = sharp.Memory.Allocate(1);

            //call update movement
            var mem_call = sharp.Memory.Allocate(1);
            string[] c_asm = {
                "call " + updPosition,
                "jmp " + jmpBackAddr,
            };
            sharp.Assembly.Inject(c_asm, mem_call.BaseAddress);

            //inject exploit
            var zPos = sharp[lPlayer + 0x9B8, false].Read<uint>();
            var xPos = sharp[lPlayer + 0x9BC, false].Read<uint>();
            var yPos = sharp[lPlayer + 0x9C0, false].Read<uint>();
            var mem_func = sharp.Memory.Allocate(1);
            string[] asm = {
                "cmp dword[eax], " + zPos,
                "jne " + mem_call.BaseAddress,
                "cmp dword[eax+4], " + xPos,
                "jne " + mem_call.BaseAddress,
                "cmp dword[eax+8], " + yPos,
                "jne " + mem_call.BaseAddress,
                "mov dword [eax], " + z,
                "mov dword [eax+4], " + x,
                "mov dword [eax+8], " + y,
                "mov dword [" + unhook_flag.BaseAddress + "], 1",
                "jmp " + mem_call.BaseAddress,
            };
            sharp.Assembly.Inject(asm, mem_func.BaseAddress);

            //set exploit jmp
            sharp.Assembly.Inject(new[] {
                "jmp " + mem_func.BaseAddress,
            }, hookAddress);

            //wait for unhook flag
            while (sharp[unhook_flag.BaseAddress, false].Read<int>() == 0)
            {
            }

            //unhook
            sharp.Assembly.Inject(new[] {
                "call " + updPosition,
            }, hookAddress);

            //wait eip to leave function
            Thread.Sleep(150);

            //avoid memory leak
            sharp.Memory.Deallocate(mem_call);
            sharp.Memory.Deallocate(mem_func);
            sharp.Memory.Deallocate(unhook_flag);

            //update movement sending key (undetected)
            sharp.Windows.MainWindow.Keyboard.PressRelease(Binarysharp.MemoryManagement.Native.Keys.D);
            sharp.Windows.MainWindow.Keyboard.PressRelease(Binarysharp.MemoryManagement.Native.Keys.A);

        }
        public void TeleportPickPocket(ulong guid, int distance)
        {
            try
            {
                //memsharp instance
                var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

                //get entity
                var entity = GetEntityByGuid(guid);

                //vars
                uint z;
                uint x;
                uint y;
                ulong EntityId;
                var OffsetZ = 0x9B8;
                var OffsetX = 0x9BC;
                var OffsetY = 0x9C0;
                var EntityIdOffset = 0x30;

                //read x, y, z and mob guid
                z = sharp[entity + OffsetZ, false].Read<uint>();
                x = sharp[entity + OffsetX, false].Read<uint>();
                y = sharp[entity + OffsetY, false].Read<uint>();
                EntityId = sharp[entity + EntityIdOffset, false].Read<ulong>();

                //teleport above
                var newY = (uint)((int)y + (int)distance);
                Teleport(z, x, newY);

                //pickpocket mob
                PickPocketForTeleport(EntityId);
            }
            catch { }
        }
        public void PickPocketForTeleport(ulong guid)
        {
            //memsharp instance
            MemorySharp sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //Select Target
            Target(guid);

            //pickpocket
            Lua("CastSpellByName(\"Pick Pocket\")");

            //wait for loot window and pickpocket
            Thread.Sleep(500);
            AutoLoot();

            //leave if mob resist
            LeaveIfSpotted();

            
        }

        public void LeaveIfSpotted()
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);

            //vars
            IntPtr localPlayer;
            uint oldZ, oldX, oldY;
            var healthOffset = 0x1DC8;
            var maxHealthOffset = 0x1DE0;
            var isStealth = new IntPtr(0x00BC6CA0); //this one is perfect

            //check if not in stealth
            var isSpotted = sharp[isStealth, false].Read<int>();
            if (isSpotted != 1)
            {
                localPlayer = GetPlayerPtr();
                oldZ = sharp[localPlayer + 0x9B8, false].Read<uint>();
                oldX = sharp[localPlayer + 0x9BC, false].Read<uint>();
                oldY = sharp[localPlayer + 0x9C0, false].Read<uint>();
                if (SharpBot.profile == "Lower Blackrock Spire")
                {
                    //Teleport(1117404522, 3277776010, 1111972990); LBRS PORTAL TP
                    Teleport(1139084535, 1109185386, 3263826840); // BLACKROCK PORTAL TP
                }
                MoveOut();
                MoveIn();
                sharp.Windows.MainWindow.Keyboard.PressRelease(Binarysharp.MemoryManagement.Native.Keys.D0);//food keybind
                localPlayer = GetPlayerPtr();
                while (sharp[localPlayer + healthOffset, false].Read<int>() <
                    sharp[localPlayer + maxHealthOffset, false].Read<int>())
                { }
                Lua("CastSpellByName(\"Stealth\")");
                Thread.Sleep(500);
                Teleport(oldZ, oldX, oldY);
                Thread.Sleep(500);
            }
        }

        internal void TestHeight(int value)
        {
            //memsharp instance
            var sharp = new MemorySharp(Process.GetProcessesByName("WoW")[0]);
            
            //vars
            var localPlayer = GetPlayerPtr();
            var oldZ = sharp[localPlayer + 0x9B8, false].Read<uint>();
            var oldX = sharp[localPlayer + 0x9BC, false].Read<uint>();
            var oldY = sharp[localPlayer + 0x9C0, false].Read<uint>();

            //test teleports
            var newY = (uint)((int)oldY + value);
            Teleport(oldZ, oldX, newY);
            Thread.Sleep(2000);

            //teleport back
            Teleport(oldZ, oldX, oldY);
        }
    }
}