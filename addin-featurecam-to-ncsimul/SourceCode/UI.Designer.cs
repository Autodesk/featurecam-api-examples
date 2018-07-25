// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToNCSIMUL
{
    partial class UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.outputDirBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.b_PreviewOffset = new System.Windows.Forms.Button();
            this.tbOffsetZ = new System.Windows.Forms.TextBox();
            this.tbOffsetY = new System.Windows.Forms.TextBox();
            this.lb_offsetZ = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_offsetY = new System.Windows.Forms.Label();
            this.lb_offsetX = new System.Windows.Forms.Label();
            this.tbOffsetX = new System.Windows.Forms.TextBox();
            this.b_select_ncsimul_machine = new System.Windows.Forms.Button();
            this.tb_ncsimul_md_fpath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.b_export = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.b_help = new System.Windows.Forms.Button();
            this.openNCSIMULPostFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openNCSIMULMDFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tb_output_dir = new System.Windows.Forms.TextBox();
            this.b_select_output_dir = new System.Windows.Forms.Button();
            this.lb_clamps = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rb_tool_number = new System.Windows.Forms.RadioButton();
            this.rb_tool_id = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.rb_indiv_offsets = new System.Windows.Forms.RadioButton();
            this.rb_DATUM = new System.Windows.Forms.RadioButton();
            this.gb_5axis = new System.Windows.Forms.GroupBox();
            this.lb_setup_warning = new System.Windows.Forms.Label();
            this.cb_setups_list = new System.Windows.Forms.ComboBox();
            this.gb_no_indexing = new System.Windows.Forms.GroupBox();
            this.gb_turret_info = new System.Windows.Forms.GroupBox();
            this.p_lsss = new System.Windows.Forms.Panel();
            this.l_lsss = new System.Windows.Forms.Label();
            this.rb_lsss_milling_head = new System.Windows.Forms.RadioButton();
            this.rb_lsss_turret = new System.Windows.Forms.RadioButton();
            this.p_lmss = new System.Windows.Forms.Panel();
            this.l_lmss = new System.Windows.Forms.Label();
            this.rb_lmss_milling_head = new System.Windows.Forms.RadioButton();
            this.rb_lmss_turret = new System.Windows.Forms.RadioButton();
            this.p_usss = new System.Windows.Forms.Panel();
            this.l_usss = new System.Windows.Forms.Label();
            this.rb_usss_milling_head = new System.Windows.Forms.RadioButton();
            this.rb_usss_turret = new System.Windows.Forms.RadioButton();
            this.p_umss = new System.Windows.Forms.Panel();
            this.rb_umss_milling_head = new System.Windows.Forms.RadioButton();
            this.rb_umss_turret = new System.Windows.Forms.RadioButton();
            this.l_umss = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lb_file_dir = new System.Windows.Forms.Label();
            this.rb_save_to_file_dir = new System.Windows.Forms.RadioButton();
            this.rb_save_to_other_dir = new System.Windows.Forms.RadioButton();
            this.cb_create_subdir = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.b_subdir_options = new System.Windows.Forms.Button();
            this.b_SaveSettings = new System.Windows.Forms.Button();
            this.cb_tool_rad_as_offset = new System.Windows.Forms.CheckBox();
            this.cb_tool_length_as_offset = new System.Windows.Forms.CheckBox();
            this.gb_5axis.SuspendLayout();
            this.gb_no_indexing.SuspendLayout();
            this.gb_turret_info.SuspendLayout();
            this.p_lsss.SuspendLayout();
            this.p_lmss.SuspendLayout();
            this.p_usss.SuspendLayout();
            this.p_umss.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_PreviewOffset
            // 
            this.b_PreviewOffset.Location = new System.Drawing.Point(604, 436);
            this.b_PreviewOffset.Margin = new System.Windows.Forms.Padding(4);
            this.b_PreviewOffset.Name = "b_PreviewOffset";
            this.b_PreviewOffset.Size = new System.Drawing.Size(153, 28);
            this.b_PreviewOffset.TabIndex = 33;
            this.b_PreviewOffset.Text = "Preview";
            this.b_PreviewOffset.UseVisualStyleBackColor = true;
            this.b_PreviewOffset.Click += new System.EventHandler(this.bPreviewOffset_Click);
            // 
            // tbOffsetZ
            // 
            this.tbOffsetZ.Location = new System.Drawing.Point(505, 439);
            this.tbOffsetZ.Margin = new System.Windows.Forms.Padding(4);
            this.tbOffsetZ.Name = "tbOffsetZ";
            this.tbOffsetZ.Size = new System.Drawing.Size(65, 22);
            this.tbOffsetZ.TabIndex = 32;
            this.tbOffsetZ.Text = "0.0";
            this.tbOffsetZ.TextChanged += new System.EventHandler(this.tbOffsetZ_TextChanged);
            // 
            // tbOffsetY
            // 
            this.tbOffsetY.Location = new System.Drawing.Point(409, 439);
            this.tbOffsetY.Margin = new System.Windows.Forms.Padding(4);
            this.tbOffsetY.Name = "tbOffsetY";
            this.tbOffsetY.Size = new System.Drawing.Size(65, 22);
            this.tbOffsetY.TabIndex = 31;
            this.tbOffsetY.Text = "0.0";
            this.tbOffsetY.TextChanged += new System.EventHandler(this.tbOffsetY_TextChanged);
            // 
            // lb_offsetZ
            // 
            this.lb_offsetZ.AutoSize = true;
            this.lb_offsetZ.Location = new System.Drawing.Point(484, 443);
            this.lb_offsetZ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_offsetZ.Name = "lb_offsetZ";
            this.lb_offsetZ.Size = new System.Drawing.Size(21, 17);
            this.lb_offsetZ.TabIndex = 30;
            this.lb_offsetZ.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 442);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Machine Zero offset from setup UCS:";
            // 
            // lb_offsetY
            // 
            this.lb_offsetY.AutoSize = true;
            this.lb_offsetY.Location = new System.Drawing.Point(389, 443);
            this.lb_offsetY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_offsetY.Name = "lb_offsetY";
            this.lb_offsetY.Size = new System.Drawing.Size(21, 17);
            this.lb_offsetY.TabIndex = 29;
            this.lb_offsetY.Text = "Y:";
            // 
            // lb_offsetX
            // 
            this.lb_offsetX.AutoSize = true;
            this.lb_offsetX.Location = new System.Drawing.Point(293, 443);
            this.lb_offsetX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_offsetX.Name = "lb_offsetX";
            this.lb_offsetX.Size = new System.Drawing.Size(21, 17);
            this.lb_offsetX.TabIndex = 27;
            this.lb_offsetX.Text = "X:";
            // 
            // tbOffsetX
            // 
            this.tbOffsetX.Location = new System.Drawing.Point(315, 439);
            this.tbOffsetX.Margin = new System.Windows.Forms.Padding(4);
            this.tbOffsetX.Name = "tbOffsetX";
            this.tbOffsetX.Size = new System.Drawing.Size(65, 22);
            this.tbOffsetX.TabIndex = 28;
            this.tbOffsetX.Text = "0.0";
            this.tbOffsetX.TextChanged += new System.EventHandler(this.tbOffsetX_TextChanged);
            // 
            // b_select_ncsimul_machine
            // 
            this.b_select_ncsimul_machine.Location = new System.Drawing.Point(604, 213);
            this.b_select_ncsimul_machine.Margin = new System.Windows.Forms.Padding(4);
            this.b_select_ncsimul_machine.Name = "b_select_ncsimul_machine";
            this.b_select_ncsimul_machine.Size = new System.Drawing.Size(153, 28);
            this.b_select_ncsimul_machine.TabIndex = 11;
            this.b_select_ncsimul_machine.Text = "Browse...";
            this.b_select_ncsimul_machine.UseVisualStyleBackColor = true;
            this.b_select_ncsimul_machine.Click += new System.EventHandler(this.b_select_ncsimul_machine_Click);
            // 
            // tb_ncsimul_md_fpath
            // 
            this.tb_ncsimul_md_fpath.Location = new System.Drawing.Point(40, 217);
            this.tb_ncsimul_md_fpath.Margin = new System.Windows.Forms.Padding(4);
            this.tb_ncsimul_md_fpath.Name = "tb_ncsimul_md_fpath";
            this.tb_ncsimul_md_fpath.Size = new System.Drawing.Size(557, 22);
            this.tb_ncsimul_md_fpath.TabIndex = 10;
            this.tb_ncsimul_md_fpath.TextChanged += new System.EventHandler(this.tb_ncsimul_md_fpath_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 192);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Select NCSIMUL machine file:";
            // 
            // b_export
            // 
            this.b_export.Location = new System.Drawing.Point(317, 705);
            this.b_export.Margin = new System.Windows.Forms.Padding(4);
            this.b_export.Name = "b_export";
            this.b_export.Size = new System.Drawing.Size(144, 31);
            this.b_export.TabIndex = 23;
            this.b_export.Text = "Export";
            this.b_export.UseVisualStyleBackColor = true;
            this.b_export.Click += new System.EventHandler(this.b_export_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(467, 705);
            this.b_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(144, 31);
            this.b_cancel.TabIndex = 24;
            this.b_cancel.Text = "Cancel/Exit";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // b_help
            // 
            this.b_help.Enabled = false;
            this.b_help.Location = new System.Drawing.Point(617, 705);
            this.b_help.Margin = new System.Windows.Forms.Padding(4);
            this.b_help.Name = "b_help";
            this.b_help.Size = new System.Drawing.Size(144, 31);
            this.b_help.TabIndex = 25;
            this.b_help.Text = "Help";
            this.b_help.UseVisualStyleBackColor = true;
            this.b_help.Click += new System.EventHandler(this.b_help_Click);
            // 
            // openNCSIMULPostFileDialog1
            // 
            this.openNCSIMULPostFileDialog1.FileName = "openFileDialog1";
            // 
            // openNCSIMULMDFileDialog1
            // 
            this.openNCSIMULMDFileDialog1.FileName = "openFileDialog1";
            // 
            // tb_output_dir
            // 
            this.tb_output_dir.Location = new System.Drawing.Point(37, 49);
            this.tb_output_dir.Margin = new System.Windows.Forms.Padding(4);
            this.tb_output_dir.Name = "tb_output_dir";
            this.tb_output_dir.Size = new System.Drawing.Size(543, 22);
            this.tb_output_dir.TabIndex = 1;
            this.tb_output_dir.TextChanged += new System.EventHandler(this.tb_output_dir_TextChanged);
            // 
            // b_select_output_dir
            // 
            this.b_select_output_dir.Location = new System.Drawing.Point(589, 46);
            this.b_select_output_dir.Margin = new System.Windows.Forms.Padding(4);
            this.b_select_output_dir.Name = "b_select_output_dir";
            this.b_select_output_dir.Size = new System.Drawing.Size(151, 28);
            this.b_select_output_dir.TabIndex = 2;
            this.b_select_output_dir.Text = "Browse...";
            this.b_select_output_dir.UseVisualStyleBackColor = true;
            this.b_select_output_dir.Click += new System.EventHandler(this.b_select_output_dir_Click);
            // 
            // lb_clamps
            // 
            this.lb_clamps.CheckOnClick = true;
            this.lb_clamps.FormattingEnabled = true;
            this.lb_clamps.Location = new System.Drawing.Point(40, 271);
            this.lb_clamps.Margin = new System.Windows.Forms.Padding(4);
            this.lb_clamps.Name = "lb_clamps";
            this.lb_clamps.Size = new System.Drawing.Size(439, 55);
            this.lb_clamps.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 251);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Select solids to be exported as clamps:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 361);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Post uses to identify tool:";
            // 
            // rb_tool_number
            // 
            this.rb_tool_number.AutoSize = true;
            this.rb_tool_number.Location = new System.Drawing.Point(219, 358);
            this.rb_tool_number.Margin = new System.Windows.Forms.Padding(4);
            this.rb_tool_number.Name = "rb_tool_number";
            this.rb_tool_number.Size = new System.Drawing.Size(109, 21);
            this.rb_tool_number.TabIndex = 28;
            this.rb_tool_number.Text = "Tool number";
            this.rb_tool_number.UseVisualStyleBackColor = true;
            this.rb_tool_number.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_tool_id
            // 
            this.rb_tool_id.AutoSize = true;
            this.rb_tool_id.Location = new System.Drawing.Point(387, 358);
            this.rb_tool_id.Margin = new System.Windows.Forms.Padding(4);
            this.rb_tool_id.Name = "rb_tool_id";
            this.rb_tool_id.Size = new System.Drawing.Size(74, 21);
            this.rb_tool_id.TabIndex = 29;
            this.rb_tool_id.Text = "Tool ID";
            this.rb_tool_id.UseVisualStyleBackColor = true;
            this.rb_tool_id.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 14);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.MaximumSize = new System.Drawing.Size(667, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(631, 32);
            this.label6.TabIndex = 24;
            this.label6.Text = "Active document is 5-axis part with \'NC Code Reference Point\' set to \'Each setup\'" +
    "s own fixture\'. Select whether post uses:";
            // 
            // rb_indiv_offsets
            // 
            this.rb_indiv_offsets.AutoSize = true;
            this.rb_indiv_offsets.Checked = true;
            this.rb_indiv_offsets.Location = new System.Drawing.Point(29, 50);
            this.rb_indiv_offsets.Margin = new System.Windows.Forms.Padding(4);
            this.rb_indiv_offsets.Name = "rb_indiv_offsets";
            this.rb_indiv_offsets.Size = new System.Drawing.Size(169, 21);
            this.rb_indiv_offsets.TabIndex = 34;
            this.rb_indiv_offsets.TabStop = true;
            this.rb_indiv_offsets.Text = "Individual fixture offset";
            this.rb_indiv_offsets.UseVisualStyleBackColor = true;
            this.rb_indiv_offsets.CheckedChanged += new System.EventHandler(this.rb_indiv_offsets_CheckedChanged);
            // 
            // rb_DATUM
            // 
            this.rb_DATUM.AutoSize = true;
            this.rb_DATUM.Location = new System.Drawing.Point(228, 50);
            this.rb_DATUM.Margin = new System.Windows.Forms.Padding(4);
            this.rb_DATUM.Name = "rb_DATUM";
            this.rb_DATUM.Size = new System.Drawing.Size(188, 21);
            this.rb_DATUM.TabIndex = 35;
            this.rb_DATUM.Text = "DATUM shift and rotation";
            this.rb_DATUM.UseVisualStyleBackColor = true;
            this.rb_DATUM.CheckedChanged += new System.EventHandler(this.rb_DATUM_CheckedChanged);
            // 
            // gb_5axis
            // 
            this.gb_5axis.Controls.Add(this.label6);
            this.gb_5axis.Controls.Add(this.rb_indiv_offsets);
            this.gb_5axis.Controls.Add(this.rb_DATUM);
            this.gb_5axis.Location = new System.Drawing.Point(16, 466);
            this.gb_5axis.Margin = new System.Windows.Forms.Padding(4);
            this.gb_5axis.Name = "gb_5axis";
            this.gb_5axis.Padding = new System.Windows.Forms.Padding(4);
            this.gb_5axis.Size = new System.Drawing.Size(745, 76);
            this.gb_5axis.TabIndex = 36;
            this.gb_5axis.TabStop = false;
            this.gb_5axis.Visible = false;
            // 
            // lb_setup_warning
            // 
            this.lb_setup_warning.Location = new System.Drawing.Point(8, 14);
            this.lb_setup_warning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_setup_warning.MaximumSize = new System.Drawing.Size(750, 62);
            this.lb_setup_warning.Name = "lb_setup_warning";
            this.lb_setup_warning.Size = new System.Drawing.Size(730, 32);
            this.lb_setup_warning.TabIndex = 22;
            this.lb_setup_warning.Text = "The stock in the project is not indexed. NCSIMUL can only simulate one setup at a" +
    " time in such cases. Select setup you want to verify in NCSIMUL:";
            // 
            // cb_setups_list
            // 
            this.cb_setups_list.FormattingEnabled = true;
            this.cb_setups_list.Location = new System.Drawing.Point(24, 52);
            this.cb_setups_list.Margin = new System.Windows.Forms.Padding(4);
            this.cb_setups_list.Name = "cb_setups_list";
            this.cb_setups_list.Size = new System.Drawing.Size(261, 24);
            this.cb_setups_list.TabIndex = 23;
            this.cb_setups_list.SelectedIndexChanged += new System.EventHandler(this.cb_setups_list_SelectedIndexChanged);
            // 
            // gb_no_indexing
            // 
            this.gb_no_indexing.Controls.Add(this.cb_setups_list);
            this.gb_no_indexing.Controls.Add(this.lb_setup_warning);
            this.gb_no_indexing.Location = new System.Drawing.Point(16, 386);
            this.gb_no_indexing.Margin = new System.Windows.Forms.Padding(4);
            this.gb_no_indexing.Name = "gb_no_indexing";
            this.gb_no_indexing.Padding = new System.Windows.Forms.Padding(4);
            this.gb_no_indexing.Size = new System.Drawing.Size(745, 84);
            this.gb_no_indexing.TabIndex = 1;
            this.gb_no_indexing.TabStop = false;
            // 
            // gb_turret_info
            // 
            this.gb_turret_info.Controls.Add(this.p_lsss);
            this.gb_turret_info.Controls.Add(this.p_lmss);
            this.gb_turret_info.Controls.Add(this.p_usss);
            this.gb_turret_info.Controls.Add(this.p_umss);
            this.gb_turret_info.Location = new System.Drawing.Point(17, 559);
            this.gb_turret_info.Margin = new System.Windows.Forms.Padding(4);
            this.gb_turret_info.Name = "gb_turret_info";
            this.gb_turret_info.Padding = new System.Windows.Forms.Padding(4);
            this.gb_turret_info.Size = new System.Drawing.Size(744, 138);
            this.gb_turret_info.TabIndex = 37;
            this.gb_turret_info.TabStop = false;
            this.gb_turret_info.Text = "Machine turret information:";
            // 
            // p_lsss
            // 
            this.p_lsss.Controls.Add(this.l_lsss);
            this.p_lsss.Controls.Add(this.rb_lsss_milling_head);
            this.p_lsss.Controls.Add(this.rb_lsss_turret);
            this.p_lsss.Location = new System.Drawing.Point(8, 100);
            this.p_lsss.Margin = new System.Windows.Forms.Padding(4);
            this.p_lsss.Name = "p_lsss";
            this.p_lsss.Size = new System.Drawing.Size(592, 27);
            this.p_lsss.TabIndex = 19;
            // 
            // l_lsss
            // 
            this.l_lsss.AutoSize = true;
            this.l_lsss.Location = new System.Drawing.Point(12, 6);
            this.l_lsss.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.l_lsss.Name = "l_lsss";
            this.l_lsss.Size = new System.Drawing.Size(202, 17);
            this.l_lsss.TabIndex = 3;
            this.l_lsss.Text = "Lower turret, Sub Spindle side:";
            // 
            // rb_lsss_milling_head
            // 
            this.rb_lsss_milling_head.AutoSize = true;
            this.rb_lsss_milling_head.Location = new System.Drawing.Point(268, 4);
            this.rb_lsss_milling_head.Margin = new System.Windows.Forms.Padding(4);
            this.rb_lsss_milling_head.Name = "rb_lsss_milling_head";
            this.rb_lsss_milling_head.Size = new System.Drawing.Size(104, 21);
            this.rb_lsss_milling_head.TabIndex = 10;
            this.rb_lsss_milling_head.TabStop = true;
            this.rb_lsss_milling_head.Text = "Milling head";
            this.rb_lsss_milling_head.UseVisualStyleBackColor = true;
            // 
            // rb_lsss_turret
            // 
            this.rb_lsss_turret.AutoSize = true;
            this.rb_lsss_turret.Checked = true;
            this.rb_lsss_turret.Location = new System.Drawing.Point(411, 4);
            this.rb_lsss_turret.Margin = new System.Windows.Forms.Padding(4);
            this.rb_lsss_turret.Name = "rb_lsss_turret";
            this.rb_lsss_turret.Size = new System.Drawing.Size(68, 21);
            this.rb_lsss_turret.TabIndex = 11;
            this.rb_lsss_turret.TabStop = true;
            this.rb_lsss_turret.Text = "Turret";
            this.rb_lsss_turret.UseVisualStyleBackColor = true;
            // 
            // p_lmss
            // 
            this.p_lmss.Controls.Add(this.l_lmss);
            this.p_lmss.Controls.Add(this.rb_lmss_milling_head);
            this.p_lmss.Controls.Add(this.rb_lmss_turret);
            this.p_lmss.Location = new System.Drawing.Point(8, 74);
            this.p_lmss.Margin = new System.Windows.Forms.Padding(4);
            this.p_lmss.Name = "p_lmss";
            this.p_lmss.Size = new System.Drawing.Size(592, 27);
            this.p_lmss.TabIndex = 18;
            // 
            // l_lmss
            // 
            this.l_lmss.AutoSize = true;
            this.l_lmss.Location = new System.Drawing.Point(12, 6);
            this.l_lmss.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.l_lmss.Name = "l_lmss";
            this.l_lmss.Size = new System.Drawing.Size(207, 17);
            this.l_lmss.TabIndex = 2;
            this.l_lmss.Text = "Lower turret, Main Spindle side:";
            // 
            // rb_lmss_milling_head
            // 
            this.rb_lmss_milling_head.AutoSize = true;
            this.rb_lmss_milling_head.Location = new System.Drawing.Point(268, 4);
            this.rb_lmss_milling_head.Margin = new System.Windows.Forms.Padding(4);
            this.rb_lmss_milling_head.Name = "rb_lmss_milling_head";
            this.rb_lmss_milling_head.Size = new System.Drawing.Size(104, 21);
            this.rb_lmss_milling_head.TabIndex = 8;
            this.rb_lmss_milling_head.TabStop = true;
            this.rb_lmss_milling_head.Text = "Milling head";
            this.rb_lmss_milling_head.UseVisualStyleBackColor = true;
            // 
            // rb_lmss_turret
            // 
            this.rb_lmss_turret.AutoSize = true;
            this.rb_lmss_turret.Checked = true;
            this.rb_lmss_turret.Location = new System.Drawing.Point(411, 4);
            this.rb_lmss_turret.Margin = new System.Windows.Forms.Padding(4);
            this.rb_lmss_turret.Name = "rb_lmss_turret";
            this.rb_lmss_turret.Size = new System.Drawing.Size(68, 21);
            this.rb_lmss_turret.TabIndex = 9;
            this.rb_lmss_turret.TabStop = true;
            this.rb_lmss_turret.Text = "Turret";
            this.rb_lmss_turret.UseVisualStyleBackColor = true;
            // 
            // p_usss
            // 
            this.p_usss.Controls.Add(this.l_usss);
            this.p_usss.Controls.Add(this.rb_usss_milling_head);
            this.p_usss.Controls.Add(this.rb_usss_turret);
            this.p_usss.Location = new System.Drawing.Point(8, 48);
            this.p_usss.Margin = new System.Windows.Forms.Padding(4);
            this.p_usss.Name = "p_usss";
            this.p_usss.Size = new System.Drawing.Size(592, 27);
            this.p_usss.TabIndex = 17;
            // 
            // l_usss
            // 
            this.l_usss.AutoSize = true;
            this.l_usss.Location = new System.Drawing.Point(12, 6);
            this.l_usss.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.l_usss.Name = "l_usss";
            this.l_usss.Size = new System.Drawing.Size(203, 17);
            this.l_usss.TabIndex = 1;
            this.l_usss.Text = "Upper turret, Sub Spindle side:";
            // 
            // rb_usss_milling_head
            // 
            this.rb_usss_milling_head.AutoSize = true;
            this.rb_usss_milling_head.Location = new System.Drawing.Point(268, 4);
            this.rb_usss_milling_head.Margin = new System.Windows.Forms.Padding(4);
            this.rb_usss_milling_head.Name = "rb_usss_milling_head";
            this.rb_usss_milling_head.Size = new System.Drawing.Size(104, 21);
            this.rb_usss_milling_head.TabIndex = 6;
            this.rb_usss_milling_head.TabStop = true;
            this.rb_usss_milling_head.Text = "Milling head";
            this.rb_usss_milling_head.UseVisualStyleBackColor = true;
            // 
            // rb_usss_turret
            // 
            this.rb_usss_turret.AutoSize = true;
            this.rb_usss_turret.Checked = true;
            this.rb_usss_turret.Location = new System.Drawing.Point(411, 4);
            this.rb_usss_turret.Margin = new System.Windows.Forms.Padding(4);
            this.rb_usss_turret.Name = "rb_usss_turret";
            this.rb_usss_turret.Size = new System.Drawing.Size(68, 21);
            this.rb_usss_turret.TabIndex = 7;
            this.rb_usss_turret.TabStop = true;
            this.rb_usss_turret.Text = "Turret";
            this.rb_usss_turret.UseVisualStyleBackColor = true;
            // 
            // p_umss
            // 
            this.p_umss.Controls.Add(this.rb_umss_milling_head);
            this.p_umss.Controls.Add(this.rb_umss_turret);
            this.p_umss.Controls.Add(this.l_umss);
            this.p_umss.Location = new System.Drawing.Point(8, 22);
            this.p_umss.Margin = new System.Windows.Forms.Padding(4);
            this.p_umss.Name = "p_umss";
            this.p_umss.Size = new System.Drawing.Size(592, 27);
            this.p_umss.TabIndex = 16;
            // 
            // rb_umss_milling_head
            // 
            this.rb_umss_milling_head.AutoSize = true;
            this.rb_umss_milling_head.Location = new System.Drawing.Point(268, 4);
            this.rb_umss_milling_head.Margin = new System.Windows.Forms.Padding(4);
            this.rb_umss_milling_head.Name = "rb_umss_milling_head";
            this.rb_umss_milling_head.Size = new System.Drawing.Size(104, 21);
            this.rb_umss_milling_head.TabIndex = 4;
            this.rb_umss_milling_head.TabStop = true;
            this.rb_umss_milling_head.Text = "Milling head";
            this.rb_umss_milling_head.UseVisualStyleBackColor = true;
            // 
            // rb_umss_turret
            // 
            this.rb_umss_turret.AutoSize = true;
            this.rb_umss_turret.Checked = true;
            this.rb_umss_turret.Location = new System.Drawing.Point(411, 4);
            this.rb_umss_turret.Margin = new System.Windows.Forms.Padding(4);
            this.rb_umss_turret.Name = "rb_umss_turret";
            this.rb_umss_turret.Size = new System.Drawing.Size(68, 21);
            this.rb_umss_turret.TabIndex = 5;
            this.rb_umss_turret.TabStop = true;
            this.rb_umss_turret.Text = "Turret";
            this.rb_umss_turret.UseVisualStyleBackColor = true;
            // 
            // l_umss
            // 
            this.l_umss.AutoSize = true;
            this.l_umss.Location = new System.Drawing.Point(12, 6);
            this.l_umss.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.l_umss.Name = "l_umss";
            this.l_umss.Size = new System.Drawing.Size(208, 17);
            this.l_umss.TabIndex = 0;
            this.l_umss.Text = "Upper turret, Main Spindle side:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 11);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(171, 17);
            this.label8.TabIndex = 41;
            this.label8.Text = "FeatureCAM file directory:";
            // 
            // lb_file_dir
            // 
            this.lb_file_dir.AutoSize = true;
            this.lb_file_dir.Location = new System.Drawing.Point(37, 32);
            this.lb_file_dir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_file_dir.Name = "lb_file_dir";
            this.lb_file_dir.Size = new System.Drawing.Size(139, 17);
            this.lb_file_dir.TabIndex = 42;
            this.lb_file_dir.Text = "<file directory value>";
            // 
            // rb_save_to_file_dir
            // 
            this.rb_save_to_file_dir.AutoSize = true;
            this.rb_save_to_file_dir.Checked = true;
            this.rb_save_to_file_dir.Location = new System.Drawing.Point(11, 1);
            this.rb_save_to_file_dir.Margin = new System.Windows.Forms.Padding(4);
            this.rb_save_to_file_dir.Name = "rb_save_to_file_dir";
            this.rb_save_to_file_dir.Size = new System.Drawing.Size(333, 21);
            this.rb_save_to_file_dir.TabIndex = 43;
            this.rb_save_to_file_dir.TabStop = true;
            this.rb_save_to_file_dir.Text = "Save NCSIMUL files to FeatureCAM file directory";
            this.rb_save_to_file_dir.UseVisualStyleBackColor = true;
            this.rb_save_to_file_dir.CheckedChanged += new System.EventHandler(this.rb_save_to_file_dir_CheckedChanged);
            // 
            // rb_save_to_other_dir
            // 
            this.rb_save_to_other_dir.AutoSize = true;
            this.rb_save_to_other_dir.Location = new System.Drawing.Point(11, 23);
            this.rb_save_to_other_dir.Margin = new System.Windows.Forms.Padding(4);
            this.rb_save_to_other_dir.Name = "rb_save_to_other_dir";
            this.rb_save_to_other_dir.Size = new System.Drawing.Size(297, 21);
            this.rb_save_to_other_dir.TabIndex = 44;
            this.rb_save_to_other_dir.Text = "Save NCSIMUL files to a different directory";
            this.rb_save_to_other_dir.UseVisualStyleBackColor = true;
            this.rb_save_to_other_dir.CheckedChanged += new System.EventHandler(this.rb_save_to_other_dir_CheckedChanged);
            // 
            // cb_create_subdir
            // 
            this.cb_create_subdir.AutoSize = true;
            this.cb_create_subdir.Location = new System.Drawing.Point(28, 129);
            this.cb_create_subdir.Margin = new System.Windows.Forms.Padding(4);
            this.cb_create_subdir.Name = "cb_create_subdir";
            this.cb_create_subdir.Size = new System.Drawing.Size(268, 21);
            this.cb_create_subdir.TabIndex = 45;
            this.cb_create_subdir.Text = "Create subdirectory for NCSIMUL files";
            this.cb_create_subdir.UseVisualStyleBackColor = true;
            this.cb_create_subdir.CheckedChanged += new System.EventHandler(this.cb_create_subdir_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 155);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(348, 28);
            this.button1.TabIndex = 48;
            this.button1.Text = "Preview directory path for output files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_save_to_other_dir);
            this.panel1.Controls.Add(this.rb_save_to_file_dir);
            this.panel1.Controls.Add(this.b_select_output_dir);
            this.panel1.Controls.Add(this.tb_output_dir);
            this.panel1.Location = new System.Drawing.Point(17, 52);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 78);
            this.panel1.TabIndex = 49;
            // 
            // b_subdir_options
            // 
            this.b_subdir_options.Location = new System.Drawing.Point(55, 155);
            this.b_subdir_options.Margin = new System.Windows.Forms.Padding(4);
            this.b_subdir_options.Name = "b_subdir_options";
            this.b_subdir_options.Size = new System.Drawing.Size(348, 28);
            this.b_subdir_options.TabIndex = 51;
            this.b_subdir_options.Text = "Subdirectory Options...";
            this.b_subdir_options.UseVisualStyleBackColor = true;
            this.b_subdir_options.Click += new System.EventHandler(this.b_subdir_options_Click);
            // 
            // b_SaveSettings
            // 
            this.b_SaveSettings.Location = new System.Drawing.Point(167, 705);
            this.b_SaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.b_SaveSettings.Name = "b_SaveSettings";
            this.b_SaveSettings.Size = new System.Drawing.Size(144, 31);
            this.b_SaveSettings.TabIndex = 52;
            this.b_SaveSettings.Text = "Save Options";
            this.b_SaveSettings.UseVisualStyleBackColor = true;
            this.b_SaveSettings.Click += new System.EventHandler(this.b_SaveSettings_Click);
            // 
            // cb_tool_rad_as_offset
            // 
            this.cb_tool_rad_as_offset.AutoSize = true;
            this.cb_tool_rad_as_offset.Location = new System.Drawing.Point(28, 385);
            this.cb_tool_rad_as_offset.Margin = new System.Windows.Forms.Padding(4);
            this.cb_tool_rad_as_offset.Name = "cb_tool_rad_as_offset";
            this.cb_tool_rad_as_offset.Size = new System.Drawing.Size(294, 21);
            this.cb_tool_rad_as_offset.TabIndex = 53;
            this.cb_tool_rad_as_offset.Text = "Export tool radius as radius compensation";
            this.cb_tool_rad_as_offset.UseVisualStyleBackColor = true;
            this.cb_tool_rad_as_offset.CheckedChanged += new System.EventHandler(this.cb_tool_rad_as_offset_CheckedChanged);
            // 
            // cb_tool_length_as_offset
            // 
            this.cb_tool_length_as_offset.AutoSize = true;
            this.cb_tool_length_as_offset.Location = new System.Drawing.Point(28, 411);
            this.cb_tool_length_as_offset.Margin = new System.Windows.Forms.Padding(4);
            this.cb_tool_length_as_offset.Name = "cb_tool_length_as_offset";
            this.cb_tool_length_as_offset.Size = new System.Drawing.Size(294, 21);
            this.cb_tool_length_as_offset.TabIndex = 54;
            this.cb_tool_length_as_offset.Text = "Export tool length as length compensation";
            this.cb_tool_length_as_offset.UseVisualStyleBackColor = true;
            this.cb_tool_length_as_offset.CheckedChanged += new System.EventHandler(this.cb_tool_length_as_offset_CheckedChanged);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 748);
            this.Controls.Add(this.cb_tool_length_as_offset);
            this.Controls.Add(this.cb_tool_rad_as_offset);
            this.Controls.Add(this.b_SaveSettings);
            this.Controls.Add(this.b_subdir_options);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gb_no_indexing);
            this.Controls.Add(this.cb_create_subdir);
            this.Controls.Add(this.lb_file_dir);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.gb_turret_info);
            this.Controls.Add(this.b_PreviewOffset);
            this.Controls.Add(this.rb_tool_id);
            this.Controls.Add(this.rb_tool_number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbOffsetZ);
            this.Controls.Add(this.lb_clamps);
            this.Controls.Add(this.tbOffsetY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_offsetZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_offsetY);
            this.Controls.Add(this.b_help);
            this.Controls.Add(this.lb_offsetX);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.tbOffsetX);
            this.Controls.Add(this.b_select_ncsimul_machine);
            this.Controls.Add(this.b_export);
            this.Controls.Add(this.tb_ncsimul_md_fpath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gb_5axis);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(795, 793);
            this.MinimumSize = new System.Drawing.Size(795, 793);
            this.Name = "UI";
            this.Text = "FeatureCAM to NCSIMUL";
            this.gb_5axis.ResumeLayout(false);
            this.gb_5axis.PerformLayout();
            this.gb_no_indexing.ResumeLayout(false);
            this.gb_turret_info.ResumeLayout(false);
            this.p_lsss.ResumeLayout(false);
            this.p_lsss.PerformLayout();
            this.p_lmss.ResumeLayout(false);
            this.p_lmss.PerformLayout();
            this.p_usss.ResumeLayout(false);
            this.p_usss.PerformLayout();
            this.p_umss.ResumeLayout(false);
            this.p_umss.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog outputDirBrowserDialog1;
        private System.Windows.Forms.Button b_export;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.Button b_help;
        private System.Windows.Forms.OpenFileDialog openNCSIMULPostFileDialog1;
        private System.Windows.Forms.Button b_select_ncsimul_machine;
        private System.Windows.Forms.TextBox tb_ncsimul_md_fpath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openNCSIMULMDFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_offsetX;
        private System.Windows.Forms.TextBox tbOffsetX;
        private System.Windows.Forms.TextBox tbOffsetZ;
        private System.Windows.Forms.TextBox tbOffsetY;
        private System.Windows.Forms.Label lb_offsetZ;
        private System.Windows.Forms.Label lb_offsetY;
        private System.Windows.Forms.Button b_PreviewOffset;
        private System.Windows.Forms.TextBox tb_output_dir;
        private System.Windows.Forms.Button b_select_output_dir;
        private System.Windows.Forms.CheckedListBox lb_clamps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb_tool_number;
        private System.Windows.Forms.RadioButton rb_tool_id;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rb_indiv_offsets;
        private System.Windows.Forms.RadioButton rb_DATUM;
        private System.Windows.Forms.GroupBox gb_5axis;
        private System.Windows.Forms.Label lb_setup_warning;
        private System.Windows.Forms.ComboBox cb_setups_list;
        private System.Windows.Forms.GroupBox gb_no_indexing;
        private System.Windows.Forms.GroupBox gb_turret_info;
        private System.Windows.Forms.Panel p_lsss;
        private System.Windows.Forms.Label l_lsss;
        private System.Windows.Forms.RadioButton rb_lsss_milling_head;
        private System.Windows.Forms.RadioButton rb_lsss_turret;
        private System.Windows.Forms.Panel p_lmss;
        private System.Windows.Forms.Label l_lmss;
        private System.Windows.Forms.RadioButton rb_lmss_milling_head;
        private System.Windows.Forms.RadioButton rb_lmss_turret;
        private System.Windows.Forms.Panel p_usss;
        private System.Windows.Forms.Label l_usss;
        private System.Windows.Forms.RadioButton rb_usss_milling_head;
        private System.Windows.Forms.RadioButton rb_usss_turret;
        private System.Windows.Forms.Panel p_umss;
        private System.Windows.Forms.RadioButton rb_umss_milling_head;
        private System.Windows.Forms.RadioButton rb_umss_turret;
        private System.Windows.Forms.Label l_umss;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_file_dir;
        private System.Windows.Forms.RadioButton rb_save_to_file_dir;
        private System.Windows.Forms.RadioButton rb_save_to_other_dir;
        private System.Windows.Forms.CheckBox cb_create_subdir;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button b_subdir_options;
        private System.Windows.Forms.Button b_SaveSettings;
        private System.Windows.Forms.CheckBox cb_tool_rad_as_offset;
        private System.Windows.Forms.CheckBox cb_tool_length_as_offset;
    }
}
