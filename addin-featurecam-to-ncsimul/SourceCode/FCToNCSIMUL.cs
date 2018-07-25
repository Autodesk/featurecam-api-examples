// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using FeatureCAM;
using Microsoft.Win32;
using System.Diagnostics;


namespace FeatureCAMToNCSIMUL
{
    public class FCToNCSIMUL
    {
        static public FeatureCAM.Application Application
        {
            get { return fc; }
            set { fc = value; }
        }
        static private FeatureCAM.Application fc;
        static public UI main_form = null;

        public FCToNCSIMUL() { }

        public static void OnConnect(object obj, tagFMAddInFlags flags)
        {
            //This is the path for version in Addins folder
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                                       @"FeatureCAMToNCSIMUL\Icons");
            //This is the path for debugging
            //path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            //                           @"Icons");
            fc.CommandBars.CreateCustomButton("Utilities", "FeatureCAMToNCSIMUL", 
                                              Path.Combine(path, "icon_16.bmp"), Path.Combine(path, "icon_24.bmp"));
            if (flags == tagFMAddInFlags.eAIF_ConnectUserLoad)
            {
            }
        }

        public static void OnDisConnect(tagFMAddInFlags flags)
        {
            fc.CommandBars.DeleteButton("Utilities", "FeatureCAMToNCSIMUL");
            if (flags == tagFMAddInFlags.eAIF_DisConnectUserUnLoad)
            {
            }
        }

        [STAThread]
        public static void Main()
        {
            FeatureCAMToNCSIMUL();
        }

