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

namespace FeatureCAMToNCSIMUL
{
    public partial class SubdirectoryOptions : Form
    {
        public SubdirectoryOptions()
        {
            //LanguageSupport.translation = new FeatureCAMToNCSIMUL_Local.StringTable();
            //LanguageSupport.FindAndReadTranslationFile();
            InitializeComponent();
            InitializeTextStrings();
            SetDefaultParams();
        }

        public void InitializeTextStrings()
        {
            LanguageSupport.Translate(lb_include_in_subdir_name, ref LanguageSupport.lb_include_in_subdir_name);
            LanguageSupport.Translate(cb_include_file_name, ref LanguageSupport.cb_include_file_name_text);
            LanguageSupport.Translate(cb_include_project_title, ref LanguageSupport.cb_include_project_title_text);
            LanguageSupport.Translate(cb_include_postprocessor, ref LanguageSupport.cb_include_postprocessor_text);
            LanguageSupport.Translate(cb_include_machine_name, ref LanguageSupport.cb_include_machine_name_text);
            LanguageSupport.Translate(cb_include_setup_name, ref LanguageSupport.cb_include_setup_name_text);
            LanguageSupport.Translate(this, ref LanguageSupport.f_subdir_options_text);
            LanguageSupport.Translate(cb_select_order, ref LanguageSupport.cb_select_order_text);
            LanguageSupport.Translate(b_Up, ref LanguageSupport.b_Up_text);
            LanguageSupport.Translate(b_Down, ref LanguageSupport.b_Down_text);
            LanguageSupport.Translate(b_OK, ref LanguageSupport.b_OK_text);
            LanguageSupport.Translate(b_Apply, ref LanguageSupport.b_Apply_text);
            LanguageSupport.Translate(b_Cancel, ref LanguageSupport.b_Cancel_text);
        }

        private void SetDefaultParams()
        {
            this.Text = LanguageSupport.f_subdir_options_text;
            lb_include_in_subdir_name.Text = LanguageSupport.lb_include_in_subdir_name;
            cb_include_file_name.Text = LanguageSupport.cb_include_file_name_text;
            cb_include_project_title.Text = LanguageSupport.cb_include_project_title_text;
            cb_include_postprocessor.Text = LanguageSupport.cb_include_postprocessor_text;
            cb_include_machine_name.Text = LanguageSupport.cb_include_machine_name_text;
            cb_include_setup_name.Text = LanguageSupport.cb_include_setup_name_text;
            cb_select_order.Text = LanguageSupport.cb_select_order_text;
            b_Up.Text = LanguageSupport.b_Up_text;
            b_Down.Text = LanguageSupport.b_Down_text;
            b_OK.Text = LanguageSupport.b_OK_text;
            b_Apply.Text = LanguageSupport.b_Apply_text;
            b_Cancel.Text = LanguageSupport.b_Cancel_text;

            cb_include_file_name.Checked = Settings.is_include_fname_in_subdir_name;
            cb_include_postprocessor.Checked = Settings.is_include_cnc_name_in_subdir_name;
            cb_include_machine_name.Checked = Settings.is_include_machine_name_in_subdir_name;
            cb_include_setup_name.Checked = Settings.is_include_setup_name_in_subdir_name;
            cb_include_project_title.Checked = Settings.is_include_title_in_subdir_name;
            UpdateFormatList();
        }

        private void b_OK_Click(object sender, EventArgs e)
        {
            Settings.is_include_fname_in_subdir_name = cb_include_file_name.Checked;
            Settings.is_include_cnc_name_in_subdir_name = cb_include_postprocessor.Checked;
            Settings.is_include_machine_name_in_subdir_name = cb_include_machine_name.Checked;
            Settings.is_include_setup_name_in_subdir_name = cb_include_setup_name.Checked;
            Settings.is_include_title_in_subdir_name = cb_include_project_title.Checked;
            this.Close();
            return;
        }

        private void b_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void cb_include_file_name_CheckedChanged(object sender, EventArgs e)
        {
            int index = Settings.FindFormatIndex(tagFormat.tF_FileName);

            if (!cb_include_file_name.Checked)
            {
                if (index >= 0) Settings.subdir_format1.RemoveAt(index);
            }
            if (cb_include_file_name.Checked)
            {
                if (index < 0) Settings.subdir_format1.Add(new Format(tagFormat.tF_FileName));
            }
            UpdateFormatList();
        }

