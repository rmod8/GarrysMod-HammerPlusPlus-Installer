namespace GarrysMod_HammerPlusPlus_Installer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textboxConsole = new System.Windows.Forms.TextBox();
            this.buttonInstall = new System.Windows.Forms.Button();
            this.buttonBrowseGmod = new System.Windows.Forms.Button();
            this.textboxGmodDir = new System.Windows.Forms.TextBox();
            this.picboxGmodCheck = new System.Windows.Forms.PictureBox();
            this.picboxSDKCheck = new System.Windows.Forms.PictureBox();
            this.textboxSDKDir = new System.Windows.Forms.TextBox();
            this.buttonBrowseSDK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picboxGmodCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxSDKCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxConsole
            // 
            this.textboxConsole.BackColor = System.Drawing.SystemColors.Control;
            this.textboxConsole.ForeColor = System.Drawing.Color.Black;
            this.textboxConsole.Location = new System.Drawing.Point(12, 183);
            this.textboxConsole.Multiline = true;
            this.textboxConsole.Name = "textboxConsole";
            this.textboxConsole.ReadOnly = true;
            this.textboxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxConsole.Size = new System.Drawing.Size(588, 209);
            this.textboxConsole.TabIndex = 0;
            // 
            // buttonInstall
            // 
            this.buttonInstall.Enabled = false;
            this.buttonInstall.Location = new System.Drawing.Point(12, 149);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(290, 28);
            this.buttonInstall.TabIndex = 1;
            this.buttonInstall.Text = "Install Hammer++ for Garry\'s Mod";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // buttonBrowseGmod
            // 
            this.buttonBrowseGmod.Location = new System.Drawing.Point(41, 39);
            this.buttonBrowseGmod.Name = "buttonBrowseGmod";
            this.buttonBrowseGmod.Size = new System.Drawing.Size(75, 20);
            this.buttonBrowseGmod.TabIndex = 2;
            this.buttonBrowseGmod.Text = "Browse...";
            this.buttonBrowseGmod.UseVisualStyleBackColor = true;
            this.buttonBrowseGmod.Click += new System.EventHandler(this.buttonBrowseGmod_Click);
            // 
            // textboxGmodDir
            // 
            this.textboxGmodDir.Location = new System.Drawing.Point(122, 40);
            this.textboxGmodDir.Name = "textboxGmodDir";
            this.textboxGmodDir.ReadOnly = true;
            this.textboxGmodDir.Size = new System.Drawing.Size(478, 20);
            this.textboxGmodDir.TabIndex = 3;
            // 
            // picboxGmodCheck
            // 
            this.picboxGmodCheck.Image = ((System.Drawing.Image)(resources.GetObject("picboxGmodCheck.Image")));
            this.picboxGmodCheck.Location = new System.Drawing.Point(12, 36);
            this.picboxGmodCheck.Name = "picboxGmodCheck";
            this.picboxGmodCheck.Size = new System.Drawing.Size(25, 25);
            this.picboxGmodCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picboxGmodCheck.TabIndex = 4;
            this.picboxGmodCheck.TabStop = false;
            // 
            // picboxSDKCheck
            // 
            this.picboxSDKCheck.Image = ((System.Drawing.Image)(resources.GetObject("picboxSDKCheck.Image")));
            this.picboxSDKCheck.Location = new System.Drawing.Point(12, 98);
            this.picboxSDKCheck.Name = "picboxSDKCheck";
            this.picboxSDKCheck.Size = new System.Drawing.Size(25, 25);
            this.picboxSDKCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picboxSDKCheck.TabIndex = 7;
            this.picboxSDKCheck.TabStop = false;
            // 
            // textboxSDKDir
            // 
            this.textboxSDKDir.Location = new System.Drawing.Point(122, 102);
            this.textboxSDKDir.Name = "textboxSDKDir";
            this.textboxSDKDir.ReadOnly = true;
            this.textboxSDKDir.Size = new System.Drawing.Size(478, 20);
            this.textboxSDKDir.TabIndex = 6;
            // 
            // buttonBrowseSDK
            // 
            this.buttonBrowseSDK.Location = new System.Drawing.Point(41, 101);
            this.buttonBrowseSDK.Name = "buttonBrowseSDK";
            this.buttonBrowseSDK.Size = new System.Drawing.Size(75, 20);
            this.buttonBrowseSDK.TabIndex = 5;
            this.buttonBrowseSDK.Text = "Browse...";
            this.buttonBrowseSDK.UseVisualStyleBackColor = true;
            this.buttonBrowseSDK.Click += new System.EventHandler(this.buttonBrowseSDK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "(1.) Please select Garry\'s Mod\'s Directory (GarrysMod)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(457, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "(2.) Please select Source SDK 2013 Multiplayer\'s Directory (Source SDK Base 2013 " +
    "Multiplayer)";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 400);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(166, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Silk Icons created by Mark James";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonConfig
            // 
            this.buttonConfig.Enabled = false;
            this.buttonConfig.Location = new System.Drawing.Point(310, 149);
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.Size = new System.Drawing.Size(290, 28);
            this.buttonConfig.TabIndex = 11;
            this.buttonConfig.Text = "Configure Hammer++ for Garry\'s Mod";
            this.buttonConfig.UseVisualStyleBackColor = true;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 422);
            this.Controls.Add(this.buttonConfig);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picboxSDKCheck);
            this.Controls.Add(this.textboxSDKDir);
            this.Controls.Add(this.buttonBrowseSDK);
            this.Controls.Add(this.picboxGmodCheck);
            this.Controls.Add(this.textboxGmodDir);
            this.Controls.Add(this.buttonBrowseGmod);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.textboxConsole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "HammerPlusPlus Installer for Garry\'s Mod";
            ((System.ComponentModel.ISupportInitialize)(this.picboxGmodCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxSDKCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxConsole;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Button buttonBrowseGmod;
        private System.Windows.Forms.TextBox textboxGmodDir;
        private System.Windows.Forms.PictureBox picboxGmodCheck;
        private System.Windows.Forms.PictureBox picboxSDKCheck;
        private System.Windows.Forms.TextBox textboxSDKDir;
        private System.Windows.Forms.Button buttonBrowseSDK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonConfig;
    }
}

