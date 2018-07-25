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
    public partial class ToolOptionsDlg : Form
    {
        public ToolOptionsDlg(eToolOptions selected_tool_option, bool prefix_turret_id, bool all_setups_milling)
        {
            InitializeComponent();

            this.Text = FeatureCAMExporter.LanguageSupport.Translate(this.Text);
            label1.Text = FeatureCAMExporter.LanguageSupport.Translate(label1.Text);
            rb_tool_id.Text = FeatureCAMExporter.LanguageSupport.Translate(rb_tool_id.Text);
            rb_tool_slot.Text = FeatureCAMExporter.LanguageSupport.Translate(rb_tool_slot.Text);
            rb_tool_slot_and_name.Text = FeatureCAMExporter.LanguageSupport.Translate(rb_tool_slot_and_name.Text);
            cb_turret_prefix.Text = FeatureCAMExporter.LanguageSupport.Translate(cb_turret_prefix.Text);
            btn_Cancel.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_Cancel.Text);
            btn_OK.Text = FeatureCAMExporter.LanguageSupport.Translate(btn_OK.Text);

            if (selected_tool_option == eToolOptions.eTO_IDOnly)
                rb_tool_id.Checked = true;
            else if (selected_tool_option == eToolOptions.eTO_PositionAndName)
                rb_tool_slot_and_name.Checked = true;
            else
                rb_tool_slot.Checked = true;

            cb_turret_prefix.Checked = prefix_turret_id;
            if (all_setups_milling)
                cb_turret_prefix.Enabled = false;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (rb_tool_id.Checked)
                Variables.doc_options.tool_id_option = eToolOptions.eTO_IDOnly;
            else if (rb_tool_slot.Checked)
                Variables.doc_options.tool_id_option = eToolOptions.eTO_PositionOnly;
            else
                Variables.doc_options.tool_id_option = eToolOptions.eTO_PositionAndName;

            Variables.doc_options.tool_turret_id_prefix = cb_turret_prefix.Checked;

            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
