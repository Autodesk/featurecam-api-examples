// -----------------------------------------------------------------------
// Copyright 2017 Autodesk, Inc. All rights reserved.
// 
// This computer source code and related instructions and comments are the 
// unpublished confidential and proprietary information of Autodesk, Inc. 
// and are protected under applicable copyright and trade secret law. They 
// may not be disclosed to, copied or used by any third party without the 
// prior written consent of Autodesk, Inc.
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
    public partial class WorkOffsetDlg : Form
    {
        public int setup_index;

        public WorkOffsetDlg(int selected_setup_index)
        {
            setup_index = selected_setup_index;

            InitializeComponent();
            this.Text = FeatureCAMExporter.LanguageSupport.Translate(this.Text);
            label3.Text = FeatureCAMExporter.LanguageSupport.Translate(label3.Text);
            groupBox1.Text = FeatureCAMExporter.LanguageSupport.Translate(groupBox1.Text);
            label1.Text = FeatureCAMExporter.LanguageSupport.Translate(label1.Text);
            label2.Text = FeatureCAMExporter.LanguageSupport.Translate(label2.Text);
            label6.Text = FeatureCAMExporter.LanguageSupport.Translate(label6.Text);
            label7.Text = FeatureCAMExporter.LanguageSupport.Translate(label7.Text);
            label8.Text = FeatureCAMExporter.LanguageSupport.Translate(label8.Text);
            btn_AddOffset.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_AddOffset.Text);
            btn_CancelAdd.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_CancelAdd.Text);
            btn_delete_offset.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_delete_offset.Text);
            btn_ModifyOffset.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_ModifyOffset.Text);
            btn_OK.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_OK.Text);
            clm_from_component.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(clm_from_component.HeaderText);
            clm_register.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(clm_register.HeaderText);
            clm_subsystem.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(clm_subsystem.HeaderText);
            clm_to_ucs.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(clm_to_ucs.HeaderText);
            clmn_table_name.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(clmn_table_name.HeaderText);            

            PopulateComboboxes();
            if (Variables.setups_info[setup_index].work_offsets == null) return;
            foreach (WorkOffset offset in Variables.setups_info[setup_index].work_offsets)
            {
                dataGridView2.Rows.Add(this.cb_offset_name.Items[offset.type].ToString(),
                                        offset.register,
                                        offset.subsystem,
                                        offset.from_component,
                                        offset.to_csys_ucs_name);
            }
        }

        private void btn_CancelAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_AddOffset_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.cb_csys_origin.Text) ||
                String.IsNullOrEmpty(this.cb_from_component.Text) ||
                String.IsNullOrEmpty(this.cb_registers.Text) ||
                String.IsNullOrEmpty(this.cb_subsystems.Text) ||
                String.IsNullOrEmpty(this.cb_offset_name.Text))
                return;
            this.dataGridView2.Rows.Add(this.cb_offset_name.Items[this.cb_offset_name.SelectedIndex].ToString(),
                                        this.cb_registers.Text,
                                        this.cb_subsystems.Text,
                                        this.cb_from_component.Text,
                                        this.cb_csys_origin.Text);
        }

        private void PopulateComboboxes()
        {
            this.cb_csys_origin.Items.Clear();
            if (Variables.all_ucss != null)
            {
                foreach (UCS ucs in Variables.all_ucss)
                    this.cb_csys_origin.Items.Add(ucs.name);
            }
            if (Variables.all_fixture_ids != null)
            {
                foreach (string fixture_id in Variables.all_fixture_ids)
                    if (!String.IsNullOrEmpty(fixture_id))
                        this.cb_registers.Items.Add(fixture_id);
            }
            this.cb_from_component.Items.Clear();
            if (Variables.setups_info[setup_index].attach_components != null)
            {
                foreach (string attach_comp in Variables.setups_info[setup_index].attach_components)
                    this.cb_from_component.Items.Add(attach_comp);
            }
            this.cb_subsystems.Items.Clear();
            if (Variables.setups_info[setup_index].subsystems != null)
            {
                foreach (string subsystem in Variables.setups_info[setup_index].subsystems)
                    this.cb_subsystems.Items.Add(subsystem);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Variables.setups_info[setup_index].work_offsets = new List<WorkOffset>();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                Variables.setups_info[setup_index].work_offsets.Add(
                    new WorkOffset(this.cb_offset_name.Items.IndexOf(dataGridView2.Rows[i].Cells[0].Value.ToString()),
                                   dataGridView2.Rows[i].Cells[1].Value.ToString(),
                                   dataGridView2.Rows[i].Cells[2].Value.ToString(),
                                   dataGridView2.Rows[i].Cells[3].Value.ToString(),
                                   dataGridView2.Rows[i].Cells[4].Value.ToString()));
                                    
            }
            this.Close();
        }

        private void btn_delete_offset_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count <= 0) return;
            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedCells[0].RowIndex);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedCells.Count <= 0) return;
            this.cb_offset_name.SelectedItem = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            this.cb_registers.Text = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            this.cb_subsystems.SelectedItem = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
            this.cb_from_component.SelectedItem = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
            this.cb_csys_origin.SelectedItem = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
        }

        private void btn_ModifyOffset_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.cb_csys_origin.Text) ||
                String.IsNullOrEmpty(this.cb_from_component.Text) ||
                String.IsNullOrEmpty(this.cb_registers.Text) ||
                String.IsNullOrEmpty(this.cb_subsystems.Text) ||
                String.IsNullOrEmpty(this.cb_offset_name.Text))
                return;

            if (dataGridView2.SelectedCells.Count <= 0) return;
            dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value = this.cb_offset_name.SelectedItem;
            dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[1].Value = this.cb_registers.Text;
            dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[2].Value = this.cb_subsystems.SelectedItem;
            dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[3].Value = this.cb_from_component.SelectedItem;
            dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[4].Value = this.cb_csys_origin.SelectedItem;
        }

    }
}
