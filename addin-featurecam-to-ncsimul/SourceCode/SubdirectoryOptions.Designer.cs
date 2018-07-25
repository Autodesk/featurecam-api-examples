// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToNCSIMUL
{
    partial class SubdirectoryOptions
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
            this.cb_include_setup_name = new System.Windows.Forms.CheckBox();
            this.cb_include_postprocessor = new System.Windows.Forms.CheckBox();
            this.cb_include_file_name = new System.Windows.Forms.CheckBox();
            this.lb_include_in_subdir_name = new System.Windows.Forms.Label();
            this.cb_include_project_title = new System.Windows.Forms.CheckBox();
            this.cb_include_machine_name = new System.Windows.Forms.CheckBox();
            this.cb_select_order = new System.Windows.Forms.Label();
            this.b_Up = new System.Windows.Forms.Button();
            this.b_Down = new System.Windows.Forms.Button();
            this.b_OK = new System.Windows.Forms.Button();
            this.b_Apply = new System.Windows.Forms.Button();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.clb_name_format = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cb_include_setup_name
            // 
            this.cb_include_setup_name.AutoSize = true;
            this.cb_include_setup_name.Location = new System.Drawing.Point(40, 142);
            this.cb_include_setup_name.Margin = new System.Windows.Forms.Padding(4);
            this.cb_include_setup_name.Name = "cb_include_setup_name";
            this.cb_include_setup_name.Size = new System.Drawing.Size(234, 21);
            this.cb_include_setup_name.TabIndex = 57;
            this.cb_include_setup_name.Text = "Setup name (non-indexed parts)";
            this.cb_include_setup_name.UseVisualStyleBackColor = true;
            this.cb_include_setup_name.CheckedChanged += new System.EventHandler(this.cb_include_setup_name_CheckedChanged);
            // 
            // cb_include_postprocessor
            // 
            this.cb_include_postprocessor.AutoSize = true;
            this.cb_include_postprocessor.Location = new System.Drawing.Point(40, 68);
            this.cb_include_postprocessor.Margin = new System.Windows.Forms.Padding(4);
            this.cb_include_postprocessor.Name = "cb_include_postprocessor";
            this.cb_include_postprocessor.Size = new System.Drawing.Size(241, 21);
            this.cb_include_postprocessor.TabIndex = 55;
            this.cb_include_postprocessor.Text = "FeatureCAM postprocessor name";
            this.cb_include_postprocessor.UseVisualStyleBackColor = true;
            this.cb_include_postprocessor.CheckedChanged += new System.EventHandler(this.cb_include_postprocessor_CheckedChanged);
            // 
            // cb_include_file_name
            // 
            this.cb_include_file_name.AutoSize = true;
            this.cb_include_file_name.Checked = true;
            this.cb_include_file_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_include_file_name.Location = new System.Drawing.Point(40, 43);
            this.cb_include_file_name.Margin = new System.Windows.Forms.Padding(4);
            this.cb_include_file_name.Name = "cb_include_file_name";
            this.cb_include_file_name.Size = new System.Drawing.Size(169, 21);
            this.cb_include_file_name.TabIndex = 54;
            this.cb_include_file_name.Text = "FeatureCAM file name";
            this.cb_include_file_name.UseVisualStyleBackColor = true;
            this.cb_include_file_name.CheckedChanged += new System.EventHandler(this.cb_include_file_name_CheckedChanged);
            // 
            // lb_include_in_subdir_name
            // 
            this.lb_include_in_subdir_name.AutoSize = true;
            this.lb_include_in_subdir_name.Location = new System.Drawing.Point(20, 20);
            this.lb_include_in_subdir_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_include_in_subdir_name.Name = "lb_include_in_subdir_name";
            this.lb_include_in_subdir_name.Size = new System.Drawing.Size(328, 17);
            this.lb_include_in_subdir_name.TabIndex = 53;
            this.lb_include_in_subdir_name.Text = "Include in the NCSIMUL project subdirectory name:";
            // 
            // cb_include_project_title
            // 
            this.cb_include_project_title.AutoSize = true;
            this.cb_include_project_title.Location = new System.Drawing.Point(40, 117);
            this.cb_include_project_title.Margin = new System.Windows.Forms.Padding(4);
            this.cb_include_project_title.Name = "cb_include_project_title";
            this.cb_include_project_title.Size = new System.Drawing.Size(181, 21);
            this.cb_include_project_title.TabIndex = 52;
            this.cb_include_project_title.Text = "FeatureCAM project title";
            this.cb_include_project_title.UseVisualStyleBackColor = true;
            this.cb_include_project_title.CheckedChanged += new System.EventHandler(this.cb_include_project_title_CheckedChanged);
            // 
            // cb_include_machine_name
            // 
            this.cb_include_machine_name.AutoSize = true;
            this.cb_include_machine_name.Location = new System.Drawing.Point(40, 92);
            this.cb_include_machine_name.Margin = new System.Windows.Forms.Padding(4);
            this.cb_include_machine_name.Name = "cb_include_machine_name";
            this.cb_include_machine_name.Size = new System.Drawing.Size(186, 21);
            this.cb_include_machine_name.TabIndex = 51;
            this.cb_include_machine_name.Text = "NCSIMUL machine name";
            this.cb_include_machine_name.UseVisualStyleBackColor = true;
            this.cb_include_machine_name.CheckedChanged += new System.EventHandler(this.cb_include_machine_name_CheckedChanged);
            // 
            // label1
            // 
            this.cb_select_order.AutoSize = true;
            this.cb_select_order.Location = new System.Drawing.Point(20, 174);
            this.cb_select_order.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cb_select_order.Name = "label1";
            this.cb_select_order.Size = new System.Drawing.Size(89, 17);
            this.cb_select_order.TabIndex = 59;
            this.cb_select_order.Text = "Select order:";
            // 
            // b_Up
            // 
            this.b_Up.Location = new System.Drawing.Point(348, 210);
            this.b_Up.Margin = new System.Windows.Forms.Padding(4);
            this.b_Up.Name = "b_Up";
            this.b_Up.Size = new System.Drawing.Size(100, 28);
            this.b_Up.TabIndex = 60;
            this.b_Up.Text = "Up";
            this.b_Up.UseVisualStyleBackColor = true;
            this.b_Up.Click += new System.EventHandler(this.b_Up_Click);
            // 
            // b_Down
            // 
            this.b_Down.Location = new System.Drawing.Point(348, 246);
            this.b_Down.Margin = new System.Windows.Forms.Padding(4);
            this.b_Down.Name = "b_Down";
            this.b_Down.Size = new System.Drawing.Size(100, 28);
            this.b_Down.TabIndex = 61;
            this.b_Down.Text = "Down";
            this.b_Down.UseVisualStyleBackColor = true;
            this.b_Down.Click += new System.EventHandler(this.b_Down_Click);
            // 
            // b_OK
            // 
            this.b_OK.Location = new System.Drawing.Point(445, 43);
            this.b_OK.Margin = new System.Windows.Forms.Padding(4);
            this.b_OK.Name = "b_OK";
            this.b_OK.Size = new System.Drawing.Size(100, 28);
            this.b_OK.TabIndex = 62;
            this.b_OK.Text = "OK";
            this.b_OK.UseVisualStyleBackColor = true;
            this.b_OK.Click += new System.EventHandler(this.b_OK_Click);
            // 
            // b_Apply
            // 
            this.b_Apply.Location = new System.Drawing.Point(445, 80);
            this.b_Apply.Margin = new System.Windows.Forms.Padding(4);
            this.b_Apply.Name = "b_Apply";
            this.b_Apply.Size = new System.Drawing.Size(100, 28);
            this.b_Apply.TabIndex = 63;
            this.b_Apply.Text = "Apply";
            this.b_Apply.UseVisualStyleBackColor = true;
            this.b_Apply.Click += new System.EventHandler(this.b_Apply_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Location = new System.Drawing.Point(445, 117);
            this.b_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(100, 28);
            this.b_Cancel.TabIndex = 64;
            this.b_Cancel.Text = "Cancel";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // clb_name_format
            // 
            this.clb_name_format.FormattingEnabled = true;
            this.clb_name_format.ItemHeight = 16;
            this.clb_name_format.Location = new System.Drawing.Point(40, 199);
            this.clb_name_format.Margin = new System.Windows.Forms.Padding(4);
            this.clb_name_format.Name = "clb_name_format";
            this.clb_name_format.Size = new System.Drawing.Size(287, 100);
            this.clb_name_format.TabIndex = 65;
            // 
            // SubdirectoryOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 469);
            this.Controls.Add(this.clb_name_format);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.b_Apply);
            this.Controls.Add(this.b_OK);
            this.Controls.Add(this.b_Down);
            this.Controls.Add(this.b_Up);
            this.Controls.Add(this.cb_select_order);
            this.Controls.Add(this.cb_include_setup_name);
            this.Controls.Add(this.cb_include_postprocessor);
            this.Controls.Add(this.cb_include_file_name);
            this.Controls.Add(this.lb_include_in_subdir_name);
            this.Controls.Add(this.cb_include_project_title);
            this.Controls.Add(this.cb_include_machine_name);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SubdirectoryOptions";
            this.Text = "Subdirectory Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_include_setup_name;
        private System.Windows.Forms.CheckBox cb_include_postprocessor;
        private System.Windows.Forms.CheckBox cb_include_file_name;
        private System.Windows.Forms.Label lb_include_in_subdir_name;
        private System.Windows.Forms.CheckBox cb_include_project_title;
        private System.Windows.Forms.CheckBox cb_include_machine_name;
        private System.Windows.Forms.Label cb_select_order;
        private System.Windows.Forms.Button b_Up;
        private System.Windows.Forms.Button b_Down;
        private System.Windows.Forms.Button b_OK;
        private System.Windows.Forms.Button b_Apply;
        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.ListBox clb_name_format;
    }
}