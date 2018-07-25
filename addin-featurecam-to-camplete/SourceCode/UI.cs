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
using System.IO;
using System.Windows.Forms;

namespace FeatureCAMToCAMplete
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
            SetDefaultParams();
        }

        public void SetDefaultParams()
        {
            if (Variables.doc == null) return;

            tb_output_dir.Text = Variables.output_dirpath;

            if (Variables.stock.IndexType == FeatureCAM.tagFMIndexType.eIT_None)
            {
                gb_no_indexing.Visible = true;
                gb_no_indexing.Location = new System.Drawing.Point(12, 433);
                gb_5axis.Visible = false;
                cb_setups_list.Items.Clear();
                if (Variables.setup_names != null)
                {
                    foreach (string setup_name in Variables.setup_names)
                        cb_setups_list.Items.Add(setup_name);
                    if (Variables.setup_names.Count > 0)
                        cb_setups_list.SelectedIndex = Variables.selected_setup_id;
                }
            }
            else if (Variables.stock.IndexType == FeatureCAM.tagFMIndexType.eIT_5thAxis)
            {
                gb_no_indexing.Visible = false;
                gb_5axis.Visible = true;
            }
            else
            {
                gb_no_indexing.Visible = false;
                gb_5axis.Visible = false;
            }
                 lb_clamps.Items.Clear();

            if (Variables.clamps != null)
            {
                foreach (SolidInfo solid_info in Variables.clamps)
                    lb_clamps.Items.Add(solid_info.solid.Name, solid_info.is_export);
            }

            if (Variables.doc.Solids.Count > 0)
                rb_create_part_stl_file.Checked = true;
            else
                rb_select_part_stl_file.Checked = true;

            cb_export_part.Checked = Variables.is_export_part;
            if (Variables.part_fpath != "")
                tb_part_stl_fpath.Text = Variables.part_fpath;
            SetExportPartValues();
            lb_clamps.Items.Clear();
            if (Variables.clamps != null)
            {
                foreach (SolidInfo solid_info in Variables.clamps)
                {
                    lb_clamps.Items.Add(solid_info.solid.Name, solid_info.is_export);
                    cbox_part_solid.Items.Add(solid_info.solid.Name);
                }
            }

            if (Variables.tool_identification == 0)
                rb_tool_number.Checked = true;
            else
                rb_tool_id.Checked = true;

            tbOffsetX.Text = Variables.offset_x.ToString();
            tbOffsetY.Text = Variables.offset_y.ToString();
            tbOffsetZ.Text = Variables.offset_z.ToString();
        }

        private void SetExportPartValues()
        {
            if (Variables.doc.Solids.Count > 0)
            {
                cb_export_part.Text = "Export Part solid (.stl file)";
            }
            else
            {
                cb_export_part.Text = "Export Part solid (.stl file). There are no solids in the project, so you can only select file.";
            }
            rb_select_part_stl_file.Enabled = cb_export_part.Checked;
            rb_create_part_stl_file.Enabled = cb_export_part.Checked;
            if (rb_select_part_stl_file.Checked && cb_export_part.Checked)
            {
                tb_part_stl_fpath.Enabled = true;
                b_select_part_stl_file.Enabled = true;
            }
            else
            {
                tb_part_stl_fpath.Enabled = false;
                b_select_part_stl_file.Enabled = false;
            }
            cbox_part_solid.Enabled = rb_create_part_stl_file.Checked && cb_export_part.Checked;
            b_select_part_solid.Enabled = rb_create_part_stl_file.Checked && cb_export_part.Checked;
        }

        private void b_export_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Variables.output_dirpath))
            {
                MessageBox.Show("Output directory path must be set. Please fix it and try again.", Variables.prog_name);
                return;
            }
            foreach (SolidInfo solid_info in Variables.clamps)
                solid_info.is_export = false;
            if (Variables.clamps != null)
            {
                foreach (string item in lb_clamps.CheckedItems)
                {
                    foreach (SolidInfo solid_info in Variables.clamps)
                    {
                        if (solid_info.solid.Name.Equals(item, StringComparison.OrdinalIgnoreCase))
                            solid_info.is_export = true;
                    }
                }
            }
            Variables.is_use_part_file = rb_select_part_stl_file.Checked;
            Variables.is_export_part_now = rb_create_part_stl_file.Checked;
            Variables.is_export_part = cb_export_part.Checked;
            if (Variables.is_export_part)
            {
                if (Variables.is_use_part_file)
                {
                    if (Variables.part_fpath == "")
                    {
                        MessageBox.Show("If using existing part solid file, file path must be set. Please fix it and try again.", Variables.prog_name);
                        return;
                    }
                    if (!File.Exists(Variables.part_fpath))
                    {
                        MessageBox.Show("If using existing part solid file, part solid file must exist. File " + Variables.part_fpath + " doesn't exist. Please select existing file and try again.", Variables.prog_name);
                        return;
                    }
                }
                if (Variables.is_export_part_now)
                {
                    if (cbox_part_solid.SelectedItem != null)
                    {
                        if (cbox_part_solid.SelectedItem.ToString() != "")
                            Variables.part_solid_name = (string)cbox_part_solid.SelectedItem;
                    }
                    else
                    {
                        MessageBox.Show("If exporting a solid as a part file, solid should be selected. Please select a solid and try again.", Variables.prog_name);
                        return;
                    }
                }
            }

            this.Close();
            FCToCAMplete.main_form = null;

            if (Variables.offset_pt != null)
                Variables.offset_pt.Delete(false);

            FCToCAMplete.Convert();

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (FCToCAMplete.main_form != null)
            {
                FCToCAMplete.main_form = null;
                if (Variables.offset_pt != null)
                    Variables.offset_pt.Delete(false);
            }
            return;
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            if (FCToCAMplete.main_form != null)
            {
                FCToCAMplete.main_form = null;
                if (Variables.offset_pt != null)
                    Variables.offset_pt.Delete(false);
            }
            return;
        }

        private void b_select_output_dir_Click(object sender, EventArgs e)
        {
            outputDirBrowserDialog1.Description = "Select output directory";
            outputDirBrowserDialog1.SelectedPath = Variables.output_dirpath;
            if (outputDirBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_output_dir.Text = outputDirBrowserDialog1.SelectedPath;
                Variables.output_dirpath = tb_output_dir.Text;
            }
        }

        /* To do */
        private void b_help_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, Path.Combine(FCToCAMplete.Application.path, "Help\\EZFM.chm"), "174713.htm");
        }

        private void tb_output_dir_TextChanged(object sender, EventArgs e)
        {
            Variables.output_dirpath = tb_output_dir.Text;
        }

        private void cb_setups_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            Variables.selected_setup_id = cb_setups_list.SelectedIndex;
        }

        private void bPreviewOffset_Click(object sender, EventArgs e)
        {
            Variables.offset_pt = Variables.doc.AddPoint(Variables.offset_x, Variables.offset_y, Variables.offset_z);

            Variables.offset_pt.Select(true, true);
        }

        private void tbOffsetX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(tbOffsetX.Text))
                    tbOffsetX.Text = Variables.offset_x.ToString();
                else if (tbOffsetX.Text != "-")
                    Variables.offset_x = Convert.ToDouble(tbOffsetX.Text);
            }
            catch
            {
                tbOffsetX.Text = Variables.offset_x.ToString();
            }
        }

        private void tbOffsetY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(tbOffsetY.Text))
                    tbOffsetY.Text = Variables.offset_y.ToString();
                else if (tbOffsetY.Text != "-")
                    Variables.offset_y = Convert.ToDouble(tbOffsetY.Text);
            }
            catch
            {
                tbOffsetY.Text = Variables.offset_y.ToString();
            }
        }

        private void tbOffsetZ_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(tbOffsetZ.Text))
                    tbOffsetZ.Text = Variables.offset_z.ToString();
                else if (tbOffsetZ.Text != "-")
                    Variables.offset_z = Convert.ToDouble(tbOffsetZ.Text);
            }
            catch
            {
                tbOffsetZ.Text = Variables.offset_z.ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Variables.tool_identification = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Variables.tool_identification = 1;
        }

        private void rb_indiv_offsets_CheckedChanged(object sender, EventArgs e)
        {
            Variables.use_DATUM = !rb_indiv_offsets.Checked;
        }

        private void rb_DATUM_CheckedChanged(object sender, EventArgs e)
        {
            Variables.use_DATUM = rb_DATUM.Checked;
        }

        private void cb_export_part_CheckedChanged(object sender, EventArgs e)
        {
            Variables.is_export_part = cb_export_part.Checked;
            SetExportPartValues();
        }

        private void b_select_part_solid_Click(object sender, EventArgs e)
        {
            FeatureCAM.FMSolid selected_solid = null;

            if (Variables.doc.Solids.Count > 0)
                foreach (FeatureCAM.FMSolid solid in Variables.doc.Solids)
                {
                    if (solid.Selected)
                    {
                        if (selected_solid == null)
                            selected_solid = solid;
                        else
                        {
                            MessageBox.Show("More than one solid is selected in the part. Please select only one solid and try again.", Variables.prog_name);
                            return;
                        }
                    }
                }
            if (selected_solid != null)
                cbox_part_solid.SelectedItem = selected_solid.Name;
            else
                MessageBox.Show("Couldn't find a selected solid in the part. Please select one solid and try again.", Variables.prog_name);
        }

        private void rb_create_part_stl_file_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_create_part_stl_file.Checked)
            {
                rb_select_part_stl_file.Checked = false;
                b_select_part_stl_file.Enabled = false;
                tb_part_stl_fpath.Enabled = false;
                cbox_part_solid.Enabled = true;
                b_select_part_solid.Enabled = true;
            }
            Variables.is_export_part_now = rb_create_part_stl_file.Checked;
            Variables.is_use_part_file = !rb_create_part_stl_file.Checked;
        }

        private void rb_select_part_stl_file_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_select_part_stl_file.Checked)
            {
                rb_create_part_stl_file.Checked = false;
                tb_part_stl_fpath.Enabled = true;
                b_select_part_stl_file.Enabled = true;
                cbox_part_solid.Enabled = false;
                b_select_part_solid.Enabled = false;
            }
            Variables.is_use_part_file = rb_select_part_stl_file.Checked;
            Variables.is_export_part_now = !rb_select_part_stl_file.Checked;

        }

        private void tb_part_stl_fpath_TextChanged(object sender, EventArgs e)
        {
            Variables.part_fpath = tb_part_stl_fpath.Text;
            if (tb_part_stl_fpath.Text != "")
                rb_select_part_stl_file.Checked = true;
        }

        private void lb_clamps_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSolidInPart((string)lb_clamps.SelectedItem);
        }

        private void cbox_part_solid_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSolidInPart((string)cbox_part_solid.SelectedItem);
        }

        private void SelectSolidInPart(string solid_name)
        {
            FeatureCAM.FMSolid solid;
            if (Variables.doc.Solids.Count > 0)
            {
                foreach (FeatureCAM.FMSolid solid1 in Variables.doc.Solids)
                    solid1.Select(false, true);

                solid = (FeatureCAM.FMSolid)Variables.doc.Solids.Item(solid_name);
                if (solid != null) solid.Select(true, true);
            }
        }

        private void b_select_part_stl_file_Click(object sender, EventArgs e)
        {
            openPartFileDialog1.Filter = "STL file (*.stl)|*.stl";
            openPartFileDialog1.FileName = "";
            openPartFileDialog1.CheckFileExists = true;
            openPartFileDialog1.Title = "Select part .stl file";
            if (openPartFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_part_stl_fpath.Text = openPartFileDialog1.FileName;
                Variables.part_fpath = tb_part_stl_fpath.Text;
                rb_select_part_stl_file.Checked = true;
            }
        }

        private void b_select_part_solids_Click(object sender, EventArgs e)
        {
            if (Variables.doc.Solids.Selected.Count == 0) return;

            for (int i = 0; i < lb_clamps.Items.Count; i++)
            {
                if (Variables.doc.Solids.Selected.Item(lb_clamps.Items[i]) != null)
                    lb_clamps.SetItemCheckState(i, CheckState.Checked);
                else
                    lb_clamps.SetItemCheckState(i, CheckState.Unchecked);
            }

        }


    }
}
