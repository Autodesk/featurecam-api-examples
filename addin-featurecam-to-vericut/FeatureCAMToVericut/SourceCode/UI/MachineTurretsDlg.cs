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
    public partial class MachineTurretsDlg : Form
    {
        public List<TurretInfo> updated_turrets;
        private const string ltss = "Lower turret, Sub Spindle side";
        private const string utss = "Upper turret, Sub Spindle side";
        private const string ltms = "Lower turret, Main Spindle side";
        private const string utms = "Upper turret, Main Spindle side";

        public MachineTurretsDlg(List<TurretInfo> turrets, List<string> subsystems)
        {
            InitializeComponent();

            this.Text = LanguageSupport.Translate(this.Text);
            btn_Ok.Text = LanguageSupport.Translate(btn_Ok.Text);
            btn_Cancel.Text = LanguageSupport.Translate(btn_Cancel.Text);
            clm_turret_name.HeaderText = LanguageSupport.Translate(clm_turret_name.HeaderText);
            clm_type.HeaderText = LanguageSupport.Translate(clm_type.HeaderText);
            clm_subsystem.HeaderText = LanguageSupport.Translate(clm_subsystem.HeaderText);
            clm_type.Items.Clear();
            clm_type.Items.AddRange(new object[] {
                                                    LanguageSupport.Translate("Milling head"),
                                                    LanguageSupport.Translate("Turret")
                                                 });
            
            SetDefaults(turrets, subsystems);
        }

        public void SetDefaults(List<TurretInfo> turrets, List<string> subsystems)
        {
            if (turrets == null) return;
            if (turrets.Count == 0) return;

            clm_subsystem.Items.Clear();
            if (subsystems != null)
                foreach (string subsystem in subsystems)
                    clm_subsystem.Items.Add(subsystem);

            dg_turrets.Rows.Clear();

            foreach (TurretInfo turret_info in turrets)
            {
                if (turret_info.available)
                    switch (turret_info.type)
                    {
                        case FeatureCAM.tagFMTurretIDType.eTIDT_SubLower:
                            dg_turrets.Rows.Add(LanguageSupport.Translate(ltss), clm_type.Items[(turret_info.b_axis ? 0 : 1)]);
                            break;
                        case FeatureCAM.tagFMTurretIDType.eTIDT_SubUpper:
                            dg_turrets.Rows.Add(LanguageSupport.Translate(utss), clm_type.Items[(turret_info.b_axis ? 0 : 1)]);
                            break;
                        case FeatureCAM.tagFMTurretIDType.eTIDT_MainLower:
                            dg_turrets.Rows.Add(LanguageSupport.Translate(ltms), clm_type.Items[(turret_info.b_axis ? 0 : 1)]);
                            break;
                        case FeatureCAM.tagFMTurretIDType.eTIDT_MainUpper:
                            dg_turrets.Rows.Add(LanguageSupport.Translate(utms), clm_type.Items[(turret_info.b_axis ? 0 : 1)]);
                            break;
                    }
            }
        }

        private void GetTurretInfoFromForm()
        {
            string ttype = "",
                   subsystem = "";
            updated_turrets = new List<TurretInfo>();

            for (int i = 0; i < dg_turrets.Rows.Count; i++)
            {
                ttype = "";
                subsystem = "";
                if (dg_turrets.Rows[i].Cells[1].Value != null)
                    ttype = dg_turrets.Rows[i].Cells[1].Value.ToString();
                if (dg_turrets.Rows[i].Cells[2].Value != null)
                    subsystem = dg_turrets.Rows[i].Cells[2].Value.ToString();
                switch (dg_turrets.Rows[i].Cells[0].Value.ToString())
                {
                    case ltss:
                        updated_turrets.Add(new TurretInfo(FeatureCAM.tagFMTurretIDType.eTIDT_SubLower, true,
                                                           ttype.ToString().Equals(clm_type.Items[0]),
                                                           subsystem));
                        break;
                    case utss:
                        updated_turrets.Add(new TurretInfo(FeatureCAM.tagFMTurretIDType.eTIDT_SubUpper, true, 
                                                           ttype.Equals(clm_type.Items[0]),
                                                           subsystem));
                        break;
                    case ltms:
                        updated_turrets.Add(new TurretInfo(FeatureCAM.tagFMTurretIDType.eTIDT_MainLower, true, 
                                                           ttype.Equals(clm_type.Items[0]),
                                                           subsystem));
                        break;
                    case utms:
                        updated_turrets.Add(new TurretInfo(FeatureCAM.tagFMTurretIDType.eTIDT_MainUpper, true, 
                                                           ttype.Equals(clm_type.Items[0]),
                                                           subsystem));
                        break;
                }
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            GetTurretInfoFromForm();
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
