// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using FeatureCAM;
using FeatureCAMExporter;

namespace FeatureCAMToVericut
{
    class VcProject_Manager
    {
        public static void ConstructNewVCProjectFile(int num_of_setups)
        {
            string before_setups_substr = "",
                   after_setups_substr = "",
                   setup_info_from_template;
            List<string> setups_info_str = new List<string>();

            try
            {
                Variables.vcproj_fpath = Path.Combine(Variables.doc_options.output_dirpath, Variables.fname_no_ext + ".vcproject");
                LogFile.Write("Prepare VERICUT project");

                GetInfoFromProjectTemplate(ref before_setups_substr, ref after_setups_substr);
                foreach (SetupInfo setup_info in Variables.setups_info)
                {
                    if (setup_info.enabled)
                    {
                        if (setup_info.options.template_fpath != "")
                            setup_info_from_template = GetSetupInfoFromTemplate(setup_info.options.template_fpath);
                        else
                            setup_info_from_template = GetSetupInfoFromTemplate(Variables.doc_options.vc_template_proj_fpath);
                        setups_info_str.Add(SetSetupInfo(setup_info_from_template, setup_info));
                    }
                }

                File.WriteAllText(Variables.vcproj_fpath, 
                                  before_setups_substr +
                                   String.Join(Environment.NewLine, setups_info_str) + Environment.NewLine +
                                   after_setups_substr);
                LogFile.Write(String.Format("Project {0} has been created", Variables.vcproj_fpath));
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "ConstructNewVCProjectFile");
            }
        }

