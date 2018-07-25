// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

namespace FeatureCAMToVericut
{
    partial class StockDesignDlg_TurnMill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockDesignDlg_TurnMill));
            this.cb_attach_stock_to = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_export_stock = new System.Windows.Forms.CheckBox();
            this.cb_export_design = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_design_solid = new System.Windows.Forms.ComboBox();
            this.cb_attach_design_to = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cb_attach_stock_to_subspindle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_attach_design_to_subspindle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_attach_stock_to
            // 
            this.cb_attach_stock_to.FormattingEnabled = true;
            this.cb_attach_stock_to.Location = new System.Drawing.Point(241, 79);
            this.cb_attach_stock_to.Name = "cb_attach_stock_to";
            this.cb_attach_stock_to.Size = new System.Drawing.Size(196, 24);
            this.cb_attach_stock_to.TabIndex = 52;
            this.cb_attach_stock_to.SelectedIndexChanged += new System.EventHandler(this.cb_attach_stock_to_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(47, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(269, 17);
            this.label8.TabIndex = 51;
            this.label8.Text = "Attach components:";
            // 
            // cb_export_stock
            // 
            this.cb_export_stock.AutoSize = true;
            this.cb_export_stock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_export_stock.Location = new System.Drawing.Point(27, 22);
            this.cb_export_stock.Margin = new System.Windows.Forms.Padding(4);
            this.cb_export_stock.Name = "cb_export_stock";
            this.cb_export_stock.Size = new System.Drawing.Size(196, 21);
            this.cb_export_stock.TabIndex = 50;
            this.cb_export_stock.Text = "Export Stock solid (.stl file)";
            this.cb_export_stock.UseVisualStyleBackColor = true;
            this.cb_export_stock.CheckedChanged += new System.EventHandler(this.cb_export_stock_CheckedChanged);
            // 
            // cb_export_design
            // 
            this.cb_export_design.AutoSize = true;
            this.cb_export_design.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_export_design.Location = new System.Drawing.Point(27, 157);
            this.cb_export_design.Margin = new System.Windows.Forms.Padding(4);
            this.cb_export_design.Name = "cb_export_design";
            this.cb_export_design.Size = new System.Drawing.Size(235, 21);
            this.cb_export_design.TabIndex = 53;
            this.cb_export_design.Text = "Export Design/Part solid (.stl file)";
            this.cb_export_design.UseVisualStyleBackColor = true;
            this.cb_export_design.CheckedChanged += new System.EventHandler(this.cb_export_design_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 54;
            this.label1.Text = "Solid name:";
            // 
            // cb_design_solid
            // 
            this.cb_design_solid.FormattingEnabled = true;
            this.cb_design_solid.Location = new System.Drawing.Point(241, 179);
            this.cb_design_solid.Name = "cb_design_solid";
            this.cb_design_solid.Size = new System.Drawing.Size(196, 24);
            this.cb_design_solid.TabIndex = 55;
            // 
            // cb_attach_design_to
            // 
            this.cb_attach_design_to.FormattingEnabled = true;
            this.cb_attach_design_to.Location = new System.Drawing.Point(241, 237);
            this.cb_attach_design_to.Name = "cb_attach_design_to";
            this.cb_attach_design_to.Size = new System.Drawing.Size(196, 24);
            this.cb_attach_design_to.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(68, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 17);
            this.label2.TabIndex = 56;
            this.label2.Text = "Main spindle:";
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(241, 320);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(95, 24);
            this.btn_Ok.TabIndex = 59;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(342, 320);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 24);
            this.btn_Cancel.TabIndex = 60;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // cb_attach_stock_to_subspindle
            // 
            this.cb_attach_stock_to_subspindle.FormattingEnabled = true;
            this.cb_attach_stock_to_subspindle.Location = new System.Drawing.Point(241, 114);
            this.cb_attach_stock_to_subspindle.Name = "cb_attach_stock_to_subspindle";
            this.cb_attach_stock_to_subspindle.Size = new System.Drawing.Size(196, 24);
            this.cb_attach_stock_to_subspindle.TabIndex = 62;
            // 
            // label3
            // 
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(68, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 17);
            this.label3.TabIndex = 61;
            this.label3.Text = "Sub spindle:";
            // 
            // cb_attach_design_to_subspindle
            // 
            this.cb_attach_design_to_subspindle.FormattingEnabled = true;
            this.cb_attach_design_to_subspindle.Location = new System.Drawing.Point(241, 272);
            this.cb_attach_design_to_subspindle.Name = "cb_attach_design_to_subspindle";
            this.cb_attach_design_to_subspindle.Size = new System.Drawing.Size(196, 24);
            this.cb_attach_design_to_subspindle.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(68, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 17);
            this.label4.TabIndex = 63;
            this.label4.Text = "Sub spindle:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 65;
            this.label5.Text = "Main spindle:";
            // 
            // label6
            // 
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(47, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(269, 17);
            this.label6.TabIndex = 66;
            this.label6.Text = "Attach components:";
            // 
            // StockDesignDlg_TurnMill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 355);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_attach_design_to_subspindle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_attach_stock_to_subspindle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.cb_attach_design_to);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_design_solid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_export_design);
            this.Controls.Add(this.cb_attach_stock_to);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cb_export_stock);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(470, 400);
            this.MinimumSize = new System.Drawing.Size(470, 400);
            this.Name = "StockDesignDlg_TurnMill";
            this.Text = "Stock and Design Export Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_attach_stock_to;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cb_export_stock;
        private System.Windows.Forms.CheckBox cb_export_design;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_design_solid;
        private System.Windows.Forms.ComboBox cb_attach_design_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cb_attach_stock_to_subspindle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_attach_design_to_subspindle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}