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
    public static class LanguageSupport
    {
        private static Dictionary<string, string> translation;

        public static string label8_text;
        public static string rb_save_to_file_dir_text;
        public static string rb_save_to_other_dir_text;
        public static string b_select_output_dir_text;
        public static string cb_create_subdir_text;
        public static string lb_include_in_subdir_name;
        public static string cb_include_file_name_text;
        public static string cb_include_project_title_text;
        public static string cb_include_postprocessor_text;
        public static string cb_include_machine_name_text;
        public static string cb_include_setup_name_text;
        public static string f_subdir_options_text;
        public static string cb_select_order_text;
        public static string b_Up_text;
        public static string b_Down_text;
        public static string b_OK_text;
        public static string b_Apply_text;
        public static string b_Cancel_text;
        public static string b_subdir_options;
        public static string button1_text;
        public static string label5_text;
        public static string b_select_ncsimul_machine_text;
        public static string label3_text;
        public static string label4_text;
        public static string rb_tool_number_text;
        public static string rb_tool_id_text;
        public static string label2_text;
        public static string label6_text;
        public static string rb_indiv_offsets_text;
        public static string rb_DATUM_text;
        public static string lb_setup_warning_text;
        public static string gb_turret_info_text;
        public static string l_umss_text;
        public static string l_usss_text;
        public static string l_lmss_text;
        public static string l_lsss_text;
        public static string rb_umss_milling_head_text;
        public static string rb_usss_milling_head_text;
        public static string rb_lmss_milling_head_text;
        public static string rb_lsss_milling_head_text;
        public static string rb_umss_turret_text;
        public static string rb_usss_turret_text;
        public static string rb_lmss_turret_text;
        public static string rb_lsss_turret_text;
        public static string b_SaveSettings_text;
        public static string b_export_text;
        public static string b_cancel_text;
        public static string b_help_text;
        public static string cb_tool_rad_as_offset_text;
        public static string cb_tool_length_as_offset_text;
        public static string b_PreviewOffset_text;

        public static void InitializeTranslation(string current_lang_abbreviation)
        {
            System.Reflection.Assembly dll_assembly;
            string dll_fpath;
            Type type;

            try
            {
                dll_fpath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                                    String.Format(@"FeatureCAMToNCSIMUL\Localization\l.{0}\StringTable_Local.dll", current_lang_abbreviation));

                if (!File.Exists(dll_fpath))
                    return;

                dll_assembly = System.Reflection.Assembly.LoadFile(dll_fpath);
                if (dll_assembly == null) return;

                type = dll_assembly.GetType("StringTable_Local.StringTable");
                var table = dll_assembly.CreateInstance("StringTable_Local.StringTable");

                translation = (Dictionary<string, string>)type.InvokeMember("GetAll",
                                  System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod,
                                  null,
                                  table,
                                  null);
            }
            catch
            {
                translation = null;
            }
            finally
            {
                dll_assembly = null;
                type = null;
            }
        }

        public static void Translate(System.Windows.Forms.Control control, ref string text_var)
        {
            if (control == null)
            {
                text_var = "";
                return;
            }

            text_var = Translate(control.Text);
        }

        public static string Translate(string english_string)
        {
            if (translation == null) return english_string;

            if (!translation.ContainsKey(english_string)) return english_string;

            if (!String.IsNullOrEmpty(translation[english_string]))
                return translation[english_string];
            else
                return english_string;
        }

    }
}
