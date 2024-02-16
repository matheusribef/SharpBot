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
            this.boxBlackrockDepths = new System.Windows.Forms.CheckBox();
            this.boxLowerBlackrockSpire = new System.Windows.Forms.CheckBox();
            this.boxRazorfenDowns = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // boxBlackrockDepths
            // 
            this.boxBlackrockDepths.AutoSize = true;
            this.boxBlackrockDepths.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.boxBlackrockDepths.FlatAppearance.BorderSize = 0;
            this.boxBlackrockDepths.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxBlackrockDepths.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxBlackrockDepths.Location = new System.Drawing.Point(12, 12);
            this.boxBlackrockDepths.Name = "boxBlackrockDepths";
            this.boxBlackrockDepths.Size = new System.Drawing.Size(135, 17);
            this.boxBlackrockDepths.TabIndex = 0;
            this.boxBlackrockDepths.Text = "Blackrock Depths";
            this.boxBlackrockDepths.UseVisualStyleBackColor = false;
            this.boxBlackrockDepths.CheckedChanged += new System.EventHandler(this.boxBlackrockDepths_CheckedChanged);
            // 
            // boxLowerBlackrockSpire
            // 
            this.boxLowerBlackrockSpire.AutoSize = true;
            this.boxLowerBlackrockSpire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.boxLowerBlackrockSpire.FlatAppearance.BorderSize = 0;
            this.boxLowerBlackrockSpire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxLowerBlackrockSpire.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxLowerBlackrockSpire.ForeColor = System.Drawing.Color.White;
            this.boxLowerBlackrockSpire.Location = new System.Drawing.Point(12, 35);
            this.boxLowerBlackrockSpire.Name = "boxLowerBlackrockSpire";
            this.boxLowerBlackrockSpire.Size = new System.Drawing.Size(170, 17);
            this.boxLowerBlackrockSpire.TabIndex = 1;
            this.boxLowerBlackrockSpire.Text = "Lower Blackrock Spire";
            this.boxLowerBlackrockSpire.UseVisualStyleBackColor = false;
            this.boxLowerBlackrockSpire.CheckedChanged += new System.EventHandler(this.boxLowerBlackrockSpire_CheckedChanged);
            // 
            // boxRazorfenDowns
            // 
            this.boxRazorfenDowns.AutoSize = true;
            this.boxRazorfenDowns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxRazorfenDowns.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxRazorfenDowns.ForeColor = System.Drawing.Color.White;
            this.boxRazorfenDowns.Location = new System.Drawing.Point(12, 58);
            this.boxRazorfenDowns.Name = "boxRazorfenDowns";
            this.boxRazorfenDowns.Size = new System.Drawing.Size(121, 17);
            this.boxRazorfenDowns.TabIndex = 2;
            this.boxRazorfenDowns.Text = "Razorfen Downs";
            this.boxRazorfenDowns.UseVisualStyleBackColor = true;
            this.boxRazorfenDowns.CheckedChanged += new System.EventHandler(this.boxRazorfenDowns_CheckedChanged);
            // 
            // Farming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.ClientSize = new System.Drawing.Size(269, 123);
            this.Controls.Add(this.boxRazorfenDowns);
            this.Controls.Add(this.boxLowerBlackrockSpire);
            this.Controls.Add(this.boxBlackrockDepths);
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

        private System.Windows.Forms.CheckBox boxBlackrockDepths;
        private System.Windows.Forms.CheckBox boxLowerBlackrockSpire;
        private System.Windows.Forms.CheckBox boxRazorfenDowns;
    }
}