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
using FeatureCAM;

namespace FeatureCAMExporter
{
    public class FCExporter
    {
        public static List<UCS> InitializeAllUCS(FMUcss fm_ucss)
        {
            List<UCS> ucss;

            try
            {
                LogFile.Write("Initialize UCSs");
                if (fm_ucss == null) return null;
                if (fm_ucss.Count == 0) return null;

                ucss = new List<UCS>();
                foreach (FeatureCAM.FMUcs ucs in fm_ucss)
                    ucss.Add(new UCS(ucs));
                return ucss;
            }
            catch
            {
                return null;
            }
            finally
            {
                fm_ucss = null;
            }
        }

        public static List<string> InitializeAllFixtureIDs(FMSetups fm_setups)
        {
            List<string> fixture_ids;

            try
            {
                LogFile.Write("Initialize Figure ids");
                if (fm_setups == null) return null;
                if (fm_setups.Count == 0) return null;

                fixture_ids = new List<string>();
                foreach (FMSetup setup in fm_setups)
                    if (!String.IsNullOrEmpty(setup.FixtureID))
                        fixture_ids.Add(setup.FixtureID);
                return fixture_ids;
            }
            catch
            {
                return null;
            }
            finally
            {
                fm_setups = null;
            }
        }

        public static List<SolidInfo> InitializeAllSolids(FMSolids fm_solids)
        {
            List<SolidInfo> solids;
            List<string> solid_names;

            try
            {
                LogFile.Write("Initialize part solids");

                if (fm_solids == null) return null;
                if (fm_solids.Count == 0) return null;
                solid_names = new List<string>();
                foreach (FeatureCAM.FMSolid solid in fm_solids)
                    solid_names.Add(solid.Name);
                solid_names.Sort();
                solids = new List<SolidInfo>();
                foreach (string solid_name in solid_names)
                    solids.Add(new SolidInfo(solid_name));
                return solids;
            }
            catch 
            {
                return null;
            }
            finally
            {
                fm_solids = null;
            }
        }

        public static List<SetupInfo> InitializeAllSetups(FMSetups fm_setups, FMSolids fm_solids, FMUcss fm_ucss,
                                                          List<UCS> doc_ucss, List<SetupOptions> all_options, 
                                                          int combine_setups,
                                                          ref bool are_all_setups_milling)
        {
            List<SetupInfo> setups;
            SetupOptions options;
            try
            {
                if (fm_setups == null) return null;
                if (fm_setups.Count == 0) return null;

                are_all_setups_milling = true;

                foreach (FMSetup setup in fm_setups)
                    if (setup.Type != FeatureCAM.tagFMSetupType.eST_Milling)
                        are_all_setups_milling = false;

                setups = new List<SetupInfo>();
                if (combine_setups == 1 && all_options != null)
                {
                    SetupInfo setup = new SetupInfo();
                    setup.name = "Combined";
                    setup.options = all_options[0];
                    setup.work_offsets = setup.options.offsets;
                    setup.part = setup.options.parts;
                    if (setup.options.clamps != null)
                    {
                        foreach (SolidInfo clamp in setup.options.clamps)
                        {
                            if (fm_solids.Item(clamp.name) != null)
                            {
                                if (setup.clamps == null) setup.clamps = new List<SolidInfo>();
                                setup.clamps.Add(clamp);
                            }
                        }
                    }
                    //setup.clamps = setup.options.clamps;
                    setup.stock = new SolidInfo("", setup.options.attach_stock_to);
                    setup.sub_spindle = setup.options.is_subspindle;
                    setup.attach_stock_to = setup.options.attach_stock_to;
                    setup.attach_stock_to_subspindle = setup.options.attach_stock_to_subspindle;
                    if (setup.options.ucs_attach != "" && fm_ucss.Item(setup.options.ucs_attach) != null)
                        setup.attach_ucs = setup.options.ucs_attach;
                    if (setup.options.ucs_attach_subspindle != "" && fm_ucss.Item(setup.options.ucs_attach_subspindle) != null)
                        setup.attach_ucs_subspindle = setup.options.ucs_attach_subspindle;
                    setup.attach_ucss_to = setup.options.attach_ucss_to;
                    setup.attach_ucss_to_subspindle = setup.options.attach_ucss_to_subspindle;
                    setup.enabled = true;
                    setup.ucss = new List<UCS>();
                    foreach (FMUcs fm_ucs in fm_ucss)
                        setup.ucss.Add(new UCS(fm_ucs));

                    setups.Add(setup);
                }
                else
                {
                    foreach (FeatureCAM.FMSetup setup in fm_setups)
                    {
                        if (setup.Enabled)
                        {
                            LogFile.Write(String.Format("Initialize setup {0}", setup.Name));

                            options = FindSetupOptions(all_options, setup.Name);
                            setups.Add(new SetupInfo(options, setup, fm_solids, doc_ucss));
                        }
                        else
                        {
                            LogFile.Write(String.Format("Skip disabled setup {0}", setup.Name));
                        }
                    }
                    if (combine_setups == 1)
                        return InitalizeCombinedSetup(setups);
                }
                return setups;
            }
            catch
            {
                return null;
            }
            finally
            {
                fm_setups = null;
                fm_solids = null;
                all_options = null;
            }
        }

