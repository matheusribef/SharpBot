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
        Thread thread;
        private readonly Profiles Farming = new Profiles();
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
                    case "Scarlet Monastery Armory":
                        thread = new Thread(Farming.ScarletMonasteryArmory);
                        thread.Start();
                        break;
                    case "Lower Blackrock Spire":
                        //thread = new Thread(test);
                        thread = new Thread(Farming.LowerBlackrockSpire);
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
            //vars
            var inverselow = -12000000;
            var inverse = -8000000;
            var verylow = -3000000;
            var low = 500000;
            var medium = 750000;
            var high = 1500000;
            var veryhigh = 1700000;
            var ogre = 2300000;
            var stairmobs = 8000000;

        }
    }
}
