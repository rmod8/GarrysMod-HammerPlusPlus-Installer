namespace HammerPP_Manager
{
    partial class DownloadWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadWindow));
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.textboxConsole = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(12, 127);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(377, 23);
            this.pbDownload.TabIndex = 0;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Location = new System.Drawing.Point(12, 156);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(377, 23);
            this.buttonAbort.TabIndex = 1;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // textboxConsole
            // 
            this.textboxConsole.BackColor = System.Drawing.SystemColors.Control;
            this.textboxConsole.ForeColor = System.Drawing.Color.Black;
            this.textboxConsole.Location = new System.Drawing.Point(12, 12);
            this.textboxConsole.Multiline = true;
            this.textboxConsole.Name = "textboxConsole";
            this.textboxConsole.ReadOnly = true;
            this.textboxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxConsole.Size = new System.Drawing.Size(377, 109);
            this.textboxConsole.TabIndex = 2;
            // 
            // DownloadWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 186);
            this.ControlBox = false;
            this.Controls.Add(this.textboxConsole);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.pbDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DownloadWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downloading Hammer++";
            this.Load += new System.EventHandler(this.DownloadWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.TextBox textboxConsole;
    }
}