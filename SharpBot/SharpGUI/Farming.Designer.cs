namespace SharpBot.SharpGUI
{
    partial class Farming
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
            this.boxScarletMonasteryArmory = new System.Windows.Forms.CheckBox();
            this.boxLowerBlackrockSpire = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // boxScarletMonasteryArmory
            // 
            this.boxScarletMonasteryArmory.AutoSize = true;
            this.boxScarletMonasteryArmory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.boxScarletMonasteryArmory.FlatAppearance.BorderSize = 0;
            this.boxScarletMonasteryArmory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxScarletMonasteryArmory.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxScarletMonasteryArmory.Location = new System.Drawing.Point(12, 12);
            this.boxScarletMonasteryArmory.Name = "boxScarletMonasteryArmory";
            this.boxScarletMonasteryArmory.Size = new System.Drawing.Size(191, 17);
            this.boxScarletMonasteryArmory.TabIndex = 0;
            this.boxScarletMonasteryArmory.Text = "Scarlet Monastery Armory";
            this.boxScarletMonasteryArmory.UseVisualStyleBackColor = false;
            this.boxScarletMonasteryArmory.CheckedChanged += new System.EventHandler(this.boxRazorfen_CheckedChanged);
            // 
            // boxLowerBlackrockSpire
            // 
            this.boxLowerBlackrockSpire.AutoSize = true;
            this.boxLowerBlackrockSpire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.boxLowerBlackrockSpire.FlatAppearance.BorderSize = 0;
            this.boxLowerBlackrockSpire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxLowerBlackrockSpire.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxLowerBlackrockSpire.Location = new System.Drawing.Point(12, 35);
            this.boxLowerBlackrockSpire.Name = "boxLowerBlackrockSpire";
            this.boxLowerBlackrockSpire.Size = new System.Drawing.Size(170, 17);
            this.boxLowerBlackrockSpire.TabIndex = 1;
            this.boxLowerBlackrockSpire.Text = "Lower Blackrock Spire";
            this.boxLowerBlackrockSpire.UseVisualStyleBackColor = false;
            this.boxLowerBlackrockSpire.CheckedChanged += new System.EventHandler(this.boxLowerBlackrockSpire_CheckedChanged);
            // 
            // Farming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.ClientSize = new System.Drawing.Size(269, 123);
            this.Controls.Add(this.boxLowerBlackrockSpire);
            this.Controls.Add(this.boxScarletMonasteryArmory);
            this.Font = new System.Drawing.Font("MS Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Farming";
            this.Text = "Farming";
            this.Load += new System.EventHandler(this.Farming_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox boxScarletMonasteryArmory;
        private System.Windows.Forms.CheckBox boxLowerBlackrockSpire;
    }
}