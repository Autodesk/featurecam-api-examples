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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using FeatureCAMExporter;
using System.Diagnostics;

namespace FeatureCAMToVericut
{
    public partial class MainForm : Form
    {


        public MainForm()
        {
            InitializeComponent();
            label1.Text = LanguageSupport.Translate(label1.Text);
            b_select_output_dir.Text = LanguageSupport.Translate(b_select_output_dir.Text);
            label3.Text = LanguageSupport.Translate(label3.Text);
            b_select_template.Text = LanguageSupport.Translate(b_select_template.Text);
            label9.Text = LanguageSupport.Translate(label9.Text);
            cb_ucs_transition.Text = LanguageSupport.Translate(cb_ucs_transition.Text);
            cb_combine_setups.Text = LanguageSupport.Translate(cb_combine_setups.Text);
            groupBox1.Text = LanguageSupport.Translate(groupBox1.Text);
            label5.Text = LanguageSupport.Translate(label5.Text);
            b_select_setup_template.Text = LanguageSupport.Translate(b_select_setup_template.Text);
            cb_export_nc.Text = LanguageSupport.Translate(cb_export_nc.Text);
            cb_export_tools.Text = LanguageSupport.Translate(cb_export_tools.Text);
            btn_toolOptions.Text = LanguageSupport.Translate(btn_toolOptions.Text);
            label2.Text = LanguageSupport.Translate(label2.Text);
            btn_UCSs.Text = LanguageSupport.Translate(btn_UCSs.Text);
            label8.Text = LanguageSupport.Translate(label8.Text);
            btn_StockDesign.Text = LanguageSupport.Translate(btn_StockDesign.Text);
            label10.Text = LanguageSupport.Translate(label10.Text);
            btn_Fixtures.Text = LanguageSupport.Translate(btn_Fixtures.Text);
            label6.Text = LanguageSupport.Translate(label6.Text);
            btn_WorkOffsets.Text = LanguageSupport.Translate(btn_WorkOffsets.Text);
            btn_MachineTurretInfo.Text = LanguageSupport.Translate(btn_MachineTurretInfo.Text);
            btn_ExportAndLoadVC.Text = LanguageSupport.Translate(btn_ExportAndLoadVC.Text);
            btn_cancel.Text = LanguageSupport.Translate(btn_cancel.Text);
            btn_help.Text = LanguageSupport.Translate(btn_help.Text);
            fileToolStripMenuItem.Text = LanguageSupport.Translate(fileToolStripMenuItem.Text);
            exportToolStripMenuItem.Text = LanguageSupport.Translate(exportToolStripMenuItem.Text);
            exportAndOpenInVericutToolStripMenuItem.Text = LanguageSupport.Translate(exportAndOpenInVericutToolStripMenuItem.Text);
            exitToolStripMenuItem.Text = LanguageSupport.Translate(exitToolStripMenuItem.Text);
            optionsToolStripMenuItem.Text = LanguageSupport.Translate(optionsToolStripMenuItem.Text);
            vericutToolStripMenuItem.Text = LanguageSupport.Translate(vericutToolStripMenuItem.Text);
            toolToolStripMenuItem.Text = LanguageSupport.Translate(toolToolStripMenuItem.Text);
            saveSettingsToolStripMenuItem.Text = LanguageSupport.Translate(saveSettingsToolStripMenuItem.Text);
            helpToolStripMenuItem.Text = LanguageSupport.Translate(helpToolStripMenuItem.Text);
            helpToolStripMenuItem1.Text = LanguageSupport.Translate(helpToolStripMenuItem1.Text);

            try
            {
                SetDefaultParams();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                MessageBox.Show(Ex.StackTrace);
            }
        }

