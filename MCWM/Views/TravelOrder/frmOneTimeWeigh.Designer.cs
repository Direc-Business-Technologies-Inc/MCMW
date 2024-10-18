namespace MCWM.Views.TravelOrder
{
    partial class frmOneTimeWeigh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOneTimeWeigh));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.btnWeigh = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label27 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtNetWeight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSecondWeight = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFirstWeight = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNoWaste = new System.Windows.Forms.Button();
            this.lblToggle = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnToggle = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lblWgtOutTime = new System.Windows.Forms.Label();
            this.lblWgtInTime = new System.Windows.Forms.Label();
            this.lblWgtOutDate = new System.Windows.Forms.Label();
            this.lblWgtInDate = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(-12, -7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 54);
            this.panel1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 104;
            this.label1.Text = "One-Time Weigh";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(471, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(42, 36);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel5.Location = new System.Drawing.Point(-10, 46);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(524, 6);
            this.panel5.TabIndex = 12;
            // 
            // cmbPorts
            // 
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(369, 46);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(61, 21);
            this.cmbPorts.TabIndex = 90;
            this.cmbPorts.TabStop = false;
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtWeight.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeight.ForeColor = System.Drawing.SystemColors.Menu;
            this.txtWeight.Location = new System.Drawing.Point(3, 3);
            this.txtWeight.Multiline = true;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(427, 38);
            this.txtWeight.TabIndex = 89;
            this.txtWeight.TabStop = false;
            this.txtWeight.Text = "0.00";
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWeight.TextChanged += new System.EventHandler(this.txtWeight_TextChanged);
            // 
            // btnWeigh
            // 
            this.btnWeigh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWeigh.BackgroundImage")));
            this.btnWeigh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWeigh.FlatAppearance.BorderSize = 0;
            this.btnWeigh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWeigh.Location = new System.Drawing.Point(185, 43);
            this.btnWeigh.Name = "btnWeigh";
            this.btnWeigh.Size = new System.Drawing.Size(67, 63);
            this.btnWeigh.TabIndex = 1;
            this.btnWeigh.UseVisualStyleBackColor = true;
            this.btnWeigh.Click += new System.EventHandler(this.btnWeigh_Click);
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDate.Location = new System.Drawing.Point(0, 465);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 11);
            this.lblDate.TabIndex = 91;
            this.lblDate.Text = "______";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label27.Location = new System.Drawing.Point(353, 439);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(39, 16);
            this.label27.TabIndex = 92;
            this.label27.Text = "Print";
            this.label27.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(338, 373);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(67, 63);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtNetWeight
            // 
            this.txtNetWeight.Enabled = false;
            this.txtNetWeight.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetWeight.Location = new System.Drawing.Point(83, 216);
            this.txtNetWeight.Name = "txtNetWeight";
            this.txtNetWeight.Size = new System.Drawing.Size(205, 26);
            this.txtNetWeight.TabIndex = 103;
            this.txtNetWeight.TabStop = false;
            this.txtNetWeight.Text = "0.00";
            this.txtNetWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label13.Location = new System.Drawing.Point(5, 224);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 102;
            this.label13.Text = "Net Weight:";
            // 
            // txtSecondWeight
            // 
            this.txtSecondWeight.Enabled = false;
            this.txtSecondWeight.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecondWeight.Location = new System.Drawing.Point(83, 190);
            this.txtSecondWeight.Name = "txtSecondWeight";
            this.txtSecondWeight.Size = new System.Drawing.Size(205, 26);
            this.txtSecondWeight.TabIndex = 101;
            this.txtSecondWeight.TabStop = false;
            this.txtSecondWeight.Text = "0.00";
            this.txtSecondWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSecondWeight.TextChanged += new System.EventHandler(this.txtSecondWeight_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label14.Location = new System.Drawing.Point(6, 198);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 100;
            this.label14.Text = "2nd Weight:";
            // 
            // txtFirstWeight
            // 
            this.txtFirstWeight.Enabled = false;
            this.txtFirstWeight.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstWeight.Location = new System.Drawing.Point(83, 164);
            this.txtFirstWeight.Name = "txtFirstWeight";
            this.txtFirstWeight.Size = new System.Drawing.Size(205, 26);
            this.txtFirstWeight.TabIndex = 99;
            this.txtFirstWeight.TabStop = false;
            this.txtFirstWeight.Text = "0.00";
            this.txtFirstWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label15.Location = new System.Drawing.Point(5, 172);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 98;
            this.label15.Text = "1st Weight:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(242, 439);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 105;
            this.label2.Text = "No Waste";
            // 
            // btnNoWaste
            // 
            this.btnNoWaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNoWaste.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNoWaste.BackgroundImage")));
            this.btnNoWaste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNoWaste.FlatAppearance.BorderSize = 0;
            this.btnNoWaste.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNoWaste.Location = new System.Drawing.Point(243, 373);
            this.btnNoWaste.Name = "btnNoWaste";
            this.btnNoWaste.Size = new System.Drawing.Size(67, 63);
            this.btnNoWaste.TabIndex = 2;
            this.btnNoWaste.UseVisualStyleBackColor = true;
            this.btnNoWaste.Click += new System.EventHandler(this.btnNoWaste_Click);
            // 
            // lblToggle
            // 
            this.lblToggle.AutoSize = true;
            this.lblToggle.Location = new System.Drawing.Point(258, 90);
            this.lblToggle.Name = "lblToggle";
            this.lblToggle.Size = new System.Drawing.Size(29, 13);
            this.lblToggle.TabIndex = 248;
            this.lblToggle.Text = "Auto";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(153, 82);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(20, 21);
            this.btnClear.TabIndex = 247;
            this.btnClear.TabStop = false;
            this.btnClear.Tag = "";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnToggle
            // 
            this.btnToggle.BackColor = System.Drawing.Color.Transparent;
            this.btnToggle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToggle.BackgroundImage")));
            this.btnToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToggle.Enabled = false;
            this.btnToggle.FlatAppearance.BorderSize = 0;
            this.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggle.Location = new System.Drawing.Point(148, 46);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(31, 32);
            this.btnToggle.TabIndex = 246;
            this.btnToggle.TabStop = false;
            this.btnToggle.Tag = "";
            this.btnToggle.UseVisualStyleBackColor = false;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label34.Location = new System.Drawing.Point(404, 196);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(38, 13);
            this.label34.TabIndex = 261;
            this.label34.Text = "Time:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label30.Location = new System.Drawing.Point(404, 169);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(38, 13);
            this.label30.TabIndex = 260;
            this.label30.Text = "Time:";
            // 
            // lblWgtOutTime
            // 
            this.lblWgtOutTime.AutoSize = true;
            this.lblWgtOutTime.BackColor = System.Drawing.Color.Transparent;
            this.lblWgtOutTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWgtOutTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWgtOutTime.Location = new System.Drawing.Point(443, 205);
            this.lblWgtOutTime.Name = "lblWgtOutTime";
            this.lblWgtOutTime.Size = new System.Drawing.Size(0, 13);
            this.lblWgtOutTime.TabIndex = 259;
            // 
            // lblWgtInTime
            // 
            this.lblWgtInTime.AutoSize = true;
            this.lblWgtInTime.BackColor = System.Drawing.Color.Transparent;
            this.lblWgtInTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWgtInTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWgtInTime.Location = new System.Drawing.Point(443, 170);
            this.lblWgtInTime.Name = "lblWgtInTime";
            this.lblWgtInTime.Size = new System.Drawing.Size(0, 13);
            this.lblWgtInTime.TabIndex = 258;
            // 
            // lblWgtOutDate
            // 
            this.lblWgtOutDate.AutoSize = true;
            this.lblWgtOutDate.BackColor = System.Drawing.Color.Transparent;
            this.lblWgtOutDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWgtOutDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWgtOutDate.Location = new System.Drawing.Point(329, 196);
            this.lblWgtOutDate.Name = "lblWgtOutDate";
            this.lblWgtOutDate.Size = new System.Drawing.Size(37, 13);
            this.lblWgtOutDate.TabIndex = 257;
            this.lblWgtOutDate.Text = "_____";
            // 
            // lblWgtInDate
            // 
            this.lblWgtInDate.AutoSize = true;
            this.lblWgtInDate.BackColor = System.Drawing.Color.Transparent;
            this.lblWgtInDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWgtInDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWgtInDate.Location = new System.Drawing.Point(329, 169);
            this.lblWgtInDate.Name = "lblWgtInDate";
            this.lblWgtInDate.Size = new System.Drawing.Size(37, 13);
            this.lblWgtInDate.TabIndex = 256;
            this.lblWgtInDate.Text = "_____";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label36.Location = new System.Drawing.Point(294, 196);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(38, 13);
            this.label36.TabIndex = 255;
            this.label36.Text = "Date:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label39.Location = new System.Drawing.Point(294, 169);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(38, 13);
            this.label39.TabIndex = 254;
            this.label39.Text = "Date:";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Controls.Add(this.label9);
            this.panel3.Location = new System.Drawing.Point(-16, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 31);
            this.panel3.TabIndex = 262;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(19, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "SAP Details";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(40, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(417, 2);
            this.panel2.TabIndex = 266;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Gray;
            this.panel6.Location = new System.Drawing.Point(40, 355);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(417, 2);
            this.panel6.TabIndex = 265;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gray;
            this.panel8.Controls.Add(this.label31);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.txtClientName);
            this.panel8.Controls.Add(this.txtWeight);
            this.panel8.Controls.Add(this.btnWeigh);
            this.panel8.Controls.Add(this.cmbPorts);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Controls.Add(this.label34);
            this.panel8.Controls.Add(this.txtFirstWeight);
            this.panel8.Controls.Add(this.label30);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Controls.Add(this.lblWgtOutTime);
            this.panel8.Controls.Add(this.txtSecondWeight);
            this.panel8.Controls.Add(this.lblWgtInTime);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.lblWgtOutDate);
            this.panel8.Controls.Add(this.txtNetWeight);
            this.panel8.Controls.Add(this.lblWgtInDate);
            this.panel8.Controls.Add(this.btnToggle);
            this.panel8.Controls.Add(this.label36);
            this.panel8.Controls.Add(this.btnClear);
            this.panel8.Controls.Add(this.label39);
            this.panel8.Controls.Add(this.lblToggle);
            this.panel8.Location = new System.Drawing.Point(7, 77);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(489, 266);
            this.panel8.TabIndex = 267;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label31.Location = new System.Drawing.Point(436, 20);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(26, 16);
            this.label31.TabIndex = 268;
            this.label31.Text = "KG";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(2, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 262;
            this.label3.Text = "Client Name:";
            // 
            // txtClientName
            // 
            this.txtClientName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientName.Location = new System.Drawing.Point(83, 135);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(341, 27);
            this.txtClientName.TabIndex = 263;
            this.txtClientName.TabStop = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label28.Location = new System.Drawing.Point(440, 440);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(40, 16);
            this.label28.TabIndex = 269;
            this.label28.Text = "Save";
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(427, 373);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 63);
            this.btnSave.TabIndex = 268;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(25, 439);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 16);
            this.label10.TabIndex = 271;
            this.label10.Text = "Search";
            // 
            // btnFind
            // 
            this.btnFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFind.BackgroundImage")));
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFind.FlatAppearance.BorderSize = 0;
            this.btnFind.Location = new System.Drawing.Point(17, 373);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(67, 63);
            this.btnFind.TabIndex = 270;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // frmOneTimeWeigh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 478);
            this.ControlBox = false;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNoWaste);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOneTimeWeigh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOneTimeWeigh_FormClosing);
            this.Load += new System.EventHandler(this.frmOneTimeWeigh_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Button btnWeigh;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtNetWeight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSecondWeight;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtFirstWeight;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNoWaste;
        private System.Windows.Forms.Label lblToggle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblWgtOutTime;
        private System.Windows.Forms.Label lblWgtInTime;
        private System.Windows.Forms.Label lblWgtOutDate;
        private System.Windows.Forms.Label lblWgtInDate;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnFind;
    }
}