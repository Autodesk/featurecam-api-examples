// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Resources;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ExtractTranslationStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            string input_fpath = @"C:\Users\pam_us\Documents\SVN\dev\Macros\ProjectVerification\FeatureCAMToVericut\Localization_SourceCode\featurecam_to_vericut_Fra.po";
            string lang_code;
            string output_fpath, 
                   po_fcontent;
            int code_page = -1, 
                msgid_ind, msgstr_ind;
            string eng_str, translated_str;
            string cs_fontent = "";

            lang_code = Path.GetFileNameWithoutExtension(input_fpath);
            lang_code = lang_code.Substring(lang_code.LastIndexOf("_") + 1);
            output_fpath = String.Format(@"C:\Users\pam_us\Documents\SVN\dev\Macros\ProjectVerification\FeatureCAMToVericut\Localization_SourceCode\l.{0}\Class1.cs", lang_code);
            switch (lang_code.ToUpper())
            {
                case "CHS":
                    code_page = 936;
                    break;
                case "JPN":
                    code_page = 932;
                    break;
                case "ESP":
                case "ITA":
                case "PTB":
                case "FRA":
                case "DEU":
                    code_page = 28591;
                    break;
                case "RUS":
                    code_page = 1251;
                    break;
            }
            if (code_page == -1) return;
            po_fcontent = File.ReadAllText(input_fpath, Encoding.GetEncoding(code_page));  
            msgid_ind = po_fcontent.IndexOf("msgid");
            cs_fontent = "using System;" + Environment.NewLine +
            "using System.Collections.Generic;" + Environment.NewLine +
            "using System.Text;" + Environment.NewLine +
            Environment.NewLine +
            "namespace StringTable_Local" + Environment.NewLine +
            "{" + Environment.NewLine +
            "    public class StringTable" + Environment.NewLine +
            "    {" + Environment.NewLine +
            "        Dictionary<string, string> strings = null;" + Environment.NewLine +
            Environment.NewLine +
            "        public Dictionary<string, string> GetAll()" + Environment.NewLine +
            "        {" + Environment.NewLine +
            "            return this.strings;" + Environment.NewLine +
            "        }" + Environment.NewLine +
            Environment.NewLine +
            "        public StringTable()" + Environment.NewLine +
            "        {" + Environment.NewLine +
            "            this.strings = new Dictionary<string, string>()" + Environment.NewLine +
            "            {" + Environment.NewLine;

            while (msgid_ind > 0)
            {
                eng_str = po_fcontent.Substring(msgid_ind + 6, po_fcontent.IndexOf(Environment.NewLine, msgid_ind) - (msgid_ind + 6));
                msgstr_ind = po_fcontent.IndexOf("msgstr", msgid_ind);
                translated_str = po_fcontent.Substring(msgstr_ind + 7, po_fcontent.IndexOf(Environment.NewLine, msgstr_ind) - (msgstr_ind + 7));
                po_fcontent = po_fcontent.Substring(po_fcontent.IndexOf(Environment.NewLine, msgstr_ind));
                msgid_ind = po_fcontent.IndexOf("msgid");

                cs_fontent += "                {" + Environment.NewLine +
                             "                " + eng_str + "," + Environment.NewLine +
                             "                " + translated_str + Environment.NewLine +
                             "                }";
                if (msgid_ind > 0)
                    cs_fontent += ",";
                cs_fontent += Environment.NewLine;
                
            }
            cs_fontent += "            };" + Environment.NewLine +
                          "        }" + Environment.NewLine +
                          "    }" + Environment.NewLine +
                          "}" + Environment.NewLine;


            File.WriteAllText(output_fpath, cs_fontent);
        }
    }
}