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
using System.IO;
using System.Text.RegularExpressions;
using FeatureCAM;

namespace FeatureCAMToNCSIMUL
{
    static class Project_Manager
    {
        /* machine_type = LATHE or MILLING */
        public static void ConstructProjectFile(string nxf_fpath, string machine_type,
                    string machine_name, string tool_info,
                    string part_stl_fpath, int num_of_setups)
        {
            string fcontent = "";

            try
            {
                /* Software (FeatureCAM) identification */
                fcontent += GetSoftwareInfo();

                /* Machine identification and NC tapes to be used */
                fcontent += GetMachineAndNCFileInfo(machine_type, machine_name);

                ///* Set ncs project name */
                //fcontent += SetProjectNCSFileName();

                /* Stock identification */
                fcontent += GetStockInfo();

                /* Setup information */
                fcontent += GetNCCodeInfo();

                /* Clamps information */
                fcontent += GetClampsInfo();

                /* Tool information */
                fcontent += tool_info;

                /* Save result */
                fcontent += "BEGIN_SAVE_STOCK" + Lib.EOL +
                            "  |1|NCMAC|" + "saved_" + Path.GetFileName(Variables.output_dirpath) + "|" + Lib.EOL +
                            "END";

                File.WriteAllText(nxf_fpath, fcontent);
                Variables.output_msg += nxf_fpath + "\n";
            }
            catch (Exception)
            { }
        }

