using System.Windows.Forms;

namespace SharpBot.SharpGUI
{
    partial class Information
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPid = new System.Windows.Forms.Label();
            this.txtInGame = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.LogConsole = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtPid
            // 
            this.txtPid.AutoSize = true;
            this.txtPid.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPid.ForeColor = System.Drawing.Color.White;
            this.txtPid.Location = new System.Drawing.Point(12, 9);
            this.txtPid.Name = "txtPid";
            this.txtPid.Size = new System.Drawing.Size(35, 12);
            this.txtPid.TabIndex = 0;
            this.txtPid.Text = "PID: ";
            // 
            // txtInGame
            // 
            this.txtInGame.AutoSize = true;
            this.txtInGame.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInGame.Location = new System.Drawing.Point(12, 34);
            this.txtInGame.Name = "txtInGame";
            this.txtInGame.Size = new System.Drawing.Size(59, 12);
            this.txtInGame.TabIndex = 1;
            this.txtInGame.Text = "IN GAME: ";
            // 
            // BtnStart
            // 
            this.BtnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BtnStart.FlatAppearance.BorderSize = 0;
            this.BtnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStart.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStart.ForeColor = System.Drawing.Color.White;
            this.BtnStart.Location = new System.Drawing.Point(14, 88);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 5;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = false;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // LogConsole
            // 
            this.LogConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.LogConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogConsole.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogConsole.ForeColor = System.Drawing.Color.White;
            this.LogConsole.Location = new System.Drawing.Point(110, 9);
            this.LogConsole.Name = "LogConsole";
            this.LogConsole.ReadOnly = true;
            this.LogConsole.Size = new System.Drawing.Size(138, 102);
            this.LogConsole.TabIndex = 6;
            this.LogConsole.Text = "";
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.ClientSize = new System.Drawing.Size(269, 123);
            this.Controls.Add(this.LogConsole);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.txtInGame);
            this.Controls.Add(this.txtPid);
            this.Font = new System.Drawing.Font("MS Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Information";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtPid;
        private System.Windows.Forms.Label txtInGame;
        private System.Windows.Forms.Button BtnStart;
        public string getBtnStart()
        {
            return BtnStart.Text;
        }
        public void setBtnStart(string str) 
        {
            BtnStart.Text = str;
        }

        private System.Windows.Forms.RichTextBox LogConsole;
        public string getLog()
        {
            return LogConsole.Text;
        }
        public void setLog(string str)
        {
            LogConsole.Text = str;
        }
    }
}