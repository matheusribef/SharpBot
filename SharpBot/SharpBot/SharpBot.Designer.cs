using System.Reflection.Emit;

namespace SharpBot
{
    partial class SharpBot
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.BtnFarming = new System.Windows.Forms.Button();
            this.BtnInformation = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.BtnFarming);
            this.buttonsPanel.Controls.Add(this.BtnInformation);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(128, 123);
            this.buttonsPanel.TabIndex = 0;
            // 
            // BtnFarming
            // 
            this.BtnFarming.FlatAppearance.BorderSize = 0;
            this.BtnFarming.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFarming.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFarming.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.BtnFarming.Location = new System.Drawing.Point(0, 63);
            this.BtnFarming.Name = "BtnFarming";
            this.BtnFarming.Size = new System.Drawing.Size(125, 34);
            this.BtnFarming.TabIndex = 2;
            this.BtnFarming.Text = "Farming";
            this.BtnFarming.UseVisualStyleBackColor = true;
            this.BtnFarming.Click += new System.EventHandler(this.BtnFarming_Click);
            // 
            // BtnInformation
            // 
            this.BtnInformation.FlatAppearance.BorderSize = 0;
            this.BtnInformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnInformation.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnInformation.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.BtnInformation.Location = new System.Drawing.Point(0, 23);
            this.BtnInformation.Name = "BtnInformation";
            this.BtnInformation.Size = new System.Drawing.Size(125, 34);
            this.BtnInformation.TabIndex = 1;
            this.BtnInformation.Text = "Information";
            this.BtnInformation.UseVisualStyleBackColor = true;
            this.BtnInformation.Click += new System.EventHandler(this.BtnInformation_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mainPanel.Location = new System.Drawing.Point(131, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(269, 123);
            this.mainPanel.TabIndex = 1;
            // 
            // SharpBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.ClientSize = new System.Drawing.Size(400, 123);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.buttonsPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SharpBot";
            this.Text = "Notepad++";
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button BtnInformation;
        private System.Windows.Forms.Button BtnFarming;
        private System.Windows.Forms.Panel mainPanel;
        public static string profile;
    }
}

