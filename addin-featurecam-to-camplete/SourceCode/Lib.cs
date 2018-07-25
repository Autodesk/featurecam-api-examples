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

namespace FeatureCAMToCAMplete
{
    public static class Lib
    {
        public const string EOL = "\r\n";
        public const string tab = "  ";
        public const string double_tab = "    ";
        public const string triple_tab = "      ";
        public const string pt_0_str = "0,0";
        public const string ccw = "CCW", cw = "CW";
        public const string line_no_cut = "|LINE|NOCUT|";
        public const string line_cut = "|LINE|CUT|";
        public const string arc_no_cut = "|ARC|NOCUT|";
        public const string arc_cut = "|ARC|CUT|";
        public const double EPS = 0.000001;

        public static string String2Xml(string tag, object value)
        {
            string val;

            val = Convert.ToString(value);
            if (val.IndexOf(Environment.NewLine) >= 0 || val.Length > 60 || val.IndexOf("<") >= 0)
                val = Environment.NewLine + Indent(val, 1) + Environment.NewLine;

            return "<" + tag + ">" + val + "</" + tag + ">";
        }

        public static string Indent(int indent_times)
        {
            string indent_str = "";

            for (int i = 1; i <= indent_times; i++)
                indent_str += "  ";

            return indent_str;
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
            return angle_degrees * Math.PI / 180;
        }

        public static double Radians2Degrees(double angle_radians)
        {
            return angle_radians * 180 / Math.PI;
        }

        public static double TanDegrees(double angle_degrees)
        {
            return Math.Tan(Degrees2Radians(angle_degrees));
        }

        public static double Inch2MM(double value)
        {
            if (!Variables.doc.Metric)
                return 25.4 * value;
            else
                return value;
        }

        public static double FromUnitsToUnits(double value, bool is_orig_value_metric, bool is_res_value_metric)
        {
            if (is_orig_value_metric && !is_res_value_metric)
                return value / 25.4;
            else if (!is_orig_value_metric && is_res_value_metric)
                return value * 25.4;
            else
                return value;
        }

    }
}
