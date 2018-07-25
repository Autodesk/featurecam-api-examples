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
    public partial class ClampsDlg_TurnMill : Form
    {
        private static int setup_index;

        public ClampsDlg_TurnMill(int selected_setup_index)
        {
            setup_index = selected_setup_index;

            InitializeComponent();
            this.Text = FeatureCAMExporter.LanguageSupport.Translate(this.Text);
            cb_export_clamps.Text = FeatureCAMExporter.LanguageSupport.Translate(cb_export_clamps.Text);
            btn_SelectPartSolids.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_SelectPartSolids.Text);
            groupBox1.Text = FeatureCAMExporter.LanguageSupport.Translate(groupBox1.Text);
            label1.Text = FeatureCAMExporter.LanguageSupport.Translate(label1.Text);
            label3.Text = FeatureCAMExporter.LanguageSupport.Translate(label3.Text);
            btn_Apply.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_Apply.Text);
            btn_OK.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_OK.Text);
            btn_Cancel.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_Cancel.Text);
            export.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(export.HeaderText);
            attach_to.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(attach_to.HeaderText);
            solid.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(solid.HeaderText);
            spindle.HeaderText = FeatureCAMExporter.LanguageSupport.Translate(spindle.HeaderText);
            export.Items.Clear();
            spindle.Items.Clear();
            export.Items.Add(FeatureCAMExporter.LanguageSupport.Translate("Yes"));
            export.Items.Add(FeatureCAMExporter.LanguageSupport.Translate("No"));
            spindle.Items.Add(FeatureCAMExporter.LanguageSupport.Translate("Main"));
            spindle.Items.Add(FeatureCAMExporter.LanguageSupport.Translate("Sub"));            

            /* Clamps and part solid */
            dataGrid_clamps_and_design1.Rows.Clear();
            attach_to.Items.Clear();
            if (Variables.setups_info[setup_index].attach_components != null)
            {
                foreach (string attach_comp in Variables.setups_info[setup_index].attach_components)
                    attach_to.Items.Add(attach_comp);
                foreach (string component in Variables.setups_info[setup_index].attach_components)
                    this.cb_attach_to.Items.Add(component);
            }
            InitializeSolidsGridView();
            cb_main_or_sub_spindle.SelectedIndex = 0;


        }

        private void InitializeSolidsGridView()
        {
            bool is_part, is_clamp, main_spindle;
            string attach_to_solid;

            dataGrid_clamps_and_design1.Rows.Clear();
            if (Variables.all_solids != null)
            {
                foreach (SolidInfo solid_info in Variables.all_solids)
                {
                    Variables.setups_info[setup_index].IsSolidAPartOrClampForSetup(
                                    solid_info.name, out is_part, out is_clamp, out attach_to_solid, out main_spindle);
                    if (!is_part)
                    {
                        if (attach_to_solid != "" && attach_to.Items.Contains(attach_to_solid))
                            dataGrid_clamps_and_design1.Rows.Add(
                                                                export.Items[(is_clamp ? 0 : 1)],
                                                                solid_info.name,
                                                                attach_to.Items[attach_to.Items.IndexOf(attach_to_solid)],
                                                                spindle.Items[(main_spindle ? 0 : 1)]);
                        else
                            dataGrid_clamps_and_design1.Rows.Add(
                                                                export.Items[(is_clamp ? 0 : 1)],
                                                                solid_info.name,
                                                                "",
                                                                spindle.Items[(main_spindle ? 0 : 1)]);
                    }
                }
            }
            cb_main_or_sub_spindle.Items.Clear();
            foreach (string spindle_name in spindle.Items)
                cb_main_or_sub_spindle.Items.Add(FeatureCAMExporter.LanguageSupport.Translate(spindle_name));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Variables.doc.Solids.Item(dataGrid_clamps_and_design1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).Select(true, true);
        }

        private void ReadClampsDesignDataGrid()
        {
            if (Variables.setups_info[setup_index].clamps == null)
                Variables.setups_info[setup_index].clamps = new List<SolidInfo>();
            else
                Variables.setups_info[setup_index].clamps.Clear();

            for (int i = 0; i < dataGrid_clamps_and_design1.Rows.Count; i++)
            {
                if (export.Items.IndexOf(dataGrid_clamps_and_design1.Rows[i].Cells[0].Value) == 0)
                {
                    Variables.setups_info[setup_index].clamps.Add(
                        new SolidInfo(dataGrid_clamps_and_design1.Rows[i].Cells[1].Value.ToString(),
                                      dataGrid_clamps_and_design1.Rows[i].Cells[2].Value.ToString(),
                                      ((spindle.Items.IndexOf(dataGrid_clamps_and_design1.Rows[i].Cells[3].Value) == 0) ? true : false)));
                }
            }
        }

        private int FindSolidInList(List<SolidInfo> all_solids, string name)
        {
            if (all_solids == null) return -1;

            for (int i = 0; i < all_solids.Count; i++)
                if (all_solids[i].name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;

            return -1;
        }

        private void dataGrid_clamps_and_design1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex].Index == 2)
            {
                grid.BeginEdit(true);
                ((ComboBox)grid.EditingControl).DroppedDown = true;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            ReadClampsDesignDataGrid();
            this.Close();
        }

        private void btn_SelectPartSolids_Click(object sender, EventArgs e)
        {
            if (Variables.doc.Solids.Selected.Count == 0) return;

            for (int i = 0; i < dataGrid_clamps_and_design1.Rows.Count; i++)
            {
                if (Variables.doc.Solids.Selected.Item(
                        dataGrid_clamps_and_design1.Rows[i].Cells[1].Value) != null)
                    dataGrid_clamps_and_design1.Rows[i].Cells[1].Selected = true;
                else
                    dataGrid_clamps_and_design1.Rows[i].Cells[1].Selected = false;
            }
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty((string)cb_attach_to.SelectedItem)) 
                return;

            for (int i = 0; i < dataGrid_clamps_and_design1.Rows.Count; i++)
            {
                if (dataGrid_clamps_and_design1.Rows[i].Cells[1].Selected)
                {
                    dataGrid_clamps_and_design1.Rows[i].Cells[0].Value = export.Items[0];
                    dataGrid_clamps_and_design1.Rows[i].Cells[2].Value = cb_attach_to.SelectedItem.ToString();
                    dataGrid_clamps_and_design1.Rows[i].Cells[3].Value = cb_main_or_sub_spindle.SelectedItem.ToString();
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
