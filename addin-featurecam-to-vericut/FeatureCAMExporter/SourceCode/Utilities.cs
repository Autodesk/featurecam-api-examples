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

namespace FeatureCAMExporter
{
    public class Utilities
    {
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

        public static double UnitsToUnits(double value, bool is_orig_value_metric, bool is_res_value_metric)
        {
            if (is_orig_value_metric && !is_res_value_metric)
                return value / 25.4;
            else if (!is_orig_value_metric && is_res_value_metric)
                return value * 25.4;
            else
                return value;
        }

        public static string Indent(string str, int indentation)
        {
            string indent_str = "";

            indent_str = "".PadLeft(2 * indentation);
            //for (int i = 1; i <= indentation; i++)
            //    indent_str += "  ";

            str = str.Replace("\n", "\n" + indent_str);
            str = str.TrimEnd(' ');

            return indent_str + str;
        }

        public static string String2XML(string tag, string value)
        {
            if (value.IndexOf("\n") >= 0 || value.Length > 60 || value.IndexOf("<") >= 0)
                value = "\n" + Indent(value, 1) + "\n";

            return String.Format("<{0}>{1}</{0}>", tag, value);
        }

        public static string String2XML(string tag, string value, int indentation)
        {
            string str;

            str = String2XML(tag, value);
            str = Indent(str, indentation);

            return str;
        }

        public static string String2XML(string tag, double value)
        {
            return String2XML(tag, value, 0);
        }

        public static string String2XML(string tag, double value, int indentation)
        {
            return String2XML(tag, String.Format("{0:0.####}", value), indentation);
        }

        public static string String2XML(string tag, int value)
        {
            return String2XML(tag, value, 0);
        }

        public static string String2XML(string tag, int value, int indentation)
        {
            string val = String.Format("{0}", value);

            return String2XML(tag, val, indentation);
        }

        public static string GetRelativePath(string absolute_path, string path_to_exclude)
        {
            int pos;
            string rel_path = absolute_path;

            pos = absolute_path.IndexOf(path_to_exclude, StringComparison.OrdinalIgnoreCase);
            if (pos >= 0)
                rel_path = absolute_path.Substring(pos + path_to_exclude.Length);
            if (rel_path.StartsWith(Path.DirectorySeparatorChar.ToString()))
                rel_path = rel_path.Substring(Path.DirectorySeparatorChar.ToString().Length);

            return rel_path;
        }


    }
}
