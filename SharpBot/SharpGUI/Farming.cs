using System;
using System.Windows.Forms;

namespace SharpBot.SharpGUI
{
    public partial class Farming : Form
    {
        public CheckBox SelectedBox;
        private void SelectBox(CheckBox NewSelectedBox)
        {
            if (SelectedBox != null)
            {
                SelectedBox.Checked = false;
                SharpBot.profile = null;
            }
            if (SelectedBox != NewSelectedBox && NewSelectedBox.CheckState != CheckState.Unchecked)
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
            if (SharpBot.profile == "Scarlet Monastery Armory")
                boxScarletMonasteryArmory.Checked = true;
        }

        private void boxLowerBlackrockSpire_CheckedChanged(object sender, EventArgs e)
        {
            SelectBox(boxLowerBlackrockSpire);
        }

        private void boxRazorfen_CheckedChanged(object sender, EventArgs e)
        {
            SelectBox(boxScarletMonasteryArmory);
        }
    }
}