        private static string GetNCCodeInfo()
        {
            string fcontent = "";
            double xx, xy, xz,
                   yx, yy, yz,
                   zx, zy, zz;
            double x = 0, y = 0, z = 0;
            double stock_x, stock_y, stock_z;
            double stock_thickness;
            double stock_bbox_xmin, stock_bbox_xmax,
                   stock_bbox_ymin, stock_bbox_ymax,
                   stock_bbox_zmin, stock_bbox_zmax;

            Variables.stock.GetLocation(out stock_x, out stock_y, out stock_z);
            Variables.stock.BoundingBox(out stock_bbox_xmin, out stock_bbox_ymin, out stock_bbox_zmin,
                            out stock_bbox_xmax, out stock_bbox_ymax, out stock_bbox_zmax,
                            tagFMCoordinateSpace.eCS_World, null);
            stock_thickness = stock_bbox_zmax - stock_bbox_zmin;

            fcontent += "BEGIN_NCFRAME" + Environment.NewLine;

            if (Variables.stock.IndexType == tagFMIndexType.eIT_None)
            {
                Variables.doc.ActiveSetup.ucs.GetVectors(out xx, out xy, out xz,
                                                         out yx, out yy, out yz,
                                                         out zx, out zy, out zz);
                double tempx, tempy, tempz;
                tempx = tempy = tempz = 0;
                if (xx == 1 && xy == 0 && xz == 0 &&
                    yx == 0 && yy == 1 && yz == 0)
                    tempz = z + stock_bbox_zmin;
                (Variables.doc.Setups.Item(Variables.selected_setup_id + 1)).MapWorldToSetup(ref tempx, ref tempy, ref tempz);
                fcontent += Lib.tab + "|FRAME|G" + Variables.doc.ActiveSetup.FixtureID + "|" +
                                                    Math.Round(Lib.FromUnitsToUnits(Variables.offset_x, Variables.doc.Metric, true), 4) + "," +
                                                    Math.Round(Lib.FromUnitsToUnits(Variables.offset_y, Variables.doc.Metric, true), 4) + "," +
                                                    Math.Round(Lib.FromUnitsToUnits(-1*Variables.offset_z, Variables.doc.Metric, true), 4) + "," +
                            Math.Round(1.0, 4) + "," + Math.Round(0.0, 4) + "," + Math.Round(0.0, 4) + "," +
                            Math.Round(0.0, 4) + "," + Math.Round(1.0, 4) + "," + Math.Round(0.0, 4) + "|" + Environment.NewLine;
            }
            else if (Variables.stock.IndexType == tagFMIndexType.eIT_4thAxisX ||
                     Variables.stock.IndexType == tagFMIndexType.eIT_4thAxisY ||
                     Variables.stock.IndexType == tagFMIndexType.eIT_4thAxisZ)
            {
                foreach (FMSetup setup in Variables.doc.Setups)
                {
                    if (Variables.stock.Type == tagFMStockType.eST_Block)
                    {
                        x = stock_x + x;
                        y = stock_y + y;
                        z = stock_z + stock_thickness + z;
                    }
                    setup.MapSetupToWorld(ref x, ref y, ref z);
                    setup.ucs.GetVectors(out xx, out xy, out xz, out yx, out yy, out yz, out zx, out zy, out zz);
                    fcontent += Lib.tab + "|FRAME|G" + setup.FixtureID + "|" +
                                    Math.Round(Lib.Inch2MM(x), 4) + "," +
                                    Math.Round(Lib.Inch2MM(y), 4) + "," +
                                    Math.Round(Lib.Inch2MM(z), 4) + "," +
                                    Math.Round(xx, 4) + "," + Math.Round(xy, 4) + "," + Math.Round(xz, 4) + "," +
                                    Math.Round(yx, 4) + "," + Math.Round(yy, 4) + "," + Math.Round(yz, 4) + "|" + Environment.NewLine;
                }
            }
            else if (Variables.stock.IndexType == tagFMIndexType.eIT_5thAxis)
            {
                if (Settings.is_use_DATUM)
                {
                    string touch_off_setup_name;

                    touch_off_setup_name = Variables.stock.TouchOffSetup;
                    FMSetup setup = Variables.doc.Setups.Item(touch_off_setup_name);
                    setup.ucs.GetLocation(out x, out y, out z);
                    x += Variables.offset_x;
                    y += Variables.offset_y;
                    z += Variables.offset_z;
                    fcontent += Lib.tab + "|FRAME|P0|" +
                                    Math.Round(Lib.FromUnitsToUnits(x, Variables.doc.Metric, true), 4) + "," +
                                    Math.Round(Lib.FromUnitsToUnits(y, Variables.doc.Metric, true), 4) + "," +
                                    Math.Round(Lib.FromUnitsToUnits(-z, Variables.doc.Metric, true), 4) + "," +
                                    "1.0,0.0,0.0" + "," + "0.0,1.0,0.0|" + Environment.NewLine;

                }
                else
                {
                    foreach (FMSetup setup in Variables.doc.Setups)
                    {
                        if (Variables.stock.Type == tagFMStockType.eST_Block)
                        {
                            x = stock_x + x;
                            y = stock_y + y;
                            z = stock_z + stock_thickness + z;
                        }
                        setup.MapSetupToWorld(ref x, ref y, ref z);
                        setup.ucs.GetVectors(out xx, out xy, out xz, out yx, out yy, out yz, out zx, out zy, out zz);
                        fcontent += Lib.tab + "|FRAME|G" + setup.FixtureID + "|" +
                                        Math.Round(Lib.Inch2MM(x), 4) + "," +
                                        Math.Round(Lib.Inch2MM(y), 4) + "," +
                                        Math.Round(Lib.Inch2MM(z), 4) + "," +
                                        Math.Round(xx, 4) + "," + Math.Round(xy, 4) + "," + Math.Round(xz, 4) + "," +
                                        Math.Round(yx, 4) + "," + Math.Round(yy, 4) + "," + Math.Round(yz, 4) + "|" + Environment.NewLine;
                    }
                }
            }

            fcontent += "END" + Environment.NewLine + Environment.NewLine;

            return fcontent;
        }

