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
    public partial class MultiSolidSelectionDlg : Form
    {
        public List<string> selected_solids = null;
        public string attach_to = "";
        public bool export_as_clamp;
        private FeatureCAM.FMDocument fm_doc;

        public MultiSolidSelectionDlg(List<SolidInfo> all_solids, FeatureCAM.FMDocument doc,
                                      List<string> attach_components)
        {
            FeatureCAM.FMSolid fm_solid;
            if (all_solids == null || attach_components == null)
            {
                this.Close();
                return;
            }

            fm_doc = doc;
            InitializeComponent();

            this.cb_solids.Items.Clear();
            foreach (SolidInfo solid in all_solids)
            {
                fm_solid = (FeatureCAM.FMSolid)doc.Solids.Item(solid.name);
                if (fm_solid != null)
                {
                    this.cb_solids.Items.Add(solid.name, fm_solid.Selected);
                }
            }

            this.cb_attach_to.Items.Clear();
            foreach (string component in attach_components)
                this.cb_attach_to.Items.Add(component);
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_SelectPartSolids_Click(object sender, EventArgs e)
        {
            int index;
            if (fm_doc.Solids.Selected.Count == 0) return;

            for (int i = 0; i < cb_solids.Items.Count; i++)
                cb_solids.SetItemChecked(i, false);

            foreach (FeatureCAM.FMSolid fm_solid in fm_doc.Solids.Selected)
            {
                index = cb_solids.Items.IndexOf(fm_solid.Name);
                if (index >= 0)
                    cb_solids.SetItemChecked(index, true);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (cb_solids.CheckedItems.Count == 0) return;

            if (cb_export_as.SelectedItem == null)
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("You have to select whether to export selected solids as clamps or design."));
                return;
            }
            if (cb_attach_to.SelectedItem == null)
            {
                MessageDisplay.ShowError(
                    LanguageSupport.Translate("You have to select attach component."));
                return;
            }

            selected_solids = new List<string>();
            for (int i = 0; i < cb_solids.CheckedItems.Count; i++)
                selected_solids.Add(cb_solids.CheckedItems[i].ToString());
            attach_to = cb_attach_to.SelectedItem.ToString();
            export_as_clamp = (cb_export_as.SelectedIndex == 0);

            fm_doc = null;
            this.Close();
        }
    }
}