        private static string GetSetupInfoFromTemplate(string template_fpath)
        {
            try
            {
                string temp_fcontent = File.ReadAllText(template_fpath);
                int pos_s = temp_fcontent.IndexOf("<Setup", StringComparison.OrdinalIgnoreCase);
                if (pos_s >= 0)
                    temp_fcontent = temp_fcontent.Substring(pos_s);
                int pos_e = temp_fcontent.IndexOf("</Setup>", StringComparison.OrdinalIgnoreCase);
                if (pos_e >= 0)
                    temp_fcontent = temp_fcontent.Substring(0, pos_e + 8);

                return temp_fcontent;
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "GetSetupInfoFromTemplate");
                return "";
            }

        }

        private static void GetInfoFromProjectTemplate(ref string before_setups_substr, ref string after_setups_substr)
        {
            string fcontent;
            int pos, pos1;

            fcontent = File.ReadAllText(Variables.doc_options.vc_template_proj_fpath);

            /* Extract setups info from fcontent
             * First find file content before first occurence of string "<setup " */
            pos = fcontent.IndexOf("<SETUP ", StringComparison.OrdinalIgnoreCase);
            if (pos >= 0)
            {
                pos1 = fcontent.LastIndexOf(Environment.NewLine, pos);
                if (pos1 > 0)
                {
                    before_setups_substr = fcontent.Substring(0, pos1 + Environment.NewLine.Length);
                    fcontent = fcontent.Substring(pos1 + Environment.NewLine.Length);
                }
            }
            /* Second find file content after last occurence of string "</setup>" */
            pos = fcontent.LastIndexOf("</SETUP>", StringComparison.OrdinalIgnoreCase);
            if (pos >= 0)
            {
                pos = fcontent.IndexOf("<", pos + 8);
                if (pos >= 0)
                {
                    pos1 = fcontent.LastIndexOf(Environment.NewLine, pos);
                    if (pos1 > 0)
                    {
                        after_setups_substr = fcontent.Substring(pos1 + Environment.NewLine.Length);
                        fcontent = fcontent.Substring(0, pos1 + Environment.NewLine.Length);
                    }
                }
            }
        }

        private static string SetSetupInfo(string orig_setup_info, SetupInfo setup_info)
        {
            try
            {
                if (orig_setup_info.Trim() == "") return "";

                SetSetupHeader(setup_info, ref orig_setup_info);
                SetCoordSystemInfo(setup_info, ref orig_setup_info);
                SetFixtureInfo(setup_info, ref orig_setup_info);
                SetStockInfo(setup_info, ref orig_setup_info);
                SetPartInfo(setup_info, ref orig_setup_info);
                SetToolFileInfo(setup_info, ref orig_setup_info);
                SetNCProgramInfo(setup_info, ref orig_setup_info);
                SetToolListInfo(setup_info, ref orig_setup_info);
                SetWorkOffsets(setup_info, ref orig_setup_info);

                return orig_setup_info;

            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "SetSetupInfo");
                return "";
            }
        }

        private static void SetSetupHeader(SetupInfo setup_info, ref string mod_setup_info)
        {
            int pos_s = 0,
                pos_e = 0;
            string header, new_header;

            pos_s = mod_setup_info.IndexOf("<Setup", StringComparison.OrdinalIgnoreCase);
            pos_e = mod_setup_info.IndexOf(">", pos_s);
            
            if (pos_e < 0 || pos_s < 0) return;

            header = mod_setup_info.Substring(pos_s, pos_e + 1);

            new_header = String.Format("  <Setup Name=\"{0}\" Active=\"on\">", setup_info.name, (setup_info.enabled ? "on" : "off"));
            mod_setup_info = new_header +
                             mod_setup_info.Substring(pos_e + 1);
        }

        private static void SetFixtureInfo(SetupInfo setup_info, ref string mod_setup_info)
        {
            try
            {
                if (setup_info.clamps == null) return;
                if (setup_info.clamps.Count == 0) return;

                SetSpindleFixtureInfo(setup_info, ref mod_setup_info, true);
            }
            catch (Exception)
            { }
        }

        private static void SetSpindleFixtureInfo(SetupInfo setup_info, ref string mod_setup_info, bool main_spindle)
        {
            int pos, pos_s, pos_e;
            string new_component_str = "",
                   stl_info = "";
            List<List<SolidInfo>> sorted_fixtures;
            try
            {
                sorted_fixtures = SortSolidsByAttachComponents(setup_info.clamps);

                if (sorted_fixtures == null) return;
                if (sorted_fixtures.Count == 0) return;

                foreach (List<SolidInfo> component_fixtures in sorted_fixtures)
                {
                    if (component_fixtures == null) continue;
                    if (component_fixtures.Count == 0) continue;

                    FindComponentDescription(mod_setup_info, out pos_s, out pos_e, new string[] { "Component", component_fixtures[0].attach_to, "Position" });
                    new_component_str = mod_setup_info.Substring(pos_s, pos_e - pos_s);

                    stl_info = "";
                    foreach (SolidInfo clamp in component_fixtures)
                        if (clamp.main_spindle)
                            stl_info += (stl_info != "" ? Environment.NewLine : "") +
                                        String.Format("        <STL Unit=\"{0}\" Normal=\"Computed\" Visible=\"on\" Mirror=\"off\" MirrorCsys=\"0\" Hue=\"-1\">\r\n", (Variables.doc.Metric ? "millimeter" : "inch")) +
                                         String.Format("            <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(setup_info.setup_solid_x, 4), Math.Round(setup_info.setup_solid_y, 4), Math.Round(setup_info.setup_solid_z, 4)) +
                                        String.Format("            <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", Math.Round(setup_info.setup_solid_i, 4), Math.Round(setup_info.setup_solid_j, 4), Math.Round(setup_info.setup_solid_k, 4)) +
                                        String.Format("            <File>{0}</File>\r\n", clamp.fpath) +
                                        String.Format("        </STL>");
                        else
                            stl_info += (stl_info != "" ? Environment.NewLine : "") +
                                        String.Format("        <STL Unit=\"{0}\" Normal=\"Computed\" Visible=\"on\" Mirror=\"off\" MirrorCsys=\"0\" Hue=\"-1\">\r\n", (Variables.doc.Metric ? "millimeter" : "inch")) +
                                        String.Format("            <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(setup_info.sub_setup_solid_x, 4), Math.Round(setup_info.sub_setup_solid_y, 4), Math.Round(setup_info.sub_setup_solid_z, 4)) +
                                        String.Format("            <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", Math.Round(setup_info.sub_setup_solid_i, 4), Math.Round(setup_info.sub_setup_solid_j, 4), Math.Round(setup_info.sub_setup_solid_k, 4)) +
                                        String.Format("            <File>{0}</File>\r\n", clamp.fpath) +
                                        String.Format("        </STL>");
                    string old_stock_block = FindXMLBlock(new_component_str, "STL");

                    pos = new_component_str.IndexOf("</Component", StringComparison.OrdinalIgnoreCase);
                    if (pos > 0)
                    {
                        pos = new_component_str.LastIndexOf(Environment.NewLine, pos) + Environment.NewLine.Length;
                        new_component_str = new_component_str.Substring(0, pos).Trim() + Environment.NewLine +
                                            (stl_info != "" ? stl_info + Environment.NewLine : "") +
                                            new_component_str.Substring(pos);
                    }

                    mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                                     new_component_str +
                                     mod_setup_info.Substring(pos_e);
                }
            }
            catch (Exception)
            { }
        }

        private static void SetStockInfo(SetupInfo setup_info, ref string mod_setup_info)
        {
            try
            {
                if (!setup_info.options.is_export_stock) return;

                SetSpindleStockInfo(setup_info, ref mod_setup_info, true);
                if (setup_info.sub_spindle)
                    SetSpindleStockInfo(setup_info, ref mod_setup_info, false);
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export");
            }
        }

        private static void SetSpindleStockInfo(SetupInfo setup_info, ref string mod_setup_info, bool main_spindle)// string attach_to)
        {
            int pos, pos_s, pos_e;
            string new_component_str = "",
                   stl_info = "";

            if (!main_spindle && String.IsNullOrEmpty(setup_info.attach_stock_to_subspindle)) return;

            FindComponentDescription(mod_setup_info, out pos_s, out pos_e, new string[] { "Component", (main_spindle ? setup_info.attach_stock_to : setup_info.attach_stock_to_subspindle), "Position" });
            new_component_str = mod_setup_info.Substring(pos_s, pos_e - pos_s);
            if (main_spindle)
                stl_info = String.Format("        <STL Unit=\"{0}\" Normal=\"outward\" Visible=\"on\" Mirror=\"off\" MirrorCsys=\"0\" Hue=\"-1\">\r\n", (Variables.doc.Metric ? "millimeter" : "inch")) +
                           String.Format("            <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(setup_info.setup_solid_x, 4), Math.Round(setup_info.setup_solid_y, 4), Math.Round(setup_info.setup_solid_z, 4)) +
                           String.Format("            <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", Math.Round(setup_info.setup_solid_i, 4), Math.Round(setup_info.setup_solid_j, 4), Math.Round(setup_info.setup_solid_k, 4)) +
                           String.Format("            <File>{0}</File>\r\n", setup_info.stock_fpath) +
                           String.Format("        </STL>");
            else
                stl_info = String.Format("        <STL Unit=\"{0}\" Normal=\"outward\" Visible=\"on\" Mirror=\"off\" MirrorCsys=\"0\" Hue=\"-1\">\r\n", (Variables.doc.Metric ? "millimeter" : "inch")) +
                           String.Format("            <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(setup_info.sub_setup_solid_x, 4), Math.Round(setup_info.sub_setup_solid_y, 4), Math.Round(setup_info.sub_setup_solid_z, 4)) +
                           String.Format("            <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", Math.Round(setup_info.sub_setup_solid_i, 4), Math.Round(setup_info.sub_setup_solid_j, 4), Math.Round(setup_info.sub_setup_solid_k, 4)) +
                           String.Format("            <File>{0}</File>\r\n", setup_info.stock_fpath) +
                           String.Format("        </STL>");
            string old_stock_block = FindXMLBlock(new_component_str, "STL");
            if (old_stock_block == "")
                old_stock_block = FindXMLBlock(new_component_str, "Block");
            if (old_stock_block == "")
                old_stock_block = FindXMLBlock(new_component_str, "Round");

            if (old_stock_block != "")
                new_component_str = new_component_str.Replace(old_stock_block, stl_info);
            else
            {
                pos = new_component_str.IndexOf("</Component", StringComparison.OrdinalIgnoreCase);
                if (pos > 0)
                {
                    pos = new_component_str.LastIndexOf(Environment.NewLine, pos) + Environment.NewLine.Length;
                    new_component_str = new_component_str.Substring(0, pos).Trim() + Environment.NewLine +
                                        stl_info + Environment.NewLine +
                                        new_component_str.Substring(pos);
                }
            }

            mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                             new_component_str +
                             mod_setup_info.Substring(pos_e);
        }

        private static void SetPartInfo(SetupInfo setup_info, ref string mod_setup_info)
        {
            try
            {
                if (setup_info.part == null) return;
                if (setup_info.part.Count == 0) return;

                SetSpindlePartInfo(setup_info, ref mod_setup_info, true);
            }
            catch (Exception)
            { }
        }

        private static void SetSpindlePartInfo(SetupInfo setup_info, ref string mod_setup_info, bool main_spindle)
        {
            int pos, pos_s, pos_e;
            string new_component_str = "",
                   stl_info = "";
            List<List<SolidInfo>> sorted_parts;

            try
            {
                sorted_parts = SortSolidsByAttachComponents(setup_info.part);

                if (sorted_parts == null) return;
                if (sorted_parts.Count == 0) return;

                foreach (List<SolidInfo> component_parts in sorted_parts)
                {
                    if (component_parts == null) continue;
                    if (component_parts.Count == 0) continue;

                    FindComponentDescription(mod_setup_info, out pos_s, out pos_e, new string[] { "Component", component_parts[0].attach_to, "Position" });
                    new_component_str = mod_setup_info.Substring(pos_s, pos_e - pos_s);

                    stl_info = "";
                    foreach (SolidInfo part in component_parts)
                    {
                        if (part.main_spindle)
                            stl_info = String.Format("        <STL Unit=\"{0}\" Normal=\"outward\" Visible=\"on\" Mirror=\"off\" MirrorCsys=\"0\" Hue=\"-1\">\r\n", (Variables.doc.Metric ? "millimeter" : "inch")) +
                                       String.Format("            <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(setup_info.setup_solid_x, 4), Math.Round(setup_info.setup_solid_y, 4), Math.Round(setup_info.setup_solid_z, 4)) +
                                       String.Format("            <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", Math.Round(setup_info.setup_solid_i, 4), Math.Round(setup_info.setup_solid_j, 4), Math.Round(setup_info.setup_solid_k, 4)) +
                                       String.Format("            <File>{0}</File>\r\n", part.fpath) +
                                       String.Format("        </STL>");
                        else
                            stl_info = String.Format("        <STL Unit=\"{0}\" Normal=\"outward\" Visible=\"on\" Mirror=\"off\" MirrorCsys=\"0\" Hue=\"-1\">\r\n", (Variables.doc.Metric ? "millimeter" : "inch")) +
                                       String.Format("            <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(setup_info.sub_setup_solid_x, 4), Math.Round(setup_info.sub_setup_solid_y, 4), Math.Round(setup_info.sub_setup_solid_z, 4)) +
                                       String.Format("            <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", Math.Round(setup_info.sub_setup_solid_i, 4), Math.Round(setup_info.sub_setup_solid_j, 4), Math.Round(setup_info.sub_setup_solid_k, 4)) +
                                       String.Format("            <File>{0}</File>\r\n", part.fpath) +
                                       String.Format("        </STL>");
                    } 
                    string old_stock_block = FindXMLBlock(new_component_str, "STL");
                    if (old_stock_block == "")
                        old_stock_block = FindXMLBlock(new_component_str, "Block");
                    if (old_stock_block == "")
                        old_stock_block = FindXMLBlock(new_component_str, "Round");

                    if (old_stock_block != "")
                        new_component_str = new_component_str.Replace(old_stock_block, stl_info);
                    else
                    {
                        pos = new_component_str.IndexOf("</Component", StringComparison.OrdinalIgnoreCase);
                        if (pos > 0)
                        {
                            pos = new_component_str.LastIndexOf(Environment.NewLine, pos) + Environment.NewLine.Length;
                            new_component_str = new_component_str.Substring(0, pos).Trim() + Environment.NewLine +
                                                stl_info + Environment.NewLine +
                                                new_component_str.Substring(pos);
                        }
                    }

                    mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                                     new_component_str +
                                     mod_setup_info.Substring(pos_e);
                }
            }
            catch (Exception)
            { }
        }

        private static void SetToolListInfo(SetupInfo setup_info, ref string mod_setup_info)
        {
            int pos_s, pos_e;

            if (!setup_info.options.is_export_tools) return;
            if (setup_info.tool_list == "") return;

            FindXMLBlock(mod_setup_info, "ToolChange", out pos_s, out pos_e);

            if (pos_s < 0)
            {
                //Tool list isn't found in the templates, so just try to add it
                FindXMLBlock(mod_setup_info, "NCPrograms", out pos_s, out pos_e);
                mod_setup_info = mod_setup_info.Insert(pos_e, Utilities.Indent(setup_info.tool_list, 1));
            }
            else
            {
                mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                                 Utilities.Indent(setup_info.tool_list, 1).Trim() +
                                 mod_setup_info.Substring(pos_e);
            }
        }

        private static void SetNCProgramInfo(SetupInfo setup_info, ref string mod_setup_info)
        {
            int pos_s, pos_e, insert_at;
            string new_component_str,
                   nc_progs_str = "";

            if (!setup_info.options.is_export_nc) return;

            FindXMLBlock(mod_setup_info, "NCPrograms", out pos_s, out pos_e);

            if (setup_info.nc_fpaths != null)
                foreach (string nc_fpath in setup_info.nc_fpaths)
                    nc_progs_str += String.Format("      <NCProgram Use=\"on\" Filter=\"off\">\r\n") +
                                    String.Format("        <File>{0}</File>\r\n", nc_fpath) +
                                    String.Format("        <Orient>None</Orient>\r\n") +
                                    String.Format("        <Ident></Ident>\r\n") +
                                    String.Format("      </NCProgram>\r\n");


            if (pos_s < 0 || pos_e < 0) //there is no NCPrograms info in the template
            {
                insert_at = mod_setup_info.IndexOf("</Setup>");
                insert_at = mod_setup_info.LastIndexOf(Environment.NewLine, insert_at) + Environment.NewLine.Length;
                new_component_str = String.Format("    <NCPrograms Type=\"gcode\" Change=\"list\" List=\"tool_num\">\r\n") +
                                    String.Format("      <Init Flag=\"off\"></Init>\r\n") +
                                    String.Format("      <Over Flag=\"off\"></Over>\r\n") +
                                    String.Format("{0}", nc_progs_str) +
                                    String.Format("    </NCPrograms>\r\n");
                mod_setup_info = mod_setup_info.Insert(insert_at, new_component_str);
            }
            else
            {
                //We want to replace all existing nc programs in the files with the ones we just created
                pos_s = mod_setup_info.IndexOf("<NCProgram", pos_s + 10, StringComparison.OrdinalIgnoreCase);
                if (pos_s < 0) //there is no NCProgram info in the template
                {
                    insert_at = mod_setup_info.IndexOf("</NCPrograms>");
                    insert_at = mod_setup_info.LastIndexOf(Environment.NewLine, insert_at) + Environment.NewLine.Length;
                    mod_setup_info = mod_setup_info.Insert(insert_at, nc_progs_str);
                }
                else
                {
                    pos_e = mod_setup_info.LastIndexOf("</NCProgram>", pos_e, StringComparison.OrdinalIgnoreCase);
                    if (pos_e > 0)
                        mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                                     nc_progs_str.Trim() +
                                     mod_setup_info.Substring(pos_e + 12);
                }
            }
        }

        private static void SetToolFileInfo(SetupInfo setup_info, ref string mod_setup_info)
        {
            int pos_s, pos_e, insert_at;
            string new_component_str;

            if (!setup_info.options.is_export_tools) return;

            FindXMLBlock(mod_setup_info, "ToolMan", out pos_s, out pos_e);

            new_component_str = String.Format("    <ToolMan>\r\n") +
                                String.Format("      <Library>{0}</Library>\r\n", setup_info.tool_fpath) +
                                String.Format("    </ToolMan>\r\n");
            
            if (pos_s < 0 || pos_e < 0) //there is no ToolMan info in the template
            {
                insert_at = mod_setup_info.IndexOf("</Setup>");
                insert_at = mod_setup_info.LastIndexOf(Environment.NewLine, insert_at) + Environment.NewLine.Length;
                mod_setup_info = mod_setup_info.Insert(insert_at, new_component_str);
            }
            else
            {
                mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                                 new_component_str.Trim() +
                                 mod_setup_info.Substring(pos_e);
            }
        }

        private static void SetCoordSystemInfo(SetupInfo setup_info, ref string mod_setup_info)
        {
            int pos_s, pos_e, insert_at;
            string coord_sys_desc,
                   new_component_str;

            try
            {
                if (setup_info.ucss == null) return;

                FindXMLBlock(mod_setup_info, "CSystems", out pos_s, out pos_e);
                if (pos_s < 0 || pos_e < 0) //there is no Setups info in the template
                    new_component_str = "    <CSystems Simulation=\"off\">" + Environment.NewLine +
                                        "      <Machine></Machine>" + Environment.NewLine +
                                        "    </CSystems>" + Environment.NewLine;
                else
                    new_component_str = mod_setup_info.Substring(pos_s, pos_e - pos_s);

                coord_sys_desc = "";
                FMUcs attach_ucs = Variables.doc.UCSs.Item(setup_info.attach_ucs);
                FMSetup temp_setup = Variables.doc.AddSetup(setup_info.attach_ucs + "_temp_setup", tagFMSetupType.eST_Milling, null, attach_ucs.Name, Type.Missing);
                temp_setup.Enabled = false;
                foreach (UCS ucs in Variables.all_ucss)
                {
                    double x, y, z, i, j, k;
                    ucs.ComputeCoordinatesInRelationToSetup(temp_setup, out x, out y, out z, out i, out j, out k);
                    coord_sys_desc += String.Format("      <CSystem Name=\"{0}\" Type=\"component\" Visible=\"none\" Color=\"-1\" Transition=\"{1}\">\r\n", (!setup_info.sub_spindle ? "" : "(Main) ") + ucs.name, (ucs.name.Equals(Variables.doc_options.trans_ucs) ? "on" : "off")) +
                                      String.Format("        <Attach>{0}</Attach>\r\n", (!ucs.name.Equals(Variables.doc_options.trans_ucs) ? setup_info.attach_ucss_to : setup_info.attach_stock_to)) +
                                      String.Format("        <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(x, 4), Math.Round(y, 4), Math.Round(z, 4)) +
                                      String.Format("        <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", i, j, k) +
                                      String.Format("      </CSystem>\r\n");
                }
                FMUcs temp_setup_ucs = temp_setup.ucs;
                temp_setup.Delete();
                temp_setup_ucs.Delete();

                if (!String.IsNullOrEmpty(setup_info.attach_ucs_subspindle))
                {
                    attach_ucs = Variables.doc.UCSs.Item(setup_info.attach_ucs_subspindle);
                    temp_setup = Variables.doc.AddSetup(setup_info.attach_ucs_subspindle + "_temp_setup", tagFMSetupType.eST_Milling, null, attach_ucs.Name, Type.Missing);
                    temp_setup.Enabled = false;
                    if (setup_info.attach_ucss_to_subspindle != "")
                    {
                        foreach (UCS ucs in Variables.all_ucss)
                        {
                            double x, y, z, i, j, k;
                            ucs.ComputeCoordinatesInRelationToSetup(temp_setup, out x, out y, out z, out i, out j, out k);
                            coord_sys_desc += String.Format("      <CSystem Name=\"{0}\" Type=\"component\" Visible=\"none\" Color=\"-1\" Transition=\"{1}\">\r\n", "(Sub) " + ucs.name, (ucs.name.Equals(Variables.doc_options.trans_ucs) ? "on" : "off")) +
                                              String.Format("        <Attach>{0}</Attach>\r\n", (!ucs.name.Equals(Variables.doc_options.trans_ucs) ? setup_info.attach_ucss_to_subspindle : setup_info.attach_stock_to_subspindle)) +
                                              String.Format("        <Position X=\"{0}\" Y=\"{1}\" Z=\"{2}\"/>\r\n", Math.Round(x, 4), Math.Round(y, 4), Math.Round(z, 4)) +
                                              String.Format("        <Rotation I=\"{0}\" J=\"{1}\" K=\"{2}\"/>\r\n", i, j, k) +
                                              String.Format("      </CSystem>\r\n");
                        }
                    }
                    temp_setup_ucs = temp_setup.ucs;
                    temp_setup.Delete();
                    temp_setup_ucs.Delete();
                }
                insert_at = new_component_str.IndexOf("</CSystems>");
                insert_at = new_component_str.LastIndexOf(Environment.NewLine, insert_at) + Environment.NewLine.Length;
                new_component_str = new_component_str.Insert(insert_at, coord_sys_desc);

                if (pos_s < 0 || pos_e < 0) //there is no Setups info in the template
                {
                    insert_at = mod_setup_info.IndexOf("</Setup>");
                    insert_at = mod_setup_info.LastIndexOf(Environment.NewLine, insert_at) + Environment.NewLine.Length;
                    mod_setup_info = mod_setup_info.Insert(insert_at, new_component_str);
                }
                else
                {
                    mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                                     new_component_str +
                                     mod_setup_info.Substring(pos_e);
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "SetCoordSystemInfo");
            }
        }

        private static void SetWorkOffsets(SetupInfo setup_info, ref string mod_setup_info)
        {
            int pos_s, pos_e, insert_at;
            string new_component_str = "";

            if (setup_info.work_offsets == null) return;
            if (setup_info.work_offsets.Count == 0) return;

            FindXMLBlock(mod_setup_info, "Table", out pos_s, out pos_e);
            while (pos_s > 0)
            {
                mod_setup_info = mod_setup_info.Substring(0, pos_s) +
                                 mod_setup_info.Substring(pos_e);
                FindXMLBlock(mod_setup_info, "Table", out pos_s, out pos_e);
            }

            List<WorkOffset> work_offsets, 
                             prog_zero_offsets;
            SortOffsets(setup_info.work_offsets, out work_offsets, out prog_zero_offsets);
            if (work_offsets.Count > 0)
            {
                new_component_str += (new_component_str != "" ? Environment.NewLine : "") +
                                      "      <Table Name=\"Work Offsets\">\r\n";
                foreach (WorkOffset offset in work_offsets)
                {
                    if (!setup_info.sub_spindle)
                        new_component_str += String.Format("        <Row Index=\"{0}\" SubIndex=\"1\" Auto=\"auto\">\r\n", offset.register) +
                                             String.Format("          <System>{0}</System>\r\n", offset.subsystem) +
                                             String.Format("          <From CSystem=\"off\" X=\"0\" Y=\"0\" Z=\"0\">{0}</From>\r\n", offset.from_component) +
                                             String.Format("          <To CSystem=\"on\" X=\"0\" Y=\"0\" Z=\"0\">{0}</To>\r\n", offset.to_csys_ucs_name) +
                                             String.Format("        </Row>\r\n");
                    else
                    {
                        string prefix = "";
                        foreach (FeatureCAM.FMSetup setup in Variables.doc.Setups)
                            if (offset.to_csys_ucs_name.Equals(setup.ucs.Name))
                                prefix = (setup.Spindle == tagFMSetupSpindleType.eSST_Main ? "(Main) " : "(Sub) ");
                        new_component_str += String.Format("        <Row Index=\"{0}\" SubIndex=\"1\" Auto=\"auto\">\r\n", offset.register) +
                                             String.Format("          <System>{0}</System>\r\n", offset.subsystem) +
                                             String.Format("          <From CSystem=\"off\" X=\"0\" Y=\"0\" Z=\"0\">{0}</From>\r\n", offset.from_component) +
                                             String.Format("          <To CSystem=\"on\" X=\"0\" Y=\"0\" Z=\"0\">{0}</To>\r\n", prefix + offset.to_csys_ucs_name) +
                                             String.Format("        </Row>\r\n");
                    }
                }
                new_component_str += String.Format("      </Table>\r\n");
            }
            if (prog_zero_offsets.Count > 0)
            {
                new_component_str += (new_component_str != "" ? Environment.NewLine : "") +
                                     "      <Table Name=\"Program Zero\">\r\n";
		foreach (WorkOffset offset in prog_zero_offsets)
                {
                    new_component_str += String.Format("        <Row Index=\"{0}\" SubIndex=\"1\" Auto=\"auto\">\r\n", offset.register) +
                                         String.Format("          <System>{0}</System>\r\n", offset.subsystem) +
                                         String.Format("          <From CSystem=\"off\" X=\"0\" Y=\"0\" Z=\"0\">{0}</From>\r\n", offset.from_component) +
                                         String.Format("          <To CSystem=\"on\" X=\"0\" Y=\"0\" Z=\"0\">{0}</To>\r\n", offset.to_csys_ucs_name) +
                                         String.Format("        </Row>\r\n");
                }
                new_component_str += String.Format("      </Table>\r\n");
            }
            insert_at = mod_setup_info.IndexOf("<GCode", StringComparison.OrdinalIgnoreCase);
            if (insert_at > 0)
            {
                insert_at = mod_setup_info.IndexOf(">" + Environment.NewLine, insert_at);
                if (insert_at > 0)
                    mod_setup_info = mod_setup_info.Insert(insert_at + Environment.NewLine.Length, new_component_str);
            }
        }

       
        private static void SortOffsets(List<WorkOffset> all_offsets, out List<WorkOffset> work_offsets, out List<WorkOffset> prog_zero_offsets)
        {
            work_offsets = new List<WorkOffset>();
            prog_zero_offsets = new List<WorkOffset>();

            if (all_offsets == null) return;
            if (all_offsets.Count == 0) return;

            foreach (WorkOffset offset in all_offsets)
            {
                if (offset.type == 0)
                    work_offsets.Add(offset);
                else
                    prog_zero_offsets.Add(offset);
            }
        }


        private static void FindComponentDescription(string text, out int pos_s, out int pos_e,
                                                     string[] keywords)
        {
            int pos1, pos2, start_search_pos;
            string temp_text;

            pos_s = pos_e = -1;
            start_search_pos = 0;
            pos1 = text.IndexOf("<Component", start_search_pos, StringComparison.OrdinalIgnoreCase);
            while (pos1 > 0)
            {
                pos2 = text.IndexOf("</Component>", pos1, StringComparison.OrdinalIgnoreCase);
                if (pos2 > 0)
                {
                    temp_text = text.Substring(pos1, pos2 - pos1 + 12);
                    int ki = 0;
                    bool all_keywords_found = true;
                    while (ki < keywords.Length && all_keywords_found)
                    {
                        all_keywords_found &= (temp_text.IndexOf(keywords[ki], StringComparison.OrdinalIgnoreCase) > 0);
                        ki++;
                    }
                    if (ki == keywords.Length && all_keywords_found)
                    {
                        pos_s = pos1;
                        pos_e = pos2 + 12;
                        return;
                    }
                }
                start_search_pos = pos2;
                pos1 = text.IndexOf("<Component", start_search_pos, StringComparison.OrdinalIgnoreCase);
            }
        }

        private static void FindComponentDescription(List<string> lines, string xml_tag,
                                    int start_search_line_num, int end_search_line_num,
                                    out int desc_start_line, out int desc_end_line)
        {
            int line_ind;

            desc_start_line = -1;
            desc_end_line = -1;

            try
            {
                xml_tag = xml_tag.ToUpper();

                line_ind = start_search_line_num;
                if (end_search_line_num == 0)
                    end_search_line_num = lines.Count;
                while (line_ind <= end_search_line_num && (desc_start_line == -1 || desc_end_line == -1))
                {
                    if (lines[line_ind].IndexOf("<" + xml_tag, StringComparison.OrdinalIgnoreCase) >= 0)
                        desc_start_line = line_ind;
                    if (lines[line_ind].IndexOf("</" + xml_tag, StringComparison.OrdinalIgnoreCase) >= 0)
                        desc_end_line = line_ind;
                    line_ind++;
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "FindComponentDescription");
            }
        }

        private static string FindXMLBlock(string new_component_str, string xml_tag)
        {
            int pos_s, pos_e;

            return FindXMLBlock(new_component_str, xml_tag, out pos_s, out pos_e);
        }

        private static string FindXMLBlock(string new_component_str, string xml_tag, out int pos_s, out int pos_e)
        {
            string tmp = "";

            pos_s = new_component_str.IndexOf("<" + xml_tag, StringComparison.OrdinalIgnoreCase);
            pos_e = new_component_str.IndexOf("</" + xml_tag + ">", StringComparison.OrdinalIgnoreCase);
            if (pos_s < 0 || pos_e < 0) return "";
            if (pos_e > 0)
            {
                pos_e += xml_tag.Length + 3;
                tmp = new_component_str.Substring(pos_s, pos_e - pos_s);
            }

            return tmp;
        }

        private static List<List<SolidInfo>> SortSolidsByAttachComponents(List<SolidInfo> solids)
        {
            List<List<SolidInfo>> sorted_solids;
            int i;
            bool solid_added;

            if (solids == null) return null;
            if (solids.Count == 0) return null;

            sorted_solids = new List<List<SolidInfo>>();

            foreach (SolidInfo solid in solids)
            {
                i = 0;
                solid_added = false;
                while (i < sorted_solids.Count && !solid_added)
                {
                    if (sorted_solids[i][0].attach_to.Equals(solid.attach_to, StringComparison.OrdinalIgnoreCase))
                    {
                        sorted_solids[i].Add(solid);
                        solid_added = true;
                    }
                    i++;
                }
                if (i == sorted_solids.Count && !solid_added)
                {
                    sorted_solids.Add(new List<SolidInfo>());
                    sorted_solids[sorted_solids.Count - 1].Add(solid);
                }
            }

            return sorted_solids;
        }

    }
}
