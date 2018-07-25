// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FeatureCAMExporter;

namespace FeatureCAMToVericut
{
    public partial class StockDesignDlg_TurnMill : Form
    {
        private static int setup_index;

        public StockDesignDlg_TurnMill(int selected_setup_index)
        {
            setup_index = selected_setup_index;

            InitializeComponent();

            this.Text = LanguageSupport.Translate(this.Text);
            cb_export_stock.Text = LanguageSupport.Translate(cb_export_stock.Text);
            label8.Text = LanguageSupport.Translate(label8.Text);
            cb_export_design.Text = LanguageSupport.Translate(cb_export_design.Text);
            label1.Text = LanguageSupport.Translate(label1.Text);
            label2.Text = LanguageSupport.Translate(label2.Text);
            btn_Ok.Text = LanguageSupport.Translate(btn_Ok.Text);
            btn_Cancel.Text = LanguageSupport.Translate(btn_Cancel.Text);
            label5.Text = LanguageSupport.Translate(label5.Text);
            label3.Text = LanguageSupport.Translate(label3.Text);
            label2.Text = LanguageSupport.Translate(label2.Text);
            label4.Text = LanguageSupport.Translate(label4.Text);
            label6.Text = LanguageSupport.Translate(label6.Text);

            this.cb_export_stock.Checked = Variables.setups_info[setup_index].options.is_export_stock;

            cb_export_stock.Checked = Variables.setups_info[setup_index].options.is_export_stock;
            this.cb_attach_stock_to.Enabled = cb_export_stock.Checked;
            this.label8.Enabled = cb_export_stock.Checked;

            cb_attach_stock_to.Items.Clear();
            cb_attach_stock_to_subspindle.Items.Clear();
            if (Variables.setups_info[setup_index].attach_components != null)
                foreach (string attach_comp in Variables.setups_info[setup_index].attach_components)
                {
                    cb_attach_stock_to.Items.Add(attach_comp);
                    cb_attach_stock_to_subspindle.Items.Add(attach_comp);
                }
            if (Variables.setups_info[setup_index].attach_stock_to != "" && cb_attach_stock_to.Items.Contains(Variables.setups_info[setup_index].attach_stock_to))
                cb_attach_stock_to.SelectedItem = Variables.setups_info[setup_index].attach_stock_to;
            if (Variables.setups_info[setup_index].attach_stock_to_subspindle != "" && cb_attach_stock_to_subspindle.Items.Contains(Variables.setups_info[setup_index].attach_stock_to_subspindle))
                cb_attach_stock_to_subspindle.SelectedItem = Variables.setups_info[setup_index].attach_stock_to_subspindle;
            cb_design_solid.Items.Clear();
            if (Variables.all_solids != null)
                foreach (SolidInfo solid in Variables.all_solids)
                    cb_design_solid.Items.Add(solid.name);
            cb_attach_design_to.Items.Clear();
            if (Variables.setups_info[setup_index].attach_components != null)
                foreach (string attach_comp in Variables.setups_info[setup_index].attach_components)
                {
                    cb_attach_design_to.Items.Add(attach_comp);
                    cb_attach_design_to_subspindle.Items.Add(attach_comp);
                }
            if (Variables.setups_info[setup_index].part != null)
            {
                if (Variables.setups_info[setup_index].part.Count != 0)
                {
                    cb_export_design.Checked = true;
                    if (cb_attach_design_to.Items.Contains(Variables.setups_info[setup_index].part[0].attach_to))
                        cb_attach_design_to.SelectedItem = Variables.setups_info[setup_index].part[0].attach_to;
                    if (cb_design_solid.Items.Contains(Variables.setups_info[setup_index].part[0].name))
                        cb_design_solid.SelectedItem = Variables.setups_info[setup_index].part[0].name;
                    if (Variables.setups_info[setup_index].part.Count >= 2)
                    {
                        if (cb_attach_design_to_subspindle.Items.Contains(Variables.setups_info[setup_index].part[1].attach_to))
                            cb_attach_design_to_subspindle.SelectedItem = Variables.setups_info[setup_index].part[1].attach_to;
                    }
                }
                else
                    cb_export_design.Checked = false;
            }
            else
                cb_export_design.Checked = false;

            label3.Enabled = !Variables.are_all_setups_milling;
            cb_attach_stock_to_subspindle.Enabled = !Variables.are_all_setups_milling;
            label4.Enabled = !Variables.are_all_setups_milling;
            cb_attach_design_to_subspindle.Enabled = !Variables.are_all_setups_milling;
        }

        private void cb_export_stock_CheckedChanged(object sender, EventArgs e)
        {
            Variables.setups_info[setup_index].options.is_export_stock = cb_export_stock.Checked;
            this.cb_attach_stock_to.Enabled = cb_export_stock.Checked;
            this.label8.Enabled = cb_export_stock.Checked;
        }

        private void cb_attach_stock_to_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            Variables.setups_info[setup_index].attach_stock_to = cb_attach_stock_to.Text;
            Variables.setups_info[setup_index].options.attach_stock_to = cb_attach_stock_to.Text;
            Variables.setups_info[setup_index].attach_stock_to_subspindle = cb_attach_stock_to_subspindle.Text;
            Variables.setups_info[setup_index].options.attach_stock_to_subspindle = cb_attach_stock_to_subspindle.Text;

            if (Variables.setups_info[setup_index].part != null)
                Variables.setups_info[setup_index].part.Clear();
            if (!cb_export_design.Checked)
                Variables.setups_info[setup_index].part = null;
            else
            {
                if (cb_design_solid.SelectedItem != null)
                {
                    if (Variables.setups_info[setup_index].part == null) Variables.setups_info[setup_index].part = new List<SolidInfo>();
                    Variables.setups_info[setup_index].part.Add(new SolidInfo(cb_design_solid.SelectedItem.ToString(),
                                                                               cb_attach_design_to.SelectedItem.ToString()));
                    if (cb_attach_design_to_subspindle.SelectedItem != null)
                        Variables.setups_info[setup_index].part.Add(new SolidInfo(cb_design_solid.SelectedItem.ToString(),
                                                                               cb_attach_design_to_subspindle.SelectedItem.ToString(),
                                                                               false));
                }
            }
            this.Close();
            return;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void cb_export_design_CheckedChanged(object sender, EventArgs e)
        {
            this.label1.Enabled = cb_export_design.Checked;
            this.cb_design_solid.Enabled = cb_export_design.Checked;
            this.label2.Enabled = cb_export_design.Checked;
            this.cb_attach_design_to.Enabled = cb_export_design.Checked;
        }

    }
}