        static public void FeatureCAMToNCSIMUL()
        {
            FeatureCAM.FMDocument doc;

            LanguageSupport.InitializeTranslation(fc.CurrentLanguage);

            doc = (FeatureCAM.FMDocument)fc.ActiveDocument;

            InitializeVariables();
            if (Variables.ncsimul_path == "")
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_no_NCSIMUL), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                return;
            }

            if (Variables.doc == null)
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_no_open_files), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                return;
            }
            Settings.ReadSettingsFromAddinIniFile();

            // helper function to force a single instance of plugin form
            if (main_form != null)
            {
                main_form.BringToFront();
            }
            else
            {
                main_form = new UI();
                main_form.Show();
                main_form.TopLevel = true;
                main_form.TopMost = true;
                System.Windows.Forms.Application.Run(main_form);
            }
        }

        private static void InitializeVariables()
        {
            FeatureCAM.FMSetup setup = null;

            Variables.ncsimul_path = FCToNCSIMUL.GetNCDelcamPath();
            if (Variables.ncsimul_md_files_dir == "")
                Variables.ncsimul_md_files_dir = @"C:\Data\Spring Technologies\NCSimul8\Users\Demo\Machines";

            if (fc != null)
                Variables.doc = (FeatureCAM.FMDocument)fc.ActiveDocument;
            if (Variables.doc == null)
            {
                Variables.prev_doc_name = "";
                Variables.output_dirpath = "";
            }
            else
            {
                Variables.stock = Variables.doc.Stock;
                Variables.setup_names = new List<string>();
                for (int i = 1; i <= Variables.doc.Setups.Count; i++)
                {
                    setup = (FeatureCAM.FMSetup)Variables.doc.Setups.Item(i);
                    if (setup != null)
                    {
                        Variables.setup_names.Add(setup.Name);
                        /* Have to subtract 1 b/c setups are 1-based and combobox values are 0-based */
                        if (Variables.doc.ActiveSetup.Name == setup.Name)
                            Variables.selected_setup_id = i - 1;
                        if (setup.Type != FeatureCAM.tagFMSetupType.eST_Milling)
                            Variables.are_all_setups_milling = false;
                    }
                }
                Variables.file_dirpath = Variables.doc.path;

                Variables.orig_single_stock = Variables.stock.SingleProgramWithProgramStop;

                if (Variables.prev_doc_name != Variables.doc.Name)
                    Variables.doc.ActiveSetup.GetMachineSimLocation(out Variables.offset_x, out Variables.offset_y, out Variables.offset_z);
                Variables.clamps = new List<SolidInfo>();
                foreach (FeatureCAM.FMSolid solid in Variables.doc.Solids)
                    Variables.clamps.Add(new SolidInfo(solid, solid.UseAsClamp));

                Variables.prev_doc_name = Variables.doc.Name;
            }

        }

        public static void Convert()
        {
            SetupInfo setup_info;
            string tool_info = "";
            string ncdelcam_fpath = "";

            try
            {
                Variables.doc = (FeatureCAM.FMDocument)fc.ActiveDocument;

                if (Variables.stock.IndexType == tagFMIndexType.eIT_None)
                {
                    Variables.doc.Setups.Item(Variables.selected_setup_id + 1).Activate();
                    Variables.stock.SingleProgramWithProgramStop = false;
                }
                /* Verify that file is open */
                if (Variables.doc == null)
                {
                    MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_no_open_files), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                    return;
                }

                Variables.is_single_program =
                    (
                        (Variables.stock.IndexType == FeatureCAM.tagFMIndexType.eIT_None &&
                            Variables.stock.SingleProgramWithProgramStop)
                        ||
                        (Variables.stock.IndexType != FeatureCAM.tagFMIndexType.eIT_None &&
                            !Variables.stock.ToolDominant &&
                            Variables.stock.SingleProgram)
                        ||
                        (Variables.stock.IndexType != FeatureCAM.tagFMIndexType.eIT_None &&
                            Variables.stock.ToolDominant)
                    );


                Directory.CreateDirectory(Variables.output_dirpath);

                /* Initialize setup information (set enabled/disabled) */
                setup_info = null;
                foreach (FeatureCAM.FMSetup setup in Variables.doc.Setups)
                {
                    if (!Variables.is_single_program)
                    {
                        setup_info = new SetupInfo(setup);
                        if (Variables.setups_info == null) Variables.setups_info = new List<SetupInfo>();
                        Variables.setups_info.Add(setup_info);
                        if (setup_info.enabled && setup_info.num_features > 0)
                            Variables.num_enabled_setups++;
                    }
                    else
                    {
                        if (setup_info == null)
                        {
                            setup_info = new SetupInfo(setup);
                            setup_info.name = Variables.doc.Name + "_combined_setup";
                            if (Variables.setups_info == null) Variables.setups_info = new List<SetupInfo>();
                            Variables.setups_info.Add(setup_info);
                        }
                        else
                        {
                            if (Variables.setups_info.Count == 1)
                                Variables.setups_info[0].ucss.Add(new UCS(setup.ucs));
                            else
                                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_failed_to_generate_setup_info) + "\n" + Variables.output_msg, LanguageSupport.Translate(Properties.Resources.str_prog_name));
                        }
                    }
                }

                if (!SaveNCCode())
                {
                    MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_failed_to_save_nc), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                    return;
                }

                tool_info = ToolsToXMLFile();

                StockToSolid();
                ClampsToSolids();

                /* Export projects file */
                Project_Manager.ConstructProjectFile(
                            Path.Combine(Variables.output_dirpath, "export.nxf"),
                            "Milling", Settings.ncsimul_md_fpath,
                            tool_info,
                            "", Variables.setups_info.Count);

                ncdelcam_fpath = GetNCDelcamPath();
                if (ncdelcam_fpath != "")
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.Arguments = "/c call \"" + ncdelcam_fpath + "\"" +
                                               " " +
                                               "\"" + Path.Combine(Variables.output_dirpath, "export.nxf") + "\"";
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.Start();
                }

                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_output_written_to) + "\n" + Variables.output_msg, LanguageSupport.Translate(Properties.Resources.str_prog_name));

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, LanguageSupport.Translate(Properties.Resources.msg_exception_title));
            }
            finally
            {
                Variables.stock.SingleProgramWithProgramStop = Variables.orig_single_stock;
                Variables.Cleanup();
            }
        }

        private static void StockToSolid()
        {
            string err_msg = "";

            try
            {
                Variables.stock_fpath = Path.Combine(Variables.output_dirpath, "stock.stl");

                Variables.stock.ExportToSTL(Variables.stock_fpath, out err_msg);
                if (err_msg == "" || err_msg == null)
                    Variables.output_msg += Variables.stock_fpath + "\n";
                else
                    MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_stock_export_exception) + "\n'" + err_msg + "'", LanguageSupport.Translate(Properties.Resources.msg_exception_title));

            }
            catch (Exception Ex)
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_general_exception) + Ex.Message, LanguageSupport.Translate(Properties.Resources.msg_exception_title));
            }
        }

        private static void ClampsToSolids()
        {
            string err_msg = "";
            string fpath;
            try
            {
                if (Variables.clamps == null)
                {
                    Variables.clamp_fpaths = null;
                    return;
                }
                if (Variables.clamps.Count == 0)
                {
                    Variables.clamp_fpaths = null;
                    return;
                }
                foreach (SolidInfo clamp in Variables.clamps)
                {
                    if (clamp.is_export)
                    {
                        fpath = Path.Combine(Variables.output_dirpath, clamp.solid.Name + "_clamp.stl");
                        clamp.solid.ExportToSTL(fpath, out err_msg);
                        if (err_msg == "" || err_msg == null)
                            Variables.output_msg += fpath + "\n";
                        else
                            MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_clamp_export_exception) + "\n'" + err_msg + "'", LanguageSupport.Translate(Properties.Resources.msg_exception_title));
                        if (Variables.clamp_fpaths == null) Variables.clamp_fpaths = new List<string>();
                        Variables.clamp_fpaths.Add(fpath);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_general_exception) + Ex.Message, LanguageSupport.Translate(Properties.Resources.msg_exception_title));
            }
        }

        private static bool SaveNCCode()
        {
            int nc_files_num, doc_files_num, macro_files_num;
            object doc_file_names, macro_file_names, nc_file_names;
            string err_msg;
            bool is_op_error;
            int nc_file_id;

            is_op_error = false;
            foreach (FeatureCAM.FMOperation op in Variables.doc.Operations)
                if (op.Errors.Trim() != "") is_op_error = true;

            if (is_op_error)
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_cannot_export_data), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                return false;
            }

            /* Set correct output units */
            fc.PostOptionsMill.SetIsInchOutputUnits(!Variables.doc.Metric);
            fc.PostOptionsTurn.SetIsInchOutputUnits(!Variables.doc.Metric);
            fc.PostOptionsWire.SetIsInchOutputUnits(!Variables.doc.Metric);

            /* If part is non-indexed, we can only generate NCSIMUL report for the active setup */
            if (Variables.stock.IndexType == tagFMIndexType.eIT_None)
                Variables.doc.SaveNC("nc_program.iso", Variables.output_dirpath, false,
                                    FeatureCAM.tagFMSaveNCFileType.eNCFT_NCCode, false, out err_msg,
                                    out nc_files_num, out nc_file_names, out doc_files_num, out doc_file_names,
                                    out macro_files_num, out macro_file_names);
            else if (Variables.stock.SingleProgram) /* We'll have NC code for one file */
                Variables.doc.SaveNC("nc_program.iso", Variables.output_dirpath, false,
                                    FeatureCAM.tagFMSaveNCFileType.eNCFT_NCCode, false, out err_msg,
                                    out nc_files_num, out nc_file_names, out doc_files_num, out doc_file_names,
                                    out macro_files_num, out macro_file_names);
            else
            {
                foreach (FMSetup setup in Variables.doc.Setups)
                {
                    if (setup.Enabled)
                    {
                        setup.Activate();
                        Variables.doc.SimToolpath(false);
                    }
                }
                Variables.doc.SaveNC("nc_program.iso", Variables.output_dirpath, false,
                    FeatureCAM.tagFMSaveNCFileType.eNCFT_NCCode, false, out err_msg,
                    out nc_files_num, out nc_file_names, out doc_files_num, out doc_file_names,
                    out macro_files_num, out macro_file_names);
            }

            if (!Variables.is_single_program && Variables.stock.IndexType != tagFMIndexType.eIT_None)
            {
                if ((int)nc_files_num == Variables.num_enabled_setups)
                {
                    nc_file_id = 1;
                    for (int i = 0; i < Variables.setups_info.Count; i++)
                    {
                        if (Variables.setups_info[i].enabled && Variables.setups_info[i].num_features > 0)
                        {
                            Variables.setups_info[i].nc_fpath = (string)(((Array)nc_file_names).GetValue(nc_file_id));
                            Variables.output_msg += Variables.setups_info[i].nc_fpath + "\n";
                            nc_file_id++;
                        }
                    }
                }
            }
            else
            {
                if ((int)nc_files_num == 1)
                {
                    for (int i = 0; i < Variables.setups_info.Count; i++)
                        Variables.setups_info[i].nc_fpath = (string)(((Array)nc_file_names).GetValue(1));
                    Variables.output_msg += Variables.setups_info[0].nc_fpath + "\n";
                }
            }
            return true;
        }

        private static string ToolsToXMLFile()
        {
            List<string> setup_tools = null;
            List<string> partline_features = null;
            string tool_info = "";
            int setup_num;
            string setup_tool_list;
            FeatureCAM.FMToolMap2 toolmap;

            setup_num = 0;
            setup_tool_list = "";
            partline_features = GetAllFeaturesUsingPartLineComp();
            foreach (FeatureCAM.FMSetup setup in Variables.doc.Setups)
            {
                if (setup_tools == null) setup_tools = new List<string>();
                foreach (FeatureCAM.FMFeature feat in setup.Features)
                {
                    if (feat.Enabled)
                        foreach (FeatureCAM.FMOperation op in feat.Operations)
                            if (op.Tool != null)
                                setup_tool_list += op.Tool.Name + ";";
                }
                setup_tools.Add(setup_tool_list);
                setup_num++;
            }

            Variables.unsupported_tool_names = "";
            Variables.doc.InvalidateAll();
            /* If we need to create separate tls file for each setup, write tools for each setup to a separate file */
            if (!Variables.is_single_program)
            {
                for (int si = 1; si <= Variables.doc.Setups.Count; si++)
                {
                    tool_info = "";
                    for (int i = 1; i <= Variables.doc.ToolMaps.Count; i++)
                    {
                        toolmap = Variables.doc.ToolMaps.Item(i);
                        if (setup_tools[si - 1].IndexOf(toolmap.Tool.Name + ";") >= 0 &&
                            toolmap.Operations.Count > 0)
                        {
                            tool_info +=
                                 Tool.ToString(toolmap, partline_features) + Environment.NewLine;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i <= Variables.doc.ToolMaps.Count; i++)
                {
                    toolmap = Variables.doc.ToolMaps.Item(i);
                    if (toolmap.Operations.Count > 0)
                        tool_info +=
                            Tool.ToString(toolmap, partline_features) + Environment.NewLine;
                }
            }

            if (Variables.unsupported_tool_names != "")
                MessageBox.Show(String.Format(LanguageSupport.Translate(Properties.Resources.msg_unsupported_tools), Variables.unsupported_tool_names),
                                LanguageSupport.Translate(Properties.Resources.str_prog_name));

            return tool_info;
        }

        private static List<string> GetAllFeaturesUsingPartLineComp()
        {
            List<string> partline_features = null;
            FMLinearPattern lin_ptrn;
            FMRadialPattern rad_ptrn;
            FMPointListPattern ptlist_ptrn;
            FMRectPattern rect_ptrn;
            string pattern_from;
            tagFMFeatureType featureType;
            object attr_val = null;

            foreach (FMFeature feat in Variables.doc.Features)
            {
                pattern_from = "";
                attr_val = null;
                feat.GetFeatureType(out featureType);
                switch (featureType)
                {
                    case tagFMFeatureType.eFT_LinearPattern:
                        lin_ptrn = (FMLinearPattern)feat;
                        attr_val = lin_ptrn.Object.get_Attribute(tagFMAttributeId.eAID_PartLineProgram, null);
                        pattern_from = lin_ptrn.Object.Name;
                        break;
                    case tagFMFeatureType.eFT_RadialPattern:
                        rad_ptrn = (FMRadialPattern)feat;
                        attr_val = rad_ptrn.Object.get_Attribute(tagFMAttributeId.eAID_PartLineProgram, null);
                        pattern_from = rad_ptrn.Object.Name;
                        break;
                    case tagFMFeatureType.eFT_RectPattern:
                        rect_ptrn = (FMRectPattern)feat;
                        attr_val = rect_ptrn.Object.get_Attribute(tagFMAttributeId.eAID_PartLineProgram, null);
                        pattern_from = rect_ptrn.Object.Name;
                        break;
                    case tagFMFeatureType.eFT_PtListPattern:
                        ptlist_ptrn = (FMPointListPattern)feat;
                        attr_val = ptlist_ptrn.Object.get_Attribute(tagFMAttributeId.eAID_PartLineProgram, null);
                        pattern_from = ptlist_ptrn.Object.Name;
                        break;
                    default:
                        attr_val = feat.get_Attribute(tagFMAttributeId.eAID_PartLineProgram, null);
                        break;
                }
                if (attr_val != null)
                {
                    if (System.Convert.ToBoolean(attr_val) == true)
                    {
                        if (partline_features == null) partline_features = new List<string>();
                        partline_features.Add(feat.Name);
                        if (pattern_from != "")
                            partline_features.Add(pattern_from);
                    }
                }
            }
            return partline_features;
        }

        public static string GetNCDelcamPath()
        {
            string ncsimul_path = "";
            string ncdelcam_fpath = "";

            ncdelcam_fpath = GetNCDelcam9Path();
            if (ncdelcam_fpath != "") return ncdelcam_fpath;

            RegistryKey regVersionString = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Spring Technologies");
            if (regVersionString == null) return "";

            string[] subkey_names = regVersionString.GetSubKeyNames();
            foreach (string subkey_name in subkey_names)
            {
                if (subkey_name.Contains("NCSIMUL"))
                    ncsimul_path = Path.Combine(regVersionString.ToString(), subkey_name);
            }
            if (ncsimul_path == "") return "";

            ncdelcam_fpath = (string)Registry.GetValue(ncsimul_path, "REPINTERFACES", 1);
            ncdelcam_fpath = Path.Combine(ncdelcam_fpath, "ncdelcam.exe");
            if (!File.Exists(ncdelcam_fpath))
                ncdelcam_fpath = "";

            return ncdelcam_fpath;
        }

        public static string GetNCDelcam9Path()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Spring Technologies\NCSIMUL Machine", false);
            if (myKey == null) return "";

            String CurrentVersion = (String)myKey.GetValue("CURRENT_VERSION");

            if (CurrentVersion != "")
            {
                RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Spring Technologies\NCSIMUL Machine\" + CurrentVersion, false);
                String Directory = (String)myKey2.GetValue("REPINTERFACES");

                string fpath = Path.Combine(Directory, "ncdelcam.exe");
                if (File.Exists(fpath)) return fpath;
            }

            return "";
        }
    }
}