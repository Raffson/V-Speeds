namespace V_Speeds
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.oat_in = new System.Windows.Forms.NumericUpDown();
            this.lsa_in = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cl_in = new System.Windows.Forms.NumericUpDown();
            this.qfe_in = new System.Windows.Forms.NumericUpDown();
            this.gw_in = new System.Windows.Forms.NumericUpDown();
            this.weightUnit = new System.Windows.Forms.ComboBox();
            this.oatUnit = new System.Windows.Forms.ComboBox();
            this.qfeUnit = new System.Windows.Forms.ComboBox();
            this.lsaUnit = new System.Windows.Forms.ComboBox();
            this.v1eas_output = new System.Windows.Forms.TextBox();
            this.thrUnit = new System.Windows.Forms.ComboBox();
            this.thr_in = new System.Windows.Forms.NumericUpDown();
            this.bfUnit = new System.Windows.Forms.ComboBox();
            this.bf_in = new System.Windows.Forms.NumericUpDown();
            this.rlUnit = new System.Windows.Forms.ComboBox();
            this.rl_in = new System.Windows.Forms.NumericUpDown();
            this.rtr_in = new System.Windows.Forms.NumericUpDown();
            this.cd_in = new System.Windows.Forms.NumericUpDown();
            this.rc_in = new System.Windows.Forms.NumericUpDown();
            this.vs_eas_output = new System.Windows.Forms.TextBox();
            this.calcV1 = new System.Windows.Forms.Button();
            this.apSelect = new System.Windows.Forms.ComboBox();
            this.vs_tas_output = new System.Windows.Forms.TextBox();
            this.v1tas_output = new System.Windows.Forms.TextBox();
            this.clg_in = new System.Windows.Forms.NumericUpDown();
            this.rfc_in = new System.Windows.Forms.NumericUpDown();
            this.dv_ft_output = new System.Windows.Forms.TextBox();
            this.dv_m_output = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.oat_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsa_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cl_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qfe_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gw_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thr_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bf_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rl_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtr_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cd_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rc_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clg_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfc_in)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(31, 183);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "OAT";
            this.toolTip1.SetToolTip(this.label1, "Outside Air Temperature");
            // 
            // oat_in
            // 
            this.oat_in.AccessibleName = "";
            this.oat_in.DecimalPlaces = 2;
            this.oat_in.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.oat_in.Location = new System.Drawing.Point(72, 178);
            this.oat_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.oat_in.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.oat_in.Minimum = new decimal(new int[] {
            273,
            0,
            0,
            -2147483648});
            this.oat_in.Name = "oat_in";
            this.oat_in.Size = new System.Drawing.Size(114, 23);
            this.oat_in.TabIndex = 6;
            this.oat_in.ThousandsSeparator = true;
            this.oat_in.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.oat_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.oat_in.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // lsa_in
            // 
            this.lsa_in.AccessibleName = "";
            this.lsa_in.DecimalPlaces = 2;
            this.lsa_in.Location = new System.Drawing.Point(73, 236);
            this.lsa_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lsa_in.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.lsa_in.Name = "lsa_in";
            this.lsa_in.Size = new System.Drawing.Size(114, 23);
            this.lsa_in.TabIndex = 8;
            this.lsa_in.Tag = "LSA";
            this.lsa_in.ThousandsSeparator = true;
            this.lsa_in.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lsa_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.lsa_in.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(32, 241);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "LSA";
            this.toolTip1.SetToolTip(this.label2, "Lifting Surface Area");
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(32, 270);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "CL";
            this.toolTip1.SetToolTip(this.label3, "Lift Coefficient");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(31, 212);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "QFE";
            this.toolTip1.SetToolTip(this.label4, "Pressure at field");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(31, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "GW";
            this.toolTip1.SetToolTip(this.label5, "Gross Weight");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(32, 413);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "V1 =";
            this.toolTip1.SetToolTip(this.label6, "Point of no return, overshoot if abort...");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(197, 413);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "KEAS";
            this.toolTip1.SetToolTip(this.label7, "Knots Indicated Airspeed");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(389, 154);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "THR";
            this.toolTip1.SetToolTip(this.label8, "Engine Thrust");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(389, 183);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "BF";
            this.toolTip1.SetToolTip(this.label9, "Brake Force (consider the brakes only)");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(389, 212);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "RL";
            this.toolTip1.SetToolTip(this.label10, "Runway Length");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(390, 299);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "RTR";
            this.toolTip1.SetToolTip(this.label11, "Reverse Thrust Ratio");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(390, 270);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "CD";
            this.toolTip1.SetToolTip(this.label12, "Drag Coefficient (varies with loadout)");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(390, 241);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "RC";
            this.toolTip1.SetToolTip(this.label13, "Reaction time, including time for engine spooldown etc.");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(198, 439);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 40;
            this.label14.Text = "KEAS";
            this.toolTip1.SetToolTip(this.label14, "Knots Indicated Airspeed");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(33, 439);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "Vs =";
            this.toolTip1.SetToolTip(this.label15, "Minimum airspeed for level flight");
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(3, 88);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = "Aircraft Profile";
            this.toolTip1.SetToolTip(this.label16, "Select a profile to lock certain parameters");
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label17.Location = new System.Drawing.Point(369, 439);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 47;
            this.label17.Text = "KTAS";
            this.toolTip1.SetToolTip(this.label17, "Knots Indicated Airspeed");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(368, 413);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "KTAS";
            this.toolTip1.SetToolTip(this.label18, "Knots Indicated Airspeed");
            // 
            // label19
            // 
            this.label19.AccessibleDescription = "";
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label19.Location = new System.Drawing.Point(32, 299);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 13);
            this.label19.TabIndex = 10;
            this.label19.Text = "CLG";
            this.toolTip1.SetToolTip(this.label19, "Lift Coefficient when rolling level on the ground in takeoff configuration");
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label20.Location = new System.Drawing.Point(390, 328);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 48;
            this.label20.Text = "RFC";
            this.toolTip1.SetToolTip(this.label20, "Rolling Friction Coefficient");
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label21.Location = new System.Drawing.Point(369, 465);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 13);
            this.label21.TabIndex = 54;
            this.label21.Text = "ft";
            this.toolTip1.SetToolTip(this.label21, "Knots Indicated Airspeed");
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label22.Location = new System.Drawing.Point(198, 465);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(16, 13);
            this.label22.TabIndex = 52;
            this.label22.Text = "m";
            this.toolTip1.SetToolTip(this.label22, "Knots Indicated Airspeed");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label23.Location = new System.Drawing.Point(33, 465);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(34, 13);
            this.label23.TabIndex = 50;
            this.label23.Text = "Dv =";
            this.toolTip1.SetToolTip(this.label23, "Runway needed to reach Vs");
            // 
            // cl_in
            // 
            this.cl_in.AccessibleName = "";
            this.cl_in.DecimalPlaces = 3;
            this.cl_in.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.cl_in.Location = new System.Drawing.Point(73, 265);
            this.cl_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cl_in.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.cl_in.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.cl_in.Name = "cl_in";
            this.cl_in.Size = new System.Drawing.Size(114, 23);
            this.cl_in.TabIndex = 9;
            this.cl_in.Tag = "";
            this.cl_in.ThousandsSeparator = true;
            this.cl_in.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cl_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.cl_in.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // qfe_in
            // 
            this.qfe_in.AccessibleName = "";
            this.qfe_in.DecimalPlaces = 2;
            this.qfe_in.Location = new System.Drawing.Point(73, 207);
            this.qfe_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.qfe_in.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.qfe_in.Name = "qfe_in";
            this.qfe_in.Size = new System.Drawing.Size(114, 23);
            this.qfe_in.TabIndex = 7;
            this.qfe_in.ThousandsSeparator = true;
            this.qfe_in.Value = new decimal(new int[] {
            101325,
            0,
            0,
            131072});
            this.qfe_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.qfe_in.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // gw_in
            // 
            this.gw_in.AccessibleName = "";
            this.gw_in.DecimalPlaces = 2;
            this.gw_in.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.gw_in.Location = new System.Drawing.Point(72, 149);
            this.gw_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gw_in.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.gw_in.Name = "gw_in";
            this.gw_in.Size = new System.Drawing.Size(114, 23);
            this.gw_in.TabIndex = 5;
            this.gw_in.ThousandsSeparator = true;
            this.gw_in.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.gw_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.gw_in.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // weightUnit
            // 
            this.weightUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.weightUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weightUnit.Items.AddRange(new object[] {
            "kgs",
            "Lbs"});
            this.weightUnit.Location = new System.Drawing.Point(194, 149);
            this.weightUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.weightUnit.MaxDropDownItems = 2;
            this.weightUnit.Name = "weightUnit";
            this.weightUnit.Size = new System.Drawing.Size(60, 23);
            this.weightUnit.TabIndex = 10;
            // 
            // oatUnit
            // 
            this.oatUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.oatUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.oatUnit.Items.AddRange(new object[] {
            "°C",
            "°F"});
            this.oatUnit.Location = new System.Drawing.Point(193, 178);
            this.oatUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.oatUnit.MaxDropDownItems = 2;
            this.oatUnit.Name = "oatUnit";
            this.oatUnit.Size = new System.Drawing.Size(60, 23);
            this.oatUnit.TabIndex = 11;
            // 
            // qfeUnit
            // 
            this.qfeUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.qfeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qfeUnit.Items.AddRange(new object[] {
            "mBar",
            "inHg"});
            this.qfeUnit.Location = new System.Drawing.Point(193, 207);
            this.qfeUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.qfeUnit.MaxDropDownItems = 2;
            this.qfeUnit.Name = "qfeUnit";
            this.qfeUnit.Size = new System.Drawing.Size(60, 23);
            this.qfeUnit.TabIndex = 12;
            // 
            // lsaUnit
            // 
            this.lsaUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.lsaUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lsaUnit.Items.AddRange(new object[] {
            "m²",
            "ft²"});
            this.lsaUnit.Location = new System.Drawing.Point(194, 236);
            this.lsaUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lsaUnit.MaxDropDownItems = 2;
            this.lsaUnit.Name = "lsaUnit";
            this.lsaUnit.Size = new System.Drawing.Size(60, 23);
            this.lsaUnit.TabIndex = 13;
            // 
            // v1eas_output
            // 
            this.v1eas_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.v1eas_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.v1eas_output.Location = new System.Drawing.Point(73, 409);
            this.v1eas_output.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.v1eas_output.Name = "v1eas_output";
            this.v1eas_output.ReadOnly = true;
            this.v1eas_output.Size = new System.Drawing.Size(116, 20);
            this.v1eas_output.TabIndex = 35;
            this.v1eas_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // thrUnit
            // 
            this.thrUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.thrUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thrUnit.Items.AddRange(new object[] {
            "N",
            "Lbf"});
            this.thrUnit.Location = new System.Drawing.Point(553, 149);
            this.thrUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.thrUnit.MaxDropDownItems = 2;
            this.thrUnit.Name = "thrUnit";
            this.thrUnit.Size = new System.Drawing.Size(60, 23);
            this.thrUnit.TabIndex = 20;
            // 
            // thr_in
            // 
            this.thr_in.AccessibleName = "";
            this.thr_in.DecimalPlaces = 2;
            this.thr_in.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.thr_in.Location = new System.Drawing.Point(430, 149);
            this.thr_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.thr_in.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.thr_in.Name = "thr_in";
            this.thr_in.Size = new System.Drawing.Size(114, 23);
            this.thr_in.TabIndex = 19;
            this.thr_in.ThousandsSeparator = true;
            this.thr_in.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.thr_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // bfUnit
            // 
            this.bfUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.bfUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bfUnit.Items.AddRange(new object[] {
            "N",
            "Lbf"});
            this.bfUnit.Location = new System.Drawing.Point(553, 178);
            this.bfUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bfUnit.MaxDropDownItems = 2;
            this.bfUnit.Name = "bfUnit";
            this.bfUnit.Size = new System.Drawing.Size(60, 23);
            this.bfUnit.TabIndex = 23;
            // 
            // bf_in
            // 
            this.bf_in.AccessibleName = "";
            this.bf_in.DecimalPlaces = 2;
            this.bf_in.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.bf_in.Location = new System.Drawing.Point(430, 178);
            this.bf_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bf_in.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.bf_in.Name = "bf_in";
            this.bf_in.Size = new System.Drawing.Size(114, 23);
            this.bf_in.TabIndex = 22;
            this.bf_in.ThousandsSeparator = true;
            this.bf_in.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.bf_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // rlUnit
            // 
            this.rlUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.rlUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rlUnit.Items.AddRange(new object[] {
            "m",
            "ft"});
            this.rlUnit.Location = new System.Drawing.Point(553, 207);
            this.rlUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rlUnit.MaxDropDownItems = 2;
            this.rlUnit.Name = "rlUnit";
            this.rlUnit.Size = new System.Drawing.Size(60, 23);
            this.rlUnit.TabIndex = 26;
            // 
            // rl_in
            // 
            this.rl_in.AccessibleName = "";
            this.rl_in.DecimalPlaces = 2;
            this.rl_in.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.rl_in.Location = new System.Drawing.Point(430, 207);
            this.rl_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rl_in.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.rl_in.Name = "rl_in";
            this.rl_in.Size = new System.Drawing.Size(114, 23);
            this.rl_in.TabIndex = 25;
            this.rl_in.ThousandsSeparator = true;
            this.rl_in.Value = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.rl_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // rtr_in
            // 
            this.rtr_in.AccessibleName = "";
            this.rtr_in.DecimalPlaces = 3;
            this.rtr_in.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rtr_in.Location = new System.Drawing.Point(431, 294);
            this.rtr_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rtr_in.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rtr_in.Name = "rtr_in";
            this.rtr_in.Size = new System.Drawing.Size(114, 23);
            this.rtr_in.TabIndex = 33;
            this.rtr_in.ThousandsSeparator = true;
            this.rtr_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // cd_in
            // 
            this.cd_in.AccessibleName = "";
            this.cd_in.DecimalPlaces = 3;
            this.cd_in.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.cd_in.Location = new System.Drawing.Point(431, 265);
            this.cd_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cd_in.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cd_in.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.cd_in.Name = "cd_in";
            this.cd_in.Size = new System.Drawing.Size(114, 23);
            this.cd_in.TabIndex = 31;
            this.cd_in.ThousandsSeparator = true;
            this.cd_in.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.cd_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // rc_in
            // 
            this.rc_in.AccessibleName = "";
            this.rc_in.DecimalPlaces = 2;
            this.rc_in.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.rc_in.Location = new System.Drawing.Point(431, 236);
            this.rc_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rc_in.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.rc_in.Name = "rc_in";
            this.rc_in.Size = new System.Drawing.Size(114, 23);
            this.rc_in.TabIndex = 28;
            this.rc_in.ThousandsSeparator = true;
            this.rc_in.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.rc_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // vs_eas_output
            // 
            this.vs_eas_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vs_eas_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.vs_eas_output.Location = new System.Drawing.Point(74, 435);
            this.vs_eas_output.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.vs_eas_output.Name = "vs_eas_output";
            this.vs_eas_output.ReadOnly = true;
            this.vs_eas_output.Size = new System.Drawing.Size(116, 20);
            this.vs_eas_output.TabIndex = 39;
            this.vs_eas_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // calcV1
            // 
            this.calcV1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.calcV1.Location = new System.Drawing.Point(410, 409);
            this.calcV1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.calcV1.Name = "calcV1";
            this.calcV1.Size = new System.Drawing.Size(202, 72);
            this.calcV1.TabIndex = 37;
            this.calcV1.Text = "Calculate";
            this.calcV1.UseVisualStyleBackColor = true;
            this.calcV1.Click += new System.EventHandler(this.DoCalculations);
            // 
            // apSelect
            // 
            this.apSelect.FormattingEnabled = true;
            this.apSelect.Items.AddRange(new object[] {
            "Custom...",
            "DCS A-10C",
            "DCS F-14A (AB)",
            "DCS F-14A (MIL)",
            "DCS F-14B (AB)",
            "DCS F-14B (MIL)",
            "DCS F-15C (AB)",
            "DCS F-15C (MIL)",
            "DCS F-16C blk50 (AB)",
            "DCS F-16C blk50 (MIL)",
            "DCS F-18C (AB)",
            "DCS F-18C (MIL)"});
            this.apSelect.Location = new System.Drawing.Point(113, 85);
            this.apSelect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.apSelect.Name = "apSelect";
            this.apSelect.Size = new System.Drawing.Size(140, 23);
            this.apSelect.TabIndex = 43;
            // 
            // vs_tas_output
            // 
            this.vs_tas_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vs_tas_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.vs_tas_output.Location = new System.Drawing.Point(245, 435);
            this.vs_tas_output.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.vs_tas_output.Name = "vs_tas_output";
            this.vs_tas_output.ReadOnly = true;
            this.vs_tas_output.Size = new System.Drawing.Size(116, 20);
            this.vs_tas_output.TabIndex = 46;
            this.vs_tas_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // v1tas_output
            // 
            this.v1tas_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.v1tas_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.v1tas_output.Location = new System.Drawing.Point(244, 409);
            this.v1tas_output.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.v1tas_output.Name = "v1tas_output";
            this.v1tas_output.ReadOnly = true;
            this.v1tas_output.Size = new System.Drawing.Size(116, 20);
            this.v1tas_output.TabIndex = 44;
            this.v1tas_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clg_in
            // 
            this.clg_in.AccessibleName = "";
            this.clg_in.DecimalPlaces = 3;
            this.clg_in.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.clg_in.Location = new System.Drawing.Point(73, 294);
            this.clg_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.clg_in.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.clg_in.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.clg_in.Name = "clg_in";
            this.clg_in.Size = new System.Drawing.Size(114, 23);
            this.clg_in.TabIndex = 11;
            this.clg_in.Tag = "";
            this.clg_in.ThousandsSeparator = true;
            this.clg_in.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.clg_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // rfc_in
            // 
            this.rfc_in.AccessibleName = "";
            this.rfc_in.DecimalPlaces = 3;
            this.rfc_in.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.rfc_in.Location = new System.Drawing.Point(431, 323);
            this.rfc_in.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rfc_in.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.rfc_in.Name = "rfc_in";
            this.rfc_in.Size = new System.Drawing.Size(114, 23);
            this.rfc_in.TabIndex = 49;
            this.rfc_in.ThousandsSeparator = true;
            this.rfc_in.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.rfc_in.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // dv_ft_output
            // 
            this.dv_ft_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dv_ft_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dv_ft_output.Location = new System.Drawing.Point(245, 461);
            this.dv_ft_output.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dv_ft_output.Name = "dv_ft_output";
            this.dv_ft_output.ReadOnly = true;
            this.dv_ft_output.Size = new System.Drawing.Size(116, 20);
            this.dv_ft_output.TabIndex = 53;
            this.dv_ft_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dv_m_output
            // 
            this.dv_m_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dv_m_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dv_m_output.Location = new System.Drawing.Point(74, 461);
            this.dv_m_output.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dv_m_output.Name = "dv_m_output";
            this.dv_m_output.ReadOnly = true;
            this.dv_m_output.Size = new System.Drawing.Size(116, 20);
            this.dv_m_output.TabIndex = 51;
            this.dv_m_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 509);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.dv_ft_output);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.dv_m_output);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.rfc_in);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.clg_in);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.vs_tas_output);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.v1tas_output);
            this.Controls.Add(this.apSelect);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.calcV1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.vs_eas_output);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.rc_in);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cd_in);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.rtr_in);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rlUnit);
            this.Controls.Add(this.rl_in);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bfUnit);
            this.Controls.Add(this.bf_in);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.thrUnit);
            this.Controls.Add(this.thr_in);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.v1eas_output);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lsaUnit);
            this.Controls.Add(this.qfeUnit);
            this.Controls.Add(this.oatUnit);
            this.Controls.Add(this.weightUnit);
            this.Controls.Add(this.gw_in);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.qfe_in);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cl_in);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lsa_in);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.oat_in);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(744, 548);
            this.Name = "Form1";
            this.Text = "V-Speeds";
            ((System.ComponentModel.ISupportInitialize)(this.oat_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsa_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cl_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qfe_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gw_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thr_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bf_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rl_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtr_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cd_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rc_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clg_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfc_in)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown oat_in;
        private System.Windows.Forms.NumericUpDown lsa_in;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown cl_in;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown qfe_in;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown gw_in;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox weightUnit;
        private System.Windows.Forms.ComboBox oatUnit;
        private System.Windows.Forms.ComboBox qfeUnit;
        private System.Windows.Forms.ComboBox lsaUnit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox v1eas_output;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox thrUnit;
        private System.Windows.Forms.NumericUpDown thr_in;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox bfUnit;
        private System.Windows.Forms.NumericUpDown bf_in;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox rlUnit;
        private System.Windows.Forms.NumericUpDown rl_in;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown rtr_in;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown cd_in;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown rc_in;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox vs_eas_output;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button calcV1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox apSelect;
        private Label label17;
        private TextBox vs_tas_output;
        private Label label18;
        private TextBox v1tas_output;
        private NumericUpDown clg_in;
        private Label label19;
        private NumericUpDown rfc_in;
        private Label label20;
        private Label label21;
        private TextBox dv_ft_output;
        private Label label22;
        private TextBox dv_m_output;
        private Label label23;
    }
}

