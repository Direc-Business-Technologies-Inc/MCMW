namespace MCWM.Views.Main

{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.travelOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findTravelOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTravelOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.OneTimeWeighMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListOfTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryOfTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.localTransactionsNotUploadedToSAPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Teal;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.travelOrderToolStripMenuItem,
            this.toolStripMenuItem2,
            this.OneTimeWeighMenuItem2,
            this.reportsToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.toolStripMenuItem4});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(1087, 58);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(49, 54);
            this.toolStripMenuItem1.Text = "          ";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.Azure;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(118, 54);
            this.toolStripMenuItem3.Text = "New Travel Order";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // travelOrderToolStripMenuItem
            // 
            this.travelOrderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findTravelOrderToolStripMenuItem,
            this.newTravelOrderToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.travelOrderToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.travelOrderToolStripMenuItem.ForeColor = System.Drawing.Color.Azure;
            this.travelOrderToolStripMenuItem.Name = "travelOrderToolStripMenuItem";
            this.travelOrderToolStripMenuItem.Size = new System.Drawing.Size(89, 54);
            this.travelOrderToolStripMenuItem.Text = "Travel Order";
            this.travelOrderToolStripMenuItem.Visible = false;
            // 
            // findTravelOrderToolStripMenuItem
            // 
            this.findTravelOrderToolStripMenuItem.Name = "findTravelOrderToolStripMenuItem";
            this.findTravelOrderToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.findTravelOrderToolStripMenuItem.Text = "Update Travel Order";
            this.findTravelOrderToolStripMenuItem.Click += new System.EventHandler(this.findTravelOrderToolStripMenuItem_Click);
            // 
            // newTravelOrderToolStripMenuItem
            // 
            this.newTravelOrderToolStripMenuItem.Name = "newTravelOrderToolStripMenuItem";
            this.newTravelOrderToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.newTravelOrderToolStripMenuItem.Text = "New Travel Order";
            this.newTravelOrderToolStripMenuItem.Click += new System.EventHandler(this.newTravelOrderToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.Azure;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(120, 54);
            this.toolStripMenuItem2.Text = "Fleet Travel Order";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // OneTimeWeighMenuItem2
            // 
            this.OneTimeWeighMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OneTimeWeighMenuItem2.ForeColor = System.Drawing.Color.Azure;
            this.OneTimeWeighMenuItem2.Name = "OneTimeWeighMenuItem2";
            this.OneTimeWeighMenuItem2.Size = new System.Drawing.Size(114, 54);
            this.OneTimeWeighMenuItem2.Text = "One-Time Weigh";
            this.OneTimeWeighMenuItem2.Click += new System.EventHandler(this.OneTimeWeighMenuItem2_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ListOfTransactionsToolStripMenuItem,
            this.summaryOfTransactionsToolStripMenuItem,
            this.localTransactionsNotUploadedToSAPToolStripMenuItem});
            this.reportsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsToolStripMenuItem.ForeColor = System.Drawing.Color.Azure;
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(63, 54);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // ListOfTransactionsToolStripMenuItem
            // 
            this.ListOfTransactionsToolStripMenuItem.Name = "ListOfTransactionsToolStripMenuItem";
            this.ListOfTransactionsToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.ListOfTransactionsToolStripMenuItem.Text = "List of Transactions Report";
            this.ListOfTransactionsToolStripMenuItem.Click += new System.EventHandler(this.ListOfTransactionsToolStripMenuItem_Click);
            // 
            // summaryOfTransactionsToolStripMenuItem
            // 
            this.summaryOfTransactionsToolStripMenuItem.Name = "summaryOfTransactionsToolStripMenuItem";
            this.summaryOfTransactionsToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.summaryOfTransactionsToolStripMenuItem.Text = "Summary of Transactions (Reprint)";
            this.summaryOfTransactionsToolStripMenuItem.Click += new System.EventHandler(this.summaryOfTransactionsToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userConfigToolStripMenuItem});
            this.configurationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configurationToolStripMenuItem.ForeColor = System.Drawing.Color.Azure;
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(95, 54);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Visible = false;
            // 
            // userConfigToolStripMenuItem
            // 
            this.userConfigToolStripMenuItem.Name = "userConfigToolStripMenuItem";
            this.userConfigToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.userConfigToolStripMenuItem.Text = "Configuration";
            this.userConfigToolStripMenuItem.Click += new System.EventHandler(this.userConfigToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.Azure;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(58, 54);
            this.toolStripMenuItem4.Text = "Logout";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Teal;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 709);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1087, 22);
            this.panel1.TabIndex = 2;
            // 
            // lblVersion
            // 
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(0, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(402, 22);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Location = new System.Drawing.Point(685, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(402, 22);
            this.lblDateTime.TabIndex = 0;
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Teal;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(954, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 9);
            this.label4.TabIndex = 11;
            this.label4.Text = "Powered by Direc Business Tech. Inc";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Teal;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(1033, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(42, 36);
            this.btnExit.TabIndex = 66;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Teal;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblUser.Location = new System.Drawing.Point(860, 24);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 15);
            this.lblUser.TabIndex = 68;
            this.lblUser.Text = "____";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // localTransactionsNotUploadedToSAPToolStripMenuItem
            // 
            this.localTransactionsNotUploadedToSAPToolStripMenuItem.Name = "localTransactionsNotUploadedToSAPToolStripMenuItem";
            this.localTransactionsNotUploadedToSAPToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.localTransactionsNotUploadedToSAPToolStripMenuItem.Text = "Local Transactions (Not Uploaded to SAP)";
            this.localTransactionsNotUploadedToSAPToolStripMenuItem.Click += new System.EventHandler(this.localTransactionsNotUploadedToSAPToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1087, 731);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MCWM Weigh Bridge System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem travelOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findTravelOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ListOfTransactionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem summaryOfTransactionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OneTimeWeighMenuItem2;
        public System.Windows.Forms.ToolStripMenuItem newTravelOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        public System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ToolStripMenuItem localTransactionsNotUploadedToSAPToolStripMenuItem;
    }
}

