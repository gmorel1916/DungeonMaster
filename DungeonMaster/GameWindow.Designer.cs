using System;

namespace DungeonMaster
{
    partial class GameWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
            this.DungeonWindow = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DungeonWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // DungeonWindow
            // 
            this.DungeonWindow.Location = new System.Drawing.Point(365, 2);
            this.DungeonWindow.Name = "DungeonWindow";
            this.DungeonWindow.Size = new System.Drawing.Size(661, 518);
            this.DungeonWindow.TabIndex = 0;
            this.DungeonWindow.TabStop = false;
            this.DungeonWindow.Click += new System.EventHandler(this.DungeonWindow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Click Here to Close or hit Esc Key.";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1318, 739);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DungeonWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameWindow";
            this.Text = "DungeonMasterHD";
            this.Load += new System.EventHandler(this.GameWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DungeonWindow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.PictureBox DungeonWindow;
        private System.Windows.Forms.Label label1;
    }
}

