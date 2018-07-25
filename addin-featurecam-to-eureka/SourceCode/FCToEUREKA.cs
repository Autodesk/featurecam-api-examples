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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Data;
using FeatureCAM;
using Microsoft.Win32;
using System.Diagnostics;


namespace FeatureCAMToEUREKA
{
    public class FCToEUREKA
    {

        static public FeatureCAM.Application Application
        {
            get { return fc; }
            set { fc = value; }
        }

        static private FeatureCAM.Application fc;
        static public UI main_form = null;

        public FCToEUREKA() { }

        public static void OnConnect(object obj, tagFMAddInFlags flags)
        {
            //This is the path for version in Addins folder
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                                       @"FeatureCAMToEUREKA\Icons");
            //This is the path for debugging
            //path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            //                           @"Icons");
            fc.CommandBars.CreateButton("Utilities", "FeatureCAMToEUREKA", tagFMMacroButtonFaceId.eMBFID_RtArrow);
            if (flags == tagFMAddInFlags.eAIF_ConnectUserLoad)
            {
            }
        }

        public static void OnDisConnect(tagFMAddInFlags flags)
        {
            fc.CommandBars.DeleteButton("Utilities", "FeatureCAMToEUREKA");
            if (flags == tagFMAddInFlags.eAIF_DisConnectUserUnLoad)
            {
            }
        }

        [STAThread]
        public static void Main()
        {
            object obj = Marshal.GetActiveObject("FeatureCAM.Application");
            if (obj == null) return;

            fc = (FeatureCAM.Application)obj;

            FeatureCAMToEUREKA();
        }

