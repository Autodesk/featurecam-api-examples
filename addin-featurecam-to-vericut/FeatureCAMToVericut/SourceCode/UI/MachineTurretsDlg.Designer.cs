// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class MachineTurretsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineTurretsDlg));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.dg_turrets = new System.Windows.Forms.DataGridView();
            this.clm_turret_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clm_subsystem = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg_turrets)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(344, 188);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(95, 24);
            this.btn_Ok.TabIndex = 20;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(445, 188);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 24);
            this.btn_Cancel.TabIndex = 21;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // dg_turrets
            // 
            this.dg_turrets.AllowUserToAddRows = false;
            this.dg_turrets.AllowUserToDeleteRows = false;
            this.dg_turrets.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dg_turrets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_turrets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clm_turret_name,
            this.clm_type,
            this.clm_subsystem});
            this.dg_turrets.Location = new System.Drawing.Point(12, 12);
            this.dg_turrets.Name = "dg_turrets";
            this.dg_turrets.RowHeadersVisible = false;
            this.dg_turrets.RowTemplate.Height = 24;
            this.dg_turrets.Size = new System.Drawing.Size(527, 159);
            this.dg_turrets.TabIndex = 22;
            // 
            // clm_turret_name
            // 
            this.clm_turret_name.HeaderText = "Turret/Spindle";
            this.clm_turret_name.Name = "clm_turret_name";
            this.clm_turret_name.Width = 180;
            // 
            // clm_type
            // 
            this.clm_type.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.clm_type.HeaderText = "Type";
            this.clm_type.Items.AddRange(new object[] {
            "Milling head",
            "Turret"});
            this.clm_type.Name = "clm_type";
            this.clm_type.Width = 170;
            // 
            // clm_subsystem
            // 
            this.clm_subsystem.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.clm_subsystem.HeaderText = "Subsystem";
            this.clm_subsystem.Name = "clm_subsystem";
            this.clm_subsystem.Width = 170;
            // 
            // MachineTurretsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 225);
            this.Controls.Add(this.dg_turrets);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(570, 270);
            this.MinimumSize = new System.Drawing.Size(570, 270);
            this.Name = "MachineTurretsDlg";
            this.Text = "Machine Turret Info";
            ((System.ComponentModel.ISupportInitialize)(this.dg_turrets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.DataGridView dg_turrets;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_turret_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn clm_type;
        private System.Windows.Forms.DataGridViewComboBoxColumn clm_subsystem;
    }
}