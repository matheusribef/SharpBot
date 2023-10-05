using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBot
{
    public partial class SharpBot : Form
    {
        private Form activeForm = null;

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(childForm);
            mainPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public SharpBot()
        {
            InitializeComponent();
        }

        private void BtnInformation_Click(object sender, EventArgs e)
        {
            openChildForm(new SharpGUI.Information());
        }

        private void BtnFarming_Click(object sender, EventArgs e)
        {
            openChildForm(new SharpGUI.Farming());
        }

        private void labelProfile_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}