        public void SetDefaultParams()
        {
            if (Variables.doc == null) return;


            tb_output_dir.Text = Variables.doc_options.output_dirpath;

            tb_proj_template_fpath.Text = Variables.doc_options.vc_template_proj_fpath;
            cb_combine_setups.Checked = (Variables.doc_options.combine_setups == 1);

            /* Set details for each setup */
            cb_setups.Items.Clear();
            foreach (SetupInfo setup_info in Variables.setups_info)
            {
                if (setup_info.enabled) cb_setups.Items.Add(setup_info.name);
            }
            if (cb_setups.Items.Count > 0) cb_setups.SelectedIndex = 0;
            cb_export_nc.Checked = Variables.setups_info[cb_setups.SelectedIndex].options.is_export_nc;
            cb_export_tools.Checked = Variables.setups_info[cb_setups.SelectedIndex].options.is_export_tools;
          
            cb_ucs_transition.Items.Clear();
            if (Variables.all_ucss != null)
            {
                foreach (UCS ucs in Variables.all_ucss)
                {
                    cb_ucs_transition.Items.Add(ucs.name);
                }
            }
            if (Variables.doc_options.trans_ucs != "" && cb_ucs_transition.Items.Contains(Variables.doc_options.trans_ucs))
                cb_ucs_transition.SelectedItem = Variables.doc_options.trans_ucs;

            SetTurretInfo();
        }


        private void SetExportPartValues()
        {
        }

        private void SetTurretInfo()
        {
            if (!Variables.are_all_setups_milling)
            {

                bool available, temp, b_axis;
                FeatureCAM.tagFMTurretIDType turret_type;

                btn_MachineTurretInfo.Visible = true;
                if (Variables.doc != null)
                {
                    Variables.doc_options.turrets_info = new List<TurretInfo>();
                    FCToVericut.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET1, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    Variables.doc_options.turrets_info.Add(new TurretInfo(turret_type, available, b_axis));
                    FCToVericut.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET2, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    Variables.doc_options.turrets_info.Add(new TurretInfo(turret_type, available, b_axis));
                    FCToVericut.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET3, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    Variables.doc_options.turrets_info.Add(new TurretInfo(turret_type, available, b_axis));
                    FCToVericut.Application.GetTurnTurretInfo(FeatureCAM.tagFMTurretType.eTT_TURRET4, out available, out turret_type, out temp, out temp, out temp, out b_axis);
                    Variables.doc_options.turrets_info.Add(new TurretInfo(turret_type, available, b_axis));
                }
            }
            else
                btn_MachineTurretInfo.Visible = false;
        }

        private void b_export_Click(object sender, EventArgs e)
        {
            GetDataFromFormAndExport();
            FCToVericut.main_form = null;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetDataFromFormAndExport();
            FCToVericut.main_form = null;
        }

