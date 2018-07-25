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
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using FeatureCAMExporter;
using FeatureCAM;

[assembly: AssemblyDescriptionAttribute("FeatureCAM to Vericut exporter. Built with FeatureCAM tlb version #4.0")]

namespace FeatureCAMToVericut
{
    public class FCToVericut
    {
        static public MainForm main_form = null;


        static public FeatureCAM.Application Application
        {
            get { return fc; }
            set { fc = value; }
        }
        static private FeatureCAM.Application fc;

        public FCToVericut() { }

        public static void OnConnect(object obj, tagFMAddInFlags flags)
        {
            //path for debug
            //string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            //                           @"..\..\FeatureCAMToVericut\Icons");
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                                       @"Icons");
            fc.CommandBars.CreateCustomButton("Utilities", "FeatureCAMToVericut",
                                              Path.Combine(path, "icon-16.bmp"), Path.Combine(path, "icon-24.bmp"));
            if (flags == tagFMAddInFlags.eAIF_ConnectUserLoad)
            {
            }
        }

        public static void OnDisConnect(tagFMAddInFlags flags)
        {
            fc.CommandBars.DeleteButton("Utilities", "FeatureCAMToVericut");
            if (flags == tagFMAddInFlags.eAIF_DisConnectUserUnLoad)
            {
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static public void FeatureCAMToVericut()
        {
            LanguageSupport.InitializeTranslation(fc.CurrentLanguage, @"Localization");
            CheckTLBCompatibility();
            if (!IsLicensedProperly()) return;

            Variables.doc = (FeatureCAM.FMDocument)fc.ActiveDocument;
            if (Variables.doc == null)
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("No files are open"));
                return;
            }

            if (File.Exists(@"C:\ProgramData\FeatureCAM\vericut_addin.ini"))
                Variables.vericut_fpath = File.ReadAllText(@"C:\ProgramData\FeatureCAM\vericut_addin.ini").Replace("VERICUT_PATH=", "").Trim();

            if (Variables.doc.path != "")
            {
                Variables.doc_ini_fpath = Variables.doc.FullName + ".fcvini";
                LogFile.SetLogFilePath(Variables.doc.FullName + ".log");
                if (File.Exists(Variables.doc_ini_fpath))
                {
                    Variables.doc_options = GetSavedOptions("", Variables.doc_ini_fpath);
                    Variables.doc_options.read_from_file = true;
                }
            }

            Init.InitializeVariables();

            // helper function to force a single instance of plugin form
            if (main_form != null)
            {
                main_form.BringToFront();
            }
            else
            {
                LogFile.Write("Initialize form");
                main_form = new MainForm();
                LogFile.Write("Display form");
                main_form.Show();
                main_form.TopLevel = true;
                main_form.TopMost = true;
                System.Windows.Forms.Application.Run(main_form);
            }
        }

        [STAThread]
        public static void Main()
        {
            object obj = Marshal.GetActiveObject("FeatureCAM.Application");
            if (obj == null) return;

            fc = (FeatureCAM.Application)obj;

            FeatureCAMToVericut();
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
                    MessageDisplay.ShowWarning(
                        LanguageSupport.Translate("Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb."), vnum.Build.ToString());
            }
            catch
            {
                // must be tlb version 1?
                MessageDisplay.ShowWarning(
                    LanguageSupport.Translate("Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM."), vnum.Build.ToString());
            }
        }

        private static bool IsLicensedProperly()
        {
            return true;
        }

        static ProjectOptions GetSavedOptions(string addin_ini_fpath, string fc_doc_ini_fpath)
        {
            LogFile.Write(String.Format("Reading options from {0}", fc_doc_ini_fpath));

            ProjectOptions options = new ProjectOptions();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(options.GetType());
            TextReader reader = new StreamReader(fc_doc_ini_fpath);
            options = (ProjectOptions)serializer.Deserialize(reader);
            reader.Close();

            return options;
        }

    }
}