        public static List<SetupInfo> InitalizeCombinedSetup(List<SetupInfo> all_setups)
        {
            List<SetupInfo> combined_setups = new List<SetupInfo>();
            SetupInfo combined_setup;

            if (all_setups == null) return null;
            if (all_setups.Count == 0) return null;

            combined_setup = all_setups[0];
            combined_setup.name = "Combined";
            combined_setup.options.setup_name = combined_setup.name;
            for (int si = 1; si < all_setups.Count; si++)
            {
                if (all_setups[si].sub_spindle)
                    combined_setup.sub_spindle = true;
                if (all_setups[si].clamps != null)
                {
                    foreach (SolidInfo clamp in all_setups[si].clamps)
                    {
                        if (combined_setup.clamps == null) combined_setup.clamps = new List<SolidInfo>();
                        if (!combined_setup.clamps.Contains(clamp))
                            combined_setup.clamps.Add(clamp);
                    }
                }
                if (all_setups[si].part != null)
                {
                    foreach (SolidInfo part in all_setups[si].part)
                    {
                        if (combined_setup.part == null) combined_setup.part = new List<SolidInfo>();
                        if (!combined_setup.part.Contains(part))
                            combined_setup.part.Add(part);
                    }
                }
                if (all_setups[si].work_offsets != null)
                {
                    foreach (WorkOffset offset in all_setups[si].work_offsets)
                    {
                        if (combined_setup.work_offsets == null) combined_setup.work_offsets = new List<WorkOffset>();
                        if (!combined_setup.work_offsets.Contains(offset))
                            combined_setup.work_offsets.Add(offset);
                    }
                }
            }
            combined_setups.Add(combined_setup);
            return combined_setups;
        }

        private static SetupOptions FindSetupOptions(List<SetupOptions> all_options, string setup_name)
        {
            if (all_options != null)
            {
                foreach (SetupOptions setup_options in all_options)
                {
                    if (setup_options.setup_name == setup_name)
                        return setup_options;
                }
            }
            return null;
        }

        private static bool AreAllSetupsMilling(FMSetups setups)
        {
            try
            {
                foreach (FMSetup setup in setups)
                {
                    if (setup.Type != FeatureCAM.tagFMSetupType.eST_Milling)
                        return false;
                }
            }
            finally
            {
                setups = null;
            }
            return true;
        }

        public static void ExportStock(FMStock stock, List<SetupInfo> all_setups,
                                        string output_dirpath)
        {
            string err_str = "",
                   stock_fpath = "";

            try
            {
                /* Each setup could have its own stock. So we save it per setup. */
                for (int si = 0; si < all_setups.Count; si++)
                {
                    if (all_setups[si].options.is_export_stock)
                    {
                        LogFile.Write(String.Format("Export stock for setup {0}", all_setups[si].name));
                        if (stock_fpath == "") //If the the variable isn't set to a non-empty value, we haven't exported stock yet, 
                        {
                            stock_fpath = Path.Combine(output_dirpath, "stock.stl");
                            stock.ExportToSTL(stock_fpath, out err_str);
                            if ((err_str == "" || err_str == null) && File.Exists(stock_fpath))
                                LogFile.Write(String.Format("Created file {0}", stock_fpath));
                            else
                                LogFile.Write(String.Format("Error occured while exporting stock to .stl file. {0}\n", err_str));
                        }
                        all_setups[si].stock_fpath = stock_fpath;
                        LogFile.Write("Stock exported");
                    }
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "ExportStock");
            }
            finally
            {
                stock = null;
            }
        }

