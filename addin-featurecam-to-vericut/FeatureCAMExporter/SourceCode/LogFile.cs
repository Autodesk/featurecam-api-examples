// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FeatureCAMExporter
{
    public class LogFile
    {
        private static string log_fpath;

        public static void SetLogFilePath(string fpath)
        {
            log_fpath = fpath;
            DeleteLogFile();
        }

        public static void DeleteLogFile()
        {
            if (log_fpath == "") return;
            if (File.Exists(log_fpath)) File.Delete(log_fpath);
        }

        public static void Write(string message)
        {
            try
            {
                if (message == "") return;
                if (log_fpath == "") return;

                message = String.Format("({0}) {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), message);
                File.AppendAllText(log_fpath, message);
            }
            catch { }
        }

        public static void WriteException(Exception Ex, string Method = "")
        {
            string msg = "";
            try
            {
                if (Method != "") msg += String.Format("Method {0} threw an exception\n", Method);
                if (Ex != null) 
                {
                    msg += String.Format("Exception details: \n{0}\n{1}\n",
                                          Ex.Message,
                                          Ex.StackTrace);
                }
                Write(msg);
            }
            catch { }
        }


    }
}
