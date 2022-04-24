﻿namespace V_Speeds
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
            this.oat_input = new System.Windows.Forms.NumericUpDown();
            this.lsa_input = new System.Windows.Forms.NumericUpDown();
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
            this.cl_input = new System.Windows.Forms.NumericUpDown();
            this.qfe_input = new System.Windows.Forms.NumericUpDown();
            this.gw_input = new System.Windows.Forms.NumericUpDown();
            this.weightUnit = new System.Windows.Forms.ComboBox();
            this.oatUnit = new System.Windows.Forms.ComboBox();
            this.qfeUnit = new System.Windows.Forms.ComboBox();
            this.lsaUnit = new System.Windows.Forms.ComboBox();
            this.v1_output = new System.Windows.Forms.TextBox();
            this.calcV2 = new System.Windows.Forms.Button();
            this.thrUnit = new System.Windows.Forms.ComboBox();
            this.thr_input = new System.Windows.Forms.NumericUpDown();
            this.bfUnit = new System.Windows.Forms.ComboBox();
            this.bf_input = new System.Windows.Forms.NumericUpDown();
            this.rlUnit = new System.Windows.Forms.ComboBox();
            this.rl_input = new System.Windows.Forms.NumericUpDown();
            this.rtr_input = new System.Windows.Forms.NumericUpDown();
            this.cd_input = new System.Windows.Forms.NumericUpDown();
            this.csaUnit = new System.Windows.Forms.ComboBox();
            this.csa_input = new System.Windows.Forms.NumericUpDown();
            this.v2_output = new System.Windows.Forms.TextBox();
            this.calcV1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.oat_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsa_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cl_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qfe_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gw_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thr_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bf_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rl_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtr_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cd_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csa_input)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "OAT";
            this.toolTip1.SetToolTip(this.label1, "Outside Air Temperature");
            // 
            // oat_input
            // 
            this.oat_input.AccessibleName = "";
            this.oat_input.DecimalPlaces = 2;
            this.oat_input.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.oat_input.Location = new System.Drawing.Point(60, 41);
            this.oat_input.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.oat_input.Minimum = new decimal(new int[] {
            273,
            0,
            0,
            -2147483648});
            this.oat_input.Name = "oat_input";
            this.oat_input.Size = new System.Drawing.Size(98, 20);
            this.oat_input.TabIndex = 6;
            this.oat_input.ThousandsSeparator = true;
            this.oat_input.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.oat_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.oat_input.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // lsa_input
            // 
            this.lsa_input.AccessibleName = "";
            this.lsa_input.DecimalPlaces = 2;
            this.lsa_input.Location = new System.Drawing.Point(60, 93);
            this.lsa_input.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.lsa_input.Name = "lsa_input";
            this.lsa_input.Size = new System.Drawing.Size(98, 20);
            this.lsa_input.TabIndex = 8;
            this.lsa_input.Tag = "LSA";
            this.lsa_input.ThousandsSeparator = true;
            this.lsa_input.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lsa_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.lsa_input.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 97);
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
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "CL";
            this.toolTip1.SetToolTip(this.label3, "Lift Coefficient");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "QFE";
            this.toolTip1.SetToolTip(this.label4, "Pressure at field");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "GW";
            this.toolTip1.SetToolTip(this.label5, "Gross Weight");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 359);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "V1 =";
            this.toolTip1.SetToolTip(this.label6, "Point of no return... IAS!");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(167, 359);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "KIAS";
            this.toolTip1.SetToolTip(this.label7, "Knots Indicated Airspeed");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(332, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "THR";
            this.toolTip1.SetToolTip(this.label8, "Engine Thrust");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(332, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "BF";
            this.toolTip1.SetToolTip(this.label9, "Brake Force");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(332, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "RL";
            this.toolTip1.SetToolTip(this.label10, "Runway Length");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(332, 149);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "RTR";
            this.toolTip1.SetToolTip(this.label11, "Reverse Thrust Ratio");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(332, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "CD";
            this.toolTip1.SetToolTip(this.label12, "Drag Coefficient");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(332, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "CSA";
            this.toolTip1.SetToolTip(this.label13, "Cross-Sectional Area");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(167, 385);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 40;
            this.label14.Text = "KIAS";
            this.toolTip1.SetToolTip(this.label14, "Knots Indicated Airspeed");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(26, 385);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "V2 =";
            this.toolTip1.SetToolTip(this.label15, "Minimum IAS for level flight");
            // 
            // cl_input
            // 
            this.cl_input.AccessibleName = "";
            this.cl_input.DecimalPlaces = 2;
            this.cl_input.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.cl_input.Location = new System.Drawing.Point(60, 119);
            this.cl_input.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.cl_input.Name = "cl_input";
            this.cl_input.Size = new System.Drawing.Size(98, 20);
            this.cl_input.TabIndex = 9;
            this.cl_input.Tag = "LSA";
            this.cl_input.ThousandsSeparator = true;
            this.cl_input.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cl_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.cl_input.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // qfe_input
            // 
            this.qfe_input.AccessibleName = "";
            this.qfe_input.DecimalPlaces = 2;
            this.qfe_input.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.qfe_input.Location = new System.Drawing.Point(61, 67);
            this.qfe_input.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.qfe_input.Name = "qfe_input";
            this.qfe_input.Size = new System.Drawing.Size(98, 20);
            this.qfe_input.TabIndex = 7;
            this.qfe_input.ThousandsSeparator = true;
            this.qfe_input.Value = new decimal(new int[] {
            101325,
            0,
            0,
            131072});
            this.qfe_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.qfe_input.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // gw_input
            // 
            this.gw_input.AccessibleName = "";
            this.gw_input.DecimalPlaces = 2;
            this.gw_input.Location = new System.Drawing.Point(60, 15);
            this.gw_input.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.gw_input.Name = "gw_input";
            this.gw_input.Size = new System.Drawing.Size(98, 20);
            this.gw_input.TabIndex = 5;
            this.gw_input.ThousandsSeparator = true;
            this.gw_input.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.gw_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            this.gw_input.Enter += new System.EventHandler(this.NumericUpDown_Focus);
            // 
            // weightUnit
            // 
            this.weightUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.weightUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weightUnit.Items.AddRange(new object[] {
            "kgs",
            "Lbs"});
            this.weightUnit.Location = new System.Drawing.Point(165, 13);
            this.weightUnit.MaxDropDownItems = 2;
            this.weightUnit.Name = "weightUnit";
            this.weightUnit.Size = new System.Drawing.Size(52, 21);
            this.weightUnit.TabIndex = 10;
            // 
            // oatUnit
            // 
            this.oatUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.oatUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.oatUnit.Items.AddRange(new object[] {
            "°C",
            "°F"});
            this.oatUnit.Location = new System.Drawing.Point(164, 40);
            this.oatUnit.MaxDropDownItems = 2;
            this.oatUnit.Name = "oatUnit";
            this.oatUnit.Size = new System.Drawing.Size(52, 21);
            this.oatUnit.TabIndex = 11;
            // 
            // qfeUnit
            // 
            this.qfeUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.qfeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qfeUnit.Items.AddRange(new object[] {
            "mBar",
            "inHg"});
            this.qfeUnit.Location = new System.Drawing.Point(164, 66);
            this.qfeUnit.MaxDropDownItems = 2;
            this.qfeUnit.Name = "qfeUnit";
            this.qfeUnit.Size = new System.Drawing.Size(52, 21);
            this.qfeUnit.TabIndex = 12;
            // 
            // lsaUnit
            // 
            this.lsaUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.lsaUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lsaUnit.Items.AddRange(new object[] {
            "m²",
            "ft²"});
            this.lsaUnit.Location = new System.Drawing.Point(164, 94);
            this.lsaUnit.MaxDropDownItems = 2;
            this.lsaUnit.Name = "lsaUnit";
            this.lsaUnit.Size = new System.Drawing.Size(52, 21);
            this.lsaUnit.TabIndex = 13;
            // 
            // v1_output
            // 
            this.v1_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.v1_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v1_output.Location = new System.Drawing.Point(61, 355);
            this.v1_output.Name = "v1_output";
            this.v1_output.ReadOnly = true;
            this.v1_output.Size = new System.Drawing.Size(100, 20);
            this.v1_output.TabIndex = 35;
            this.v1_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // calcV2
            // 
            this.calcV2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calcV2.Location = new System.Drawing.Point(208, 380);
            this.calcV2.Name = "calcV2";
            this.calcV2.Size = new System.Drawing.Size(173, 23);
            this.calcV2.TabIndex = 41;
            this.calcV2.Text = "Calculate V2";
            this.calcV2.UseVisualStyleBackColor = true;
            this.calcV2.Click += new System.EventHandler(this.CalcV2);
            // 
            // thrUnit
            // 
            this.thrUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.thrUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thrUnit.Items.AddRange(new object[] {
            "N",
            "Lbf"});
            this.thrUnit.Location = new System.Drawing.Point(472, 13);
            this.thrUnit.MaxDropDownItems = 2;
            this.thrUnit.Name = "thrUnit";
            this.thrUnit.Size = new System.Drawing.Size(52, 21);
            this.thrUnit.TabIndex = 20;
            // 
            // thr_input
            // 
            this.thr_input.AccessibleName = "";
            this.thr_input.DecimalPlaces = 2;
            this.thr_input.Location = new System.Drawing.Point(367, 15);
            this.thr_input.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.thr_input.Name = "thr_input";
            this.thr_input.Size = new System.Drawing.Size(98, 20);
            this.thr_input.TabIndex = 19;
            this.thr_input.ThousandsSeparator = true;
            this.thr_input.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.thr_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // bfUnit
            // 
            this.bfUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.bfUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bfUnit.Items.AddRange(new object[] {
            "N",
            "Lbf"});
            this.bfUnit.Location = new System.Drawing.Point(472, 39);
            this.bfUnit.MaxDropDownItems = 2;
            this.bfUnit.Name = "bfUnit";
            this.bfUnit.Size = new System.Drawing.Size(52, 21);
            this.bfUnit.TabIndex = 23;
            // 
            // bf_input
            // 
            this.bf_input.AccessibleName = "";
            this.bf_input.DecimalPlaces = 2;
            this.bf_input.Location = new System.Drawing.Point(367, 41);
            this.bf_input.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.bf_input.Name = "bf_input";
            this.bf_input.Size = new System.Drawing.Size(98, 20);
            this.bf_input.TabIndex = 22;
            this.bf_input.ThousandsSeparator = true;
            this.bf_input.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.bf_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // rlUnit
            // 
            this.rlUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.rlUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rlUnit.Items.AddRange(new object[] {
            "m",
            "ft"});
            this.rlUnit.Location = new System.Drawing.Point(472, 65);
            this.rlUnit.MaxDropDownItems = 2;
            this.rlUnit.Name = "rlUnit";
            this.rlUnit.Size = new System.Drawing.Size(52, 21);
            this.rlUnit.TabIndex = 26;
            // 
            // rl_input
            // 
            this.rl_input.AccessibleName = "";
            this.rl_input.DecimalPlaces = 2;
            this.rl_input.Location = new System.Drawing.Point(367, 67);
            this.rl_input.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.rl_input.Name = "rl_input";
            this.rl_input.Size = new System.Drawing.Size(98, 20);
            this.rl_input.TabIndex = 25;
            this.rl_input.ThousandsSeparator = true;
            this.rl_input.Value = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.rl_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // rtr_input
            // 
            this.rtr_input.AccessibleName = "";
            this.rtr_input.DecimalPlaces = 3;
            this.rtr_input.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rtr_input.Location = new System.Drawing.Point(367, 147);
            this.rtr_input.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rtr_input.Name = "rtr_input";
            this.rtr_input.Size = new System.Drawing.Size(98, 20);
            this.rtr_input.TabIndex = 33;
            this.rtr_input.ThousandsSeparator = true;
            this.rtr_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // cd_input
            // 
            this.cd_input.AccessibleName = "";
            this.cd_input.DecimalPlaces = 2;
            this.cd_input.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.cd_input.Location = new System.Drawing.Point(367, 121);
            this.cd_input.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cd_input.Name = "cd_input";
            this.cd_input.Size = new System.Drawing.Size(98, 20);
            this.cd_input.TabIndex = 31;
            this.cd_input.ThousandsSeparator = true;
            this.cd_input.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.cd_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // csaUnit
            // 
            this.csaUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.csaUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.csaUnit.Items.AddRange(new object[] {
            "m²",
            "ft²"});
            this.csaUnit.Location = new System.Drawing.Point(472, 93);
            this.csaUnit.MaxDropDownItems = 2;
            this.csaUnit.Name = "csaUnit";
            this.csaUnit.Size = new System.Drawing.Size(52, 21);
            this.csaUnit.TabIndex = 29;
            // 
            // csa_input
            // 
            this.csa_input.AccessibleName = "";
            this.csa_input.DecimalPlaces = 2;
            this.csa_input.Location = new System.Drawing.Point(367, 95);
            this.csa_input.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.csa_input.Name = "csa_input";
            this.csa_input.Size = new System.Drawing.Size(98, 20);
            this.csa_input.TabIndex = 28;
            this.csa_input.ThousandsSeparator = true;
            this.csa_input.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.csa_input.ValueChanged += new System.EventHandler(this.UpdateModel);
            // 
            // v2_output
            // 
            this.v2_output.BackColor = System.Drawing.SystemColors.ControlLight;
            this.v2_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v2_output.Location = new System.Drawing.Point(61, 381);
            this.v2_output.Name = "v2_output";
            this.v2_output.ReadOnly = true;
            this.v2_output.Size = new System.Drawing.Size(100, 20);
            this.v2_output.TabIndex = 39;
            this.v2_output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // calcV1
            // 
            this.calcV1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calcV1.Location = new System.Drawing.Point(208, 354);
            this.calcV1.Name = "calcV1";
            this.calcV1.Size = new System.Drawing.Size(173, 23);
            this.calcV1.TabIndex = 37;
            this.calcV1.Text = "Calculate V1";
            this.calcV1.UseVisualStyleBackColor = true;
            this.calcV1.Click += new System.EventHandler(this.CalcV1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.calcV1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.v2_output);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.csaUnit);
            this.Controls.Add(this.csa_input);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cd_input);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.rtr_input);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rlUnit);
            this.Controls.Add(this.rl_input);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bfUnit);
            this.Controls.Add(this.bf_input);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.thrUnit);
            this.Controls.Add(this.thr_input);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.calcV2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.v1_output);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lsaUnit);
            this.Controls.Add(this.qfeUnit);
            this.Controls.Add(this.oatUnit);
            this.Controls.Add(this.weightUnit);
            this.Controls.Add(this.gw_input);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.qfe_input);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cl_input);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lsa_input);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.oat_input);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form1";
            this.Text = "V-Speeds";
            ((System.ComponentModel.ISupportInitialize)(this.oat_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsa_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cl_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qfe_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gw_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thr_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bf_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rl_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtr_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cd_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csa_input)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown oat_input;
        private System.Windows.Forms.NumericUpDown lsa_input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown cl_input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown qfe_input;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown gw_input;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox weightUnit;
        private System.Windows.Forms.ComboBox oatUnit;
        private System.Windows.Forms.ComboBox qfeUnit;
        private System.Windows.Forms.ComboBox lsaUnit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox v1_output;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button calcV2;
        private System.Windows.Forms.ComboBox thrUnit;
        private System.Windows.Forms.NumericUpDown thr_input;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox bfUnit;
        private System.Windows.Forms.NumericUpDown bf_input;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox rlUnit;
        private System.Windows.Forms.NumericUpDown rl_input;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown rtr_input;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown cd_input;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox csaUnit;
        private System.Windows.Forms.NumericUpDown csa_input;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox v2_output;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button calcV1;
    }
}
