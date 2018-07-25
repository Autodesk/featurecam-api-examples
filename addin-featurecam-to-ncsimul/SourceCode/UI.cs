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
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FeatureCAMToNCSIMUL
{
    public partial class UI : Form
    {

        public UI()
        {
            //LanguageSupport.translation = new FeatureCAMToNCSIMUL_Local.StringTable();
            //LanguageSupport.FindAndReadTranslationFile();
            InitializeComponent();
            SetDefaultParams();
        }

        public void SetDefaultParams()
        {
            if (Variables.doc == null) return;

            InitializeTextStrings();
            label8.Text = LanguageSupport.label8_text;
            rb_save_to_file_dir.Text = LanguageSupport.rb_save_to_file_dir_text;
            rb_save_to_other_dir.Text = LanguageSupport.rb_save_to_other_dir_text;
            b_select_output_dir.Text = LanguageSupport.b_select_output_dir_text;
            cb_create_subdir.Text = LanguageSupport.cb_create_subdir_text;
            b_subdir_options.Text = LanguageSupport.b_subdir_options;
            button1.Text = LanguageSupport.button1_text;
            label5.Text = LanguageSupport.label5_text;
            b_select_ncsimul_machine.Text = LanguageSupport.b_select_ncsimul_machine_text;
            label3.Text = LanguageSupport.label3_text;
            label4.Text = LanguageSupport.label4_text;
            rb_tool_number.Text = LanguageSupport.rb_tool_number_text;
            rb_tool_id.Text = LanguageSupport.rb_tool_id_text;
            label2.Text = LanguageSupport.label2_text;
            label6.Text = LanguageSupport.label6_text;
            rb_indiv_offsets.Text = LanguageSupport.rb_indiv_offsets_text;
            rb_DATUM.Text = LanguageSupport.rb_DATUM_text;
            lb_setup_warning.Text = LanguageSupport.lb_setup_warning_text;
            gb_turret_info.Text = LanguageSupport.gb_turret_info_text;
            l_umss.Text = LanguageSupport.l_umss_text;
            l_usss.Text = LanguageSupport.l_usss_text;
            l_lmss.Text = LanguageSupport.l_lmss_text;
            l_lsss.Text = LanguageSupport.l_lsss_text;
            rb_umss_milling_head.Text = LanguageSupport.rb_umss_milling_head_text;
            rb_usss_milling_head.Text = LanguageSupport.rb_usss_milling_head_text;
            rb_lmss_milling_head.Text = LanguageSupport.rb_lmss_milling_head_text;
            rb_lsss_milling_head.Text = LanguageSupport.rb_lsss_milling_head_text;
            rb_umss_turret.Text = LanguageSupport.rb_umss_turret_text;
            rb_usss_turret.Text = LanguageSupport.rb_usss_turret_text;
            rb_lmss_turret.Text = LanguageSupport.rb_lmss_turret_text;
            rb_lsss_turret.Text = LanguageSupport.rb_lsss_turret_text;
            b_SaveSettings.Text = LanguageSupport.b_SaveSettings_text;
            b_export.Text = LanguageSupport.b_export_text;
            b_cancel.Text = LanguageSupport.b_cancel_text;
            b_help.Text = LanguageSupport.b_help_text;
            cb_tool_rad_as_offset.Text = LanguageSupport.cb_tool_rad_as_offset_text;
            cb_tool_length_as_offset.Text = LanguageSupport.cb_tool_length_as_offset_text;
            b_PreviewOffset.Text = LanguageSupport.b_PreviewOffset_text;


            lb_file_dir.Text = Variables.file_dirpath;
            EnableDesableOutputDirpathControls();
            cb_create_subdir.Checked = Settings.is_create_subdir;

			tb_output_dir.Text = Settings.alt_output_dirpath;

            tb_ncsimul_md_fpath.Text = Settings.ncsimul_md_fpath;

            tb_ncsimul_md_fpath.Enabled = Variables.is_export_project;
            b_select_ncsimul_machine.Enabled = Variables.is_export_project;
            label5.Enabled = Variables.is_export_project;

            if (Variables.stock.IndexType == FeatureCAM.tagFMIndexType.eIT_None)
            {
                gb_no_indexing.Visible = true;
                gb_no_indexing.Location = new System.Drawing.Point(16, 466);
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

            ReinitializeClamps();

            if (Variables.clamps != null)
            {
                foreach (SolidInfo solid_info in Variables.clamps)
                    lb_clamps.Items.Add(solid_info.solid.Name, solid_info.is_export);
            }

            if (Settings.tool_identification == 0)
                rb_tool_number.Checked = true;
            else
                rb_tool_id.Checked = true;

            if (Settings.is_use_DATUM)
                rb_DATUM.Checked = true;
            else
                rb_indiv_offsets.Checked = true;

            cb_tool_rad_as_offset.Checked = Settings.is_export_tool_rad_compensation;
            cb_tool_length_as_offset.Checked = Settings.is_export_tool_len_compensation;

            tbOffsetX.Text = Variables.offset_x.ToString();
            tbOffsetY.Text = Variables.offset_y.ToString();
            tbOffsetZ.Text = Variables.offset_z.ToString();
            SetTurretInfo();
        }

        public void InitializeTextStrings()
        {
            LanguageSupport.Translate(label8, ref LanguageSupport.label8_text);
            LanguageSupport.Translate(rb_save_to_file_dir, ref LanguageSupport.rb_save_to_file_dir_text);
            LanguageSupport.Translate(rb_save_to_other_dir, ref LanguageSupport.rb_save_to_other_dir_text);
            LanguageSupport.Translate(b_select_output_dir, ref LanguageSupport.b_select_output_dir_text);
            LanguageSupport.Translate(cb_create_subdir, ref LanguageSupport.cb_create_subdir_text);
            LanguageSupport.Translate(b_subdir_options, ref LanguageSupport.b_subdir_options);
            LanguageSupport.Translate(button1, ref LanguageSupport.button1_text);
            LanguageSupport.Translate(label5, ref LanguageSupport.label5_text);
            LanguageSupport.Translate(b_select_ncsimul_machine, ref LanguageSupport.b_select_ncsimul_machine_text);
            LanguageSupport.Translate(label3, ref LanguageSupport.label3_text);
            LanguageSupport.Translate(label4, ref LanguageSupport.label4_text);
            LanguageSupport.Translate(rb_tool_number, ref LanguageSupport.rb_tool_number_text);
            LanguageSupport.Translate(rb_tool_id, ref LanguageSupport.rb_tool_id_text);
            LanguageSupport.Translate(label2, ref LanguageSupport.label2_text);
            LanguageSupport.Translate(label6, ref LanguageSupport.label6_text);
            LanguageSupport.Translate(rb_indiv_offsets, ref LanguageSupport.rb_indiv_offsets_text);
            LanguageSupport.Translate(rb_DATUM, ref LanguageSupport.rb_DATUM_text);
            LanguageSupport.Translate(lb_setup_warning, ref LanguageSupport.lb_setup_warning_text);
            LanguageSupport.Translate(gb_turret_info, ref LanguageSupport.gb_turret_info_text);
            LanguageSupport.Translate(l_umss, ref LanguageSupport.l_umss_text);
            LanguageSupport.Translate(l_usss, ref LanguageSupport.l_usss_text);
            LanguageSupport.Translate(l_lmss, ref LanguageSupport.l_lmss_text);
            LanguageSupport.Translate(l_lsss, ref LanguageSupport.l_lsss_text);
            LanguageSupport.Translate(rb_umss_milling_head, ref LanguageSupport.rb_umss_milling_head_text);
            LanguageSupport.rb_usss_milling_head_text = LanguageSupport.rb_umss_milling_head_text;
            LanguageSupport.rb_lmss_milling_head_text = LanguageSupport.rb_umss_milling_head_text;
            LanguageSupport.rb_lsss_milling_head_text = LanguageSupport.rb_umss_milling_head_text;
            LanguageSupport.Translate(rb_umss_turret, ref LanguageSupport.rb_umss_turret_text);
            LanguageSupport.rb_usss_turret_text = LanguageSupport.rb_umss_turret_text;
            LanguageSupport.rb_lmss_turret_text = LanguageSupport.rb_umss_turret_text;
            LanguageSupport.rb_lsss_turret_text = LanguageSupport.rb_umss_turret_text;
            LanguageSupport.Translate(b_SaveSettings, ref LanguageSupport.b_SaveSettings_text);
            LanguageSupport.Translate(b_export, ref LanguageSupport.b_export_text);
            LanguageSupport.Translate(b_cancel, ref LanguageSupport.b_cancel_text);
            LanguageSupport.Translate(b_help, ref LanguageSupport.b_help_text);
            LanguageSupport.Translate(cb_tool_rad_as_offset, ref LanguageSupport.cb_tool_rad_as_offset_text);
            LanguageSupport.Translate(cb_tool_length_as_offset, ref LanguageSupport.cb_tool_length_as_offset_text);
            LanguageSupport.Translate(b_PreviewOffset, ref LanguageSupport.b_PreviewOffset_text);
        }



        private static void ReinitializeClamps()
        {
            Variables.clamps = new List<SolidInfo>();
            foreach (FeatureCAM.FMSolid solid in Variables.doc.Solids)
                Variables.clamps.Add(new SolidInfo(solid, solid.UseAsClamp));
        }

        private void SetTurretInfo()
        {
            if (!Variables.are_all_setups_milling)
            {
                bool available, temp, b_axis;
                FeatureCAM.tagFMTurretIDType turret_type;

                p_lmss.Visible = false;
                p_lsss.Visible = false;
                p_umss.Visible = false;
                p_usss.Visible = false;
                gb_turret_info.Visible = true;
                if (Variables.doc != null)
                {
                    FCToNCSIMUL.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET1, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    SetSpecificTurretInfo(turret_type, available, b_axis);
                    FCToNCSIMUL.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET2, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    SetSpecificTurretInfo(turret_type, available, b_axis);
                    FCToNCSIMUL.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET3, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    SetSpecificTurretInfo(turret_type, available, b_axis);
                    FCToNCSIMUL.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET4, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    SetSpecificTurretInfo(turret_type, available, b_axis);
                }
            }
            else
                gb_turret_info.Visible = false;
        }

        private void SetSpecificTurretInfo(FeatureCAM.tagFMTurretIDType turret_type, bool available, bool b_axis)
        {
            if (available)
                switch (turret_type)
                {
                    case FeatureCAM.tagFMTurretIDType.eTIDT_SubLower:
                        p_lsss.Visible = true;
                        rb_lsss_milling_head.Checked = b_axis;
                        break;
                    case FeatureCAM.tagFMTurretIDType.eTIDT_SubUpper:
                        p_usss.Visible = true;
                        rb_usss_milling_head.Checked = b_axis;
                        break;
                    case FeatureCAM.tagFMTurretIDType.eTIDT_MainLower:
                        p_lmss.Visible = true;
                        rb_lmss_milling_head.Checked = b_axis;
                        break;
                    case FeatureCAM.tagFMTurretIDType.eTIDT_MainUpper:
                        p_umss.Visible = true;
                        rb_umss_milling_head.Checked = b_axis;
                        break;
                }
        }

        private void b_export_Click(object sender, EventArgs e)
        {
            Variables.output_dirpath = ConstructOutputDirPath();
            if (Variables.output_dirpath == "")
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_no_output_folder_set), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                return;
            }
            if (Settings.ncsimul_md_fpath.Trim() == "")
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_no_machine_set), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                return;
            }
            if (Settings.ncsimul_md_fpath.Trim() != "")
                if (!File.Exists(Settings.ncsimul_md_fpath))
                {
                    MessageBox.Show(String.Format(LanguageSupport.Translate(Properties.Resources.msg_machine_file_doesnot_exist), Settings.ncsimul_md_fpath), 
                                    LanguageSupport.Translate(Properties.Resources.str_prog_name));
                    return;
                }
            if (Variables.clamps != null)
            {
                foreach (SolidInfo solid_info in Variables.clamps)
                    solid_info.is_export = false;

                foreach (string item in lb_clamps.CheckedItems)
                {
                    foreach (SolidInfo solid_info in Variables.clamps)
                    {
                        if (solid_info.solid.Name.Equals(item, StringComparison.OrdinalIgnoreCase))
                            solid_info.is_export = true;
                    }
                }
            }
            this.Close();
            FCToNCSIMUL.main_form = null;

            if (Variables.offset_pt != null)
                Variables.offset_pt.Delete(false);
            if (p_lmss.Visible)
            {
                Variables.lmss.available = true;
                Variables.lmss.is_milling_head = rb_lmss_milling_head.Checked;
            }
            if (p_lsss.Visible)
            {
                Variables.lsss.available = true;
                Variables.lsss.is_milling_head = rb_lsss_milling_head.Checked;
            }
            if (p_umss.Visible)
            {
                Variables.umss.available = true;
                Variables.umss.is_milling_head = rb_umss_milling_head.Checked;
            }
            if (p_usss.Visible)
            {
                Variables.usss.available = true;
                Variables.usss.is_milling_head = rb_usss_milling_head.Checked;
            }

            FCToNCSIMUL.Convert();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (FCToNCSIMUL.main_form != null)
            {
                FCToNCSIMUL.main_form = null;
                if (Variables.offset_pt != null)
                    Variables.offset_pt.Delete(false);
            }
            return;
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            if (FCToNCSIMUL.main_form != null)
            {
                FCToNCSIMUL.main_form = null;
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
                Settings.alt_output_dirpath = tb_output_dir.Text;
            }
        }

        /* To do */
        private void b_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help page is under construction. Please check back.");
        }

        private void b_select_ncsimul_machine_Click(object sender, EventArgs e)
        {
            openNCSIMULMDFileDialog1.Filter = "NCSIMUL machine file (*.mac)|*.mac";
            openNCSIMULMDFileDialog1.FileName = "";
            openNCSIMULMDFileDialog1.InitialDirectory = Variables.ncsimul_md_files_dir;
            openNCSIMULMDFileDialog1.CheckFileExists = true;
            openNCSIMULMDFileDialog1.Title = "Select NCSIMUL machine file";
            if (openNCSIMULMDFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_ncsimul_md_fpath.Text = openNCSIMULMDFileDialog1.FileName;
                Settings.ncsimul_md_fpath = tb_ncsimul_md_fpath.Text;
            }

        }

        private void tb_output_dir_TextChanged(object sender, EventArgs e)
        {
            Settings.alt_output_dirpath = tb_output_dir.Text;
        }

        private void tb_ncsimul_md_fpath_TextChanged(object sender, EventArgs e)
        {
            Settings.ncsimul_md_fpath = tb_ncsimul_md_fpath.Text;
            string new_output_dirpath;
            if (Variables.stock.IndexType != FeatureCAM.tagFMIndexType.eIT_None)
                new_output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName);
            else
                new_output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName) + "_" + Variables.setup_names[Variables.selected_setup_id];
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
            Settings.tool_identification = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.tool_identification = 1;
        }

        private void rb_indiv_offsets_CheckedChanged(object sender, EventArgs e)
        {
            Settings.is_use_DATUM = !rb_indiv_offsets.Checked;
        }

        private void rb_DATUM_CheckedChanged(object sender, EventArgs e)
        {
            Settings.is_use_DATUM = rb_DATUM.Checked;
        }

        private void rb_save_to_file_dir_CheckedChanged(object sender, EventArgs e)
        {
            Settings.is_file_dir = rb_save_to_file_dir.Checked;
            EnableDesableOutputDirpathControls();
        }

        private void rb_save_to_other_dir_CheckedChanged(object sender, EventArgs e)
        {
            Settings.is_file_dir = rb_save_to_file_dir.Checked;
            EnableDesableOutputDirpathControls();
        }

        private void EnableDesableOutputDirpathControls()
        {
            tb_output_dir.Enabled = !Settings.is_file_dir;
            b_select_output_dir.Enabled = !Settings.is_file_dir;
            rb_save_to_file_dir.Checked = Settings.is_file_dir;
            rb_save_to_other_dir.Checked = !Settings.is_file_dir;
        }

        private void cb_create_subdir_CheckedChanged(object sender, EventArgs e)
        {
            b_subdir_options.Enabled = true;
            Settings.is_create_subdir = cb_create_subdir.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ConstructOutputDirPath(), LanguageSupport.Translate(Properties.Resources.str_prog_name));
        }

        private string ConstructOutputDirPath()
        {
            string subdir_name = "";
            Variables.output_dirpath = "";
            if (rb_save_to_file_dir.Checked)
                Variables.output_dirpath = lb_file_dir.Text;
            else
                Variables.output_dirpath = tb_output_dir.Text;

            if (Settings.is_create_subdir)
            {
                subdir_name = "";
                if (Settings.subdir_format1 != null)
                {
                    foreach (Format format in Settings.subdir_format1)
                    {
                        switch (format.id)
                        {
                            case tagFormat.tF_CNCFileName:
                                break;
                            case tagFormat.tF_FileName:
                                subdir_name += (subdir_name != "" ? "_" : "") + Variables.doc.PartName;
                                break;
                            case tagFormat.tF_MachineName:
                                if (tb_ncsimul_md_fpath.Text != "")
                                    subdir_name += (subdir_name != "" ? "_" : "") + Path.GetFileNameWithoutExtension(tb_ncsimul_md_fpath.Text);
                                break;
                            case tagFormat.tF_SetupName:
                                if (Variables.stock.IndexType == FeatureCAM.tagFMIndexType.eIT_None && Settings.is_include_setup_name_in_subdir_name)
                                {
                                    if (cb_setups_list.SelectedItem.ToString() != "")
                                        subdir_name += (subdir_name != "" ? "_" : "") + cb_setups_list.SelectedItem.ToString();
                                }
                                break;
                            case tagFormat.tF_Title:
                                if (Variables.doc.PartDocumentation.Title != "")
                                    subdir_name += (subdir_name != "" ? "_" : "") + Variables.doc.PartDocumentation.Title;
                                break;
                        }
                    }
                }
            }
            return Path.Combine(Variables.output_dirpath, subdir_name);
        }


        private void b_subdir_options_Click(object sender, EventArgs e)
        {
            SubdirectoryOptions form2 = new SubdirectoryOptions();
            DialogResult res = form2.ShowDialog(this);
            form2.TopLevel = true;
            form2.TopMost = true;
        }

        private void b_SaveSettings_Click(object sender, EventArgs e)
        {
            Settings.SaveSettingsToIniFile();
        }

        private void cb_tool_rad_as_offset_CheckedChanged(object sender, EventArgs e)
        {
            Settings.is_export_tool_rad_compensation = cb_tool_rad_as_offset.Checked;
        }

        private void cb_tool_length_as_offset_CheckedChanged(object sender, EventArgs e)
        {
            Settings.is_export_tool_len_compensation = cb_tool_length_as_offset.Checked;
        }

    }
}
