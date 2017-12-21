namespace RepoRepo
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.drawTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawTypeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.pot1Button = new System.Windows.Forms.Button();
            this.pot2Button = new System.Windows.Forms.Button();
            this.pot3Button = new System.Windows.Forms.Button();
            this.pot4Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(276, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(664, 165);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawTypeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1374, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // drawTypeToolStripMenuItem
            // 
            this.drawTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawTypeToolStripMenuItem1});
            this.drawTypeToolStripMenuItem.Name = "drawTypeToolStripMenuItem";
            this.drawTypeToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.drawTypeToolStripMenuItem.Text = "Options";
            // 
            // drawTypeToolStripMenuItem1
            // 
            this.drawTypeToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem,
            this.automaticToolStripMenuItem});
            this.drawTypeToolStripMenuItem1.Name = "drawTypeToolStripMenuItem1";
            this.drawTypeToolStripMenuItem1.Size = new System.Drawing.Size(154, 26);
            this.drawTypeToolStripMenuItem1.Text = "Draw Type";
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.manualToolStripMenuItem.Text = "Manual";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // automaticToolStripMenuItem
            // 
            this.automaticToolStripMenuItem.Name = "automaticToolStripMenuItem";
            this.automaticToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.automaticToolStripMenuItem.Text = "Automatic";
            this.automaticToolStripMenuItem.Click += new System.EventHandler(this.automaticToolStripMenuItem_Click);
            // 
            // buttonValidate
            // 
            this.buttonValidate.Location = new System.Drawing.Point(764, 712);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(223, 61);
            this.buttonValidate.TabIndex = 3;
            this.buttonValidate.Text = "Send to database";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // pot1Button
            // 
            this.pot1Button.Location = new System.Drawing.Point(407, 721);
            this.pot1Button.Name = "pot1Button";
            this.pot1Button.Size = new System.Drawing.Size(75, 42);
            this.pot1Button.TabIndex = 4;
            this.pot1Button.Text = "Pot 1";
            this.pot1Button.UseVisualStyleBackColor = true;
            this.pot1Button.Click += new System.EventHandler(this.pot1Button_Click);
            // 
            // pot2Button
            // 
            this.pot2Button.Location = new System.Drawing.Point(488, 721);
            this.pot2Button.Name = "pot2Button";
            this.pot2Button.Size = new System.Drawing.Size(75, 42);
            this.pot2Button.TabIndex = 4;
            this.pot2Button.Text = "Pot 2";
            this.pot2Button.UseVisualStyleBackColor = true;
            this.pot2Button.Click += new System.EventHandler(this.pot2Button_Click);
            // 
            // pot3Button
            // 
            this.pot3Button.Location = new System.Drawing.Point(1172, 721);
            this.pot3Button.Name = "pot3Button";
            this.pot3Button.Size = new System.Drawing.Size(75, 42);
            this.pot3Button.TabIndex = 4;
            this.pot3Button.Text = "Pot 3";
            this.pot3Button.UseVisualStyleBackColor = true;
            this.pot3Button.Click += new System.EventHandler(this.pot3Button_Click);
            // 
            // pot4Button
            // 
            this.pot4Button.Location = new System.Drawing.Point(1253, 721);
            this.pot4Button.Name = "pot4Button";
            this.pot4Button.Size = new System.Drawing.Size(75, 42);
            this.pot4Button.TabIndex = 4;
            this.pot4Button.Text = "Pot 4";
            this.pot4Button.UseVisualStyleBackColor = true;
            this.pot4Button.Click += new System.EventHandler(this.pot4Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1374, 842);
            this.Controls.Add(this.pot4Button);
            this.Controls.Add(this.pot3Button);
            this.Controls.Add(this.pot2Button);
            this.Controls.Add(this.pot1Button);
            this.Controls.Add(this.buttonValidate);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem drawTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawTypeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automaticToolStripMenuItem;
        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.Button pot1Button;
        private System.Windows.Forms.Button pot2Button;
        private System.Windows.Forms.Button pot3Button;
        private System.Windows.Forms.Button pot4Button;
    }
}