        private static string GetStockInfo()
        {
            string fcontent = "";
            double stock_x, stock_y, stock_z;
            double stock_thickness;
            double stock_bbox_xmin, stock_bbox_xmax,
                   stock_bbox_ymin, stock_bbox_ymax,
                   stock_bbox_zmin, stock_bbox_zmax;

            if (!(Variables.stock.Type == tagFMStockType.eST_Block &&
                  Variables.stock.IndexType == tagFMIndexType.eIT_None))
            {
                Variables.stock.BoundingBox(out stock_bbox_xmin, out stock_bbox_ymin, out stock_bbox_zmin,
                                out stock_bbox_xmax, out stock_bbox_ymax, out stock_bbox_zmax,
                                tagFMCoordinateSpace.eCS_World, null);
                stock_thickness = stock_bbox_zmax - stock_bbox_zmin;

                Variables.stock.GetLocation(out stock_x, out stock_y, out stock_z);
                if (Variables.stock.Type == tagFMStockType.eST_Block)
                    stock_z = stock_z - stock_thickness;
                else if (Variables.stock.IndexType == tagFMIndexType.eIT_4thAxisX)
                    stock_y = stock_z = 0;
                else if (Variables.stock.IndexType == tagFMIndexType.eIT_4thAxisY)
                    stock_x = stock_z = 0;
                else if (Variables.stock.IndexType == tagFMIndexType.eIT_4thAxisZ)
                    stock_y = stock_z = 0;

                fcontent +=
                    "BEGIN_MODEL" + Environment.NewLine +
                        Lib.tab + "|UNITS|" + (Variables.doc.Metric ? "MM" : "INCH") + "|" + Environment.NewLine +
                        Lib.tab + "|VECTOR|" + Math.Round(Lib.FromUnitsToUnits(Variables.offset_x, Variables.doc.Metric, true), 4) + "," +
                                               Math.Round(Lib.FromUnitsToUnits(Variables.offset_y, Variables.doc.Metric, true), 4) + "," +
                                               Math.Round(Lib.FromUnitsToUnits(Variables.offset_z, Variables.doc.Metric, true), 4) +
                                               "|0.0,0.0,0.0|" + Environment.NewLine +
                        Lib.tab + "|MATRICE_OXY|" + Math.Round(stock_x, 4) + "," + Math.Round(stock_y, 4) + "," + Math.Round(stock_z, 4) + "," + "1.0,0.0,0.0,0.0,1.0,0.0|" + Environment.NewLine +
                        Lib.tab + "|STOCK|STL|stock.stl|0.1|" + Environment.NewLine +
                    "END" + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                FMSetup setup = Variables.doc.Setups.Item(Variables.selected_setup_id + 1);
                FMUcs ucs = setup.ucs;

                double x, y, z,
                       xx, xy, xz,
                       yx, yy, yz,
                       zx, zy, zz;
                ucs.GetLocation(out x, out y, out z);
                ucs.GetVectors(out xx, out xy, out xz,
                               out yx, out yy, out yz,
                               out zx, out zy, out zz);
                Variables.stock.BoundingBox(out stock_bbox_xmin, out stock_bbox_ymin, out stock_bbox_zmin,
                    out stock_bbox_xmax, out stock_bbox_ymax, out stock_bbox_zmax,
                    tagFMCoordinateSpace.eCS_World, null);
                double tempx, tempy, tempz;
                tempx = tempy = tempz = 0;
				
				if (xx == 1 && xy == 0 && xz == 0 &&
                    yx == 0 && yy == 1 && yz == 0)
                    tempz = (stock_bbox_zmax-stock_bbox_zmin) + z;
                setup.MapWorldToSetup(ref tempx, ref tempy, ref tempz);

                fcontent +=
                    "BEGIN_MODEL" + Environment.NewLine +
                        Lib.tab + "|UNITS|" + (Variables.doc.Metric ? "MM" : "INCH") + "|" + Environment.NewLine +
                        Lib.tab + "|VECTOR|" + Math.Round(Lib.FromUnitsToUnits(Variables.offset_x, Variables.doc.Metric, true), 4) + "," +
                                               Math.Round(Lib.FromUnitsToUnits(Variables.offset_y, Variables.doc.Metric, true), 4) + "," + 
                                               Math.Round(Lib.FromUnitsToUnits(Variables.offset_z, Variables.doc.Metric, true), 4) + 
                                               "|0.0,0.0,0.0|" + Environment.NewLine +
                        Lib.tab + "|MATRICE_OXY|" + Math.Round(x, 4) + "," + Math.Round(y, 4) + "," + Math.Round(z, 4) + "," +
                        xx + "," + xy + "," + xz + "," + yx + "," + yy + "," + yz + "|" + Environment.NewLine +
                        Lib.tab + "|STOCK|STL|stock.stl|0.1|" + Environment.NewLine +
                    "END" + Environment.NewLine + Environment.NewLine;
            }
            return fcontent;
        }

