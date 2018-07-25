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
using System.Text;
using FeatureCAM;
using Microsoft.Win32;
using System.Diagnostics;


namespace FeatureCAMToCAMplete
{
    public class FCToCAMplete
    {
        static public UI main_form = null;

        static public FeatureCAM.Application Application
        {
            get { return fc; }
            set { fc = value; }
        }
        static private FeatureCAM.Application fc;

        public FCToCAMplete() { }

        public static void OnConnect(object obj, tagFMAddInFlags flags)
        {
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                           @"FeatureCAMToCAMplete\Icons");
            //This is the path for debugging
            //path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            //                           @"Icons");
            fc.CommandBars.CreateCustomButton("Utilities", "FeatureCAMToCAMplete",
                                              Path.Combine(path, "icon_16.bmp"), Path.Combine(path, "icon_24.bmp"));

            fc.CommandBars.CreateCustomButton("Utilities", "FeatureCAMToCAMplete",
                                              Path.Combine(path, "icon_16.bmp"), Path.Combine(path, "icon_24.bmp"));
            if (flags == tagFMAddInFlags.eAIF_ConnectUserLoad)
            {
            }
        }

        public static void OnDisConnect(tagFMAddInFlags flags)
        {
            fc.CommandBars.DeleteButton("Utilities", "FeatureCAMToCAMplete");
            if (flags == tagFMAddInFlags.eAIF_DisConnectUserUnLoad)
            {
            }
        }

