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
using System.Threading;
using System.Windows.Forms;

namespace FeatureCAMToVericut
{
    class MessageDisplay
    {
        public static void ShowMessage(string message)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    message,
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                );
            })).Start();
        }

        public static void ShowMessage(string message, object[] args)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    String.Format(message, args),
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                );
            })).Start();
        }

        public static void ShowMessage(string message, string arg1)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    String.Format(message, arg1),
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                );
            })).Start();
        }


        public static void ShowError(string message)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    message,
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error
                );
            })).Start();
        }
        
        public static void ShowError(string message, object[] args)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    String.Format(message, args),
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error
                );
            })).Start();
        }

        public static void ShowError(string message, string arg1)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    String.Format(message, arg1),
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error
                );
            })).Start();
        }


        public static void ShowWarning(string message)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    message,
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
            })).Start();
        }

        public static void ShowWarning(string message, object[] args)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    String.Format(message, args),
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
            })).Start();
        }

        public static void ShowWarning(string message, string arg1)
        {
            new Thread(new ThreadStart(delegate
            {
                MessageBox.Show
                (
                    String.Format(message, arg1),
                    Variables.prog_name,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
            })).Start();
        }

    }
}