        public static void ExportDesign(FMSolids solids, List<SetupInfo> all_setups, 
                                        string output_dirpath)
        {
            string err_str = "";
            FMSolid solid;
            List<string> exported_parts = new List<string>();

            try
            {
                /* Each setup has its own parts. So we save them per setup. */
                for (int si = 0; si < all_setups.Count; si++)
                {
                    if (all_setups[si].part != null)
                    {
                        for (int pi = 0; pi < all_setups[si].part.Count; pi++)
                        {
                            if (all_setups[si].part[pi] != null)
                            {
                                solid = (FMSolid)solids.Item(all_setups[si].part[pi].name);
                                if (solid != null)
                                {
                                    LogFile.Write(String.Format("Export design {0} for setup {1}", all_setups[si].part[pi].name, all_setups[si].name));
                                    all_setups[si].part[pi].fpath = Path.Combine(output_dirpath, "part_" + solid.Name + ".stl");
                                    if (!exported_parts.Contains(solid.Name)) //There is no need to reexport solid if we already exported it
                                    {
                                        solid.ExportToSTL(all_setups[si].part[pi].fpath, out err_str);
                                        if ((err_str == "" || err_str == null) && File.Exists(all_setups[si].part[pi].fpath))
                                            LogFile.Write(String.Format("Created file {0}", all_setups[si].part[pi].fpath));
                                        else
                                            LogFile.Write(String.Format("Error occured while exporting solid to .stl file. {0}\n", err_str));
                                        exported_parts.Add(solid.Name);
                                    }
                                    LogFile.Write("Design exported");
                                }
                            }
                        }
                    }
                    else
                    {
                        LogFile.Write(String.Format("Setup {0} doesn't have any design solids", all_setups[si].name));
                    }
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "ExportDesign");
            }
            finally
            {
                solids = null;
                solid = null;
                exported_parts.Clear();
                exported_parts = null;
            }
        }

        public static void ExportClamps(FMSolids solids, List<SetupInfo> all_setups,
                                        string output_dirpath)
        {
            string err_str = "";
            FMSolid clamp;
            List<string> exported_clamps = new List<string>();

            try
            {
                /* Each setup has its own clamps. So we save them per setup. */
                for (int si = 0; si < all_setups.Count; si++)
                {
                    if (all_setups[si].clamps != null)
                    {
                        if (!Directory.Exists(Path.Combine(output_dirpath, "Clamps")))
                            Directory.CreateDirectory(Path.Combine(output_dirpath, "Clamps"));
                        for (int ci = 0; ci < all_setups[si].clamps.Count; ci++)
                        {
                            LogFile.Write(String.Format("Export clamp {0} for setup {1}", all_setups[si].clamps[ci].name, all_setups[si].name));
                            all_setups[si].clamps[ci].fpath = Path.Combine(output_dirpath, "Clamps\\" + all_setups[si].clamps[ci].name + ".stl");

                            if (!exported_clamps.Contains(all_setups[si].clamps[ci].name)) //There is no need to reexport solid if we already exported it
                            {
                                clamp = (FMSolid)solids.Item(all_setups[si].clamps[ci].name);
                                if (clamp != null)
                                {
                                    clamp.ExportToSTL(all_setups[si].clamps[ci].fpath, out err_str);
                                    if ((err_str == "" || err_str == null) && File.Exists(all_setups[si].clamps[ci].fpath))
                                        LogFile.Write(String.Format("Created file {0}", all_setups[si].clamps[ci].fpath));
                                    else
                                        LogFile.Write(String.Format("Error occured while exporting clamp solid to .stl file. {0}\n", err_str));
                                    exported_clamps.Add(clamp.Name);
                                }
                            }
                            LogFile.Write("Clamp exported");
                        }
                    }
                    else
                    {
                        LogFile.Write(String.Format("Setup {0} doesn't have any clamps", all_setups[si].name));
                    }
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "ExportClamps");
            }
            finally
            {
                solids = null;
                clamp = null;
                exported_clamps.Clear();
                exported_clamps = null;
            }
        }

