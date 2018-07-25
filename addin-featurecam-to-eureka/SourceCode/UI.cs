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

namespace FeatureCAMToEUREKA
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

            tb_eureka_template_fpath.Text = Variables.eureka_template_fpath;

            tb_eureka_template_fpath.Enabled = Variables.is_export_project;
            b_select_eureka_template.Enabled = Variables.is_export_project;
            label5.Enabled = Variables.is_export_project;
            
            if (Variables.stock.IndexType == FeatureCAM.tagFMIndexType.eIT_None)
            {
                gb_no_indexing.Visible = true;
                gb_no_indexing.Location = new System.Drawing.Point(12, 287);
                gb_5axis.Visible = false;
                //lb_setup_warning.Visible = true;
                //cb_setups_list.Visible = true;
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
                 clb_solids_to_export.Items.Clear();

            if (Variables.clamps != null)
            {
                foreach (SolidInfo solid_info in Variables.clamps)
                    clb_solids_to_export.Items.Add(solid_info.solid.Name, solid_info.is_export);
            }

            if (Variables.tool_identification == 0)
                rb_tool_number.Checked = true;
            else
                rb_tool_id.Checked = true;

            tbOffsetX.Text = Variables.offset_x.ToString();
            tbOffsetY.Text = Variables.offset_y.ToString();
            tbOffsetZ.Text = Variables.offset_z.ToString();
        }

        private void b_export_Click(object sender, EventArgs e)
        {
            if (Variables.output_dirpath == "")
            {
                MessageBox.Show(
                    "Output directory path must be set. Please fix it and try again.",
                    Variables.prog_name,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Variables.eureka_template_fpath.Trim() != "")
                if (!File.Exists(Variables.eureka_template_fpath))
                {
                    MessageBox.Show(
                        "If EUREKA machine file path is set, file must exist. File " + Variables.eureka_template_fpath + " doesn't exist. Please select existing file and try again.", 
                        Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            foreach (SolidInfo solid_info in Variables.clamps)
                solid_info.is_export = false;
            if (Variables.clamps != null)
            {
                foreach (string item in clb_solids_to_export.CheckedItems)
                {
                    foreach (SolidInfo solid_info in Variables.clamps)
                    {
                        if (solid_info.solid.Name.Equals(item, StringComparison.OrdinalIgnoreCase))
                            solid_info.is_export = true;
                    }
                }
            }
            this.Close();
            if (Variables.offset_pt != null)
                Variables.offset_pt.Delete(false);

            FCToEUREKA.Convert();
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Variables.offset_pt != null)
                Variables.offset_pt.Delete(false);
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
            MessageBox.Show(
                "Help page is under construction. Please check back.",
                Variables.prog_name,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void b_select_eureka_template_Click(object sender, EventArgs e)
        {
            openEurekaTemplateFileDialog1.Filter = "EUREKA template or project file (*.epf, *.ept)|*.epf; *.ept";
            openEurekaTemplateFileDialog1.FileName = "";
            openEurekaTemplateFileDialog1.InitialDirectory = Variables.eureka_template_files_dir;
            openEurekaTemplateFileDialog1.CheckFileExists = true;
            openEurekaTemplateFileDialog1.Title = "Select EUREKA template file";
            if (openEurekaTemplateFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_eureka_template_fpath.Text = openEurekaTemplateFileDialog1.FileName;
                Variables.eureka_template_fpath = tb_eureka_template_fpath.Text;
            }

        }

        private void tb_output_dir_TextChanged(object sender, EventArgs e)
        {
            Variables.output_dirpath = tb_output_dir.Text;
        }

        private void tb_eureka_template_fpath_TextChanged(object sender, EventArgs e)
        {
            Variables.eureka_template_fpath = tb_eureka_template_fpath.Text;
        }

        private void cb_setups_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            Variables.selected_setup_id = cb_setups_list.SelectedIndex;
            Variables.output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName) + "_" + Variables.setup_names[Variables.selected_setup_id];
            tb_output_dir.Text = Variables.output_dirpath;
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
                if (tbOffsetX.Text == "")
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
                if (tbOffsetY.Text == "")
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
                if (tbOffsetZ.Text == "")
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

    }
}
