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
using FeatureCAMExporter;

namespace FeatureCAMToVericut
{
    public partial class UCSsDlg : Form
    {
        private static int setup_index;

        public UCSsDlg(int selected_setup_index)
        {
            setup_index = selected_setup_index;
            InitializeComponent();

            this.Text = LanguageSupport.Translate(this.Text);
            label2.Text = LanguageSupport.Translate(label2.Text);
            label7.Text = LanguageSupport.Translate(label7.Text);
            btn_OK.Text = LanguageSupport.Translate(btn_OK.Text);
            btn_Cancel.Text = LanguageSupport.Translate(btn_Cancel.Text);

            /* Attach axis */
            cb_attach_ucss_to.Items.Clear();
            if (Variables.setups_info[setup_index].attach_components != null)
                foreach (string attach_comp in Variables.setups_info[setup_index].attach_components)
                    cb_attach_ucss_to.Items.Add(attach_comp);
            if (Variables.setups_info[setup_index].attach_ucss_to != "" && cb_attach_ucss_to.Items.Contains(Variables.setups_info[setup_index].attach_ucss_to))
                cb_attach_ucss_to.SelectedItem = Variables.setups_info[setup_index].attach_ucss_to;

            cb_ucs_location.Items.Clear();
            if (Variables.all_ucss != null)
            {
                foreach (UCS ucs in Variables.all_ucss)
                    cb_ucs_location.Items.Add(ucs.name);
            }
            if (Variables.setups_info[setup_index].attach_ucs != "" && cb_ucs_location.Items.Contains(Variables.setups_info[setup_index].attach_ucs))
                cb_ucs_location.SelectedItem = Variables.setups_info[setup_index].attach_ucs;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Variables.setups_info[setup_index].attach_ucs = cb_ucs_location.Text;
            Variables.setups_info[setup_index].options.ucs_attach = cb_ucs_location.Text;
            Variables.setups_info[setup_index].attach_ucss_to = cb_attach_ucss_to.Text;
            Variables.setups_info[setup_index].options.attach_ucss_to = cb_attach_ucss_to.Text;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
