// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToCAMplete
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
            this.bPreviewOffset = new System.Windows.Forms.Button();
            this.tbOffsetZ = new System.Windows.Forms.TextBox();
            this.tbOffsetY = new System.Windows.Forms.TextBox();
            this.lb_offsetZ = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_offsetY = new System.Windows.Forms.Label();
            this.lb_offsetX = new System.Windows.Forms.Label();
            this.tbOffsetX = new System.Windows.Forms.TextBox();
            this.b_export = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.b_help = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.b_select_part_solid = new System.Windows.Forms.Button();
            this.cbox_part_solid = new System.Windows.Forms.ComboBox();
            this.b_select_part_stl_file = new System.Windows.Forms.Button();
            this.tb_part_stl_fpath = new System.Windows.Forms.TextBox();
            this.rb_select_part_stl_file = new System.Windows.Forms.RadioButton();
            this.rb_create_part_stl_file = new System.Windows.Forms.RadioButton();
            this.cb_export_part = new System.Windows.Forms.CheckBox();
            this.openPartFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.b_select_part_solids = new System.Windows.Forms.Button();
            this.gb_5axis.SuspendLayout();
            this.gb_no_indexing.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bPreviewOffset
            // 
            this.bPreviewOffset.Location = new System.Drawing.Point(515, 399);
            this.bPreviewOffset.Margin = new System.Windows.Forms.Padding(4);
            this.bPreviewOffset.Name = "bPreviewOffset";
            this.bPreviewOffset.Size = new System.Drawing.Size(127, 28);
            this.bPreviewOffset.TabIndex = 33;
            this.bPreviewOffset.Text = "Preview";
            this.bPreviewOffset.UseVisualStyleBackColor = true;
            this.bPreviewOffset.Click += new System.EventHandler(this.bPreviewOffset_Click);
            // 
            // tbOffsetZ
            // 
            this.tbOffsetZ.Location = new System.Drawing.Point(248, 408);
            this.tbOffsetZ.Margin = new System.Windows.Forms.Padding(4);
            this.tbOffsetZ.Name = "tbOffsetZ";
            this.tbOffsetZ.Size = new System.Drawing.Size(65, 22);
            this.tbOffsetZ.TabIndex = 32;
            this.tbOffsetZ.Text = "0.0";
            this.tbOffsetZ.TextChanged += new System.EventHandler(this.tbOffsetZ_TextChanged);
            // 
            // tbOffsetY
            // 
            this.tbOffsetY.Location = new System.Drawing.Point(152, 408);
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
            this.lb_offsetZ.Location = new System.Drawing.Point(227, 411);
            this.lb_offsetZ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_offsetZ.Name = "lb_offsetZ";
            this.lb_offsetZ.Size = new System.Drawing.Size(21, 17);
            this.lb_offsetZ.TabIndex = 30;
            this.lb_offsetZ.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 383);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Offset from setup UCS to pallet:";
            // 
            // lb_offsetY
            // 
            this.lb_offsetY.AutoSize = true;
            this.lb_offsetY.Location = new System.Drawing.Point(132, 411);
            this.lb_offsetY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_offsetY.Name = "lb_offsetY";
            this.lb_offsetY.Size = new System.Drawing.Size(21, 17);
            this.lb_offsetY.TabIndex = 29;
            this.lb_offsetY.Text = "Y:";
            // 
            // lb_offsetX
            // 
            this.lb_offsetX.AutoSize = true;
            this.lb_offsetX.Location = new System.Drawing.Point(36, 411);
            this.lb_offsetX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_offsetX.Name = "lb_offsetX";
            this.lb_offsetX.Size = new System.Drawing.Size(21, 17);
            this.lb_offsetX.TabIndex = 27;
            this.lb_offsetX.Text = "X:";
            // 
            // tbOffsetX
            // 
            this.tbOffsetX.Location = new System.Drawing.Point(57, 408);
            this.tbOffsetX.Margin = new System.Windows.Forms.Padding(4);
            this.tbOffsetX.Name = "tbOffsetX";
            this.tbOffsetX.Size = new System.Drawing.Size(65, 22);
            this.tbOffsetX.TabIndex = 28;
            this.tbOffsetX.Text = "0.0";
            this.tbOffsetX.TextChanged += new System.EventHandler(this.tbOffsetX_TextChanged);
            // 
            // b_export
            // 
            this.b_export.Location = new System.Drawing.Point(227, 638);
            this.b_export.Margin = new System.Windows.Forms.Padding(4);
            this.b_export.Name = "b_export";
            this.b_export.Size = new System.Drawing.Size(133, 31);
            this.b_export.TabIndex = 23;
            this.b_export.Text = "Export";
            this.b_export.UseVisualStyleBackColor = true;
            this.b_export.Click += new System.EventHandler(this.b_export_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Location = new System.Drawing.Point(368, 638);
            this.b_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(133, 31);
            this.b_cancel.TabIndex = 24;
            this.b_cancel.Text = "Cancel/Exit";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // b_help
            // 
            this.b_help.Location = new System.Drawing.Point(509, 638);
            this.b_help.Margin = new System.Windows.Forms.Padding(4);
            this.b_help.Name = "b_help";
            this.b_help.Size = new System.Drawing.Size(133, 31);
            this.b_help.TabIndex = 25;
            this.b_help.Text = "Help";
            this.b_help.UseVisualStyleBackColor = true;
            this.b_help.Click += new System.EventHandler(this.b_help_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select output directory:";
            // 
            // tb_output_dir
            // 
            this.tb_output_dir.Location = new System.Drawing.Point(40, 42);
            this.tb_output_dir.Margin = new System.Windows.Forms.Padding(4);
            this.tb_output_dir.Name = "tb_output_dir";
            this.tb_output_dir.Size = new System.Drawing.Size(460, 22);
            this.tb_output_dir.TabIndex = 1;
            this.tb_output_dir.TextChanged += new System.EventHandler(this.tb_output_dir_TextChanged);
            // 
            // b_select_output_dir
            // 
            this.b_select_output_dir.Location = new System.Drawing.Point(515, 42);
            this.b_select_output_dir.Margin = new System.Windows.Forms.Padding(4);
            this.b_select_output_dir.Name = "b_select_output_dir";
            this.b_select_output_dir.Size = new System.Drawing.Size(127, 28);
            this.b_select_output_dir.TabIndex = 2;
            this.b_select_output_dir.Text = "Browse...";
            this.b_select_output_dir.UseVisualStyleBackColor = true;
            this.b_select_output_dir.Click += new System.EventHandler(this.b_select_output_dir_Click);
            // 
            // lb_clamps
            // 
            this.lb_clamps.CheckOnClick = true;
            this.lb_clamps.FormattingEnabled = true;
            this.lb_clamps.Location = new System.Drawing.Point(40, 96);
            this.lb_clamps.Margin = new System.Windows.Forms.Padding(4);
            this.lb_clamps.Name = "lb_clamps";
            this.lb_clamps.Size = new System.Drawing.Size(232, 72);
            this.lb_clamps.TabIndex = 26;
            this.lb_clamps.SelectedIndexChanged += new System.EventHandler(this.lb_clamps_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Select solids to be exported as clamps:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 326);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Post uses to identify tool:";
            // 
            // rb_tool_number
            // 
            this.rb_tool_number.AutoSize = true;
            this.rb_tool_number.Checked = true;
            this.rb_tool_number.Location = new System.Drawing.Point(40, 347);
            this.rb_tool_number.Margin = new System.Windows.Forms.Padding(4);
            this.rb_tool_number.Name = "rb_tool_number";
            this.rb_tool_number.Size = new System.Drawing.Size(109, 21);
            this.rb_tool_number.TabIndex = 28;
            this.rb_tool_number.TabStop = true;
            this.rb_tool_number.Text = "Tool number";
            this.rb_tool_number.UseVisualStyleBackColor = true;
            this.rb_tool_number.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_tool_id
            // 
            this.rb_tool_id.AutoSize = true;
            this.rb_tool_id.Location = new System.Drawing.Point(193, 347);
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
            this.label6.Location = new System.Drawing.Point(8, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.MaximumSize = new System.Drawing.Size(667, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(595, 32);
            this.label6.TabIndex = 24;
            this.label6.Text = "Active document is 5-axis part with \"NC Code Reference Point\" set to \"Each setup\'" +
    "s own fixture\". Select whether post uses:";
            // 
            // rb_indiv_offsets
            // 
            this.rb_indiv_offsets.AutoSize = true;
            this.rb_indiv_offsets.Checked = true;
            this.rb_indiv_offsets.Location = new System.Drawing.Point(29, 57);
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
            this.rb_DATUM.Location = new System.Drawing.Point(228, 57);
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
            this.gb_5axis.Location = new System.Drawing.Point(16, 433);
            this.gb_5axis.Margin = new System.Windows.Forms.Padding(4);
            this.gb_5axis.Name = "gb_5axis";
            this.gb_5axis.Padding = new System.Windows.Forms.Padding(4);
            this.gb_5axis.Size = new System.Drawing.Size(627, 85);
            this.gb_5axis.TabIndex = 36;
            this.gb_5axis.TabStop = false;
            this.gb_5axis.Visible = false;
            // 
            // lb_setup_warning
            // 
            this.lb_setup_warning.Location = new System.Drawing.Point(8, 15);
            this.lb_setup_warning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_setup_warning.MaximumSize = new System.Drawing.Size(667, 62);
            this.lb_setup_warning.Name = "lb_setup_warning";
            this.lb_setup_warning.Size = new System.Drawing.Size(595, 34);
            this.lb_setup_warning.TabIndex = 22;
            this.lb_setup_warning.Text = "The stock in the project is not indexed. CAMplete can only simulate one setup at " +
    "a time in such cases. Select setup you want to verify in CAMplete:";
            // 
            // cb_setups_list
            // 
            this.cb_setups_list.FormattingEnabled = true;
            this.cb_setups_list.Location = new System.Drawing.Point(24, 53);
            this.cb_setups_list.Margin = new System.Windows.Forms.Padding(4);
            this.cb_setups_list.Name = "cb_setups_list";
            this.cb_setups_list.Size = new System.Drawing.Size(240, 24);
            this.cb_setups_list.TabIndex = 23;
            this.cb_setups_list.SelectedIndexChanged += new System.EventHandler(this.cb_setups_list_SelectedIndexChanged);
            // 
            // gb_no_indexing
            // 
            this.gb_no_indexing.Controls.Add(this.cb_setups_list);
            this.gb_no_indexing.Controls.Add(this.lb_setup_warning);
            this.gb_no_indexing.Location = new System.Drawing.Point(16, 526);
            this.gb_no_indexing.Margin = new System.Windows.Forms.Padding(4);
            this.gb_no_indexing.Name = "gb_no_indexing";
            this.gb_no_indexing.Padding = new System.Windows.Forms.Padding(4);
            this.gb_no_indexing.Size = new System.Drawing.Size(627, 85);
            this.gb_no_indexing.TabIndex = 1;
            this.gb_no_indexing.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.b_select_part_solid);
            this.panel2.Controls.Add(this.cbox_part_solid);
            this.panel2.Controls.Add(this.b_select_part_stl_file);
            this.panel2.Controls.Add(this.tb_part_stl_fpath);
            this.panel2.Controls.Add(this.rb_select_part_stl_file);
            this.panel2.Controls.Add(this.rb_create_part_stl_file);
            this.panel2.Controls.Add(this.cb_export_part);
            this.panel2.Location = new System.Drawing.Point(7, 179);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(645, 133);
            this.panel2.TabIndex = 37;
            // 
            // b_select_part_solid
            // 
            this.b_select_part_solid.Location = new System.Drawing.Point(411, 53);
            this.b_select_part_solid.Margin = new System.Windows.Forms.Padding(4);
            this.b_select_part_solid.Name = "b_select_part_solid";
            this.b_select_part_solid.Size = new System.Drawing.Size(224, 28);
            this.b_select_part_solid.TabIndex = 33;
            this.b_select_part_solid.Text = "Use solid selected in the part";
            this.b_select_part_solid.UseVisualStyleBackColor = true;
            this.b_select_part_solid.Click += new System.EventHandler(this.b_select_part_solid_Click);
            // 
            // cbox_part_solid
            // 
            this.cbox_part_solid.FormattingEnabled = true;
            this.cbox_part_solid.Location = new System.Drawing.Point(64, 53);
            this.cbox_part_solid.Margin = new System.Windows.Forms.Padding(4);
            this.cbox_part_solid.Name = "cbox_part_solid";
            this.cbox_part_solid.Size = new System.Drawing.Size(337, 24);
            this.cbox_part_solid.TabIndex = 32;
            this.cbox_part_solid.SelectedIndexChanged += new System.EventHandler(this.cbox_part_solid_SelectedIndexChanged);
            // 
            // b_select_part_stl_file
            // 
            this.b_select_part_stl_file.Location = new System.Drawing.Point(508, 101);
            this.b_select_part_stl_file.Margin = new System.Windows.Forms.Padding(4);
            this.b_select_part_stl_file.Name = "b_select_part_stl_file";
            this.b_select_part_stl_file.Size = new System.Drawing.Size(127, 28);
            this.b_select_part_stl_file.TabIndex = 22;
            this.b_select_part_stl_file.Text = "Browse...";
            this.b_select_part_stl_file.UseVisualStyleBackColor = true;
            this.b_select_part_stl_file.Click += new System.EventHandler(this.b_select_part_stl_file_Click);
            // 
            // tb_part_stl_fpath
            // 
            this.tb_part_stl_fpath.Location = new System.Drawing.Point(64, 103);
            this.tb_part_stl_fpath.Margin = new System.Windows.Forms.Padding(4);
            this.tb_part_stl_fpath.Name = "tb_part_stl_fpath";
            this.tb_part_stl_fpath.Size = new System.Drawing.Size(429, 22);
            this.tb_part_stl_fpath.TabIndex = 21;
            // 
            // rb_select_part_stl_file
            // 
            this.rb_select_part_stl_file.AutoSize = true;
            this.rb_select_part_stl_file.Location = new System.Drawing.Point(39, 78);
            this.rb_select_part_stl_file.Margin = new System.Windows.Forms.Padding(4);
            this.rb_select_part_stl_file.Name = "rb_select_part_stl_file";
            this.rb_select_part_stl_file.Size = new System.Drawing.Size(167, 21);
            this.rb_select_part_stl_file.TabIndex = 19;
            this.rb_select_part_stl_file.TabStop = true;
            this.rb_select_part_stl_file.Text = "Select existing .stl file:";
            this.rb_select_part_stl_file.UseVisualStyleBackColor = true;
            this.rb_select_part_stl_file.CheckedChanged += new System.EventHandler(this.rb_select_part_stl_file_CheckedChanged);
            // 
            // rb_create_part_stl_file
            // 
            this.rb_create_part_stl_file.AutoSize = true;
            this.rb_create_part_stl_file.Location = new System.Drawing.Point(39, 27);
            this.rb_create_part_stl_file.Margin = new System.Windows.Forms.Padding(4);
            this.rb_create_part_stl_file.Name = "rb_create_part_stl_file";
            this.rb_create_part_stl_file.Size = new System.Drawing.Size(212, 21);
            this.rb_create_part_stl_file.TabIndex = 31;
            this.rb_create_part_stl_file.Text = "Select solid to export as part:";
            this.rb_create_part_stl_file.UseVisualStyleBackColor = true;
            this.rb_create_part_stl_file.CheckedChanged += new System.EventHandler(this.rb_create_part_stl_file_CheckedChanged);
            // 
            // cb_export_part
            // 
            this.cb_export_part.AutoSize = true;
            this.cb_export_part.Location = new System.Drawing.Point(13, 4);
            this.cb_export_part.Margin = new System.Windows.Forms.Padding(4);
            this.cb_export_part.Name = "cb_export_part";
            this.cb_export_part.Size = new System.Drawing.Size(187, 21);
            this.cb_export_part.TabIndex = 18;
            this.cb_export_part.Text = "Export Part solid (.stl file)";
            this.cb_export_part.UseVisualStyleBackColor = true;
            this.cb_export_part.CheckedChanged += new System.EventHandler(this.cb_export_part_CheckedChanged);
            // 
            // openPartFileDialog1
            // 
            this.openPartFileDialog1.FileName = "openFileDialog1";
            // 
            // b_select_part_solids
            // 
            this.b_select_part_solids.Location = new System.Drawing.Point(418, 96);
            this.b_select_part_solids.Name = "b_select_part_solids";
            this.b_select_part_solids.Size = new System.Drawing.Size(224, 72);
            this.b_select_part_solids.TabIndex = 38;
            this.b_select_part_solids.Text = "Select solids selected in the part";
            this.b_select_part_solids.UseVisualStyleBackColor = true;
            this.b_select_part_solids.Click += new System.EventHandler(this.b_select_part_solids_Click);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.b_cancel;
            this.ClientSize = new System.Drawing.Size(659, 682);
            this.Controls.Add(this.b_select_part_solids);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gb_no_indexing);
            this.Controls.Add(this.bPreviewOffset);
            this.Controls.Add(this.rb_tool_id);
            this.Controls.Add(this.rb_tool_number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbOffsetZ);
            this.Controls.Add(this.lb_clamps);
            this.Controls.Add(this.tbOffsetY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_offsetZ);
            this.Controls.Add(this.b_select_output_dir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_offsetY);
            this.Controls.Add(this.b_help);
            this.Controls.Add(this.lb_offsetX);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.tbOffsetX);
            this.Controls.Add(this.tb_output_dir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_export);
            this.Controls.Add(this.gb_5axis);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(677, 727);
            this.MinimumSize = new System.Drawing.Size(677, 727);
            this.Name = "UI";
            this.Text = "FeatureCAM to CAMplete";
            this.gb_5axis.ResumeLayout(false);
            this.gb_5axis.PerformLayout();
            this.gb_no_indexing.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog outputDirBrowserDialog1;
        private System.Windows.Forms.Button b_export;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.Button b_help;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_offsetX;
        private System.Windows.Forms.TextBox tbOffsetX;
        private System.Windows.Forms.TextBox tbOffsetZ;
        private System.Windows.Forms.TextBox tbOffsetY;
        private System.Windows.Forms.Label lb_offsetZ;
        private System.Windows.Forms.Label lb_offsetY;
        private System.Windows.Forms.Button bPreviewOffset;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button b_select_part_solid;
        private System.Windows.Forms.ComboBox cbox_part_solid;
        private System.Windows.Forms.Button b_select_part_stl_file;
        private System.Windows.Forms.TextBox tb_part_stl_fpath;
        private System.Windows.Forms.RadioButton rb_select_part_stl_file;
        private System.Windows.Forms.RadioButton rb_create_part_stl_file;
        private System.Windows.Forms.CheckBox cb_export_part;
        private System.Windows.Forms.OpenFileDialog openPartFileDialog1;
        private System.Windows.Forms.Button b_select_part_solids;
    }
}