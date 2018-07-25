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
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FeatureCAM;
using FeatureCAMExporter;

namespace FeatureCAMToVericut
{
    class Exporter
    {
        private static bool IsSetupOptionsAlreadyInTheList(SetupOptions setup_options)
        {
            foreach (SetupOptions saved_setup_options in Variables.doc_options.all_setup_options)
                if (saved_setup_options.setup_name == setup_options.setup_name)
                    return true;

            return false;
        }

        public static void Export()
        {
            try
            {
                if (!Directory.Exists(Variables.doc_options.output_dirpath)) Directory.CreateDirectory(Variables.doc_options.output_dirpath);

                if (!FCExporter.SaveNCCode(FCToVericut.Application, Variables.doc,
                                            Variables.fname_no_ext, ".mcd", Variables.doc_options.output_dirpath,
                                            Variables.setups_info, Variables.GetSaveNCForAllSetups(),
                                            Variables.is_single_program, Variables.doc_options.combine_setups))
                {
                    LogFile.Write("Failed to save nc code");
                    MessageDisplay.ShowError(
                        LanguageSupport.Translate("Abort export: Failed to save NC code. Check for errors in the Op List."));
                    return;
                }

                Variables.doc.InvalidateToolpaths();
                ExportTools(FCToVericut.Application, Variables.doc);
                
                FCExporter.ExportStock((FMStock)Variables.doc.Stock, Variables.setups_info,
                                       Variables.doc_options.output_dirpath);

                /* Each setup has it's own clamps and possibly part solid. So we save them per setup. */
                FCExporter.ExportClamps((FMSolids)Variables.doc.Solids, Variables.setups_info, 
                                       Variables.doc_options.output_dirpath);
                
                FCExporter.ExportDesign((FMSolids)Variables.doc.Solids, Variables.setups_info, 
                                       Variables.doc_options.output_dirpath);

                /* If we need to update projects file, do it now */
                if (Variables.doc_options.is_export_project)
                {
                    VcProject_Manager.ConstructNewVCProjectFile(
                        Variables.setups_info.Count);
                }
                MessageDisplay.ShowMessage(LanguageSupport.Translate("Export completed."));
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export");
            }
            finally
            {
            }
        }

        private static void ExportTools(FeatureCAM.Application app, FMDocument doc)
        {
            StringBuilder tool_info = new StringBuilder(),
                          tls_fcontent = new StringBuilder(),
                          tool_list = new StringBuilder();
            int nc_prog = 1,
                init_num = 1;
            string crib_pos = "",
                   nc_prog_fpath = "";

            try
            {

                /* At the moment we construct full tool list, which can be time consuming.
                 * So if we have a lot of setups that don't get exported, we do have a pointless delay
                 */
                FCExporter.ToolsToList(app, doc, Variables.setups_info, Variables.GetSaveToolsForAllSetups(),
                                       (Variables.doc_options.combine_setups == 1), Variables.doc_options.output_dirpath, Variables.doc.Metric);
                if (Variables.doc_options.combine_setups == 0)
                {
                    for (int si = 0; si < Variables.setups_info.Count; si++)
                    {
                        if (Variables.setups_info[si].options.is_export_tools)
                        {
                            Variables.setups_info[si].tool_fpath = Path.Combine(Variables.doc_options.output_dirpath,
                                                                                Variables.fname_no_ext + "_" + (string)Variables.setups_info[si].name + ".tls");
                            tool_info = new StringBuilder();
                            tls_fcontent = new StringBuilder();
                            tool_list = new StringBuilder();
                            for (int i = 0; i < Variables.setups_info[si].tools.Count; i++)
                            {
                                if (Variables.setups_info[si].tools[i].cutter_geom.Count == 0) continue;
                                if (tool_info.Length > 0) tool_info.AppendLine("");
                                tool_info.Append(
                                    VericutTool.ToXML(Variables.setups_info[si].tools[i], doc.Metric));
                                nc_prog = Convert.ToInt32(Convert.ToString(Variables.setups_info[si].tools[i].turr_type).Replace("eTT_TURRET", ""));
                                nc_prog_fpath = Variables.setups_info[si].nc_fpaths[nc_prog - 1];
                                crib_pos = String.Format("{0}:{1}",
                                                            Convert.ToString(Variables.setups_info[si].tools[i].turr_type).Replace("eTT_TURRET", ""),
                                                            Convert.ToInt32(Variables.setups_info[si].tools[i].id));
                                AppendToToolList(tool_list, nc_prog, init_num, crib_pos, nc_prog_fpath, Variables.setups_info[si].tools[i].optional_id);
                            }
                            Variables.setups_info[si].tool_list = "<ToolChange>" + Environment.NewLine +
                                                                  Utilities.Indent(tool_list.ToString(), 1) +
                                                                  "</ToolChange>";
                            tls_fcontent.AppendLine("<?xml version=\"1.0\"?>");
                            tls_fcontent.AppendLine("<CGTechToolLibrary>");
                            tls_fcontent.AppendLine(Utilities.Indent("<Tools>", 1));
                            tls_fcontent.AppendLine(Utilities.Indent(tool_info.ToString(), 2));
                            tls_fcontent.AppendLine(Utilities.Indent("</Tools>", 1));
                            tls_fcontent.Append("</CGTechToolLibrary>");
                            File.WriteAllText(Variables.setups_info[si].tool_fpath, tls_fcontent.ToString());
                            LogFile.Write(String.Format("Tool info was written to {0}", Variables.setups_info[si].tool_fpath));
                        }
                    }
                }
                else
                {
                    if (Variables.setups_info[0].options.is_export_tools)
                    {
                        for (int si = 0; si < Variables.setups_info.Count; si++)
                            if (Variables.setups_info[si].options.is_export_tools)
                                Variables.setups_info[si].tool_fpath = Path.Combine(Variables.doc_options.output_dirpath,
                                                                                    Variables.fname_no_ext + ".tls");
                        for (int i = 0; i < Variables.setups_info[0].tools.Count; i++)
                        {
                            if (Variables.setups_info[0].tools[i].cutter_geom.Count == 0) continue;
                            tool_info.Append(tool_info.Length > 0 ? "\n" : "");
                            tool_info.Append(
                                VericutTool.ToXML(Variables.setups_info[0].tools[i], Variables.doc.Metric));
                            nc_prog = Convert.ToInt32(Convert.ToString(Variables.setups_info[0].tools[i].turr_type).Replace("eTT_TURRET", ""));
                            nc_prog_fpath = Variables.setups_info[0].nc_fpaths[nc_prog - 1];
                            crib_pos = String.Format("{0}:{1}",
                                                        Convert.ToString(Variables.setups_info[0].tools[i].turr_type).Replace("eTT_TURRET", ""),
                                                        Convert.ToInt32(Variables.setups_info[0].tools[i].id));
                            AppendToToolList(tool_list, nc_prog, init_num, crib_pos, nc_prog_fpath, Variables.setups_info[0].tools[i].optional_id);
                        }
                        Variables.setups_info[0].tool_list = "<ToolChange>" + Environment.NewLine +
                                                             Utilities.Indent(tool_list.ToString(), 1) +
                                                             "</ToolChange>";
                        tls_fcontent.AppendLine("<?xml version=\"1.0\"?>");
                        tls_fcontent.AppendLine("<CGTechToolLibrary>");
                        tls_fcontent.AppendLine(Utilities.Indent("<Tools>", 1));
                        tls_fcontent.AppendLine(Utilities.Indent(tool_info.ToString(), 2));
                        tls_fcontent.AppendLine(Utilities.Indent("</Tools>", 1));
                        tls_fcontent.Append("</CGTechToolLibrary>");
                        File.WriteAllText(Variables.setups_info[0].tool_fpath, tls_fcontent.ToString());
                        LogFile.Write(String.Format("Tool info was written to {0}", Variables.setups_info[0].tool_fpath));
                    }
                }
            }
            catch (Exception Ex)
            {
                LogFile.Write(String.Format("Exception occured during tool export. Exception details: {0}\n", Ex.Message));
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("Failed to export tools"));
            }
        }

