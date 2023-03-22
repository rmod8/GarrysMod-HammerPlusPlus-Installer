namespace HammerPP_Manager
{
    partial class StartupEnterDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupEnterDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tboxSDKPath = new System.Windows.Forms.TextBox();
            this.buttonSDKBrowse = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.pboxOkay = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxOkay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(90, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(447, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Hammer++ Manager!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(479, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please select the game directory for Source SDK Base 2013 Multiplayer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tboxSDKPath);
            this.groupBox2.Controls.Add(this.buttonSDKBrowse);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(584, 55);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source SDK Base 2013 Multiplayer";
            // 
            // tboxSDKPath
            // 
            this.tboxSDKPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxSDKPath.Location = new System.Drawing.Point(84, 26);
            this.tboxSDKPath.Name = "tboxSDKPath";
            this.tboxSDKPath.ReadOnly = true;
            this.tboxSDKPath.Size = new System.Drawing.Size(494, 21);
            this.tboxSDKPath.TabIndex = 4;
            this.tboxSDKPath.GotFocus += new System.EventHandler(this.tboxSDKPath_Focus);
            // 
            // buttonSDKBrowse
            // 
            this.buttonSDKBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSDKBrowse.Location = new System.Drawing.Point(6, 23);
            this.buttonSDKBrowse.Name = "buttonSDKBrowse";
            this.buttonSDKBrowse.Size = new System.Drawing.Size(72, 24);
            this.buttonSDKBrowse.TabIndex = 3;
            this.buttonSDKBrowse.Text = "Browse...";
            this.buttonSDKBrowse.UseVisualStyleBackColor = true;
            this.buttonSDKBrowse.Click += new System.EventHandler(this.buttonSDKBrowse_Click);
            // 
            // buttonContinue
            // 
            this.buttonContinue.Enabled = false;
            this.buttonContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonContinue.Location = new System.Drawing.Point(496, 132);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(100, 26);
            this.buttonContinue.TabIndex = 6;
            this.buttonContinue.Text = "Continue...";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // pboxOkay
            // 
            this.pboxOkay.Image = global::HammerPP_Manager.Properties.Resources.cross;
            this.pboxOkay.InitialImage = global::HammerPP_Manager.Properties.Resources.cross;
            this.pboxOkay.Location = new System.Drawing.Point(464, 132);
            this.pboxOkay.Name = "pboxOkay";
            this.pboxOkay.Size = new System.Drawing.Size(26, 26);
            this.pboxOkay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxOkay.TabIndex = 7;
            this.pboxOkay.TabStop = false;
            // 
            // StartupEnterDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 165);
            this.Controls.Add(this.pboxOkay);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartupEnterDetails";
            this.Text = "Welcome! - Hammer++ Manager";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxOkay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tboxSDKPath;
        private System.Windows.Forms.Button buttonSDKBrowse;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.PictureBox pboxOkay;
    }
}