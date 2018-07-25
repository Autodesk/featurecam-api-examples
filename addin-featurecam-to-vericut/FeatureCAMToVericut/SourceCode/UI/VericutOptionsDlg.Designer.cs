// -----------------------------------------------------------------------
// Copyright 2017 Autodesk, Inc. All rights reserved.
// 
// This computer source code and related instructions and comments are the 
// unpublished confidential and proprietary information of Autodesk, Inc. 
// and are protected under applicable copyright and trade secret law. They 
// may not be disclosed to, copied or used by any third party without the 
// prior written consent of Autodesk, Inc.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class VericutOptionsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VericutOptionsDlg));
            this.label1 = new System.Windows.Forms.Label();
            this.tb_vericut_path = new System.Windows.Forms.TextBox();
            this.btn_browse_vericut = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.vericut_file_browser = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select location of VERICUT batch file:";
            // 
            // tb_vericut_path
            // 
            this.tb_vericut_path.Location = new System.Drawing.Point(35, 36);
            this.tb_vericut_path.Name = "tb_vericut_path";
            this.tb_vericut_path.Size = new System.Drawing.Size(524, 22);
            this.tb_vericut_path.TabIndex = 1;
            // 
            // btn_browse_vericut
            // 
            this.btn_browse_vericut.Location = new System.Drawing.Point(585, 34);
            this.btn_browse_vericut.Name = "btn_browse_vericut";
            this.btn_browse_vericut.Size = new System.Drawing.Size(100, 26);
            this.btn_browse_vericut.TabIndex = 2;
            this.btn_browse_vericut.Text = "Browse...";
            this.btn_browse_vericut.UseVisualStyleBackColor = true;
            this.btn_browse_vericut.Click += new System.EventHandler(this.btn_browse_vericut_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(488, 81);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(95, 24);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(590, 81);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 24);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // vericut_file_browser
            // 
            this.vericut_file_browser.FileName = "openFileDialog1";
            // 
            // VericutOptionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 116);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_browse_vericut);
            this.Controls.Add(this.tb_vericut_path);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(720, 161);
            this.MinimumSize = new System.Drawing.Size(720, 161);
            this.Name = "VericutOptionsDlg";
            this.Text = "VERICUT Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_vericut_path;
        private System.Windows.Forms.Button btn_browse_vericut;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.OpenFileDialog vericut_file_browser;
    }
}