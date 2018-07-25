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
    partial class WorkOffsetDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkOffsetDlg));
            this.cb_csys_origin = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_offset_name = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_subsystems = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_from_component = new System.Windows.Forms.ComboBox();
            this.btn_AddOffset = new System.Windows.Forms.Button();
            this.btn_CancelAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.clmn_table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_subsystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_register = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_from_component = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_to_ucs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_registers = new System.Windows.Forms.ComboBox();
            this.btn_ModifyOffset = new System.Windows.Forms.Button();
            this.btn_delete_offset = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_csys_origin
            // 
            this.cb_csys_origin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_csys_origin.FormattingEnabled = true;
            this.cb_csys_origin.Location = new System.Drawing.Point(161, 148);
            this.cb_csys_origin.Name = "cb_csys_origin";
            this.cb_csys_origin.Size = new System.Drawing.Size(285, 24);
            this.cb_csys_origin.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "\'To\' CSYS Origin:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Register:";
            // 
            // cb_offset_name
            // 
            this.cb_offset_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_offset_name.FormattingEnabled = true;
            this.cb_offset_name.Items.AddRange(new object[] {
            "Work Offsets",
            "Program Zero"});
            this.cb_offset_name.Location = new System.Drawing.Point(161, 28);
            this.cb_offset_name.Name = "cb_offset_name";
            this.cb_offset_name.Size = new System.Drawing.Size(285, 24);
            this.cb_offset_name.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Offset Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Subsystem:";
            // 
            // cb_subsystems
            // 
            this.cb_subsystems.Location = new System.Drawing.Point(161, 88);
            this.cb_subsystems.Name = "cb_subsystems";
            this.cb_subsystems.Size = new System.Drawing.Size(285, 24);
            this.cb_subsystems.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "\'From\' Component:";
            // 
            // cb_from_component
            // 
            this.cb_from_component.FormattingEnabled = true;
            this.cb_from_component.Location = new System.Drawing.Point(161, 118);
            this.cb_from_component.Name = "cb_from_component";
            this.cb_from_component.Size = new System.Drawing.Size(285, 24);
            this.cb_from_component.TabIndex = 3;
            // 
            // btn_AddOffset
            // 
            this.btn_AddOffset.Location = new System.Drawing.Point(452, 25);
            this.btn_AddOffset.Name = "btn_AddOffset";
            this.btn_AddOffset.Size = new System.Drawing.Size(176, 45);
            this.btn_AddOffset.TabIndex = 5;
            this.btn_AddOffset.Text = "Add new offset";
            this.btn_AddOffset.UseVisualStyleBackColor = true;
            this.btn_AddOffset.Click += new System.EventHandler(this.btn_AddOffset_Click);
            // 
            // btn_CancelAdd
            // 
            this.btn_CancelAdd.Location = new System.Drawing.Point(551, 366);
            this.btn_CancelAdd.Name = "btn_CancelAdd";
            this.btn_CancelAdd.Size = new System.Drawing.Size(95, 24);
            this.btn_CancelAdd.TabIndex = 7;
            this.btn_CancelAdd.Text = "Cancel";
            this.btn_CancelAdd.UseVisualStyleBackColor = true;
            this.btn_CancelAdd.Click += new System.EventHandler(this.btn_CancelAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(13, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 46;
            this.label3.Text = "Work offsets:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmn_table_name,
            this.clm_subsystem,
            this.clm_register,
            this.clm_from_component,
            this.clm_to_ucs});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.Location = new System.Drawing.Point(34, 39);
            this.dataGridView2.MaximumSize = new System.Drawing.Size(612, 150);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 20;
            this.dataGridView2.RowTemplate.Height = 20;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView2.Size = new System.Drawing.Size(612, 125);
            this.dataGridView2.TabIndex = 45;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // clmn_table_name
            // 
            this.clmn_table_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmn_table_name.FillWeight = 24F;
            this.clmn_table_name.HeaderText = "Table name";
            this.clmn_table_name.Name = "clmn_table_name";
            this.clmn_table_name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // clm_subsystem
            // 
            this.clm_subsystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clm_subsystem.FillWeight = 16F;
            this.clm_subsystem.HeaderText = "Register";
            this.clm_subsystem.Name = "clm_subsystem";
            this.clm_subsystem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // clm_register
            // 
            this.clm_register.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clm_register.FillWeight = 18F;
            this.clm_register.HeaderText = "Subsystem";
            this.clm_register.Name = "clm_register";
            this.clm_register.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // clm_from_component
            // 
            this.clm_from_component.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clm_from_component.FillWeight = 22F;
            this.clm_from_component.HeaderText = "\'From\' component";
            this.clm_from_component.Name = "clm_from_component";
            this.clm_from_component.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // clm_to_ucs
            // 
            this.clm_to_ucs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clm_to_ucs.FillWeight = 20F;
            this.clm_to_ucs.HeaderText = "\'To\' UCS";
            this.clm_to_ucs.Name = "clm_to_ucs";
            this.clm_to_ucs.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_registers);
            this.groupBox1.Controls.Add(this.btn_ModifyOffset);
            this.groupBox1.Controls.Add(this.btn_delete_offset);
            this.groupBox1.Controls.Add(this.btn_AddOffset);
            this.groupBox1.Controls.Add(this.cb_from_component);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cb_subsystems);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_csys_origin);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cb_offset_name);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 185);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add/delete work offset:";
            // 
            // cb_registers
            // 
            this.cb_registers.FormattingEnabled = true;
            this.cb_registers.Location = new System.Drawing.Point(161, 58);
            this.cb_registers.Name = "cb_registers";
            this.cb_registers.Size = new System.Drawing.Size(285, 24);
            this.cb_registers.TabIndex = 17;
            // 
            // btn_ModifyOffset
            // 
            this.btn_ModifyOffset.Location = new System.Drawing.Point(452, 76);
            this.btn_ModifyOffset.Name = "btn_ModifyOffset";
            this.btn_ModifyOffset.Size = new System.Drawing.Size(176, 45);
            this.btn_ModifyOffset.TabIndex = 16;
            this.btn_ModifyOffset.Text = "Modify selected offset";
            this.btn_ModifyOffset.UseVisualStyleBackColor = true;
            this.btn_ModifyOffset.Click += new System.EventHandler(this.btn_ModifyOffset_Click);
            // 
            // btn_delete_offset
            // 
            this.btn_delete_offset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_delete_offset.Location = new System.Drawing.Point(452, 127);
            this.btn_delete_offset.Name = "btn_delete_offset";
            this.btn_delete_offset.Size = new System.Drawing.Size(176, 45);
            this.btn_delete_offset.TabIndex = 15;
            this.btn_delete_offset.Text = "Delete selected offset";
            this.btn_delete_offset.UseVisualStyleBackColor = true;
            this.btn_delete_offset.Click += new System.EventHandler(this.btn_delete_offset_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(450, 366);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(95, 24);
            this.btn_OK.TabIndex = 48;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // WorkOffsetDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 401);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btn_CancelAdd);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(679, 446);
            this.MinimumSize = new System.Drawing.Size(679, 446);
            this.Name = "WorkOffsetDlg";
            this.Text = "Add Work Offset";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_csys_origin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_offset_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_subsystems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_from_component;
        private System.Windows.Forms.Button btn_AddOffset;
        private System.Windows.Forms.Button btn_CancelAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_delete_offset;
        private System.Windows.Forms.Button btn_ModifyOffset;
        private System.Windows.Forms.ComboBox cb_registers;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmn_table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_subsystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_register;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_from_component;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_to_ucs;
    }
}