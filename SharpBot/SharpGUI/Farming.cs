using System;
using System.Windows.Forms;

namespace SharpBot.SharpGUI
{
    public partial class Farming : Form
    {
        private CheckBox SelectedBox;
        public void SelectBox(CheckBox NewSelectedBox)
        {
            if (SelectedBox != null)
            {
                SelectedBox.Checked = false;
                SharpBot.profile = null;
            }
            if (SelectedBox != NewSelectedBox && NewSelectedBox.CheckState == CheckState.Checked)
            {
                SelectedBox = NewSelectedBox;
                SharpBot.profile = NewSelectedBox.Text;
            }
        }

        public Farming()
        {
            InitializeComponent();
        }

        private void Farming_Load(object sender, EventArgs e)
        {

            //Initialize Checked Saved Box
            if (SharpBot.profile == "Lower Blackrock Spire")
                boxLowerBlackrockSpire.Checked = true;
            if (SharpBot.profile == "Blackrock Depths")
                boxBlackrockDepths.Checked = true;
            if (SharpBot.profile == "Razorfen Downs")
                boxRazorfenDowns.Checked = true;
        }

        private void boxLowerBlackrockSpire_CheckedChanged(object sender, EventArgs e)
        {
            SelectBox(boxLowerBlackrockSpire);
        }

        private void boxBlackrockDepths_CheckedChanged(object sender, EventArgs e)
        {
            SelectBox(boxBlackrockDepths);
        }

        private void boxRazorfenDowns_CheckedChanged(object sender, EventArgs e)
        {
            SelectBox(boxRazorfenDowns);
        }
    }
}
