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

namespace FeatureCAMToEUREKA
{
    public interface _FeatureCAMToEUREKA
    {
        [DispId(1)]
        void Init(FeatureCAM.Application app);
        [DispId(2)]
        void ExportData();
        [DispId(3)]
        void Disconnect();
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IFeatureCAMToEUREKA
    {
    }

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IFeatureCAMToEUREKA))]
    public class FeatureCAMToEUREKA : _FeatureCAMToEUREKA
    {
        private FeatureCAM.Application _app;
        private string tb_name = "Macros";
        private string tb_icon_name = "FeatureCAMToEUREKA";

        public FeatureCAMToEUREKA() { }

        public void Init(FeatureCAM.Application app)
        {

            const int WHATEVER_VERSION_I_COMPILED_AGAINST = 2;
            string vrsionStr = "2 ";

            _app = app;

            string warning = "Warning: This dll add-in was compiled against v";
            string more = "of FeatureCAM tlb";

            try
            {
                // the featurecam tlb version is a property on the app now adays...
                // if the app doesn't have that propety, the next line will throw a com exception
                if (app.MajorTLBVersionNum != WHATEVER_VERSION_I_COMPILED_AGAINST)
                    MessageBox.Show(warning + vrsionStr + more);
            }
            catch
            {
                // must be tlb version 1?
                MessageBox.Show(warning + vrsionStr + more + " and should not be run with older versions of FeatureCAM.");
            }


            AddToolbarButton(app);
            Variables.app = app;
            InitializeVariables();
        }

        public void Disconnect()
        {
            RemoveToolbarButton(_app);

            _app = null;
            GC.Collect();
            System.Threading.Thread.Sleep(1000);
        }

        public void ExportData()
        {
            /* Get input (what to import) */
            InitializeVariables();
            if (Variables.eureka_path == "")
            {
                MessageBox.Show("Failed to find EUREKA on this computer. Cannot continue.", Variables.prog_name);
                return;
            }
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
                MessageBox.Show("Failed to add toolbar button for FeatureCAM to EUREKA macro");
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
                MessageBox.Show("Failed to remove FeatureCAM to EUREKA macro toolbar button");
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

    }

}
