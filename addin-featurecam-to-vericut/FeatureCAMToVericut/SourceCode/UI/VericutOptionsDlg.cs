// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FeatureCAMToVericut
{
    public partial class VericutOptionsDlg : Form
    {
        public VericutOptionsDlg()
        {
            InitializeComponent();
            
            this.Text = FeatureCAMExporter.LanguageSupport.Translate(this.Text);
            label1.Text = FeatureCAMExporter.LanguageSupport.Translate(label1.Text);
            btn_browse_vericut.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_browse_vericut.Text);
            btn_OK.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_OK.Text);
            btn_Cancel.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_Cancel.Text);

            if (Variables.vericut_fpath != null && Variables.vericut_fpath != "")
                tb_vericut_path.Text = Variables.vericut_fpath;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Variables.vericut_fpath = tb_vericut_path.Text;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_browse_vericut_Click(object sender, EventArgs e)
        {

            vericut_file_browser.Filter = FeatureCAMExporter.LanguageSupport.Translate("VERICUT batch file (*.bat)|*.bat");
            vericut_file_browser.FileName = "";
            vericut_file_browser.CheckFileExists = true;
            vericut_file_browser.Title = FeatureCAMExporter.LanguageSupport.Translate("Select VERICUT batch file");
            if (vericut_file_browser.ShowDialog() == DialogResult.OK)
            {
                tb_vericut_path.Text = vericut_file_browser.FileName;
                Variables.vericut_fpath = tb_vericut_path.Text;
            }

        }

    }
}
