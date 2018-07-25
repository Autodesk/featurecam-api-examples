// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace FeatureCAMToCAMplete
{

    public interface _FeatureCAMToCAMplete
    {
        [DispId(1)]
        void Init(FeatureCAM.Application app);
        [DispId(2)]
        void ExportData();
        [DispId(3)]
        void Disconnect();
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IFeatureCAMToCAMplete
    {
    }

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IFeatureCAMToCAMplete))]
    public class FeatureCAMToCAMplete : _FeatureCAMToCAMplete
    {
        private FeatureCAM.Application _app;
        private string tb_name = "Macros";
        private string tb_icon_name = "FeatureCAMToCAMplete";

        public FeatureCAMToCAMplete() { }

        public void Init(FeatureCAM.Application app)
        {
            const int WHATEVER_VERSION_I_COMPILED_AGAINST = 5;

            _app = app;

            string warning1, warning2;

            try
            {
                warning1 = String.Format("Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.", WHATEVER_VERSION_I_COMPILED_AGAINST);
                // the featurecam tlb version is a property on the app now adays...
                // if the app doesn't have that propety, the next line will throw a com exception
                if (app.MajorTLBVersionNum != WHATEVER_VERSION_I_COMPILED_AGAINST)
                    MessageBox.Show(warning1);
            }
            catch
            {
                warning2 = String.Format("Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.", WHATEVER_VERSION_I_COMPILED_AGAINST);
                // must be tlb version 1?
                MessageBox.Show(warning2);
            }


            AddToolbarButton(app);
            Variables.app = app;
            InitializeVariables();
        }

        public void Disconnect()
        {
            RemoveToolbarButton(_app);
            _app = null;
            Marshal.FinalReleaseComObject(Variables.app);
            GC.Collect();
            System.Threading.Thread.Sleep(1000);
        }

        public void ExportData()
        {
            /* Get input (what to import) */
            InitializeVariables();
            if (Variables.doc == null)
                MessageBox.Show("No documents are open. Cannot continue.", Variables.prog_name);
            UI form = new UI();
            form.Show();
            form.TopLevel = true;
            form.TopMost = true;
        }

        private void AddToolbarButton(FeatureCAM.Application app)
        {
            FeatureCAM.FMCmdBars bars;
            FeatureCAM.FMCmdBar bar;
            FeatureCAM.FMCmdBarCtrl ctrl;

            try
            {
                if (app == null) return;

                bars = app.CommandBars;
                bar = (FeatureCAM.FMCmdBar)bars.Item(tb_name);
                if (bar == null)
                    bar = bars.Add(tb_name, Type.Missing, Type.Missing);

                ctrl = (FeatureCAM.FMCmdBarCtrl)bar.Controls.Item(tb_icon_name);
                if (ctrl == null)
                    ctrl = (FeatureCAM.FMCmdBarCtrl)bar.Controls.Add(Type.Missing, Type.Missing, tb_icon_name, Type.Missing);
                bar.Visible = true;
            }
            catch
            {
                MessageBox.Show("Failed to add toolbar button for FeatureCAM to CAMplete macro");
            }
            finally
            {
                ctrl = null;
                bar = null;
                bars = null;
                app = null;
            }
        }

        private void RemoveToolbarButton(FeatureCAM.Application app)
        {
            FeatureCAM.FMCmdBars bars;
            FeatureCAM.FMCmdBar bar;
            FeatureCAM.FMCmdBarCtrl ctrl;

            try
            {
                if (app == null) return;

                bars = app.CommandBars;
                bar = (FeatureCAM.FMCmdBar)bars.Item(tb_name);
                if (bar == null) return;

                ctrl = (FeatureCAM.FMCmdBarCtrl)bar.Controls.Item(tb_icon_name);
                if (ctrl != null) ctrl.Delete();
            }
            catch
            {
                MessageBox.Show("Failed to remove FeatureCAM to CAMplete macro toolbar button");
            }
            finally
            {
                ctrl = null;
                bar = null;
                bars = null;
                app = null;
                GC.Collect();
            }
        }

        public void InitializeVariables()
        {
            FeatureCAM.FMSetup setup = null;

            if (Variables.app != null)
                Variables.doc = (FeatureCAM.FMDocument)Variables.app.ActiveDocument;
            if (Variables.doc == null)
            {
                Variables.prev_doc_name = "";
                Variables.output_dirpath = "";
            }
            else
            {
                Variables.stock = Variables.doc.Stock;
                Variables.setup_names = new List<string>();
                for (int i = 1; i <= Variables.doc.Setups.Count; i++)
                {
                    setup = (FeatureCAM.FMSetup)Variables.doc.Setups.Item(i);
                    if (setup != null)
                    {
                        Variables.setup_names.Add(setup.Name);
                        /* Have to subtract 1 b/c setups are 1-based and combobox values are 0-based */
                        if (Variables.doc.ActiveSetup.Name == setup.Name)
                            Variables.selected_setup_id = i-1;
                    }
                }
                Variables.orig_single_stock = Variables.stock.SingleProgramWithProgramStop;
                if (Variables.prev_doc_name != Variables.doc.Name)
                {
                    Variables.clamps = new List<SolidInfo>();
                    foreach (FeatureCAM.FMSolid solid in Variables.doc.Solids)
                        Variables.clamps.Add(new SolidInfo(solid, solid.UseAsClamp));
                    if (Variables.stock.IndexType != FeatureCAM.tagFMIndexType.eIT_None)
                        Variables.output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName);
                    else
                        Variables.output_dirpath = Path.Combine(Variables.doc.path, Variables.doc.PartName) + "_" + Variables.setup_names[Variables.selected_setup_id];
                }
                Variables.doc.ActiveSetup.GetMachineSimLocation(out Variables.offset_x, out Variables.offset_y, out Variables.offset_z);

                Variables.prev_doc_name = Variables.doc.Name;
            }
        }
    }

}