        private static string GetClampsInfo()
        {
            string fcontent = "";

            if (Variables.clamp_fpaths == null) return "";
            if (Variables.clamp_fpaths.Count == 0) return "";

            foreach (string fpath in Variables.clamp_fpaths)
            {
                fcontent +=
                    "BEGIN_MODEL" + Environment.NewLine +
                        Lib.tab + "|UNITS|" + (Variables.doc.Metric ? "MM" : "INCH") + "|" + Environment.NewLine +
                        Lib.tab + "|VECTOR|" + Math.Round(Lib.FromUnitsToUnits(Variables.offset_x, Variables.doc.Metric, true), 4) + "," +
                                               Math.Round(Lib.FromUnitsToUnits(Variables.offset_y, Variables.doc.Metric, true), 4) + "," +
                                               Math.Round(Lib.FromUnitsToUnits(Variables.offset_z, Variables.doc.Metric, true), 4) +
                                               "|0.0,0.0,0.0|" + Environment.NewLine +
                        Lib.tab + "|MATRICE_OXY|0.0,0.0,0.0,1.0,0.0,0.0,0.0,1.0,0.0|" + Environment.NewLine +
                        Lib.tab + "|CLAMP|STL|" + Path.GetFileName(fpath) + "|0.1|" + Environment.NewLine +
                    "END" + Environment.NewLine + Environment.NewLine;
            }
            return fcontent;
        }


        private static string GetMachineAndNCFileInfo(string machine_type, string machine_name)
        {
            string fcontent = "";

            fcontent +=
                "BEGIN_MACHINE" + Environment.NewLine +
                    Lib.tab + "|MACHINE|" + machine_type + "|" + Path.GetFileNameWithoutExtension(machine_name) + "|" + Environment.NewLine;
            if (Variables.stock.IndexType == tagFMIndexType.eIT_None)
                fcontent +=
                    Lib.tab + "|POST-PROCESSOR| |" + (Variables.setups_info[Variables.selected_setup_id]).nc_fpath + "|" + Environment.NewLine;
            else
                for (int setup_ind = 0; setup_ind < Variables.setups_info.Count; setup_ind++)
                    if ((Variables.setups_info[setup_ind]).nc_fpath != "")
                        fcontent +=
                            Lib.tab + "|POST-PROCESSOR| |" + (Variables.setups_info[setup_ind]).nc_fpath + "|" + Environment.NewLine;
            fcontent +=
                "END" + Environment.NewLine + Environment.NewLine;
            return fcontent;
        }

        private static string GetSoftwareInfo()
        {
            string fcontent;
            fcontent =
                "BEGIN_PARAMETERS" + Environment.NewLine +
                    Lib.tab + "|SOFTWARE|FeatureCAM|" + Environment.NewLine +
                    Lib.tab + "|VERSION|" + FCToNCSIMUL.Application.Version + "|" + Environment.NewLine +
                    Lib.tab + "|DATE|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + Environment.NewLine +
                "END" + Environment.NewLine + Environment.NewLine;
            return fcontent;
        }

        private static string SetProjectNCSFileName()
        {
            string fcontent;
            fcontent =
                "BEGIN_NCSIMUL_SETTING" + Environment.NewLine +
                    Lib.tab + "|TEMPLATE_JOB|" + Path.GetFileName(Variables.output_dirpath) + ".ncs" + "|" + Environment.NewLine +
                    Lib.tab + "|TOOLLIBNAME|" + Path.GetFileName(Variables.output_dirpath) + ".lib" + "|" + Environment.NewLine +
                "END" + Environment.NewLine + Environment.NewLine;
            return fcontent;
        }


    }
}