        private void GetDataFromFormAndExport()
        {
            Variables.doc_options.is_export_project = (tb_proj_template_fpath.Text != "");

            if (Variables.doc_options.output_dirpath == "")
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("Output directory path must be set. Please fix it and try again."));
                return;
            }
            else
            {
                Variables.doc_options.output_dirpath = tb_output_dir.Text;
            }
            if (Variables.fname_no_ext == "")
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("Output file name must be set. Please fix it and try again."));
                return;
            }
            if (Variables.doc_options.is_export_project && Variables.doc_options.vc_template_proj_fpath == "")
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("If exporting VERICUT project file, template file path must be set. Please fix it and try again."));
                return;
            }
            if (Variables.doc_options.is_export_project && !File.Exists(Variables.doc_options.vc_template_proj_fpath))
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again."), Variables.doc_options.vc_template_proj_fpath);
                return;
            }

			FeatureCAM.FMSetup world = Variables.doc.Setups.Item("STOCK");
            if (Variables.setups_info[cb_setups.SelectedIndex].attach_ucs == null ||
                Variables.setups_info[cb_setups.SelectedIndex].attach_ucs == "")
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again."));
                return;
            }
            for (int si = 0; si < Variables.setups_info.Count; si++)
            {
                if (!Variables.setups_info[si].enabled) continue;
                /* Main spindle */
                FeatureCAM.FMUcs ucs = Variables.doc.UCSs.Item(Variables.setups_info[si].attach_ucs);
                Variables.setups_info[si].options.ucs_attach = ucs.Name;
                if (ucs != null)
                {
                    FeatureCAM.FMSetup temp_setup = Variables.doc.AddSetup(ucs.Name + "_temp_setup", FeatureCAM.tagFMSetupType.eST_Milling, null, ucs.Name, Type.Missing);
                    temp_setup.Enabled = false;
                    Variables.all_ucss[0].ComputeCoordinatesInRelationToSetup(temp_setup,
                                                            out Variables.setups_info[si].setup_solid_x, out Variables.setups_info[si].setup_solid_y, out Variables.setups_info[si].setup_solid_z,
                                                            out Variables.setups_info[si].setup_solid_i, out Variables.setups_info[si].setup_solid_j, out Variables.setups_info[si].setup_solid_k);
                    FeatureCAM.FMUcs temp_setup_ucs = temp_setup.ucs;
                    temp_setup.Delete();
                    temp_setup_ucs.Delete();
                }
                if (world != null && ucs != null)
                {
                    double ucs_x, ucs_y, ucs_z;
                    ucs.GetLocation(out ucs_x, out ucs_y, out ucs_z);
                    world.MapWorldToSetup(ref ucs_x, ref ucs_y, ref ucs_z);
                }
                /* Subspindle */
                if (!String.IsNullOrEmpty(Variables.setups_info[si].attach_ucs_subspindle))
                {
                    ucs = Variables.doc.UCSs.Item(Variables.setups_info[si].attach_ucs_subspindle);
                    Variables.setups_info[si].options.ucs_attach_subspindle = ucs.Name;
                    if (ucs != null)
                    {
                        FeatureCAM.FMSetup temp_setup = Variables.doc.AddSetup(ucs.Name + "_temp_setup_sub", FeatureCAM.tagFMSetupType.eST_Milling, null, ucs.Name, Type.Missing);
                        temp_setup.Enabled = false;
                        Variables.all_ucss[0].ComputeCoordinatesInRelationToSetup(temp_setup,
                                                                out Variables.setups_info[si].sub_setup_solid_x, out Variables.setups_info[si].sub_setup_solid_y, out Variables.setups_info[si].sub_setup_solid_z,
                                                                out Variables.setups_info[si].sub_setup_solid_i, out Variables.setups_info[si].sub_setup_solid_j, out Variables.setups_info[si].sub_setup_solid_k);
                        FeatureCAM.FMUcs temp_setup_ucs = temp_setup.ucs;
                        temp_setup.Delete();
                        temp_setup_ucs.Delete();
                    }
                }
            }
            Close();
            Exporter.Export();
        }

        static public void SaveOptions(ProjectOptions options)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProjectOptions));
            TextWriter textWriter = new StreamWriter(Path.Combine(Variables.doc_options.doc_fpath, "options.fvproj"));
            serializer.Serialize(textWriter, options);
            textWriter.Close();
        }

        static public void ReadOptions(ProjectOptions options)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProjectOptions));
            TextWriter textWriter = new StreamWriter(Path.Combine(Variables.doc_options.doc_fpath, "options.fvproj"));
            serializer.Serialize(textWriter, options);
            textWriter.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            FCToVericut.main_form = null;
        }


        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            FCToVericut.main_form = null;
            return;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            FCToVericut.main_form = null;
            return;
        }


        private void cb_export_nc_CheckedChanged(object sender, EventArgs e)
        {
            Variables.setups_info[this.cb_setups.SelectedIndex].options.is_export_nc = cb_export_nc.Checked;
        }

        private void cb_export_tools_CheckedChanged(object sender, EventArgs e)
        {
            Variables.setups_info[this.cb_setups.SelectedIndex].options.is_export_tools = cb_export_tools.Checked;
        }

        private void b_select_output_dir_Click(object sender, EventArgs e)
        {
            outputDirBrowserDialog1.Description = LanguageSupport.Translate("Select output directory");
            outputDirBrowserDialog1.SelectedPath = Variables.doc_options.output_dirpath;
            if (outputDirBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_output_dir.Text = outputDirBrowserDialog1.SelectedPath;
                Variables.doc_options.output_dirpath = tb_output_dir.Text;
            }
        }

        private void b_select_template_Click(object sender, EventArgs e)
        {
            openProjectFileDialog1.Filter = LanguageSupport.Translate("VCPROJECT file (*.vcproject)|*.vcproject");
            openProjectFileDialog1.FileName = "";
            openProjectFileDialog1.CheckFileExists = true;
            openProjectFileDialog1.Title = LanguageSupport.Translate("Select template VERICUT project");
            if (openProjectFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_proj_template_fpath.Text = openProjectFileDialog1.FileName;
                Variables.doc_options.vc_template_proj_fpath = tb_proj_template_fpath.Text;
                Variables.doc_options.vc_template_proj_fpath = tb_proj_template_fpath.Text;
                Variables.doc_options.is_export_project = (tb_proj_template_fpath.Text != "");
                if (cb_setups.SelectedIndex >= 0)
                    if (this.tb_setup_template_fpath.Text == "" && this.tb_proj_template_fpath.Text != "")
                    {
                        Variables.setups_info[cb_setups.SelectedIndex].attach_components = FindAllAttachComponents(Path.GetFullPath(this.tb_proj_template_fpath.Text));
                        Variables.setups_info[cb_setups.SelectedIndex].attach_components.Sort();
                        Variables.setups_info[cb_setups.SelectedIndex].subsystems = VericutMachineReader.GetMachineSubsystems(Path.GetFullPath(this.tb_proj_template_fpath.Text));
                    }
            }
        }

        private void b_help_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, Path.Combine(FCToVericut.Application.path, "Help\\EZFM.chm"), "129387.htm");
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, Path.Combine(FCToVericut.Application.path, "Help\\EZFM.chm"), "129387.htm");
        }


        private void tb_output_dir_TextChanged(object sender, EventArgs e)
        {
            Variables.doc_options.output_dirpath = tb_output_dir.Text;
        }

        private void tb_proj_template_fpath_TextChanged(object sender, EventArgs e)
        {
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
                            MessageDisplay.ShowError(
                                LanguageSupport.Translate("More than one solid is selected in the part. Please select only one solid and try again."));
                            return;
                        }
                    }
                }
        }

        private void cb_setups_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupInfo selected_setup = Variables.setups_info[cb_setups.SelectedIndex];
            this.cb_export_nc.Checked = selected_setup.options.is_export_nc;
            this.cb_export_tools.Checked = selected_setup.options.is_export_tools;
            this.tb_setup_template_fpath.Text = selected_setup.options.template_fpath;
            if (this.tb_setup_template_fpath.Text != "")
            {
                Variables.setups_info[cb_setups.SelectedIndex].attach_components = FindAllAttachComponents(Path.GetFullPath(this.tb_setup_template_fpath.Text));
                Variables.setups_info[cb_setups.SelectedIndex].attach_components.Sort();
                Variables.setups_info[cb_setups.SelectedIndex].subsystems = VericutMachineReader.GetMachineSubsystems(Path.GetFullPath(this.tb_setup_template_fpath.Text));
            }
            else if (this.tb_proj_template_fpath.Text != "")
            {
                Variables.setups_info[cb_setups.SelectedIndex].attach_components = FindAllAttachComponents(Path.GetFullPath(this.tb_proj_template_fpath.Text));
                Variables.setups_info[cb_setups.SelectedIndex].attach_components.Sort();
                Variables.setups_info[cb_setups.SelectedIndex].subsystems = VericutMachineReader.GetMachineSubsystems(Path.GetFullPath(this.tb_proj_template_fpath.Text));
            }

        }

        private void b_select_setup_template_Click(object sender, EventArgs e)
        {
            openSetupTemplateFileDialog1.Filter = LanguageSupport.Translate("VCPROJECT file (*.vcproject)|*.vcproject");
            openSetupTemplateFileDialog1.FileName = "";
            openSetupTemplateFileDialog1.CheckFileExists = true;
            openSetupTemplateFileDialog1.Title = LanguageSupport.Translate("Select template VERICUT project for the setup");
            if (openSetupTemplateFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_setup_template_fpath.Text = openSetupTemplateFileDialog1.FileName;
                Variables.setups_info[cb_setups.SelectedIndex].options.template_fpath = tb_setup_template_fpath.Text;
            }

        }

        private void tb_setup_template_TextChanged(object sender, EventArgs e)
        {
        }

        private void btn_AddOffset_Click(object sender, EventArgs e)
        {
            using (WorkOffsetDlg work_offset_form = new WorkOffsetDlg(cb_setups.SelectedIndex))
            {
                DialogResult res = work_offset_form.ShowDialog(this);
                work_offset_form.TopLevel = true;
                work_offset_form.TopMost = true;
            }
        }

        private void btn_MachineTurretInfo_Click(object sender, EventArgs e)
        {
            using (MachineTurretsDlg machine_info_form = new MachineTurretsDlg(Variables.doc_options.turrets_info, Variables.setups_info[cb_setups.SelectedIndex].subsystems))
            {
                DialogResult res = machine_info_form.ShowDialog(this);
                machine_info_form.TopLevel = true;
                machine_info_form.TopMost = true;
                if (machine_info_form.updated_turrets != null)
                    Variables.doc_options.turrets_info = machine_info_form.updated_turrets;
            }

        }
        
        private List<string> FindAllAttachComponents(string template_fpath)
        {
            string fcontent;
            string str_to_add;
            List<string> components = new List<string>();
            MatchCollection allMatches;
            Regex expr;

            try
            {
                fcontent = File.ReadAllText(template_fpath);
                expr = new Regex(@"<Component Name="".*?""");
                allMatches = expr.Matches(fcontent);
                foreach (Match match in allMatches)
                {
                    str_to_add = match.Value.Replace("<Component Name=\"", "").Replace("\"", "").Trim();
                    if (!components.Contains(str_to_add))
                        components.Add(str_to_add);
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "FindAllAttachComponents");
            }
            finally
            {
                fcontent = "";
                allMatches = null;
                expr = null;
            }
            return components;
        }

        private List<string> FindAllSubsystems(string template_fpath)
        {
            string fcontent;
            string str_to_add;
            List<string> subsystems = new List<string>();
            MatchCollection allMatches;
            Regex expr;

            try
            {
                fcontent = File.ReadAllText(template_fpath);
                expr = new Regex(@"Subsystem="".*?""");
                allMatches = expr.Matches(fcontent);
                foreach (Match match in allMatches)
                {
                    str_to_add = match.Value.Replace("Subsystem=\"", "").Replace("\"", "").Trim();
                    if (!subsystems.Contains(str_to_add))
                        subsystems.Add(str_to_add);
                }
                subsystems.Sort();
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "FindAllSubsystems");
            }
            finally
            {
                fcontent = "";
                allMatches = null;
                expr = null;
            }
            return subsystems;

        }

        private void btn_ExportAndLoadVC_Click(object sender, EventArgs e)
        {
            Exporter.SaveOptions(Variables.doc_options, "");
            b_export_Click(sender, e);
            FCToVericut.main_form = null;
            StartVericut();
        }

        private void exportAndOpenInVericutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exporter.SaveOptions(Variables.doc_options, "");
            b_export_Click(sender, e);
            FCToVericut.main_form = null;
            StartVericut();
        }


        private void StartVericut()
        {
            try
            {
                if (!File.Exists(Variables.vericut_fpath))
                {
                    MessageDisplay.ShowError(
                        LanguageSupport.Translate("Vericut path {0} is invalid. The file doesn't exist."), Variables.vericut_fpath);
                    return;
                }
                if (!File.Exists(Variables.vcproj_fpath))
                {
                    MessageDisplay.ShowError(
                        LanguageSupport.Translate("Vericut project {0} doesn't exist. Can't open it in VERICUT."), Variables.vcproj_fpath);
                    return;
                }

                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = Variables.vericut_fpath;
                    proc.StartInfo.Arguments = String.Format("\"{0}\"", Variables.vcproj_fpath);
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.Start();
                    proc.WaitForExit(0);
                    if (proc != null)
                        proc.Dispose();
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "StartVericut");
            }

        }

        private void cb_combine_setups_CheckedChanged(object sender, EventArgs e)
        {
            Variables.doc_options.combine_setups = (cb_combine_setups.Checked ? 1 : 0);
            Init.InitializeVariables();
            SetDefaultParams();
            this.Refresh();
        }

        private void cb_ucs_transition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Variables.doc_options.trans_ucs = cb_ucs_transition.SelectedItem.ToString();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exporter.SaveOptions(Variables.doc_options, "");
        }



        private void toolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ToolOptionsDlg tool_options = new ToolOptionsDlg(
                                                        Variables.doc_options.tool_id_option, 
                                                        Variables.doc_options.tool_turret_id_prefix,
                                                        Variables.are_all_setups_milling))
            {
                DialogResult res = tool_options.ShowDialog(this);
                tool_options.TopLevel = true;
                tool_options.TopMost = true;

            }
        }

        private void btn_StockDesign_Click(object sender, EventArgs e)
        {
            if (Variables.are_all_setups_milling)
            {
                using (StockDesignDlg stock_design_options = new StockDesignDlg(cb_setups.SelectedIndex))
                {
                    DialogResult res = stock_design_options.ShowDialog(this);
                    stock_design_options.TopLevel = true;
                    stock_design_options.TopMost = true;
                }
            }
            else
            {
                using (StockDesignDlg_TurnMill stock_design_options = new StockDesignDlg_TurnMill(cb_setups.SelectedIndex))
                {
                    DialogResult res = stock_design_options.ShowDialog(this);
                    stock_design_options.TopLevel = true;
                    stock_design_options.TopMost = true;
                }
            }
        }

        private void btn_Fixtures_Click(object sender, EventArgs e)
        {
            if (Variables.are_all_setups_milling)
            {
                using (ClampsDlg fixture_options = new ClampsDlg(cb_setups.SelectedIndex))
                {
                    DialogResult res = fixture_options.ShowDialog(this);
                    fixture_options.TopLevel = true;
                    fixture_options.TopMost = true;
                }
            }
            else
            {
                using (ClampsDlg_TurnMill fixture_options = new ClampsDlg_TurnMill(cb_setups.SelectedIndex))
                {
                    DialogResult res = fixture_options.ShowDialog(this);
                    fixture_options.TopLevel = true;
                    fixture_options.TopMost = true;
                }
            }
        }

        private void btn_toolOptions_Click(object sender, EventArgs e)
        {
            using (ToolOptionsDlg tool_options = new ToolOptionsDlg(
                                                        Variables.doc_options.tool_id_option,
                                                        Variables.doc_options.tool_turret_id_prefix,
                                                        Variables.are_all_setups_milling))
            {
                DialogResult res = tool_options.ShowDialog(this);
                tool_options.TopLevel = true;
                tool_options.TopMost = true;

            }
        }

        private void btn_UCSs_Click(object sender, EventArgs e)
        {
            if (Variables.are_all_setups_milling)
            {
                using (UCSsDlg fixture_options = new UCSsDlg(cb_setups.SelectedIndex))
                {
                    DialogResult res = fixture_options.ShowDialog(this);
                    fixture_options.TopLevel = true;
                    fixture_options.TopMost = true;
                }
            }
            else
            {
                using (UCSsDlg_TurnMill fixture_options = new UCSsDlg_TurnMill(cb_setups.SelectedIndex))
                {
                    DialogResult res = fixture_options.ShowDialog(this);
                    fixture_options.TopLevel = true;
                    fixture_options.TopMost = true;
                }
            }
        }

        private void vericutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (VericutOptionsDlg vericut_options = new VericutOptionsDlg())
            {
                DialogResult res = vericut_options.ShowDialog(this);
                vericut_options.TopLevel = true;
                vericut_options.TopMost = true;
            }
        }


    }
}
