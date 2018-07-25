// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class UCSsDlg_TurnMill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSsDlg_TurnMill));
            this.cb_attach_ucss_to = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_ucs_location = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_attach_ucss_to_subspindle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_ucs_location_subspindle = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cb_attach_ucss_to
            // 
            this.cb_attach_ucss_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_attach_ucss_to.FormattingEnabled = true;
            this.cb_attach_ucss_to.Location = new System.Drawing.Point(51, 106);
            this.cb_attach_ucss_to.Name = "cb_attach_ucss_to";
            this.cb_attach_ucss_to.Size = new System.Drawing.Size(303, 24);
            this.cb_attach_ucss_to.TabIndex = 51;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(30, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(239, 17);
            this.label7.TabIndex = 50;
            this.label7.Text = "Select component to attach UCSs to:";
            // 
            // cb_ucs_location
            // 
            this.cb_ucs_location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ucs_location.FormattingEnabled = true;
            this.cb_ucs_location.Location = new System.Drawing.Point(51, 52);
            this.cb_ucs_location.Margin = new System.Windows.Forms.Padding(4);
            this.cb_ucs_location.Name = "cb_ucs_location";
            this.cb_ucs_location.Size = new System.Drawing.Size(303, 24);
            this.cb_ucs_location.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(30, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 17);
            this.label2.TabIndex = 48;
            this.label2.Text = "Select UCS to use as an attach point:";
            // 
            // cb_attach_ucss_to_subspindle
            // 
            this.cb_attach_ucss_to_subspindle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_attach_ucss_to_subspindle.FormattingEnabled = true;
            this.cb_attach_ucss_to_subspindle.Location = new System.Drawing.Point(51, 240);
            this.cb_attach_ucss_to_subspindle.Name = "cb_attach_ucss_to_subspindle";
            this.cb_attach_ucss_to_subspindle.Size = new System.Drawing.Size(303, 24);
            this.cb_attach_ucss_to_subspindle.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(30, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 17);
            this.label3.TabIndex = 63;
            this.label3.Text = "Select component to attach UCSs to:";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(158, 278);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(95, 24);
            this.btn_OK.TabIndex = 65;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(259, 278);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 24);
            this.btn_Cancel.TabIndex = 66;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 17);
            this.label1.TabIndex = 67;
            this.label1.Text = "Select UCS to use as an attach point:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 68;
            this.label4.Text = "Main spindle:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 69;
            this.label5.Text = "Sub spindle:";
            // 
            // cb_ucs_location_subspindle
            // 
            this.cb_ucs_location_subspindle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ucs_location_subspindle.FormattingEnabled = true;
            this.cb_ucs_location_subspindle.Location = new System.Drawing.Point(51, 185);
            this.cb_ucs_location_subspindle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_ucs_location_subspindle.Name = "cb_ucs_location_subspindle";
            this.cb_ucs_location_subspindle.Size = new System.Drawing.Size(303, 24);
            this.cb_ucs_location_subspindle.TabIndex = 70;
            // 
            // UCSsDlg_TurnMill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 311);
            this.Controls.Add(this.cb_ucs_location_subspindle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.cb_attach_ucss_to_subspindle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_attach_ucss_to);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_ucs_location);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(405, 358);
            this.MinimumSize = new System.Drawing.Size(405, 358);
            this.Name = "UCSsDlg_TurnMill";
            this.Text = "UCSs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_attach_ucss_to;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_ucs_location;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_attach_ucss_to_subspindle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_ucs_location_subspindle;
    }
}