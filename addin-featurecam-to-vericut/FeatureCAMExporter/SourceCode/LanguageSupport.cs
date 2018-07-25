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
using System.Threading.Tasks;

namespace FeatureCAMExporter
{
    public class LanguageSupport
    {
        private static Dictionary<string, string> translation;

        public static void InitializeTranslation(string current_lang_abbreviation, string folder_path)
        {
            System.Reflection.Assembly dll_assembly;
            string dll_fpath;
            Type type;

            try
            {
                dll_fpath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                                    String.Format(@"{0}\l.{1}\StringTable_Local.dll", folder_path, current_lang_abbreviation));
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
