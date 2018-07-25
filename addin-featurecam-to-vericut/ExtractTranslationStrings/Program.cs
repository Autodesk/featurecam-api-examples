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
            string input_fpath = @"C:\SVN\dev\Macros\ProjectVerification\ExtractTranslationStrings\languages.txt";
            string output_fpath;
            string output_fcontent = "";
            string output_translated = "",
                   output_nottranslated = "";
            string[] lang_info;
            List<string> strings_to_add = new List<string>();

            string[] langs = File.ReadAllLines(input_fpath);
            foreach (string lang in langs)
            {
                output_translated = "";
                output_nottranslated = "";
                lang_info = lang.Split(';');
                if (lang_info.Count() != 4) continue;

                strings_to_add.Clear();
                output_fpath = String.Format(@"C:\SVN\dev\Macros\ProjectVerification\FeatureCAMToVericut\Localization\l.{0}\featurecam_to_vericut_{0}.po", lang_info[1]);
                if (File.Exists(output_fpath))
                {
                    output_fcontent = File.ReadAllText(output_fpath);
                    int pos1 = output_fcontent.IndexOf("#: **************** Translated strings *****************");
                    int pos2 = output_fcontent.IndexOf("#: ************** Non translated strings ***************");
                    if (pos1 > pos2)
                    {
                        output_translated = output_fcontent.Substring(pos1);
                        output_nottranslated = output_fcontent.Substring(pos2, pos1 - pos2);
                    }
                    else
                    {
                        output_translated = output_fcontent.Substring(pos1, pos2 - pos1);
                        output_nottranslated = output_fcontent.Substring(pos2);
                    }
                }

                if (output_nottranslated == "")
                    output_nottranslated = "#: ************** Non translated strings ***************" + Environment.NewLine + Environment.NewLine;
                if (output_translated == "")
                    output_translated = "#: **************** Translated strings *****************" + Environment.NewLine + Environment.NewLine;

                //Process all .cs files to get labels
                string[] files = Directory.GetFiles(@"C:\SVN\dev\Macros\ProjectVerification\FeatureCAMToVericut", "*.cs", SearchOption.AllDirectories);
                foreach (string cs_fpath in files)
                {
                    List<string> result = System.IO.File
                                                    .ReadAllLines(cs_fpath)
                                                    .Where(i => i.Contains(".Translate(\""))
                                                    .ToList();
                    if (result.Count > 0)
                    {
                        string tmp;
                        foreach (string subres in result)
                        {
                            tmp = subres.Trim();
                            if (!tmp.Trim().StartsWith("//"))
                            {
                                tmp = tmp.Substring(tmp.IndexOf("Translate(\"") + ("Translate(\"").Length);
                                if (tmp.IndexOf("\"),") > 0)
                                    tmp = tmp.Substring(0, tmp.IndexOf("\"),")) + "\"";
                                else if (tmp.IndexOf("));") > 0)
                                    tmp = tmp.Substring(0, tmp.IndexOf("));"));
                                else if (tmp.IndexOf(");") > 0)
                                    tmp = tmp.Substring(0, tmp.IndexOf(");"));
                                else if (tmp.IndexOf(")") > 0)
                                    tmp = tmp.Substring(0, tmp.IndexOf(")"));
                                tmp = "\"" + tmp;
                                if (!output_fcontent.Contains(tmp))
                                {
                                    if (!strings_to_add.Contains(tmp))
                                        strings_to_add.Add(tmp);
                                }
                            }
                        }
                    }

                    result = System.IO.File
                                    .ReadAllLines(cs_fpath)
                                    .Where(i => i.Contains(".Text = \""))
                                    .ToList();
                    if (result.Count > 0)
                    {
                        string tmp;
                        foreach (string subres in result)
                        {
                            tmp = subres.Trim();
                            if (!tmp.Trim().StartsWith("//"))
                            {
                                tmp = tmp.Substring(tmp.IndexOf(".Text = \"") + (".Text = \"").Length);
                                if (tmp.IndexOf(";") > 0)
                                    tmp = tmp.Substring(0, tmp.IndexOf(";"));
                                if (!output_fcontent.Contains(tmp))
                                {
                                    tmp = "\"" + tmp;
                                    if (!strings_to_add.Contains(tmp))
                                        strings_to_add.Add(tmp);
                                }
                            }
                        }
                    }

                    result = System.IO.File
                                    .ReadAllLines(cs_fpath)
                                    .Where(i => i.Contains(".HeaderText = \""))
                                    .ToList();
                    if (result.Count > 0)
                    {
                        string tmp;
                        foreach (string subres in result)
                        {
                            tmp = subres.Trim();
                            if (!tmp.Trim().StartsWith("//"))
                            {
                                tmp = tmp.Substring(tmp.IndexOf(".HeaderText = \"") + (".HeaderText = \"").Length);
                                if (tmp.IndexOf(";") > 0)
                                    tmp = tmp.Substring(0, tmp.IndexOf(";"));
                                if (!output_fcontent.Contains(tmp))
                                {
                                    tmp = "\"" + tmp;
                                    if (!strings_to_add.Contains(tmp))
                                        strings_to_add.Add(tmp);
                                }
                            }
                        }
                    }

                    result = System.IO.File
                                    .ReadAllLines(cs_fpath)
                                    .Where(i => i.Contains("const string "))
                                    .ToList();
                    if (result.Count > 0)
                    {
                        string tmp;
                        foreach (string subres in result)
                        {
                            tmp = subres.Trim();
                            if (!tmp.Trim().StartsWith("//"))
                            {
                                tmp = tmp.Substring(tmp.IndexOf("\"") + 1);
                                if (tmp.IndexOf("\"") > 0)
                                    tmp = tmp.Substring(0, tmp.IndexOf("\""));
                                if (!output_fcontent.Contains(tmp))
                                {
                                    tmp = "\"" + tmp + "\"";
                                    if (!strings_to_add.Contains(tmp))
                                        strings_to_add.Add(tmp);
                                }
                            }
                        }
                    }
                }

                Console.WriteLine(strings_to_add.Count);
                foreach (string str in strings_to_add)
                {
                    output_nottranslated += String.Format("msgid {0}", str) + Environment.NewLine +
                                 "msgstr \"\"" + Environment.NewLine +
                                 Environment.NewLine;
                }
                if (!Directory.Exists(Path.GetDirectoryName(output_fpath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(output_fpath));
                File.WriteAllText(output_fpath,
                                 String.Format("#, Lang = {0}", lang_info[2]) + Environment.NewLine +
                                 String.Format("<meta http-equiv=\"Content-Type\" content=\"text/html; charset={0}\">", lang_info[3]) + Environment.NewLine +
                                 String.Format("_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss")) + Environment.NewLine +
                                 String.Format("#: file: {0}", Path.GetFileName(output_fpath)) + Environment.NewLine + 
                                 output_nottranslated +
                                 output_translated);
            }
        }
    }
}