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
using System.Runtime.ExceptionServices;

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
                    case "Blackrock Depths":
                        thread = new Thread(Farming.BlackrockDepthsExploit);
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
        }
    }
}