		public static void FeatureCAMToCAMplete()
        {
            Variables.is_export_project = true;
            Variables.output_dirpath = "";

            FeatureCAM.FMDocument doc;
            doc = (FeatureCAM.FMDocument)fc.ActiveDocument;
            InitializeVariables();

            if (Variables.doc == null)
            {
                MessageBox.Show("No files are open", Variables.prog_name);
                return;
            }

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
                    }
                }
                Variables.orig_single_stock = Variables.stock.SingleProgramWithProgramStop;

				Variables.clamps = new List<SolidInfo>();
				foreach (FeatureCAM.FMSolid solid in Variables.doc.Solids)
					Variables.clamps.Add(new SolidInfo(solid, solid.UseAsClamp));

					if (Variables.stock.IndexType != FeatureCAM.tagFMIndexType.eIT_None)
					Variables.output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName);
				else
					Variables.output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName) + "_" + Variables.setup_names[Variables.selected_setup_id];

				Variables.doc.ActiveSetup.GetMachineSimLocation(out Variables.offset_x, out Variables.offset_y, out Variables.offset_z);

                Variables.prev_doc_name = Variables.doc.Name;
            }
        }

        public static void Convert()
        {
            SetupInfo setup_info;
            string tool_info = "";
            bool all_setups_milling;

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
                    MessageBox.Show("No files are open", Variables.prog_name);
                    return;
                }

                all_setups_milling = true;
                foreach (FeatureCAM.FMSetup setup in Variables.doc.Setups)
                {
                    if (setup.Type != FeatureCAM.tagFMSetupType.eST_Milling)
                        all_setups_milling = false;
                }
                if (!all_setups_milling)
                {
                    MessageBox.Show("The addin doesn't support non-Milling setups yet. Cannot continue since your part has non-Milling setups.", Variables.prog_name);
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
                                MessageBox.Show("Program failed to generate setup information.\n" + Variables.output_msg, Variables.prog_name);
                        }
                    }
                }

                if (!SaveNCCode())
                {
                    MessageBox.Show("SaveNCCode returned false");
                    return;
                }

                tool_info = ToolsToXmlFile();
                File.WriteAllText(Path.Combine(Variables.output_dirpath, "tools.tdb"), tool_info);

                ExportStock();
                ExportPartSolid();
                ExportClamps();

                CreateCAMpleteProject();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "From Convert");
            }
            finally
            {
                Variables.stock.SingleProgramWithProgramStop = Variables.orig_single_stock;
                Variables.Cleanup();
            }
        }

        private static void CreateCAMpleteProject()
        {
            string fpath = Path.Combine(Variables.output_dirpath,
                                        Path.GetFileNameWithoutExtension(Variables.doc.Name) + ".proj");

            StringBuilder fcontent = new StringBuilder();

            string units = (Variables.doc.Metric ? "MM" : "INCHES");

            fcontent.AppendLine("<PROJECTCONFIG>");
            fcontent.AppendLine(Lib.tab + "<SOURCE>");
            fcontent.AppendLine(Lib.double_tab + "<CAMSYSTEM>FeatureCAM</CAMSYSTEM>");
            fcontent.AppendFormat(Lib.double_tab + "<VERSION>{0}</VERSION>\n", fc.Version);
            fcontent.AppendLine(Lib.tab + "</SOURCE>");
            fcontent.AppendFormat(Lib.tab + "<NAME>{0}</NAME>\n", Path.GetFileNameWithoutExtension(Variables.doc.Name));
            fcontent.AppendLine(Lib.tab + "<TOOLING>");
            fcontent.AppendFormat(Lib.double_tab + "<TOOLLIBRARY LOADER=\"FEATURECAM_XML_TOOLING\">{0}</TOOLLIBRARY>\n", ".\\tools.tdb");
            fcontent.AppendLine(Lib.tab + "</TOOLING>");
            fcontent.AppendLine(Lib.tab + "<TOOLPATHS>");
            if (!Variables.is_single_program && Variables.stock.IndexType != tagFMIndexType.eIT_None)
            {
                for (int i = 0; i < Variables.setups_info.Count; i++)
                {
                    if (Variables.setups_info[i].enabled && Variables.setups_info[i].num_features > 0)
                    {
                        fcontent.AppendFormat(Lib.double_tab + "<TOOLPATH LOADER=\"FEATURECAM_ACL\" UNITS=\"{0}\">{1}</TOOLPATH>\n", units, Variables.setups_info[i].nc_fpath.Replace(Variables.output_dirpath, "."));
                    }
                }
            }
            else
            {
                fcontent.AppendFormat(Lib.double_tab + "<TOOLPATH LOADER=\"FEATURECAM_ACL\" UNITS=\"{0}\">{1}</TOOLPATH>\n", units, Variables.setups_info[0].nc_fpath.Replace(Variables.output_dirpath, "."));
            }
            fcontent.AppendLine(Lib.tab + "</TOOLPATHS>");
            fcontent.AppendFormat(Lib.tab + "<OFFSETS>\n");
            fcontent.AppendFormat(Lib.double_tab + "<OFFSET TYPE=\"GCODETOPALLETSHIFT\" X=\"{0}\" Y=\"{1}\" Z=\"{2}\" UNITS=\"INCH\"></OFFSET>\n",
                                Variables.offset_x, Variables.offset_y, Variables.offset_z, units);
            fcontent.AppendFormat(Lib.tab + "</OFFSETS>\n");
            fcontent.AppendLine(Lib.tab + "<PARTINFO>");
            fcontent.AppendFormat(Lib.double_tab + "<STOCK LOADER=\"GENERIC_STL\" UNITS=\"{0}\">{1}</STOCK>\n",
                                  units, Variables.stock_fpath.Replace(Variables.output_dirpath, "."));
            if (Variables.clamp_fpaths != null)
                if (Variables.clamp_fpaths.Count > 0)
                {
                    foreach (string clamp_fpath in Variables.clamp_fpaths)
                        fcontent.AppendFormat(Lib.double_tab + "<FIXTURE LOADER=\"GENERIC_STL\" UNITS=\"{0}\">{1}</FIXTURE>\n",
                                              units, clamp_fpath.Replace(Variables.output_dirpath, "."));
                }
            if (Variables.is_export_part)
            {
                if (!String.IsNullOrEmpty(Variables.part_fpath))

                    fcontent.AppendFormat(Lib.double_tab + "<TARGETMODEL LOADER=\"GENERIC_STL\" UNITS=\"{0}\">{1}</TARGETMODEL>\n",
                                      units, Variables.part_fpath.Replace(Variables.output_dirpath, "."));
            }
            fcontent.AppendLine(Lib.tab + "</PARTINFO>");
            fcontent.AppendLine("</PROJECTCONFIG>");

            File.WriteAllText(fpath, fcontent.ToString());
        }

        private static void ExportPartSolid()
        {
            string err_str;

            if (!Variables.is_export_part_now) return;

            FeatureCAM.FMSolid solid = (FeatureCAM.FMSolid)Variables.doc.Solids.Item(Variables.part_solid_name);
            if (solid != null)
            {
                Variables.part_fpath = Path.Combine(Variables.output_dirpath, solid.Name + "_part.stl");
                solid.ExportToSTL(Variables.part_fpath, out err_str);
                if (err_str == "" || err_str == null)
                    Variables.output_msg += Variables.part_fpath + "\n";
                else
                    MessageBox.Show("Error occured while exporting part solid to .stl file: \n'" + err_str + "'", Variables.prog_name);
            }
        }

        private static void ExportStock()
        {
            string err_msg = "";

            try
            {
                Variables.stock_fpath = Path.Combine(Variables.output_dirpath, "stock.stl");

                Variables.stock.ExportToSTL(Variables.stock_fpath, out err_msg);
                if (String.IsNullOrEmpty(err_msg))
                    Variables.output_msg += Variables.stock_fpath + "\n";
                else
                    MessageBox.Show("Error occurred while exporting stock to .stl file: \n'" + err_msg + "'", Variables.prog_name);

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Exception occurred: " + Ex.Message, Variables.prog_name);
            }
        }

        private static void ExportClamps()
        {
            string err_msg = "";
            string fpath;
            try
            {
                Variables.clamp_fpaths = null;
                if (Variables.clamps == null)
                {
                    return;
                }
                if (Variables.clamps.Count == 0)
                {
                    return;
                }
                foreach (SolidInfo clamp in Variables.clamps)
                {
                    if (clamp.is_export)
                    {
                        fpath = Path.Combine(Variables.output_dirpath, clamp.solid.Name + "_clamp.stl");
                        clamp.solid.ExportToSTL(fpath, out err_msg);
                        if (String.IsNullOrEmpty(err_msg))
                            Variables.output_msg += Variables.stock_fpath + "\n";
                        else
                            MessageBox.Show("Error occurred while exporting stock to .stl file: \n'" + err_msg + "'", Variables.prog_name);
                        if (Variables.clamp_fpaths == null) Variables.clamp_fpaths = new List<string>();
                        Variables.clamp_fpaths.Add(fpath);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Exception occurred: " + Ex.Message, Variables.prog_name);
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
                if (!String.IsNullOrEmpty(op.Errors.Trim())) is_op_error = true;

            if (is_op_error)
            {
                MessageBox.Show("Cannot export data to CAMplete: there are errors in the document and nc code cannot be generated.", Variables.prog_name);
                return false;
            }

            /* Set correct output units */
            fc.PostOptionsMill.SetIsInchOutputUnits(!Variables.doc.Metric);
            fc.PostOptionsTurn.SetIsInchOutputUnits(!Variables.doc.Metric);
            fc.PostOptionsWire.SetIsInchOutputUnits(!Variables.doc.Metric);

            /* If part is non-indexed, we can only generate CAMplete report for the active setup */
            if (Variables.stock.IndexType == tagFMIndexType.eIT_None)
                Variables.doc.SaveNC("nc_program.acl", Variables.output_dirpath, false,
                                    FeatureCAM.tagFMSaveNCFileType.eNCFT_NCCode, false, out err_msg,
                                    out nc_files_num, out nc_file_names, out doc_files_num, out doc_file_names,
                                    out macro_files_num, out macro_file_names);
            else if (Variables.stock.SingleProgram) /* We'll have NC code for one file */
                Variables.doc.SaveNC("nc_program.acl", Variables.output_dirpath, false,
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
                Variables.doc.SaveNC("nc_program.acl", Variables.output_dirpath, false,
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

        private static string ToolsToXmlFile()
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
                    foreach (FeatureCAM.FMOperation op in feat.Operations)
                        setup_tool_list += op.Tool.Name + ";";
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
                        if (setup_tools[si - 1].IndexOf(toolmap.Tool.Name + ";") >= 0)
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
                    tool_info +=
                        Tool.ToString(toolmap, partline_features) + Environment.NewLine;
                }
            }

            if (!String.IsNullOrEmpty(Variables.unsupported_tool_names))
                MessageBox.Show("Warning: Tools info was exported, but information for following tool(s) " +
                                Variables.unsupported_tool_names +
                                " was not exported completely, because the tool group(s) are unsupported by this addin.", Variables.prog_name);

            return "<TOOLDB VER=\"2\">" + Lib.EOL +
                        tool_info + //already had end of line at the end
                    "</TOOLDB>";
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
                    if (System.Convert.ToBoolean(attr_val) == false)
                    {
                        if (partline_features == null) partline_features = new List<string>();
                        partline_features.Add(feat.Name);
                        if (!String.IsNullOrEmpty(pattern_from))
                            partline_features.Add(pattern_from);
                    }
                }
            }
            return partline_features;
        }


    }
}