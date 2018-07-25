// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class ToolOptionsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolOptionsDlg));
            this.label1 = new System.Windows.Forms.Label();
            this.rb_tool_slot = new System.Windows.Forms.RadioButton();
            this.rb_tool_slot_and_name = new System.Windows.Forms.RadioButton();
            this.rb_tool_id = new System.Windows.Forms.RadioButton();
            this.cb_turret_prefix = new System.Windows.Forms.CheckBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.MaximumSize = new System.Drawing.Size(515, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(487, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select one of the following options to use for identifying tools in VERICUT:";
            // 
            // rb_tool_slot
            // 
            this.rb_tool_slot.AutoSize = true;
            this.rb_tool_slot.Location = new System.Drawing.Point(34, 52);
            this.rb_tool_slot.Name = "rb_tool_slot";
            this.rb_tool_slot.Size = new System.Drawing.Size(252, 21);
            this.rb_tool_slot.TabIndex = 0;
            this.rb_tool_slot.TabStop = true;
            this.rb_tool_slot.Text = "Tool numbers (positions in the crib)";
            this.rb_tool_slot.UseVisualStyleBackColor = true;
            // 
            // rb_tool_slot_and_name
            // 
            this.rb_tool_slot_and_name.AutoSize = true;
            this.rb_tool_slot_and_name.Location = new System.Drawing.Point(34, 79);
            this.rb_tool_slot_and_name.Name = "rb_tool_slot_and_name";
            this.rb_tool_slot_and_name.Size = new System.Drawing.Size(190, 21);
            this.rb_tool_slot_and_name.TabIndex = 1;
            this.rb_tool_slot_and_name.TabStop = true;
            this.rb_tool_slot_and_name.Text = "Tool numbers and names";
            this.rb_tool_slot_and_name.UseVisualStyleBackColor = true;
            // 
            // rb_tool_id
            // 
            this.rb_tool_id.AutoSize = true;
            this.rb_tool_id.Location = new System.Drawing.Point(34, 107);
            this.rb_tool_id.Name = "rb_tool_id";
            this.rb_tool_id.Size = new System.Drawing.Size(81, 21);
            this.rb_tool_id.TabIndex = 2;
            this.rb_tool_id.TabStop = true;
            this.rb_tool_id.Text = "Tool IDs";
            this.rb_tool_id.UseVisualStyleBackColor = true;
            // 
            // cb_turret_prefix
            // 
            this.cb_turret_prefix.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cb_turret_prefix.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cb_turret_prefix.Location = new System.Drawing.Point(34, 135);
            this.cb_turret_prefix.Name = "cb_turret_prefix";
            this.cb_turret_prefix.Size = new System.Drawing.Size(466, 42);
            this.cb_turret_prefix.TabIndex = 3;
            this.cb_turret_prefix.Text = "Prefix tool ids with turret identifier (for multi-turret parts)";
            this.cb_turret_prefix.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cb_turret_prefix.UseVisualStyleBackColor = true;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(288, 193);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(95, 24);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(389, 193);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 24);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // ToolOptionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 228);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.cb_turret_prefix);
            this.Controls.Add(this.rb_tool_id);
            this.Controls.Add(this.rb_tool_slot_and_name);
            this.Controls.Add(this.rb_tool_slot);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(534, 273);
            this.MinimumSize = new System.Drawing.Size(534, 273);
            this.Name = "ToolOptionsDlg";
            this.Text = "Tool Export Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_tool_slot;
        private System.Windows.Forms.RadioButton rb_tool_slot_and_name;
        private System.Windows.Forms.RadioButton rb_tool_id;
        private System.Windows.Forms.CheckBox cb_turret_prefix;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
    }
}