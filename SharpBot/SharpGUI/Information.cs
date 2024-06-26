﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using SharpBot.Game.Functions;
using Binarysharp.MemoryManagement;
using SharpBot.Game.Profiles;
using System.Runtime.ExceptionServices;

namespace SharpBot.SharpGUI
{
    public partial class Information : Form
    {
        Thread thread;
        private readonly Profiles farming = new Profiles();
        private readonly Functions Player = new Functions(); 

        public Information()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (getBtnStart() == "Start")
            {
                setBtnStart("Stop");
                setLog("Starting selected profile!");

                switch (SharpBot.profile)
                {
                    case "Blackrock Depths":
                        thread = new Thread(farming.BlackrockDepths);
                        thread.Start();
                        break;
                    case "Lower Blackrock Spire":
                        thread = new Thread(farming.LowerBlackrockSpire);
                        thread.Start();
                        break;
                    case "Razorfen Downs":
                        thread = new Thread(farming.RazorfenDowns);
                        thread.Start();
                        break;
                    default:
                        setLog("Please select a profile!");
                        setBtnStart("Start");
                        break;
                }

            }
            else
            {
                setBtnStart("Start");
                setLog("Closing farm thread!");
                thread.Abort();
                thread = null;
            }
            Thread.Sleep(100);
        }

        public void test()
        {
            Player.GatherAllNodes("Weapon Crate");
            //Player.GatherAllNodes("Copper Vein"); // 1
            //Player.GatherAllNodes("Tin Vein"); // 65
            //Player.GatherAllNodes("Silver Vein"); // 75
            //Player.GatherAllNodes("Iron Deposit"); // 125
            //Player.GatherAllNodes("Gold Vein"); // 150
        }

        public void LockPickTest()
        {
            while (true)
            {
                Player.InteractWithObject(17370385831902067472);
                Thread.Sleep(2500);
                Player.AutoLoot();
                Player.InteractWithObject(17370385831902067475);
                Thread.Sleep(2500);
                Player.AutoLoot();
            }
        }

        public void bwlAttunement()
        {
            Player.Teleport(1110557529, 3280907967, 1121728863);
            Player.InteractWithObject(17370386780653653021);
        }
    }
}
