// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class MultiSolidSelectionDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiSolidSelectionDlg));
            this.cb_solids = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_attach_to = new System.Windows.Forms.ComboBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_SelectPartSolids = new System.Windows.Forms.Button();
            this.cb_export_as = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cb_solids
            // 
            this.cb_solids.CheckOnClick = true;
            this.cb_solids.FormattingEnabled = true;
            this.cb_solids.Location = new System.Drawing.Point(26, 33);
            this.cb_solids.Name = "cb_solids";
            this.cb_solids.Size = new System.Drawing.Size(267, 174);
            this.cb_solids.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = FeatureCAMExporter.LanguageSupport.Translate("Select solids:");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = FeatureCAMExporter.LanguageSupport.Translate("Export as:");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = FeatureCAMExporter.LanguageSupport.Translate("Attach to:");
            // 
            // cb_attach_to
            // 
            this.cb_attach_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_attach_to.FormattingEnabled = true;
            this.cb_attach_to.Location = new System.Drawing.Point(24, 324);
            this.cb_attach_to.Name = "cb_attach_to";
            this.cb_attach_to.Size = new System.Drawing.Size(269, 24);
            this.cb_attach_to.TabIndex = 2;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(24, 365);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(132, 23);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = FeatureCAMExporter.LanguageSupport.Translate("OK");
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(161, 365);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(132, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = FeatureCAMExporter.LanguageSupport.Translate("Cancel");
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_SelectPartSolids
            // 
            this.btn_SelectPartSolids.Location = new System.Drawing.Point(26, 213);
            this.btn_SelectPartSolids.Name = "btn_SelectPartSolids";
            this.btn_SelectPartSolids.Size = new System.Drawing.Size(267, 23);
            this.btn_SelectPartSolids.TabIndex = 9;
            this.btn_SelectPartSolids.TabStop = false;
            this.btn_SelectPartSolids.Text = FeatureCAMExporter.LanguageSupport.Translate("Check solids selected in the project");
            this.btn_SelectPartSolids.UseVisualStyleBackColor = true;
            this.btn_SelectPartSolids.Click += new System.EventHandler(this.btn_SelectPartSolids_Click);
            // 
            // cb_export_as
            // 
            this.cb_export_as.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_export_as.FormattingEnabled = true;
            this.cb_export_as.Items.AddRange(new object[] {
            "Clamps",
            "Design"});
            this.cb_export_as.Location = new System.Drawing.Point(26, 268);
            this.cb_export_as.Name = "cb_export_as";
            this.cb_export_as.Size = new System.Drawing.Size(267, 24);
            this.cb_export_as.TabIndex = 1;
            // 
            // MultiSolidSelectionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 401);
            this.Controls.Add(this.cb_export_as);
            this.Controls.Add(this.btn_SelectPartSolids);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.cb_attach_to);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_solids);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiSolidSelectionDlg";
            this.Text = FeatureCAMExporter.LanguageSupport.Translate("Solid Selection");
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cb_solids;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_attach_to;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_SelectPartSolids;
        private System.Windows.Forms.ComboBox cb_export_as;
    }
}