        public static bool SaveNCCode(FeatureCAM.Application app, FeatureCAM.FMDocument doc,
                                       string fname_no_ext, string ext, string output_dirpath,
                                       List<SetupInfo> setups_info, List<bool> save_nc_for_all_setups,
                                       bool is_single_program, int combine_setups)
        {
            int nc_files_num;
            object nc_file_names;
            string err_msg;
            bool is_op_error = false;
            int nc_file_id;

            /* Set output units, so they match document units */
            app.PostOptionsMill.SetIsInchOutputUnits(!doc.Metric);
            app.PostOptionsTurn.SetIsInchOutputUnits(!doc.Metric);

            if (!is_single_program && combine_setups == 0)
            {
                for (int i = 0; i < setups_info.Count; i++)
                {
                    if (save_nc_for_all_setups[i] && setups_info[i].enabled && setups_info[i].num_features > 0)
                    {
                        SaveNCForSetup(doc, doc.Setups.Item(i + 1), fname_no_ext, ext, output_dirpath, out nc_files_num, out nc_file_names, out err_msg, ref is_op_error);

                        if ((int)nc_files_num > 0)
                        {
                            nc_file_id = 1;
                            foreach (string nc_fpath in (Array)nc_file_names)
                                setups_info[i].nc_fpaths.Add((string)nc_fpath);
                            LogFile.Write(String.Format("List of created files: {0}",
                                                        String.Join("\n", setups_info[i].nc_fpaths.ToArray())));
                            nc_file_id++;
                        }
                    }
                    else
                    {
                        LogFile.Write(String.Format("Skip saving nc code for setup {0}. User selected not to export it, setup isn't enabled or there are no features in the setup", setups_info[i].name));
                    }
                }
            }
            else if (!is_single_program && combine_setups == 1)
            {
                for (int i = 0; i < doc.Setups.Count; i++)
                {
                    if (doc.Setups.Item(i + 1).Enabled && doc.Setups.Item(i + 1).Features.Count > 0)
                    {
                        SaveNCForSetup(doc, doc.Setups.Item(i + 1), fname_no_ext, ext, output_dirpath, out nc_files_num, out nc_file_names, out err_msg, ref is_op_error);

                        if ((int)nc_files_num > 0)
                        {
                            nc_file_id = 1;
                            if (setups_info[0].nc_fpaths == null) setups_info[0].nc_fpaths = new List<string>();
                            foreach (string nc_fpath in (Array)nc_file_names)
                            {
                                if (!setups_info[0].nc_fpaths.Contains((string)nc_fpath))
                                    setups_info[0].nc_fpaths.Add((string)nc_fpath);
                            }
                            nc_file_id++;
                        }
                    }
                    else
                    {
                        LogFile.Write(String.Format("Skip saving nc code for setup {0}. User selected not to export it, setup isn't enabled or there are no features in the setup", setups_info[i].name));
                    }
                }
                LogFile.Write(String.Format("List of created files: {0}",
                                String.Join("\n", setups_info[0].nc_fpaths.ToArray())));
            }
            else
            {
                bool save_nc = true;
                foreach (bool save_nc_for_setup in save_nc_for_all_setups)
                    save_nc &= save_nc_for_setup;
                if (save_nc)
                {
                    if (SaveOneNCProgForAllSetups(doc, fname_no_ext, ext, output_dirpath, setups_info, ref is_op_error))
                        LogFile.Write(String.Format("List of created files: {0}",
                                                  String.Join("\n", setups_info[0].nc_fpaths.ToArray())));
                }
                else
                {
                    LogFile.Write("Skip saving nc code. User selected not to export it");
                }
            }
            return !is_op_error;
        }

