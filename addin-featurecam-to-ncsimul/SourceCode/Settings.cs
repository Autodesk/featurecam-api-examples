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
using System.Text;

namespace FeatureCAMToNCSIMUL
{

    public enum tagFormat
    {
        tF_FileName,
        tF_CNCFileName,
        tF_MachineName,
        tF_Title,
        tF_SetupName,
    };

    public class Format
    {
        public string display_name;
        public string eng_name;
        public string cb_name;
        public string ini_file_keyword;
        public tagFormat id;

        public Format(tagFormat Id)
        {
            switch (Id)
            {
                case tagFormat.tF_CNCFileName:
                    this.display_name = LanguageSupport.Translate(Properties.Resources.str_post_name);
                    this.eng_name = Properties.Resources.str_post_name;
                    this.ini_file_keyword = Settings.str_ini_format_cnc_file_name;
                    this.id = Id;
                    break;
                case tagFormat.tF_FileName:
                    this.display_name = LanguageSupport.Translate(Properties.Resources.str_file_name);
                    this.eng_name = Properties.Resources.str_file_name;
                    this.ini_file_keyword = Settings.str_ini_format_file_name;
                    this.id = Id;
                    break;
                case tagFormat.tF_MachineName:
                    this.display_name = LanguageSupport.Translate(Properties.Resources.str_machine_name);
                    this.eng_name = Properties.Resources.str_machine_name;
                    this.ini_file_keyword = Settings.str_ini_format_machine_name;
                    this.id = Id;
                    break;
                case tagFormat.tF_SetupName:
                    this.display_name = LanguageSupport.Translate(Properties.Resources.str_setup_name);
                    this.eng_name = Properties.Resources.str_setup_name;
                    this.ini_file_keyword = Settings.str_ini_format_setup_name;
                    this.id = Id;
                    break;
                case tagFormat.tF_Title:
                    this.display_name = LanguageSupport.Translate(Properties.Resources.str_title);
                    this.eng_name = Properties.Resources.str_title;
                    this.ini_file_keyword = Settings.str_ini_format_title;
                    this.id = Id;
                    break;
            }

        }

        public string DisplayName
        {
            get
            {
                return display_name;
            }
        }

        public tagFormat Id
        {
            get
            {
                return id;
            }
        }
    }

    static class Settings
    {
        public const string str_ini_format_file_name = "file_name";
        public const string str_ini_format_cnc_file_name = "cnc_file_name"; 
        public const string str_ini_format_machine_name = "machine_name"; 
        public const string str_ini_format_setup_name = "setup_name";
        public const string str_ini_format_title = "title";
        public const string str_ini_file_format = "FileFormat=";
        public const string str_ini_save_to = "SaveToFileFolder=";
        public const string str_ini_create_subdir = "CreateSubdirectory=";
        public const string str_ini_alt_output_dir = "OtherOutputDirPath=";
        public const string str_ini_machine_file = "NCSIMULMDFilePath=";
        public const string str_ini_tool_id = "ToolID=";
        public const string str_ini_use_datum = "UseDATUM=";
        public const string str_ini_export_tool_rad_comp = "ExportToolRadiusComp=";
        public const string str_ini_export_tool_len_comp = "ExportToolLengthComp=";
        public const string ini_fname = "ncsimul_addin_ui.ini";


        public static bool is_include_fname_in_subdir_name = true,
                           is_include_cnc_name_in_subdir_name = false,
                           is_include_title_in_subdir_name = false,
                           is_include_machine_name_in_subdir_name = false,
                           is_include_setup_name_in_subdir_name = false;
        public static bool is_file_dir,
                           is_create_subdir,
                           is_use_DATUM = false,
                           is_export_tool_rad_compensation,
                           is_export_tool_len_compensation;
        public static List<Format> subdir_format1;
        public static int tool_identification = 0;
        public static string ncsimul_md_fpath = "",
                             alt_output_dirpath = "";

        public static void SetSubDirectoryFormat1(List<Format> format)
        {
            subdir_format1 = format;
        }

        /* Extract format from .ini file. If file doesn't exist, use default format. */
        public static void ReadSettingsFromAddinIniFile()
        {
            string ini_fpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"FeatureCAM\" + ini_fname);

            if (File.Exists(ini_fpath))
                ReadSettingsFromIniFile(ini_fpath);
            else
                SetDefaultSettings();

        }