        static public void FeatureCAMToEUREKA()
        {
            CheckTLBCompatibility();
            if (!IsLicensedProperly()) return;


            Variables.is_export_project = true;
            Variables.output_dirpath = ""; // @"C:\EUREKA\";

            FeatureCAM.FMDocument doc;
            doc = (FeatureCAM.FMDocument)fc.ActiveDocument;

            InitializeVariables();

            if (String.IsNullOrEmpty(Variables.eureka_path)) //Shouldn't run addin if EUREKA isn't installed
            {
                MessageBox.Show(
                    "Failed to find EUREKA on this computer. Cannot continue.",
                    Variables.prog_name,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            UI form = new UI();
            form.Show();
            form.TopLevel = true;
            form.TopMost = true;
            System.Windows.Forms.Application.Run(form);
        }

        private static void CheckTLBCompatibility()
        {
            Version vnum = new Version(1, 0, 0, 0);

            try
            {
                try
                {
                    vnum = new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
                }
                catch { }

                // the featurecam tlb version is a property on the app now adays...
                // if the app doesn't have that propety, the next line will throw a com exception
                if (fc.MajorTLBVersionNum != vnum.Build)
                    MessageBox.Show(
                        String.Format("Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.", vnum.Build.ToString()),
                        Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                // must be tlb version 1?
                MessageBox.Show(
                    String.Format("Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.", vnum.Build.ToString()),
                        Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static bool IsLicensedProperly()
        {
            return true;
        }

        private static void InitializeVariables()
        {
            FeatureCAM.FMSetup setup = null;
            Eureka.Application eureka_app = null;
            string eureka_template_name;

            Variables.eureka_path = FCToEUREKA.GetEUREKAPath();
            if (Variables.eureka_template_files_dir == "")
            {
                eureka_app = FCToEUREKA.StartEUREKA();
                if (eureka_app != null)
                {
                    Variables.eureka_template_files_dir = eureka_app.TemplateDirectory;
                    eureka_app.Quit();
                    eureka_app = null;
                }
            }

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
                if (Variables.stock.IndexType != FeatureCAM.tagFMIndexType.eIT_None)
                    Variables.output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName);
                else
                    Variables.output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName) + "_" + Variables.setup_names[Variables.selected_setup_id];

                Variables.orig_single_stock = Variables.stock.SingleProgramWithProgramStop;
                if (Variables.prev_doc_name != Variables.doc.Name)
                {
                    Variables.clamps = new List<SolidInfo>();
                    foreach (FeatureCAM.FMSolid solid in Variables.doc.Solids)
                        Variables.clamps.Add(new SolidInfo(solid, false));
                }
                Variables.doc.ActiveSetup.GetMachineSimLocation(out Variables.offset_x, out Variables.offset_y, out Variables.offset_z);
            }
        }

        public static Eureka.Application StartEUREKA()
        {
            Eureka.Application eureka_app = null;

            object obj = Activator.CreateInstance(Type.GetTypeFromProgID("Eureka.Application"));
            if (obj == null) return null;

            eureka_app = (Eureka.Application)obj;
            if (eureka_app != null)
            eureka_app.Visible = 3;

            return eureka_app;
 
        }

        public static void Convert()
        {
            SetupInfo setup_info;
            string tool_info = "";
            string eureka_fpath = "";
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
                    MessageBox.Show(
                        "No files are open",
                        Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(
                        "The addin doesn't support non-Milling setups yet. Cannot continue since your part has non-Milling setups.",
                        Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                                MessageBox.Show(
                                    "Program failed to generate setup information.\n" + Variables.output_msg,
                                    Variables.prog_name,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }

                if (!SaveNCCode())
                {
                    MessageBox.Show(
                        "SaveNCCode returned false",
                        Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                tool_info = ToolsToXMLFile();
                File.WriteAllText(Path.Combine(Variables.output_dirpath, "tools.tdb"), tool_info);

                StockToSolid();
                ClampsToSolids();

                /* Export projects file */
                Project_Manager.LoadProjectToEureka(Path.Combine(Variables.output_dirpath, "stock.stl"),
                                                    Path.Combine(Variables.output_dirpath, "tools.tdb"),
                                                    Path.Combine(Variables.output_dirpath, Variables.doc.NameWithoutExtension + ".epf"));

            }
            catch (Exception Ex)
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {

                MessageBox.Show(
                        Ex.Message, Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                })).Start();
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
                    MessageBox.Show(
                        "Error occured while exporting stock to .stl file: \n'" + err_msg + "'",
                        Variables.prog_name,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(
                    "Exception occured: " + Ex.Message,
                    Variables.prog_name,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            Variables.output_msg += Variables.stock_fpath + "\n";
                        else
                            MessageBox.Show(
                                "Error occured while exporting stock to .stl file: \n'" + err_msg + "'",
                                Variables.prog_name,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (Variables.clamp_fpaths == null) Variables.clamp_fpaths = new List<string>();
                        Variables.clamp_fpaths.Add(fpath);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(
                    "Exception occured: " + Ex.Message,
                    Variables.prog_name,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(
                    "Cannot export data to EUREKA: there are errors in the document and nc code cannot be generated.",
                    Variables.prog_name,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            /* Set correct output units */
            fc.PostOptionsMill.SetIsInchOutputUnits(!Variables.doc.Metric);
            fc.PostOptionsTurn.SetIsInchOutputUnits(!Variables.doc.Metric);
            fc.PostOptionsWire.SetIsInchOutputUnits(!Variables.doc.Metric);

            /* If part is non-indexed, we can only generate EUREKA report for the active setup */
            if (Variables.stock.IndexType == tagFMIndexType.eIT_None)
                Variables.doc.SaveNC2(Variables.output_dirpath, null, null, false,
                                    FeatureCAM.tagFMSaveNCFileType.eNCFT_NCCode, false, out err_msg,
                                    out nc_files_num, out nc_file_names, out doc_files_num, out doc_file_names,
                                    out macro_files_num, out macro_file_names);
            else if (Variables.stock.SingleProgram) /* We'll have NC code for one file */
                Variables.doc.SaveNC2(Variables.output_dirpath, null, null, false,
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
                Variables.doc.SaveNC2(Variables.output_dirpath, null, null, false,
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

            if (Variables.unsupported_tool_names != "")
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
                        if (pattern_from != "")
                            partline_features.Add(pattern_from);
                    }
                }
            }
            return partline_features;
        }

        private static void AllToolsToFile_Debug(FeatureCAM.FMDocument doc)
        {
            string all_tools = "";
            List<FeatureCAM.FMBoringBar> bbars = new List<FeatureCAM.FMBoringBar>();
            List<FeatureCAM.FMChamferMill> cmills = new List<FeatureCAM.FMChamferMill>();
            List<FeatureCAM.FMCounterBore> cbores = new List<FeatureCAM.FMCounterBore>();
            List<FeatureCAM.FMCounterSink> csinks = new List<FeatureCAM.FMCounterSink>();
            List<FeatureCAM.FMEndMill> emills = new List<FeatureCAM.FMEndMill>();
            List<FeatureCAM.FMFaceMill> fmills = new List<FeatureCAM.FMFaceMill>();
            List<FeatureCAM.FMPlungeMill> pmills = new List<FeatureCAM.FMPlungeMill>();
            List<FeatureCAM.FMRoundingMill> rmills = new List<FeatureCAM.FMRoundingMill>();
            List<FeatureCAM.FMSideMill> smills = new List<FeatureCAM.FMSideMill>();
            List<FeatureCAM.FMSpotDrill> sdrills = new List<FeatureCAM.FMSpotDrill>();
            List<FeatureCAM.FMTap> taps = new List<FeatureCAM.FMTap>();
            List<FeatureCAM.FMThreadMill> thmills = new List<FeatureCAM.FMThreadMill>();
            List<FeatureCAM.FMTwistDrill> tdrills = new List<FeatureCAM.FMTwistDrill>();

            foreach (FeatureCAM.FMToolCrib crib in doc.ToolCribs)
            {
                foreach (FeatureCAM.FMBoringBar tool in crib.BoringBars)
                    bbars.Add(tool);
                foreach (FeatureCAM.FMChamferMill tool in crib.ChamferMills)
                    cmills.Add(tool);
                foreach (FeatureCAM.FMCounterBore tool in crib.CounterBores)
                    cbores.Add(tool);
                foreach (FeatureCAM.FMCounterSink tool in crib.CounterSinks)
                    csinks.Add(tool);
                foreach (FeatureCAM.FMEndMill tool in crib.EndMills)
                    emills.Add(tool);
                foreach (FeatureCAM.FMFaceMill tool in crib.FaceMills)
                    fmills.Add(tool);
                foreach (FeatureCAM.FMPlungeMill tool in crib.PlungeRoughers)
                    pmills.Add(tool);
                foreach (FeatureCAM.FMRoundingMill tool in crib.RoundingMills)
                    rmills.Add(tool);
                foreach (FeatureCAM.FMSideMill tool in crib.SideMills)
                    smills.Add(tool);
                foreach (FeatureCAM.FMSpotDrill tool in crib.SpotDrills)
                    sdrills.Add(tool);
                foreach (FeatureCAM.FMTap tool in crib.Taps)
                    taps.Add(tool);
                foreach (FeatureCAM.FMThreadMill tool in crib.ThreadMills)
                    thmills.Add(tool);
                foreach (FeatureCAM.FMTwistDrill tool in crib.TwistDrills)
                    tdrills.Add(tool);
            }
            all_tools += "/* Boring bars */";
            foreach (FeatureCAM.FMBoringBar tool in bbars)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Chamfer mills */";
            foreach (FeatureCAM.FMChamferMill tool in cmills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Counter bores */";
            foreach (FeatureCAM.FMCounterBore tool in cbores)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Counter sinks */";
            foreach (FeatureCAM.FMCounterSink tool in csinks)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Endmills */";
            foreach (FeatureCAM.FMEndMill tool in emills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Facemills */";
            foreach (FeatureCAM.FMFaceMill tool in fmills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Plungemills */";
            foreach (FeatureCAM.FMPlungeMill tool in pmills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Roundingmills */";
            foreach (FeatureCAM.FMRoundingMill tool in rmills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Sidemills */";
            foreach (FeatureCAM.FMSideMill tool in smills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Spotdrills */";
            foreach (FeatureCAM.FMSpotDrill tool in sdrills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Taps */";
            foreach (FeatureCAM.FMTap tool in taps)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Threadmills */";
            foreach (FeatureCAM.FMThreadMill tool in thmills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;
            all_tools += "/* Twistdrills */";
            foreach (FeatureCAM.FMTwistDrill tool in tdrills)
                all_tools += Tool.AllToolsToString((FeatureCAM.FMTool)tool, null) + Environment.NewLine;

            File.WriteAllText("C:\\all_tools1.txt", all_tools);
        }

        public static string GetEUREKAPath()
        {
            string eureka_path = "";
            string eureka_fpath = "";

            RegistryKey regVersionString = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Roboris");
            if (regVersionString == null) return "";

            string[] subkey_names = regVersionString.GetSubKeyNames();
            foreach (string subkey_name in subkey_names)
            {
                if (subkey_name.Contains("EUREKA"))
                    eureka_path = Path.Combine(regVersionString.ToString(), subkey_name);
            }
            if (eureka_path == "") return "";

            eureka_fpath = (string)Registry.GetValue(eureka_path, "TemplateDirectory", 1); /* Returns templates folder */
            if (eureka_fpath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                eureka_fpath = eureka_fpath.Substring(0, eureka_fpath.Length - Path.DirectorySeparatorChar.ToString().Length);
            eureka_fpath = Path.Combine(Path.GetDirectoryName(eureka_fpath), "Eureka.exe");
            if (!File.Exists(eureka_fpath))
                eureka_fpath = "";

            return eureka_fpath;
        }

    }
}