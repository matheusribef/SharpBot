using System;
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

namespace SharpBot.SharpGUI
{
    public partial class Information : Form
    {
        Thread antiAfk;
        Thread thread;
        private readonly Profiles Farming = new Profiles();

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
                    case "Scarlet Monastery Armory":
                        antiAfk = new Thread(AntiAFK);
                        antiAfk.Start();
                        thread = new Thread(Farming.ScarletMonasteryArmory);
                        thread.Start();
                        break;
                    case "Lower Blackrock Spire":
                        antiAfk = new Thread(AntiAFK);
                        antiAfk.Start();
                        thread = new Thread(Farming.LowerBlackrockSpire);
                        thread.Start();
                        break;
                    default:
                        setLog("Please select a profile!");
                        setBtnStart("Start");
                        antiAfk.Abort();
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

        void AntiAFK()
        {
            Game.Functions.Functions Player = new Game.Functions.Functions();
            while (true)
            {
                Player.avoidAFK();
            }
        }
    }
}