        private static void AppendToToolList(StringBuilder tool_list, int nc_prog, int init_num, string crib_pos, string nc_prog_fpath, string tool_id)
        {
            tool_list.AppendLine(String.Format("<Event NCProgram=\"{0}\" Filter=\"on\" Init=\"{1}\">", nc_prog, init_num));
            tool_list.AppendLine(Utilities.Indent(String.Format("<Cutter Ident=\"{0}\">{1}</Cutter>", tool_id, crib_pos), 1));
            tool_list.AppendLine(Utilities.Indent(String.Format("<Holder Ident=\"{0}\"/>", tool_id), 1));
            tool_list.AppendLine(Utilities.Indent(String.Format("<Tool Use=\"other\" Teeth=\"0\" File=\"{0}\" Line=\"0\"></Tool>", nc_prog_fpath), 1));
            tool_list.AppendLine("</Event>");
        }

        public static void SaveOptions(ProjectOptions options, string addin_ini_fpath)
        {
            if (Variables.doc.path == "")
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("Cannot save selected options to the file. File needs to be saved first."));
                LogFile.Write("Cannot save selected options to the file. File needs to be saved first.");
                return;
            }

            File.WriteAllText(@"C:\ProgramData\FeatureCAM\vericut_addin.ini",
                               string.Format("VERICUT_PATH={0}", Variables.vericut_fpath));
            /* This function will save shared options to addin ini file 
             * and 
             * project specific options to doc ini file
             */
            if (options.all_setup_options == null)
                options.all_setup_options = new List<FeatureCAMExporter.SetupOptions>();
            else
                options.all_setup_options.Clear();
            for (int i = 0; i < Variables.setups_info.Count; i++)
            {
                Variables.setups_info[i].options.clamps = Variables.setups_info[i].clamps;
                Variables.setups_info[i].options.parts = Variables.setups_info[i].part;
                Variables.setups_info[i].options.fixture = Variables.setups_info[i].fixture_id;
                Variables.setups_info[i].options.offsets = Variables.setups_info[i].work_offsets;
                Variables.setups_info[i].options.is_subspindle = Variables.setups_info[i].sub_spindle; 

                options.all_setup_options.Add(Variables.setups_info[i].options);
            }
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(options.GetType());
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.NewLineChars = Environment.NewLine;
            settings.NewLineOnAttributes = true;
            settings.Indent = true;
            settings.NewLineHandling = System.Xml.NewLineHandling.Replace;
            settings.OmitXmlDeclaration = true;
            settings.CloseOutput = true;
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(Variables.doc_ini_fpath, settings);
            serializer.Serialize(writer, options);
            writer.Close();
        }

    }
}