        /* Save nc code for all setups (i.e., if part is non-indexed save 1 nc program is on).
         * In this case we save 1 nc program and set that program as nc code program for all setups.
         */
        private static bool SaveOneNCProgForAllSetups(FeatureCAM.FMDocument doc, string fname_no_ext, 
                                                      string ext, string output_dirpath, 
                                                      List<SetupInfo> setups_info, ref bool is_op_error)
        {
            int nc_files_num, 
                doc_files_num, 
                macro_files_num;
            object doc_file_names,
                   macro_file_names, 
                   nc_file_names;
            string err_msg;

            for (int i = 1; i <= doc.Setups.Count; i++)
            {
                if (doc.Setups.Item(i).Enabled)
                {
                    doc.Setups.Item(i).Activate();
                    break;
                }
            }
            doc.SimToolpath(false);
            
            LogFile.Write("Run sim and save nc for the document (1 program for entire part)");

            is_op_error = false;
            foreach (FeatureCAM.FMOperation op in doc.Operations)
                if (op.Errors.Trim() != "") is_op_error = true;

            if (is_op_error)
            {
                LogFile.Write("NC code will not be exported, because there are errors in the Operations list. Project file won't be exported as well.");
                return false;
            }

            doc.SaveNC(fname_no_ext + ext, output_dirpath, false,
                                FeatureCAM.tagFMSaveNCFileType.eNCFT_NCCode, false, out err_msg,
                                out nc_files_num, out nc_file_names, out doc_files_num, out doc_file_names,
                                out macro_files_num, out macro_file_names);


            if ((int)nc_files_num > 0)
            {
                for (int i = 0; i < setups_info.Count; i++)
                {
                    foreach (string nc_fpath in (Array)nc_file_names)
                    {
                        if (setups_info[i].nc_fpaths == null) setups_info[i].nc_fpaths = new List<string>();
                        setups_info[i].nc_fpaths.Add((string)nc_fpath);
                    }
                }
                LogFile.Write(String.Format("{0} file(s) created", nc_files_num));
                return true;
            }
            else
            {
                is_op_error = true;
                return false;
            }
        }

        private static void SaveNCForSetup(FMDocument doc, FMSetup setup,
                                           string fname_no_ext, string ext, string output_dirpath,
                                           out int nc_files_num, out object nc_file_names,
                                           out string err_msg, ref bool is_op_error)
        {
            int doc_files_num, macro_files_num;
            object doc_file_names, macro_file_names;


            nc_files_num = 0;
            err_msg = "";
            nc_file_names = null;

            LogFile.Write(String.Format("Run sim and save nc for setup {0}", setup.Name));
            setup.Activate();
            doc.SimToolpath(false);

            is_op_error = false;
            foreach (FeatureCAM.FMFeature feat in setup.Features)
                for (int i = 1; i <= feat.Operations.Count; i++)
                    if (feat.Operations.Item(i).Errors.Trim() != "") is_op_error = true;

            if (is_op_error)
            {
                LogFile.Write("There are errors in the setup's operations list. NC code will not be exported. Project file won't be exported as well.");
                return;
            }

            doc.SaveNC(fname_no_ext + ext, output_dirpath, true,
                                FeatureCAM.tagFMSaveNCFileType.eNCFT_NCCode, false, out err_msg,
                                out nc_files_num, out nc_file_names, out doc_files_num, out doc_file_names,
                                out macro_files_num, out macro_file_names);
            LogFile.Write(String.Format("{0} file(s) created", nc_files_num));
        }

