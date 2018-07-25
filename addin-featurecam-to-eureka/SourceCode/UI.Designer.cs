// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToEUREKA
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
            this.outputDirBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.bPreviewOffset = new System.Windows.Forms.Button();
            this.tbOffsetZ = new System.Windows.Forms.TextBox();
            this.tbOffsetY = new System.Windows.Forms.TextBox();
            this.lb_offsetZ = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_offsetY = new System.Windows.Forms.Label();
            this.lb_offsetX = new System.Windows.Forms.Label();
            this.tbOffsetX = new System.Windows.Forms.TextBox();
            this.b_select_eureka_template = new System.Windows.Forms.Button();
            this.tb_eureka_template_fpath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.b_export = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.b_help = new System.Windows.Forms.Button();
            this.openEurekaTemplateFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_output_dir = new System.Windows.Forms.TextBox();
            this.b_select_output_dir = new System.Windows.Forms.Button();
            this.clb_solids_to_export = new System.Windows.Forms.CheckedListBox();
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
            this.gb_5axis.SuspendLayout();
            this.gb_no_indexing.SuspendLayout();
            this.SuspendLayout();
            // 
            // bPreviewOffset
            // 
            this.bPreviewOffset.Location = new System.Drawing.Point(386, 256);
            this.bPreviewOffset.Name = "bPreviewOffset";
            this.bPreviewOffset.Size = new System.Drawing.Size(75, 23);
            this.bPreviewOffset.TabIndex = 33;
            this.bPreviewOffset.Text = "Preview";
            this.bPreviewOffset.UseVisualStyleBackColor = true;
            this.bPreviewOffset.Click += new System.EventHandler(this.bPreviewOffset_Click);
            // 
            // tbOffsetZ
            // 
            this.tbOffsetZ.Location = new System.Drawing.Point(186, 263);
            this.tbOffsetZ.Name = "tbOffsetZ";
            this.tbOffsetZ.Size = new System.Drawing.Size(50, 20);
            this.tbOffsetZ.TabIndex = 32;
            this.tbOffsetZ.Text = "0.0";
            this.tbOffsetZ.TextChanged += new System.EventHandler(this.tbOffsetZ_TextChanged);
            // 
            // tbOffsetY
            // 
            this.tbOffsetY.Location = new System.Drawing.Point(114, 263);
            this.tbOffsetY.Name = "tbOffsetY";
            this.tbOffsetY.Size = new System.Drawing.Size(50, 20);
            this.tbOffsetY.TabIndex = 31;
            this.tbOffsetY.Text = "0.0";
            this.tbOffsetY.TextChanged += new System.EventHandler(this.tbOffsetY_TextChanged);
            // 
            // lb_offsetZ
            // 
            this.lb_offsetZ.AutoSize = true;
            this.lb_offsetZ.Location = new System.Drawing.Point(170, 266);
            this.lb_offsetZ.Name = "lb_offsetZ";
            this.lb_offsetZ.Size = new System.Drawing.Size(17, 13);
            this.lb_offsetZ.TabIndex = 30;
            this.lb_offsetZ.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Machine Zero offset from setup UCS:";
            // 
            // lb_offsetY
            // 
            this.lb_offsetY.AutoSize = true;
            this.lb_offsetY.Location = new System.Drawing.Point(99, 266);
            this.lb_offsetY.Name = "lb_offsetY";
            this.lb_offsetY.Size = new System.Drawing.Size(17, 13);
            this.lb_offsetY.TabIndex = 29;
            this.lb_offsetY.Text = "Y:";
            // 
            // lb_offsetX
            // 
            this.lb_offsetX.AutoSize = true;
            this.lb_offsetX.Location = new System.Drawing.Point(27, 266);
            this.lb_offsetX.Name = "lb_offsetX";
            this.lb_offsetX.Size = new System.Drawing.Size(17, 13);
            this.lb_offsetX.TabIndex = 27;
            this.lb_offsetX.Text = "X:";
            // 
            // tbOffsetX
            // 
            this.tbOffsetX.Location = new System.Drawing.Point(43, 263);
            this.tbOffsetX.Name = "tbOffsetX";
            this.tbOffsetX.Size = new System.Drawing.Size(50, 20);
            this.tbOffsetX.TabIndex = 28;
            this.tbOffsetX.Text = "0.0";
            this.tbOffsetX.TextChanged += new System.EventHandler(this.tbOffsetX_TextChanged);
            // 
            // b_select_eureka_template
            // 
            this.b_select_eureka_template.Location = new System.Drawing.Point(386, 81);
            this.b_select_eureka_template.Name = "b_select_eureka_template";
            this.b_select_eureka_template.Size = new System.Drawing.Size(75, 23);
            this.b_select_eureka_template.TabIndex = 11;
            this.b_select_eureka_template.Text = "Browse...";
            this.b_select_eureka_template.UseVisualStyleBackColor = true;
            this.b_select_eureka_template.Click += new System.EventHandler(this.b_select_eureka_template_Click);
            // 
            // tb_eureka_template_fpath
            // 
            this.tb_eureka_template_fpath.Location = new System.Drawing.Point(30, 83);
            this.tb_eureka_template_fpath.Name = "tb_eureka_template_fpath";
            this.tb_eureka_template_fpath.Size = new System.Drawing.Size(346, 20);
            this.tb_eureka_template_fpath.TabIndex = 10;
            this.tb_eureka_template_fpath.TextChanged += new System.EventHandler(this.tb_eureka_template_fpath_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Select EUREKA machine template:";
            // 
            // b_export
            // 
            this.b_export.Location = new System.Drawing.Point(73, 476);
            this.b_export.Name = "b_export";
            this.b_export.Size = new System.Drawing.Size(100, 25);
            this.b_export.TabIndex = 23;
            this.b_export.Text = "Export";
            this.b_export.UseVisualStyleBackColor = true;
            this.b_export.Click += new System.EventHandler(this.b_export_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(183, 476);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(100, 25);
            this.b_cancel.TabIndex = 24;
            this.b_cancel.Text = "Cancel/Exit";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // b_help
            // 
            this.b_help.Enabled = false;
            this.b_help.Location = new System.Drawing.Point(293, 476);
            this.b_help.Name = "b_help";
            this.b_help.Size = new System.Drawing.Size(100, 25);
            this.b_help.TabIndex = 25;
            this.b_help.Text = "Help";
            this.b_help.UseVisualStyleBackColor = true;
            this.b_help.Click += new System.EventHandler(this.b_help_Click);
            // 
            // openEurekaTemplateFileDialog1
            // 
            this.openEurekaTemplateFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select output directory:";
            // 
            // tb_output_dir
            // 
            this.tb_output_dir.Location = new System.Drawing.Point(30, 34);
            this.tb_output_dir.Name = "tb_output_dir";
            this.tb_output_dir.Size = new System.Drawing.Size(346, 20);
            this.tb_output_dir.TabIndex = 1;
            this.tb_output_dir.TextChanged += new System.EventHandler(this.tb_output_dir_TextChanged);
            // 
            // b_select_output_dir
            // 
            this.b_select_output_dir.Location = new System.Drawing.Point(386, 34);
            this.b_select_output_dir.Name = "b_select_output_dir";
            this.b_select_output_dir.Size = new System.Drawing.Size(75, 23);
            this.b_select_output_dir.TabIndex = 2;
            this.b_select_output_dir.Text = "Browse...";
            this.b_select_output_dir.UseVisualStyleBackColor = true;
            this.b_select_output_dir.Click += new System.EventHandler(this.b_select_output_dir_Click);
            // 
            // clb_solids_to_export
            // 
            this.clb_solids_to_export.CheckOnClick = true;
            this.clb_solids_to_export.FormattingEnabled = true;
            this.clb_solids_to_export.Location = new System.Drawing.Point(30, 126);
            this.clb_solids_to_export.Name = "clb_solids_to_export";
            this.clb_solids_to_export.Size = new System.Drawing.Size(175, 64);
            this.clb_solids_to_export.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Select solids to be exported as clamps:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Post uses to identify tool:";
            // 
            // rb_tool_number
            // 
            this.rb_tool_number.AutoSize = true;
            this.rb_tool_number.Checked = true;
            this.rb_tool_number.Location = new System.Drawing.Point(30, 214);
            this.rb_tool_number.Name = "rb_tool_number";
            this.rb_tool_number.Size = new System.Drawing.Size(84, 17);
            this.rb_tool_number.TabIndex = 28;
            this.rb_tool_number.TabStop = true;
            this.rb_tool_number.Text = "Tool number";
            this.rb_tool_number.UseVisualStyleBackColor = true;
            this.rb_tool_number.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_tool_id
            // 
            this.rb_tool_id.AutoSize = true;
            this.rb_tool_id.Location = new System.Drawing.Point(145, 214);
            this.rb_tool_id.Name = "rb_tool_id";
            this.rb_tool_id.Size = new System.Drawing.Size(60, 17);
            this.rb_tool_id.TabIndex = 29;
            this.rb_tool_id.Text = "Tool ID";
            this.rb_tool_id.UseVisualStyleBackColor = true;
            this.rb_tool_id.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.MaximumSize = new System.Drawing.Size(500, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(446, 26);
            this.label6.TabIndex = 24;
            this.label6.Text = "Active document is 5-axis part with \"NC Code Reference Point\" set to \"Each setup\'" +
                "s own fixture\". Select whether post uses:";
            // 
            // rb_indiv_offsets
            // 
            this.rb_indiv_offsets.AutoSize = true;
            this.rb_indiv_offsets.Checked = true;
            this.rb_indiv_offsets.Location = new System.Drawing.Point(22, 46);
            this.rb_indiv_offsets.Name = "rb_indiv_offsets";
            this.rb_indiv_offsets.Size = new System.Drawing.Size(130, 17);
            this.rb_indiv_offsets.TabIndex = 34;
            this.rb_indiv_offsets.TabStop = true;
            this.rb_indiv_offsets.Text = "Individual fixture offset";
            this.rb_indiv_offsets.UseVisualStyleBackColor = true;
            this.rb_indiv_offsets.CheckedChanged += new System.EventHandler(this.rb_indiv_offsets_CheckedChanged);
            // 
            // rb_DATUM
            // 
            this.rb_DATUM.AutoSize = true;
            this.rb_DATUM.Location = new System.Drawing.Point(171, 46);
            this.rb_DATUM.Name = "rb_DATUM";
            this.rb_DATUM.Size = new System.Drawing.Size(145, 17);
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
            this.gb_5axis.Location = new System.Drawing.Point(12, 287);
            this.gb_5axis.Name = "gb_5axis";
            this.gb_5axis.Size = new System.Drawing.Size(470, 69);
            this.gb_5axis.TabIndex = 36;
            this.gb_5axis.TabStop = false;
            this.gb_5axis.Visible = false;
            // 
            // lb_setup_warning
            // 
            this.lb_setup_warning.Location = new System.Drawing.Point(6, 12);
            this.lb_setup_warning.MaximumSize = new System.Drawing.Size(500, 50);
            this.lb_setup_warning.Name = "lb_setup_warning";
            this.lb_setup_warning.Size = new System.Drawing.Size(446, 26);
            this.lb_setup_warning.TabIndex = 22;
            this.lb_setup_warning.Text = "The stock in the project is not indexed. EUREKA can only simulate one setup at a" +
                " time in such cases. Select setup you want to verify in EUREKA:";
            // 
            // cb_setups_list
            // 
            this.cb_setups_list.FormattingEnabled = true;
            this.cb_setups_list.Location = new System.Drawing.Point(18, 43);
            this.cb_setups_list.Name = "cb_setups_list";
            this.cb_setups_list.Size = new System.Drawing.Size(181, 21);
            this.cb_setups_list.TabIndex = 23;
            this.cb_setups_list.SelectedIndexChanged += new System.EventHandler(this.cb_setups_list_SelectedIndexChanged);
            // 
            // gb_no_indexing
            // 
            this.gb_no_indexing.Controls.Add(this.cb_setups_list);
            this.gb_no_indexing.Controls.Add(this.lb_setup_warning);
            this.gb_no_indexing.Location = new System.Drawing.Point(12, 362);
            this.gb_no_indexing.Name = "gb_no_indexing";
            this.gb_no_indexing.Size = new System.Drawing.Size(470, 69);
            this.gb_no_indexing.TabIndex = 1;
            this.gb_no_indexing.TabStop = false;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 508);
            this.Controls.Add(this.gb_no_indexing);
            this.Controls.Add(this.bPreviewOffset);
            this.Controls.Add(this.rb_tool_id);
            this.Controls.Add(this.rb_tool_number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbOffsetZ);
            this.Controls.Add(this.clb_solids_to_export);
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
            this.Controls.Add(this.b_select_eureka_template);
            this.Controls.Add(this.b_export);
            this.Controls.Add(this.tb_eureka_template_fpath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gb_5axis);
            this.Name = "UI";
            this.Text = "FeatureCAM to EUREKA";
            this.gb_5axis.ResumeLayout(false);
            this.gb_5axis.PerformLayout();
            this.gb_no_indexing.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog outputDirBrowserDialog1;
        private System.Windows.Forms.Button b_export;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.Button b_help;
        private System.Windows.Forms.Button b_select_eureka_template;
        private System.Windows.Forms.TextBox tb_eureka_template_fpath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openEurekaTemplateFileDialog1;
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
        private System.Windows.Forms.CheckedListBox clb_solids_to_export;
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
    }
}