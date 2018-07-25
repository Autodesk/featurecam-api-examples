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
using System.Text;

namespace FeatureCAMToVericut
{
    public static class Lib
    {
        public static string String2XML(string tag, object value)
        {
            string val;

            val = Convert.ToString(value);
            if (val.IndexOf(Environment.NewLine) >= 0 || val.Length > 60 || val.IndexOf("<") >= 0)
                val = Environment.NewLine + Indent(val, 1) + Environment.NewLine;

            return "<" + tag + ">" + val + "</" + tag + ">";
        }

        public static string Indent(string str, int indent_times)
        {
            string indent_str = "";

            for (int i = 1; i <= indent_times; i++)
                indent_str += "  ";

            return indent_str + str.Replace(Environment.NewLine, Environment.NewLine + indent_str);
        }

        public static double Degrees2Radians(double angle_degrees)
        {
            return Math.Round(angle_degrees * Math.PI / 180, 4);
        }

        public static double Radians2Degrees(double angle_radians)
        {
            return Math.Round(angle_radians * 180 / Math.PI, 4);
        }

        public static double TanDegrees(double angle_degrees)
        {
            return Math.Round(Math.Tan(Degrees2Radians(angle_degrees)), 4);
        }

    }
}
