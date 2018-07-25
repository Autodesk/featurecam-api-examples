// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.outputDirBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cb_export_tools = new System.Windows.Forms.CheckBox();
            this.cb_export_nc = new System.Windows.Forms.CheckBox();
            this.b_select_template = new System.Windows.Forms.Button();
            this.tb_proj_template_fpath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openProjectFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openPartFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openVericutPostFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openVericutMDFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_output_dir = new System.Windows.Forms.TextBox();
            this.b_select_output_dir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_setups = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_UCSs = new System.Windows.Forms.Button();
            this.btn_toolOptions = new System.Windows.Forms.Button();
            this.btn_Fixtures = new System.Windows.Forms.Button();
            this.btn_MachineTurretInfo = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_StockDesign = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.b_select_setup_template = new System.Windows.Forms.Button();
            this.tb_setup_template_fpath = new System.Windows.Forms.TextBox();
            this.btn_WorkOffsets = new System.Windows.Forms.Button();
            this.openSetupTemplateFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cb_combine_setups = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_ucs_transition = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAndOpenInVericutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vericutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_form = new System.Windows.Forms.Panel();
            this.btn_help = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ExportAndLoadVC = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel_form.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_export_tools
            // 
            this.cb_export_tools.AutoSize = true;
            this.cb_export_tools.Checked = true;
            this.cb_export_tools.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_export_tools.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_export_tools.Location = new System.Drawing.Point(25, 141);
            this.cb_export_tools.Margin = new System.Windows.Forms.Padding(4);
            this.cb_export_tools.Name = "cb_export_tools";
            this.cb_export_tools.Size = new System.Drawing.Size(109, 21);
            this.cb_export_tools.TabIndex = 13;
            this.cb_export_tools.Text = "Export Tools";
            this.cb_export_tools.UseVisualStyleBackColor = true;
            this.cb_export_tools.CheckedChanged += new System.EventHandler(this.cb_export_tools_CheckedChanged);
            // 
            // cb_export_nc
            // 
            this.cb_export_nc.AutoSize = true;
            this.cb_export_nc.Checked = true;
            this.cb_export_nc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_export_nc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_export_nc.Location = new System.Drawing.Point(25, 115);
            this.cb_export_nc.Margin = new System.Windows.Forms.Padding(4);
            this.cb_export_nc.Name = "cb_export_nc";
            this.cb_export_nc.Size = new System.Drawing.Size(151, 21);
            this.cb_export_nc.TabIndex = 12;
            this.cb_export_nc.Text = "Export NC Program";
            this.cb_export_nc.UseVisualStyleBackColor = true;
            this.cb_export_nc.CheckedChanged += new System.EventHandler(this.cb_export_nc_CheckedChanged);
            // 
            // b_select_template
            // 
            this.b_select_template.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.b_select_template.Location = new System.Drawing.Point(724, 102);
            this.b_select_template.Margin = new System.Windows.Forms.Padding(4);
            this.b_select_template.Name = "b_select_template";
            this.b_select_template.Size = new System.Drawing.Size(100, 26);
            this.b_select_template.TabIndex = 7;
            this.b_select_template.Text = "Browse...";
            this.b_select_template.UseVisualStyleBackColor = true;
            this.b_select_template.Click += new System.EventHandler(this.b_select_template_Click);
            // 
            // tb_proj_template_fpath
            // 
            this.tb_proj_template_fpath.Enabled = false;
            this.tb_proj_template_fpath.Location = new System.Drawing.Point(33, 104);
            this.tb_proj_template_fpath.Margin = new System.Windows.Forms.Padding(4);
            this.tb_proj_template_fpath.Name = "tb_proj_template_fpath";
            this.tb_proj_template_fpath.Size = new System.Drawing.Size(683, 22);
            this.tb_proj_template_fpath.TabIndex = 6;
            this.tb_proj_template_fpath.TextChanged += new System.EventHandler(this.tb_proj_template_fpath_TextChanged);
            // 
            // label3
            // 
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(9, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(707, 36);
            this.label3.TabIndex = 1;
            this.label3.Text = "Exported project will be based upon this VERICUT template (leave the field blank," +
    " if you don\'t want to export the project):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // openProjectFileDialog1
            // 
            this.openProjectFileDialog1.FileName = "openFileDialog1";
            // 
            // openPartFileDialog1
            // 
            this.openPartFileDialog1.FileName = "openFileDialog1";
            // 
            // openVericutPostFileDialog1
            // 
            this.openVericutPostFileDialog1.FileName = "openFileDialog1";
            // 
            // openVericutMDFileDialog1
            // 
            this.openVericutMDFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select output directory:";
            // 
            // tb_output_dir
            // 
            this.tb_output_dir.Location = new System.Drawing.Point(33, 36);
            this.tb_output_dir.Margin = new System.Windows.Forms.Padding(4);
            this.tb_output_dir.Name = "tb_output_dir";
            this.tb_output_dir.Size = new System.Drawing.Size(683, 22);
            this.tb_output_dir.TabIndex = 1;
            this.tb_output_dir.TextChanged += new System.EventHandler(this.tb_output_dir_TextChanged);
            // 
            // b_select_output_dir
            // 
            this.b_select_output_dir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.b_select_output_dir.Location = new System.Drawing.Point(724, 35);
            this.b_select_output_dir.Margin = new System.Windows.Forms.Padding(4);
            this.b_select_output_dir.Name = "b_select_output_dir";
            this.b_select_output_dir.Size = new System.Drawing.Size(100, 26);
            this.b_select_output_dir.TabIndex = 2;
            this.b_select_output_dir.Text = "Browse...";
            this.b_select_output_dir.UseVisualStyleBackColor = true;
            this.b_select_output_dir.Click += new System.EventHandler(this.b_select_output_dir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(22, 175);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 31;
            this.label2.Text = "Establish UCSs:";
            // 
            // cb_setups
            // 
            this.cb_setups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setups.FormattingEnabled = true;
            this.cb_setups.Location = new System.Drawing.Point(25, 21);
            this.cb_setups.Name = "cb_setups";
            this.cb_setups.Size = new System.Drawing.Size(250, 24);
            this.cb_setups.TabIndex = 42;
            this.cb_setups.SelectedIndexChanged += new System.EventHandler(this.cb_setups_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_UCSs);
            this.groupBox1.Controls.Add(this.btn_toolOptions);
            this.groupBox1.Controls.Add(this.btn_Fixtures);
            this.groupBox1.Controls.Add(this.btn_MachineTurretInfo);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cb_setups);
            this.groupBox1.Controls.Add(this.btn_StockDesign);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.b_select_setup_template);
            this.groupBox1.Controls.Add(this.tb_setup_template_fpath);
            this.groupBox1.Controls.Add(this.cb_export_tools);
            this.groupBox1.Controls.Add(this.cb_export_nc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_WorkOffsets);
            this.groupBox1.Location = new System.Drawing.Point(12, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 339);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings for setup:";
            // 
            // btn_UCSs
            // 
            this.btn_UCSs.Location = new System.Drawing.Point(479, 172);
            this.btn_UCSs.Name = "btn_UCSs";
            this.btn_UCSs.Size = new System.Drawing.Size(333, 23);
            this.btn_UCSs.TabIndex = 57;
            this.btn_UCSs.Text = "UCSs...";
            this.btn_UCSs.UseVisualStyleBackColor = true;
            this.btn_UCSs.Click += new System.EventHandler(this.btn_UCSs_Click);
            // 
            // btn_toolOptions
            // 
            this.btn_toolOptions.Location = new System.Drawing.Point(479, 136);
            this.btn_toolOptions.Name = "btn_toolOptions";
            this.btn_toolOptions.Size = new System.Drawing.Size(333, 26);
            this.btn_toolOptions.TabIndex = 56;
            this.btn_toolOptions.Text = "Tool Options...";
            this.btn_toolOptions.UseVisualStyleBackColor = true;
            this.btn_toolOptions.Click += new System.EventHandler(this.btn_toolOptions_Click);
            // 
            // btn_Fixtures
            // 
            this.btn_Fixtures.Location = new System.Drawing.Point(479, 229);
            this.btn_Fixtures.Name = "btn_Fixtures";
            this.btn_Fixtures.Size = new System.Drawing.Size(333, 26);
            this.btn_Fixtures.TabIndex = 55;
            this.btn_Fixtures.Text = "Fixtures...";
            this.btn_Fixtures.UseVisualStyleBackColor = true;
            this.btn_Fixtures.Click += new System.EventHandler(this.btn_Fixtures_Click);
            // 
            // btn_MachineTurretInfo
            // 
            this.btn_MachineTurretInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_MachineTurretInfo.Location = new System.Drawing.Point(203, 310);
            this.btn_MachineTurretInfo.Name = "btn_MachineTurretInfo";
            this.btn_MachineTurretInfo.Size = new System.Drawing.Size(453, 23);
            this.btn_MachineTurretInfo.TabIndex = 44;
            this.btn_MachineTurretInfo.Text = "Machine turret information...";
            this.btn_MachineTurretInfo.UseVisualStyleBackColor = true;
            this.btn_MachineTurretInfo.Click += new System.EventHandler(this.btn_MachineTurretInfo_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(218, 17);
            this.label10.TabIndex = 54;
            this.label10.Text = "Export solids as clamps (fixtures):";
            // 
            // btn_StockDesign
            // 
            this.btn_StockDesign.Location = new System.Drawing.Point(479, 199);
            this.btn_StockDesign.Name = "btn_StockDesign";
            this.btn_StockDesign.Size = new System.Drawing.Size(333, 26);
            this.btn_StockDesign.TabIndex = 53;
            this.btn_StockDesign.Text = "Stock and Design...";
            this.btn_StockDesign.UseVisualStyleBackColor = true;
            this.btn_StockDesign.Click += new System.EventHandler(this.btn_StockDesign_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(319, 17);
            this.label8.TabIndex = 51;
            this.label8.Text = "Export initial stock and target part (design) solids:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(22, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 17);
            this.label6.TabIndex = 44;
            this.label6.Text = "Establish work offsets:";
            // 
            // label5
            // 
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(22, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(682, 36);
            this.label5.TabIndex = 43;
            this.label5.Text = "Exported setup properties will be based upon this VERICUT template (leave the fie" +
    "ld blank to use project template selected above):";
            // 
            // b_select_setup_template
            // 
            this.b_select_setup_template.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.b_select_setup_template.Location = new System.Drawing.Point(712, 86);
            this.b_select_setup_template.Name = "b_select_setup_template";
            this.b_select_setup_template.Size = new System.Drawing.Size(100, 26);
            this.b_select_setup_template.TabIndex = 42;
            this.b_select_setup_template.Text = "Browse...";
            this.b_select_setup_template.UseVisualStyleBackColor = true;
            this.b_select_setup_template.Click += new System.EventHandler(this.b_select_setup_template_Click);
            // 
            // tb_setup_template_fpath
            // 
            this.tb_setup_template_fpath.Enabled = false;
            this.tb_setup_template_fpath.Location = new System.Drawing.Point(43, 88);
            this.tb_setup_template_fpath.Name = "tb_setup_template_fpath";
            this.tb_setup_template_fpath.Size = new System.Drawing.Size(661, 22);
            this.tb_setup_template_fpath.TabIndex = 41;
            this.tb_setup_template_fpath.TextChanged += new System.EventHandler(this.tb_setup_template_TextChanged);
            // 
            // btn_WorkOffsets
            // 
            this.btn_WorkOffsets.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_WorkOffsets.Location = new System.Drawing.Point(479, 261);
            this.btn_WorkOffsets.Name = "btn_WorkOffsets";
            this.btn_WorkOffsets.Size = new System.Drawing.Size(333, 26);
            this.btn_WorkOffsets.TabIndex = 0;
            this.btn_WorkOffsets.Text = "Work Offsets...";
            this.btn_WorkOffsets.UseVisualStyleBackColor = true;
            this.btn_WorkOffsets.Click += new System.EventHandler(this.btn_AddOffset_Click);
            // 
            // openSetupTemplateFileDialog1
            // 
            this.openSetupTemplateFileDialog1.FileName = "openFileDialog1";
            // 
            // cb_combine_setups
            // 
            this.cb_combine_setups.AutoSize = true;
            this.cb_combine_setups.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_combine_setups.Location = new System.Drawing.Point(15, 161);
            this.cb_combine_setups.Name = "cb_combine_setups";
            this.cb_combine_setups.Size = new System.Drawing.Size(131, 21);
            this.cb_combine_setups.TabIndex = 46;
            this.cb_combine_setups.Text = "Combine setups";
            this.cb_combine_setups.UseVisualStyleBackColor = true;
            this.cb_combine_setups.CheckedChanged += new System.EventHandler(this.cb_combine_setups_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(9, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(278, 17);
            this.label9.TabIndex = 47;
            this.label9.Text = "Select UCS to use for Cut Stock Transition:";
            // 
            // cb_ucs_transition
            // 
            this.cb_ucs_transition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ucs_transition.FormattingEnabled = true;
            this.cb_ucs_transition.Location = new System.Drawing.Point(471, 133);
            this.cb_ucs_transition.Name = "cb_ucs_transition";
            this.cb_ucs_transition.Size = new System.Drawing.Size(245, 24);
            this.cb_ucs_transition.TabIndex = 48;
            this.cb_ucs_transition.SelectedIndexChanged += new System.EventHandler(this.cb_ucs_transition_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(842, 28);
            this.menuStrip1.TabIndex = 51;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.exportAndOpenInVericutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // exportAndOpenInVericutToolStripMenuItem
            // 
            this.exportAndOpenInVericutToolStripMenuItem.Name = "exportAndOpenInVericutToolStripMenuItem";
            this.exportAndOpenInVericutToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.exportAndOpenInVericutToolStripMenuItem.Text = "Export and open in VERICUT";
            this.exportAndOpenInVericutToolStripMenuItem.Click += new System.EventHandler(this.exportAndOpenInVericutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vericutToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // vericutToolStripMenuItem
            // 
            this.vericutToolStripMenuItem.Name = "vericutToolStripMenuItem";
            this.vericutToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.vericutToolStripMenuItem.Text = "VERICUT...";
            this.vericutToolStripMenuItem.Click += new System.EventHandler(this.vericutToolStripMenuItem_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.toolToolStripMenuItem.Text = "Tool...";
            this.toolToolStripMenuItem.Click += new System.EventHandler(this.toolToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.saveSettingsToolStripMenuItem.Text = "Save settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(110, 24);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // panel_form
            // 
            this.panel_form.AutoScroll = true;
            this.panel_form.Controls.Add(this.btn_help);
            this.panel_form.Controls.Add(this.cb_ucs_transition);
            this.panel_form.Controls.Add(this.btn_cancel);
            this.panel_form.Controls.Add(this.label9);
            this.panel_form.Controls.Add(this.btn_ExportAndLoadVC);
            this.panel_form.Controls.Add(this.cb_combine_setups);
            this.panel_form.Controls.Add(this.groupBox1);
            this.panel_form.Controls.Add(this.b_select_output_dir);
            this.panel_form.Controls.Add(this.tb_output_dir);
            this.panel_form.Controls.Add(this.label1);
            this.panel_form.Controls.Add(this.label3);
            this.panel_form.Controls.Add(this.b_select_template);
            this.panel_form.Controls.Add(this.tb_proj_template_fpath);
            this.panel_form.Location = new System.Drawing.Point(0, 31);
            this.panel_form.MaximumSize = new System.Drawing.Size(840, 728);
            this.panel_form.MinimumSize = new System.Drawing.Size(840, 728);
            this.panel_form.Name = "panel_form";
            this.panel_form.Size = new System.Drawing.Size(840, 728);
            this.panel_form.TabIndex = 50;
            // 
            // btn_help
            // 
            this.btn_help.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_help.Location = new System.Drawing.Point(694, 545);
            this.btn_help.Margin = new System.Windows.Forms.Padding(4);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(130, 45);
            this.btn_help.TabIndex = 25;
            this.btn_help.Text = "Help";
            this.btn_help.UseVisualStyleBackColor = true;
            this.btn_help.Click += new System.EventHandler(this.b_help_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_cancel.Location = new System.Drawing.Point(556, 545);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(130, 45);
            this.btn_cancel.TabIndex = 24;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // btn_ExportAndLoadVC
            // 
            this.btn_ExportAndLoadVC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_ExportAndLoadVC.Location = new System.Drawing.Point(215, 545);
            this.btn_ExportAndLoadVC.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ExportAndLoadVC.Name = "btn_ExportAndLoadVC";
            this.btn_ExportAndLoadVC.Size = new System.Drawing.Size(333, 45);
            this.btn_ExportAndLoadVC.TabIndex = 45;
            this.btn_ExportAndLoadVC.Text = "Export and Open in Vericut";
            this.btn_ExportAndLoadVC.UseVisualStyleBackColor = true;
            this.btn_ExportAndLoadVC.Click += new System.EventHandler(this.btn_ExportAndLoadVC_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(842, 634);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel_form);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(860, 679);
            this.MinimumSize = new System.Drawing.Size(860, 679);
            this.Name = "MainForm";
            this.Text = "FeatureCAM to VERICUT";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_form.ResumeLayout(false);
            this.panel_form.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog outputDirBrowserDialog1;
        private System.Windows.Forms.Button b_select_template;
        private System.Windows.Forms.TextBox tb_proj_template_fpath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openProjectFileDialog1;
        private System.Windows.Forms.CheckBox cb_export_nc;
        private System.Windows.Forms.CheckBox cb_export_tools;
        private System.Windows.Forms.OpenFileDialog openPartFileDialog1;
        private System.Windows.Forms.OpenFileDialog openVericutPostFileDialog1;
        private System.Windows.Forms.OpenFileDialog openVericutMDFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_output_dir;
        private System.Windows.Forms.Button b_select_output_dir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_setups;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button b_select_setup_template;
        private System.Windows.Forms.TextBox tb_setup_template_fpath;
        private System.Windows.Forms.OpenFileDialog openSetupTemplateFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_WorkOffsets;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_MachineTurretInfo;
        private System.Windows.Forms.CheckBox cb_combine_setups;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_ucs_transition;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAndOpenInVericutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vericutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.Panel panel_form;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_StockDesign;
        private System.Windows.Forms.Button btn_Fixtures;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_toolOptions;
        private System.Windows.Forms.Button btn_UCSs;
        private System.Windows.Forms.Button btn_ExportAndLoadVC;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_help;
    }
}