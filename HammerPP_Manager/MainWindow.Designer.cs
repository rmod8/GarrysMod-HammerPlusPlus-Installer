namespace HammerPP_Manager
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.buttonChangeSDKPath = new System.Windows.Forms.Button();
            this.listConfigs = new System.Windows.Forms.ListView();
            this.columnCName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCGameType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonChangeSDKPath
            // 
            this.buttonChangeSDKPath.Location = new System.Drawing.Point(402, 247);
            this.buttonChangeSDKPath.Name = "buttonChangeSDKPath";
            this.buttonChangeSDKPath.Size = new System.Drawing.Size(126, 23);
            this.buttonChangeSDKPath.TabIndex = 0;
            this.buttonChangeSDKPath.Text = "Change SDK Path...";
            this.buttonChangeSDKPath.UseVisualStyleBackColor = true;
            this.buttonChangeSDKPath.Click += new System.EventHandler(this.buttonChangeSDKPath_Click);
            // 
            // listConfigs
            // 
            this.listConfigs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCName,
            this.columnCGameType});
            this.listConfigs.FullRowSelect = true;
            this.listConfigs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listConfigs.HideSelection = false;
            this.listConfigs.Location = new System.Drawing.Point(12, 12);
            this.listConfigs.Name = "listConfigs";
            this.listConfigs.Size = new System.Drawing.Size(552, 177);
            this.listConfigs.TabIndex = 3;
            this.listConfigs.UseCompatibleStateImageBehavior = false;
            this.listConfigs.View = System.Windows.Forms.View.Details;
            // 
            // columnCName
            // 
            this.columnCName.Text = "Config Name:";
            this.columnCName.Width = 265;
            // 
            // columnCGameType
            // 
            this.columnCGameType.Text = "Game Type:";
            this.columnCGameType.Width = 267;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.Location = new System.Drawing.Point(12, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add Configuration";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button2.Location = new System.Drawing.Point(198, 195);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Edit Configuration";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button3.Location = new System.Drawing.Point(384, 195);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(180, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Remove Configuration";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 293);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listConfigs);
            this.Controls.Add(this.buttonChangeSDKPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu - Hammer++ Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonChangeSDKPath;
        private System.Windows.Forms.ListView listConfigs;
        private System.Windows.Forms.ColumnHeader columnCName;
        private System.Windows.Forms.ColumnHeader columnCGameType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}