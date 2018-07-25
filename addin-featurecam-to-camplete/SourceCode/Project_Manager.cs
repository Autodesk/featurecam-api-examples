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

namespace FeatureCAMToCAMplete
{
    static class Project_Manager
    {
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
                }
            }
            else if (Variables.stock.IndexType == tagFMIndexType.eIT_5thAxis)
            {
                if (Variables.use_DATUM)
                {
                    string touch_off_setup_name;

                    touch_off_setup_name = Variables.stock.TouchOffSetup;
                    FMSetup setup = Variables.doc.Setups.Item(touch_off_setup_name);
                    setup.ucs.GetLocation(out x, out y, out z);
                    x += Variables.offset_x;
                    y += Variables.offset_y;
                    z += Variables.offset_z;
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

        private static void LoadStockInfo(string stock_fpath, string workpieceID)
        {
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
            }
            else
            {
                FMSetup setup = Variables.doc.Setups.Item(Variables.selected_setup_id + 1);
                FMUcs ucs = setup.ucs;
                double i, j, k;

                double x, y, z,
                       xx, xy, xz,
                       yx, yy, yz,
                       zx, zy, zz;
                ucs.GetLocation(out x, out y, out z);
                ucs.GetVectors(out xx, out xy, out xz,
                               out yx, out yy, out yz,
                               out zx, out zy, out zz);
                ComputeEulerAngles(ucs, out i, out j, out k);


            }
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


        private static void ComputeEulerAngles(FeatureCAM.FMUcs ucs,
                out double i, out double j, out double k)
        {
            double
                x1, y1, z1,
                x2, y2, z2,
                x3, y3, z3,
                i1, j1, k1,
                j1_rad;

            ucs.GetVectors(out x1, out x2, out x3, out y1, out y2, out y3, out z1, out z2, out z3);

            if (z1 != 1 && z1 != -1)
            {
                j1_rad = -Math.Asin(x3);
                j1 = Lib.Radians2Degrees(j1_rad);
                i1 = Lib.Radians2Degrees(Math.Atan2(y3 / Math.Cos(j1_rad), z3 / Math.Cos(j1_rad)));
                k1 = Lib.Radians2Degrees(Math.Atan2(x2 / Math.Cos(j1_rad), x1 / Math.Cos(j1_rad)));
                i = i1;
                j = j1;
                k = k1;
            }
            else
            {
                k = 0;
                if (z1 == -1)
                {
                    j = 90;
                    i = k + Lib.Radians2Degrees(Math.Atan2(x2, x3));
                }
                else
                {
                    j = -90;
                    i = -k + Lib.Radians2Degrees(Math.Atan2(-x2, -x3));
                }
            }
            i = Math.Round(i, 4);
            j = Math.Round(j, 4);
            k = Math.Round(k, 4);
        }


    }
}