        public static void ToolsToList(FeatureCAM.Application app, FMDocument doc,
                                       List<SetupInfo> setups_info, List<bool> save_tools_for_all_setups,
                                       bool is_single_program, string output_dir, bool is_doc_metric)
        {
            List<string> setup_tools = new List<string>();
            //string unsupported_tool_names;
            string tool_name;
            FMTool tool;
            FMToolMap2 toolmap;
            tagFMTurretIDType turret_id;
            bool temp;


            for (int i = 1; i <= doc.Setups.Count; i++)
            {
                if (doc.Setups.Item(i).Enabled)
                {
                    doc.Setups.Item(i).Activate();
                    if (!is_single_program)
                    {
                        if (save_tools_for_all_setups[i - 1])
                            setup_tools.Add(GetToolsInSetups(doc.Setups.Item(i)));
                        else
                            setup_tools.Add("");
                    }
                    else
                    {
                        setup_tools.Add(GetToolsInSetups(doc.Setups.Item(i)));
                    }
                }
            }

            /* If we need to create separate tls file for each setup, write tools for each setup to a separate file */
            doc.InvalidateToolpaths();
            if (!is_single_program)
            {
                for (int si = 1; si <= doc.Setups.Count && doc.Setups.Item(si).Enabled; si++ )
                {
                    ((FMSetup)doc.Setups.Item(si)).Activate(); //Need this line for A-038162 VERICUT: If option Combine setups isn't turned on and exporting 2 milling setups, we get tool export error. 
                    if (save_tools_for_all_setups[si - 1])
                    {
                        LogFile.Write(String.Format("Construct tool list for setup {0}", doc.Setups.Item(si).Name));
                        for (int i = 1; i <= doc.ToolMaps.Count; i++)
                        {
                            toolmap = doc.ToolMaps.Item(i);
                            if (toolmap.Operations.Count > 0)
                            {
                                tool = (FMTool)toolmap.Tool;
                                tool_name = "";
                                tool_name += tool.Name;
                                if (setup_tools[si - 1].IndexOf(tool_name) >= 0)
                                {
                                    app.GetTurnTurretInfo(toolmap.turret, out temp, out turret_id, out temp, out temp, out temp, out temp);
                                    FeatureCAMTool fc_tool = new FeatureCAMTool(toolmap, tool, turret_id, app.TurningInputMode, output_dir, is_doc_metric);
                                    if (setups_info[si - 1].tools == null) setups_info[si - 1].tools = new List<FeatureCAMTool>();
                                    setups_info[si - 1].tools.Add(fc_tool);
                                }
                            }
                        }
                    }
                    else
                    {
                        LogFile.Write(String.Format("Skip setup {0}. User chose not to export tools for it", doc.Setups.Item(si).Name));
                    }
                }
            }
            else
            {
                string all_tools;
                all_tools = String.Join(";", setup_tools);

                bool save_tools = true;
                foreach (bool save_tools_for_setup in save_tools_for_all_setups)
                    save_tools &= save_tools_for_setup;
                if (save_tools)
                {
                    LogFile.Write("Construct tool list for all features in the part");
                    for (int i = 1; i <= doc.ToolMaps.Count; i++)
                    {
                        toolmap = doc.ToolMaps.Item(i);
                        /* Doing below check, because toolmap.Operations.Count didn't seem to work every time */ 
                        if (all_tools.Contains(toolmap.Tool.Name + ";"))
                        {
                            tool = (FMTool)toolmap.Tool;
                            app.GetTurnTurretInfo(toolmap.turret, out temp, out turret_id, out temp, out temp, out temp, out temp);
                            FeatureCAMTool fc_tool = new FeatureCAMTool(toolmap, tool, turret_id, app.TurningInputMode, output_dir, is_doc_metric);
                            if (setups_info[0].tools == null) setups_info[0].tools = new List<FeatureCAMTool>();
                            setups_info[0].tools.Add(fc_tool);
                        }
                    }
                }
            }
        }


        private static string GetToolsInSetups(FMSetup setup)
        {
            string setup_tool_list = "";
            FMFeatures feats;
            FMFeature feat;
            FMOperations ops;
            FMOperation op;
            FMTool tool;

            if (setup == null) return "";

            feats = setup.Features;

            if (feats == null) return "";

            for (int fi = 1; fi <= feats.Count; fi++)
            {
                feats.Item(1);
                feat = (FMFeature)feats.Item(fi);
                string featname = feat.Name;
                ops = feat.Operations;
                for (int oi = 1; oi <= ops.Count; oi++)
                {
                    op = (FMOperation)ops.Item(oi);
                    string opname = (string)op.OperationName;
                    tool = (FMTool)op.Tool;
                    if (tool != null)
                        setup_tool_list += (string)tool.Name + ';';
                }
            }
            return setup_tool_list;
        }

    }
}
