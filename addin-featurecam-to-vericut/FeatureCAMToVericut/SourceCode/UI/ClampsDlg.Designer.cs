// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class ClampsDlg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClampsDlg));
            this.cb_export_clamps = new System.Windows.Forms.Label();
            this.dataGrid_clamps_and_design1 = new System.Windows.Forms.DataGridView();
            this.export = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.solid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attach_to = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btn_SelectPartSolids = new System.Windows.Forms.Button();
            this.cb_attach_to = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_clamps_and_design1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_export_clamps
            // 
            this.cb_export_clamps.AutoSize = true;
            this.cb_export_clamps.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_export_clamps.Location = new System.Drawing.Point(10, 12);
            this.cb_export_clamps.Margin = new System.Windows.Forms.Padding(4);
            this.cb_export_clamps.Name = "cb_export_clamps";
            this.cb_export_clamps.Size = new System.Drawing.Size(416, 17);
            this.cb_export_clamps.TabIndex = 51;
            this.cb_export_clamps.Text = "Check solids to export as Fixtures and select Attach components:";
            // 
            // dataGrid_clamps_and_design1
            // 
            this.dataGrid_clamps_and_design1.AllowUserToAddRows = false;
            this.dataGrid_clamps_and_design1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_clamps_and_design1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid_clamps_and_design1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid_clamps_and_design1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_clamps_and_design1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.export,
            this.solid,
            this.attach_to});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid_clamps_and_design1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGrid_clamps_and_design1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGrid_clamps_and_design1.Location = new System.Drawing.Point(29, 37);
            this.dataGrid_clamps_and_design1.Name = "dataGrid_clamps_and_design1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid_clamps_and_design1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGrid_clamps_and_design1.RowHeadersVisible = false;
            this.dataGrid_clamps_and_design1.RowTemplate.Height = 24;
            this.dataGrid_clamps_and_design1.Size = new System.Drawing.Size(650, 296);
            this.dataGrid_clamps_and_design1.TabIndex = 54;
            this.dataGrid_clamps_and_design1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_clamps_and_design1_CellClick);
            // 
            // export
            // 
            this.export.HeaderText = "Export";
            this.export.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.export.MaxDropDownItems = 2;
            this.export.Name = "export";
            this.export.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // solid
            // 
            this.solid.FillWeight = 260F;
            this.solid.HeaderText = "Solid";
            this.solid.Name = "solid";
            this.solid.ReadOnly = true;
            // 
            // attach_to
            // 
            this.attach_to.FillWeight = 260F;
            this.attach_to.HeaderText = "Attach to";
            this.attach_to.Name = "attach_to";
            this.attach_to.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btn_SelectPartSolids
            // 
            this.btn_SelectPartSolids.Location = new System.Drawing.Point(29, 350);
            this.btn_SelectPartSolids.Name = "btn_SelectPartSolids";
            this.btn_SelectPartSolids.Size = new System.Drawing.Size(650, 23);
            this.btn_SelectPartSolids.TabIndex = 55;
            this.btn_SelectPartSolids.TabStop = false;
            this.btn_SelectPartSolids.Text = "Select solids selected in the project";
            this.btn_SelectPartSolids.UseVisualStyleBackColor = true;
            this.btn_SelectPartSolids.Click += new System.EventHandler(this.btn_SelectPartSolids_Click);
            // 
            // cb_attach_to
            // 
            this.cb_attach_to.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_attach_to.FormattingEnabled = true;
            this.cb_attach_to.Location = new System.Drawing.Point(356, 27);
            this.cb_attach_to.Name = "cb_attach_to";
            this.cb_attach_to.Size = new System.Drawing.Size(171, 24);
            this.cb_attach_to.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 17);
            this.label3.TabIndex = 57;
            this.label3.Text = "Attach solids selected in the list to:";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(483, 491);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(95, 24);
            this.btn_OK.TabIndex = 58;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(584, 491);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 24);
            this.btn_Cancel.TabIndex = 59;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(537, 27);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(107, 24);
            this.btn_Apply.TabIndex = 60;
            this.btn_Apply.Text = "Apply";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_Apply);
            this.groupBox1.Controls.Add(this.cb_attach_to);
            this.groupBox1.Location = new System.Drawing.Point(29, 391);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 68);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select export options for the selected solids";
            // 
            // ClampsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 527);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_SelectPartSolids);
            this.Controls.Add(this.dataGrid_clamps_and_design1);
            this.Controls.Add(this.cb_export_clamps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(725, 572);
            this.MinimumSize = new System.Drawing.Size(725, 572);
            this.Name = "ClampsDlg";
            this.Text = "Fixture Export Options";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_clamps_and_design1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cb_export_clamps;
        private System.Windows.Forms.DataGridView dataGrid_clamps_and_design1;
        private System.Windows.Forms.Button btn_SelectPartSolids;
        private System.Windows.Forms.ComboBox cb_attach_to;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewComboBoxColumn export;
        private System.Windows.Forms.DataGridViewTextBoxColumn solid;
        private System.Windows.Forms.DataGridViewComboBoxColumn attach_to;
    }
}