        private void cb_include_postprocessor_CheckedChanged(object sender, EventArgs e)
        {
            int index = Settings.FindFormatIndex(tagFormat.tF_CNCFileName);

            if (!cb_include_postprocessor.Checked)
            {
                if (index >= 0) Settings.subdir_format1.RemoveAt(index);
            }
            if (cb_include_postprocessor.Checked)
            {
                if (index < 0) Settings.subdir_format1.Add(new Format(tagFormat.tF_CNCFileName));
            }

            UpdateFormatList();
        }

        private void cb_include_machine_name_CheckedChanged(object sender, EventArgs e)
        {
            int index = Settings.FindFormatIndex(tagFormat.tF_MachineName);

            if (!cb_include_machine_name.Checked)
            {
                if (index >= 0) Settings.subdir_format1.RemoveAt(index);
            }
            if (cb_include_machine_name.Checked)
            {
                if (index < 0) Settings.subdir_format1.Add(new Format(tagFormat.tF_MachineName));
            }
            UpdateFormatList();
        }

        private void cb_include_project_title_CheckedChanged(object sender, EventArgs e)
        {
            int index = Settings.FindFormatIndex(tagFormat.tF_Title);

            if (!cb_include_project_title.Checked)
            {
                if (index >= 0) Settings.subdir_format1.RemoveAt(index);
            }
            if (cb_include_project_title.Checked)
            {
                if (index < 0) Settings.subdir_format1.Add(new Format(tagFormat.tF_Title));
            }
            UpdateFormatList();
        }

        private void cb_include_setup_name_CheckedChanged(object sender, EventArgs e)
        {
            int index = Settings.FindFormatIndex(tagFormat.tF_SetupName);

            if (!cb_include_setup_name.Checked)
            {
                if (index >= 0) Settings.subdir_format1.RemoveAt(index);
            }
            if (cb_include_setup_name.Checked)
            {
                if (index < 0) Settings.subdir_format1.Add(new Format(tagFormat.tF_SetupName));
            }
            UpdateFormatList();
        }

        private void UpdateFormatList(object selected_item)
        {
            UpdateFormatList();
        }

        private void UpdateFormatList()
        {
            if (Settings.subdir_format1 != null)
            {
                for (int i = 0; i < Settings.subdir_format1.Count; i++)
                    if (Settings.subdir_format1[i].display_name == null)
                        Settings.subdir_format1[i].display_name = Settings.subdir_format1[i].eng_name;
            }
            clb_name_format.DataSource = Settings.subdir_format1;
            clb_name_format.DisplayMember = "DisplayName";
            ((CurrencyManager)clb_name_format.BindingContext[Settings.subdir_format1]).Refresh();
        }

        private void b_Apply_Click(object sender, EventArgs e)
        {
            Settings.is_include_fname_in_subdir_name = cb_include_file_name.Checked;
            Settings.is_include_cnc_name_in_subdir_name = cb_include_postprocessor.Checked;
            Settings.is_include_machine_name_in_subdir_name = cb_include_machine_name.Checked;
            Settings.is_include_setup_name_in_subdir_name = cb_include_setup_name.Checked;
            Settings.is_include_title_in_subdir_name = cb_include_project_title.Checked;
        }

        private void b_Up_Click(object sender, EventArgs e)
        {
            if (clb_name_format.SelectedItem == null)
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_no_Order_list_selection), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                return;
            }

            int i;
            Format temp1;

            i = clb_name_format.SelectedIndex;
            if (i > 0)
            {
                temp1 = Settings.subdir_format1[i - 1];
                Settings.subdir_format1[i - 1] = (Format)clb_name_format.SelectedItem;
                Settings.subdir_format1[i] = temp1;
                clb_name_format.SelectedIndex--;
            }
            UpdateFormatList();
        }

        private void b_Down_Click(object sender, EventArgs e)
        {
            if (clb_name_format.SelectedItem == null)
            {
                MessageBox.Show(LanguageSupport.Translate(Properties.Resources.msg_no_Order_list_selection), LanguageSupport.Translate(Properties.Resources.str_prog_name));
                return;
            }

            List<string> list_items = new List<string>();
            Format temp1;
            
            int i = clb_name_format.SelectedIndex;
            if (i < Settings.subdir_format1.Count - 1)
            {
                temp1 = Settings.subdir_format1[i + 1];
                Settings.subdir_format1[i + 1] = (Format)clb_name_format.SelectedItem;
                Settings.subdir_format1[i] = temp1;
                clb_name_format.SelectedIndex++;
            }
            UpdateFormatList();
        }

    }
}