        private static void ReadSettingsFromIniFile(string fpath)
        {
            string[] settings;
            string setting;
            string tmp;
            int index;

            settings = File.ReadAllLines(fpath);

            foreach (string temp_setting in settings)
            {
                setting = temp_setting;
                if (setting.StartsWith(str_ini_file_format))
                {
                    setting = setting.Replace(str_ini_file_format, "");
                    string[] order = setting.Split("+".ToCharArray());
                    if (order != null)
                    {
                        for (int i = 0; i < order.Length; i++)
                        {
                            switch (order[i])
                            {
                                case str_ini_format_file_name:
                                    index = FindFormatIndex(tagFormat.tF_FileName);
                                    if (index < 0)
                                    {
                                        if (subdir_format1 == null) subdir_format1 = new List<Format>();
                                        subdir_format1.Add(new Format(tagFormat.tF_FileName));
                                    }
                                    is_include_fname_in_subdir_name = true;
                                    break;
                                case str_ini_format_cnc_file_name:
                                    index = FindFormatIndex(tagFormat.tF_CNCFileName);
                                    if (index < 0)
                                    {
                                        if (subdir_format1 == null) subdir_format1 = new List<Format>();
                                        subdir_format1.Add(new Format(tagFormat.tF_CNCFileName));
                                    }
                                    is_include_cnc_name_in_subdir_name = true;
                                    break;
                                case str_ini_format_machine_name:
                                    index = FindFormatIndex(tagFormat.tF_MachineName);
                                    if (index < 0)
                                    {
                                        if (subdir_format1 == null) subdir_format1 = new List<Format>();
                                        subdir_format1.Add(new Format(tagFormat.tF_MachineName));
                                    }
                                    is_include_machine_name_in_subdir_name = true;
                                    break;
                                case str_ini_format_setup_name:
                                    index = FindFormatIndex(tagFormat.tF_SetupName);
                                    if (index < 0)
                                    {
                                        if (subdir_format1 == null) subdir_format1 = new List<Format>();
                                        subdir_format1.Add(new Format(tagFormat.tF_SetupName));
                                    }
                                    is_include_setup_name_in_subdir_name = true;
                                    break;
                                case str_ini_format_title:
                                    index = FindFormatIndex(tagFormat.tF_Title);
                                    if (index < 0)
                                    {
                                        if (subdir_format1 == null) subdir_format1 = new List<Format>();
                                        subdir_format1.Add(new Format(tagFormat.tF_Title));
                                    }
                                    is_include_title_in_subdir_name = true;
                                    break;
                            }
                        }
                    }
                }
                else if (setting.StartsWith(str_ini_save_to))
                {
                    tmp = setting.Replace(str_ini_save_to, "");
                    if (tmp != "")
                        is_file_dir = Convert.ToBoolean(tmp);
                    else
                        is_file_dir = true;
                }
                else if (setting.StartsWith(str_ini_create_subdir))
                {
                    tmp = setting.Replace(str_ini_create_subdir, "");
                    if (tmp != "")
                        is_create_subdir = Convert.ToBoolean(tmp);
                    else
                        is_create_subdir = true;
                }
                else if (setting.StartsWith(str_ini_alt_output_dir))
                {
                    tmp = setting.Replace(str_ini_alt_output_dir, "");
                    alt_output_dirpath = tmp;
                }
                else if (setting.StartsWith(str_ini_machine_file))
                {
                    tmp = setting.Replace(str_ini_machine_file, "");
                    ncsimul_md_fpath = tmp;
                }
                else if (setting.StartsWith(str_ini_tool_id))
                {
                    tmp = setting.Replace(str_ini_tool_id, "");
                    if (tmp != "")
                        tool_identification = Convert.ToInt32(tmp);
                    else
                        tool_identification = 0;
                }
                else if (setting.StartsWith(str_ini_use_datum))
                {
                    tmp = setting.Replace(str_ini_use_datum, "");
                    if (tmp != "")
                        is_use_DATUM = Convert.ToBoolean(tmp);
                    else
                        is_use_DATUM = false;
                }
                else if (setting.StartsWith(str_ini_export_tool_len_comp))
                {
                    tmp = setting.Replace(str_ini_export_tool_len_comp, "");
                    if (tmp != "")
                        is_export_tool_len_compensation = Convert.ToBoolean(tmp);
                    else
                        is_export_tool_len_compensation = false;
                }
                else if (setting.StartsWith(str_ini_export_tool_rad_comp))
                {
                    tmp = setting.Replace(str_ini_export_tool_rad_comp, "");
                    if (tmp != "")
                        is_export_tool_rad_compensation = Convert.ToBoolean(tmp);
                    else
                        is_export_tool_rad_compensation = false;
                }
            }
        }

        public static void SetDefaultSettings()
        {
            subdir_format1 = new List<Format>();
            subdir_format1.Add(new Format(tagFormat.tF_FileName));
            is_include_fname_in_subdir_name = true;
            is_export_tool_len_compensation = false;
            is_export_tool_rad_compensation = false;

            is_file_dir = true;
        }

        public static void SaveSettingsToIniFile()
        {
            string ini_fpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"FeatureCAM\" + ini_fname);
            StringBuilder str_to_write = new StringBuilder();
            string tmp = "";

            if (File.Exists(ini_fpath)) File.Delete(ini_fpath);

            if (subdir_format1 != null)
                if (subdir_format1.Count > 0)
                {
                    tmp = "";
                    foreach (Format format in subdir_format1)
                        tmp += (tmp != "" ? "+" : "") + format.ini_file_keyword;
                    str_to_write.AppendLine(str_ini_file_format + tmp);
                }
            str_to_write.AppendLine(str_ini_save_to + is_file_dir);
            str_to_write.AppendLine(str_ini_create_subdir + is_create_subdir);
            str_to_write.AppendLine(str_ini_alt_output_dir + alt_output_dirpath);
            str_to_write.AppendLine(str_ini_machine_file + ncsimul_md_fpath);
            str_to_write.AppendLine(str_ini_tool_id + tool_identification);
            str_to_write.AppendLine(str_ini_use_datum + is_use_DATUM);
            str_to_write.AppendLine(str_ini_export_tool_rad_comp + is_export_tool_rad_compensation);
            str_to_write.AppendLine(str_ini_export_tool_len_comp + is_export_tool_len_compensation);
            
            File.AppendAllText(ini_fpath, str_to_write.ToString());

        }

        public static int FindFormatIndex(tagFormat id)
        {
            if (subdir_format1 == null) return -1;
            if (subdir_format1.Count == 0) return -1;

            for (int i = 0; i < subdir_format1.Count; i++)
                if (subdir_format1[i].id == id) return i;

            return -1;
        }
    }
}
