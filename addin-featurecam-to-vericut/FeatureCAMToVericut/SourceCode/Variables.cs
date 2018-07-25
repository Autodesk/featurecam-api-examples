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
using System.Text;
using FeatureCAM;
using FeatureCAMExporter;
using System.Xml.Serialization;

namespace FeatureCAMToVericut
{
    public enum eToolOptions
    {
        eTO_PositionOnly,
        eTO_PositionAndName,
        eTO_IDOnly
    };

    public class Turret
    {
        public bool available = false;
        public bool is_milling_head = false;

        public Turret() { }
    }

    [Serializable]
    public class ProjectOptions
    {
        [XmlIgnore]
        public bool read_from_file;
        [XmlElement("OutputDirectory")]
        public string output_dirpath;
        [XmlElement("VericutProjectTemplate")]
        public string vc_template_proj_fpath;
        [XmlElement("ExportProject")]
        public bool is_export_project;
        [XmlArray("SetupOptions")]
        public List<SetupOptions> all_setup_options;
        [XmlElement("CombineSetups")]
        public int combine_setups;
        [XmlElement("Transition_UCS")]
        public string trans_ucs;
        [XmlElement("ToolIDOption")]
        public eToolOptions tool_id_option;
        [XmlElement("ToolTurretIDPrefix")]
        public bool tool_turret_id_prefix;
        [XmlIgnore]
        public List<TurretInfo> turrets_info;

        public string doc_fpath;

        public ProjectOptions()
        {
            read_from_file = false;
            output_dirpath = "";
            vc_template_proj_fpath = "";
            trans_ucs = "";
            is_export_project = true;
            combine_setups = -1;
        }

        public static List<string> ucss;

    }

    static class Variables
    {
        public static string prog_name = LanguageSupport.Translate("FeatureCAM to VERICUT");
        public static FeatureCAM.FMDocument doc = null;
        public static ProjectOptions doc_options = new ProjectOptions();
        public static List<SolidInfo> all_solids;
        public static List<SetupInfo> setups_info;
        public static string fname_no_ext = "";
        public static string
            stock_fpath = "",
            prev_doc_name = "",
            vericut_fpath = "", // @"C:\Program Files\CGTech\VERICUT 7.2.3\windows64\commands\vericut.bat",
            vcproj_fpath, 
            doc_ini_fpath,
            unsupported_tool_names = "";
        public static bool
            is_single_program,
            are_all_setups_milling = true;
        public static List<UCS> all_ucss;
        public static List<string> all_fixture_ids;
        public static int num_enabled_setups;


        public static List<bool> GetSaveNCForAllSetups()
        {
            List<bool> ret_vals = new List<bool>();

            if (setups_info == null) return ret_vals;
            if (setups_info.Count == 0) return ret_vals;

            foreach (SetupInfo info in setups_info)
            {
                if (info.options != null)
                    ret_vals.Add(info.options.is_export_nc);
                else
                    ret_vals.Add(false);
            }

            return ret_vals;
        }

        public static List<bool> GetSaveToolsForAllSetups()
        {
            List<bool> ret_vals = new List<bool>();

            if (setups_info == null) return ret_vals;
            if (setups_info.Count == 0) return ret_vals;

            foreach (SetupInfo info in setups_info)
            {
                if (info.options != null)
                    ret_vals.Add(info.options.is_export_tools);
                else
                    ret_vals.Add(false);
            }

            return ret_vals;
        }

        public static List<bool> GetExportStockForAllSetups()
        {
            List<bool> ret_vals = new List<bool>();

            if (setups_info == null) return ret_vals;
            if (setups_info.Count == 0) return ret_vals;

            foreach (SetupInfo info in setups_info)
            {
                if (info.options != null)
                    ret_vals.Add(info.options.is_export_stock);
                else
                    ret_vals.Add(false);
            }

            return ret_vals;
        }

        public static void Cleanup()
        {
            doc = null;

            if (setups_info != null)
            {
                for (int i = 0; i < setups_info.Count; i++)
                    setups_info[i] = null;
                setups_info.Clear();
            }
            setups_info = null;
            num_enabled_setups = 0;

            if (all_solids != null)
            {
                all_solids.Clear();
                all_solids = null;
            }
            if (all_ucss != null)
            {
                all_ucss.Clear();
                all_ucss = null;
            }

            doc_options = null;
        }


    }
}

