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
using System.Text;
using FeatureCAM;

namespace FeatureCAMToNCSIMUL
{
    class Point
    {
        public double x;
        public double y;

        public Point(double X, double Y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            return Math.Round(x, 4) + "," + Math.Round(y, 4);
        }
    }

    class GeomSegment
    {
        bool is_arc;
        Point pt1;
        Point pt2;
        Point arc_center;
        bool is_arc_cw;
        bool is_cut;

        /* Line constructor */
        public GeomSegment(Point P1, Point P2, bool Is_Cut)
        {
            pt1 = P1;
            pt2 = P2;
            is_cut = Is_Cut;
            is_arc = false;
        }

        /* Arc constructor */
        public GeomSegment(Point P1, Point P2, Point Center, bool Is_CW, bool Is_Cut)
        {
            pt1 = P1;
            pt2 = P2;
            arc_center = Center;
            is_arc_cw = Is_CW;
            is_cut = Is_Cut;
            is_arc = true;
        }

        public Point GetPoint1()
        {
            return this.pt1;
        }

        public Point GetPoint2()
        {
            return this.pt2;
        }

        public string ToString(int indentation)
        {
            if (!is_arc) /* line */
                return Lib.Indent(indentation) + "|LINE|" +
                                        (is_cut ? "CUT" : "NOCUT") + "|" +
                                        pt1.ToString() + "," + pt2.ToString() + "|";
            else
                return Lib.Indent(indentation) + "|ARC|" +
                                        (is_cut ? "CUT" : "NOCUT") + "|" +
                                        pt1.ToString() + "," + pt2.ToString() + "," + arc_center.ToString() + "|" +
                                        (is_arc_cw ? "CW" : "CCW") + "|";
        }
    }

    class Tool
    {
        static bool is_metric;

        protected Tool() { }

        public static string ToString(FeatureCAM.FMToolMap2 toolmap, List<string> partline_features)
        {
            List<GeomSegment> tool_geom = null, holder_geom = null;
            FMToolHolder holder;
            string tool_desc = "";
            int tool_special_type_id = 5;
            string tool_name = "";
            int pot_number = -1; //is this spot in the holder 
            int tool_num = -1;
            int rad_comp = -1; //DiamC/number of the radius compensation
            int comp_1st_len = -1; //LenC/Number of the 1st length compensation
            int comp_2nd_len = 0; //LenC2/Number of the 2nd length compensation 
            double tool_diam = 0;
            double tool_len = 0;

            try
            {
                if (toolmap == null) return "";
                if (toolmap.Tool == null) return "";

                is_metric = toolmap.Tool.Metric;

                tool_name = toolmap.Tool.Name;
                pot_number = toolmap.ToolNumber;
                tool_num = toolmap.ToolNumber;
                rad_comp = (toolmap.DiameterOffsetRegister != -1
                                ? toolmap.DiameterOffsetRegister / 2 : toolmap.ToolNumber);
                comp_1st_len = (toolmap.LengthOffsetRegister != -1
                                ? toolmap.LengthOffsetRegister : toolmap.ToolNumber);
                comp_2nd_len = (toolmap.Length2ndOffsetRegister != -1
                                ? toolmap.Length2ndOffsetRegister : 0);
                switch (toolmap.Tool.ToolGroup)
                {
                    case tagFMToolGroup.eTG_EndMill:
                        tool_geom = EndMillToGeometry((FMEndMill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_ChamferMill:
                        tool_geom = ChamferMillToGeometry((FMChamferMill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_CounterBore:
                        tool_geom = CounterBoreToGeometry((FMCounterBore)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_CounterSink:
                        tool_geom = CounterSinkToGeometry((FMCounterSink)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_FaceMill:
                        tool_geom = FaceMillToGeometry((FMFaceMill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_PlungeRough:
                        tool_geom = PlungeMillToGeometry((FMPlungeMill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_Ream:
                        tool_geom = ReamToGeometry((FMReam)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_RoundingMill:
                        tool_geom = RoundingMillToGeometry((FMRoundingMill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_SideMill:
                        tool_geom = SideMillToGeometry((FMSideMill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_SpotDrill:
                        tool_geom = SpotDrillToGeometry((FMSpotDrill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_Tap:
                        tool_geom = TapToGeometry((FMTap)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_ThreadMill:
                        tool_geom = ThreadMillToGeometry((FMThreadMill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_TwistDrill:
                        tool_geom = TwistDrillToGeometry((FMTwistDrill)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_BoringBar:
                        tool_geom = BoringBarToGeometry((FMBoringBar)toolmap.Tool, ref tool_diam, ref tool_len);
                        break;
                    case tagFMToolGroup.eTG_TurnID:
                        tool_special_type_id = 25;
                        tool_geom = TurnODToGeometry((FMLatheTool)toolmap.Tool, ref tool_diam, ref tool_len, toolmap.turret, out holder_geom);
                        break;
                    case tagFMToolGroup.eTG_TurnOD:
                        tool_special_type_id = 25;
                        tool_geom = TurnODToGeometry((FMLatheTool)toolmap.Tool, ref tool_diam, ref tool_len, toolmap.turret, out holder_geom);
                        break;
                    case tagFMToolGroup.eTG_TurnGroove:
                        tool_special_type_id = 25;
                        tool_geom = TurnODToGeometry((FMLatheTool)toolmap.Tool, ref tool_diam, ref tool_len, toolmap.turret, out holder_geom);
                        break;
                    case tagFMToolGroup.eTG_TurnThread:
                        tool_special_type_id = 25;
                        Variables.warning_msg = "Unsupported tool type";
                        Variables.unsupported_tool_names += (Variables.unsupported_tool_names != "" ? "; " : "") +
                                                            toolmap.Tool.Name;
                        break;
                    default:
                        Variables.warning_msg = "Unsupported tool type";
                        Variables.unsupported_tool_names += (Variables.unsupported_tool_names != "" ? "; " : "") +
                                                            toolmap.Tool.Name;
                        break;
                }

                bool is_comp = false;
                foreach (FMOperToolMap op in toolmap.Operations)
                {
                    if (partline_features != null)
                        if (partline_features.Contains(op.FeatureName))
                            is_comp = true;
                }

                //HolderToString(toolmap);
                holder = GetHolder(toolmap);

                string tool_id = "";
                if (Settings.tool_identification == 0) /* tool number */
                    tool_id = toolmap.Tool.Name;
                else
                    tool_id = (toolmap.ToolID != "" ? toolmap.ToolID.ToString() : toolmap.ToolNumber.ToString());

                if (tool_special_type_id != 25)
                    tool_desc = "BEGIN_TOOL" + Lib.EOL +
                                       Lib.tab + "|TOOL|" +
                                                    tool_special_type_id + "|" +
                                                    tool_id + "|" +
                                                    pot_number + "|" +
                                                    (Settings.tool_identification == 0 ? Convert.ToString(tool_num) : tool_id) + "|" +
                                                    rad_comp + "|" +
                                                    comp_1st_len + "|" +
                                                    comp_2nd_len + "|" + Lib.EOL +
                                       Lib.tab + "|UNITS|" +
                                                    (toolmap.Tool.Metric ? "MM" : "INCH") + "|" + Lib.EOL +
                                       Lib.tab + "|GAUGE|" + //lenth under spindle
                                                    Math.Round((toolmap.Tool.ExposedLength + Lib.FromUnitsToUnits(holder.Length, holder.Metric, toolmap.Tool.Metric)), 4) + "|" +
                                                    "0" + "|" + Lib.EOL +
                                       (is_comp || Settings.is_export_tool_rad_compensation ? Lib.tab + "|RADIUS_COMPENSATION|" + Math.Round(tool_diam / 2, 4) + "|" + Environment.NewLine : "") +
                                       (Settings.is_export_tool_len_compensation ? Lib.tab + "|LENGTH_COMPENSATION|" + Math.Round((toolmap.Tool.ExposedLength + Lib.FromUnitsToUnits(holder.Length, holder.Metric, toolmap.Tool.Metric)), 4) + ",0,0|" + Environment.NewLine : "") +
                                       ToolProfileToString(tool_geom, 2) + Lib.EOL +
                                       HolderToString(toolmap, holder) + Lib.EOL +
                                   "END";
                else
                    tool_desc = "BEGIN_TOOL" + Lib.EOL +
                                       Lib.tab + "|TOOL|" +
                                                    tool_special_type_id + "|" +
                                                    tool_id + "|" +
                                                    pot_number + "|" +
                                                    (Settings.tool_identification == 0 ? Convert.ToString(tool_num) : tool_id) + "|" + Lib.EOL +
                                       Lib.tab + "|UNITS|" +
                                                    (toolmap.Tool.Metric ? "MM" : "INCH") + "|" + Lib.EOL +
                                       Lib.tab + "|GAUGE|" + //lenth under spindle
                                                    Math.Round((toolmap.Tool.ExposedLength + Lib.FromUnitsToUnits(holder.Length, holder.Metric, toolmap.Tool.Metric)), 4) + "|" +
                                                    "0" + "|" + Lib.EOL +
                                       Lib.tab + "|PARAMETER|0|" + Lib.EOL +
                                       Lib.tab + "|CLASS|3|" + Lib.EOL +
                                       Lib.tab + "|ORIENT|0|0|0|" + Lib.EOL +
                                       (is_comp || Settings.is_export_tool_rad_compensation ? Lib.tab + "|RADIUS_COMPENSATION|" + Math.Round(tool_diam / 2, 4) + "|" + Environment.NewLine : "") +
                                       (Settings.is_export_tool_len_compensation ? Lib.tab + "|LENGTH_COMPENSATION|" + Math.Round((toolmap.Tool.ExposedLength + Lib.FromUnitsToUnits(holder.Length, holder.Metric, toolmap.Tool.Metric)), 4) + ",0,0|" + Environment.NewLine : "") +
                                       LatheToolProfileToString(tool_geom, 2, true) + Lib.EOL +
                                       Lib.tab + "BEGIN_TOOLHOLDERS" + Lib.EOL +
                                       Lib.double_tab + "|TOOLHOLDERS|" + holder.Name + "|" + Lib.EOL +
                                       Lib.double_tab + "|VECTOR|0.0,0.0,0.0|" + Lib.EOL +
                                       Lib.double_tab + "|3DEF_1|STL|" + LatheHolderToSTL((FMLatheTool)toolmap.Tool) + "|" + Lib.EOL +
                                       Lib.tab + "END" + Lib.EOL +
                                       "END" + Lib.EOL;
            }
            catch (Exception)
            { }

            return tool_desc;
        }

        private static string LatheHolderToSTL(FeatureCAM.FMLatheTool tool)
        {
            string holder_fpath = Path.Combine(Variables.output_dirpath, "holder_" + tool.Name + ".stl");
            tool.SaveHolderGeomAsSTL(holder_fpath, tool.Metric);

            return holder_fpath;
        }

        private static string HolderToString(FeatureCAM.FMToolMap2 toolmap, FMToolHolder holder)
        {
            List<GeomSegment> holder_geom = null;
            string holder_name = toolmap.Tool.Holder;
            string holder_str = "";
            
            if (holder == null) return "";
            switch (holder.HolderType)
            {
                case tagFMToolHolderType.eTHT_ColletHolder:
                    holder_geom = ColletHolderToGeometry(holder);
                    break;
                case tagFMToolHolderType.eTHT_EndmillHolder:
                    holder_str = "endmill";
                    break;
                case tagFMToolHolderType.eTHT_SpindleHolder:
                    holder_str = "spindle holder";
                    break;
                default:
                    holder_str = "unsupported holder type";
                    break;
            }
            holder_geom = HolderToGeometry(holder, toolmap.Tool.Metric);

            holder_str = Lib.tab + "BEGIN_TOOLHOLDERS" + Lib.EOL +
                         Lib.double_tab + "|TOOLHOLDERS|" + holder.Name + "|" + Lib.EOL +
                         Lib.double_tab + "|VECTOR|0.000,0.000," + Math.Round(toolmap.Tool.ExposedLength, 3) + "|" + Lib.EOL +
                         ToolProfileToString(holder_geom, 3) + Lib.EOL +
                         Lib.tab + "END";

            return holder_str;
        }

        private static FMToolHolder GetHolder(FeatureCAM.FMToolMap2 toolmap)
        {
            FMToolCrib tc;
            FMToolHolder holder = null;

            tc = Variables.doc.get_ActiveToolCrib();

            string holder_name = toolmap.Tool.Holder;

            if (holder_name == "")
            {
                holder = (FMToolHolder)toolmap.Tool.DefaultHolder;
            }
            else
            {
                foreach (FMToolHolder tc_holder in tc.ToolHolders)
                {
                    if (tc_holder != null)
                        if (holder_name.Equals(tc_holder.Name, StringComparison.OrdinalIgnoreCase))
                            holder = tc_holder;
                }
                if (holder == null)
                    holder = (FMToolHolder)toolmap.Tool.DefaultHolder;
            }
            return holder;
        }

        private static List<GeomSegment> HolderToGeometry(FMToolHolder h, bool is_tool_metric)
        {
            List<GeomSegment> holder_segments;
            Point pt1, pt2;

            if (h == null) return null;

            holder_segments = new List<GeomSegment>();

            /* Holder is from curve */
            if (h.IsFormHolder)
            {
                FMGeoms holder_prof = h.GetCopyOfFormCurve(h.Metric);
                IFMGCurve holder_crv;
                bool is_prev_piece_circle = false;
                if (holder_prof.Count > 0)
                {
                    holder_crv = (IFMGCurve)holder_prof.Item(1);
                    FMGeoms crv_pieces = (FMGeoms)holder_crv.ToGeometry(h.Metric);
                    if (crv_pieces != null)
                    {
                        for (int i = 1; i <= crv_pieces.Count; i++)
                            holder_segments.Add(CurveSegmentToGeomSegment((FMGeom)crv_pieces.Item(i), i, ref is_prev_piece_circle, false, is_tool_metric, h.Metric, 1, 1, false));
                    }
                }
            }
            else /* Standard holder */
            {
                pt2 = new Point(Lib.FromUnitsToUnits(h.Diameter, h.Metric, is_tool_metric) / 2, 0);
                holder_segments.Add(new GeomSegment(new Point(0, 0), pt2, false));

                pt1 = pt2;
                pt2 = new Point(Lib.FromUnitsToUnits(h.Diameter, h.Metric, is_tool_metric) / 2,
                                Lib.FromUnitsToUnits(h.Length, h.Metric, is_tool_metric));
                holder_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            return holder_segments;
        }

        private static bool IsLine(FMGeom geom)
        {
            FMGLine line;
            try
            {
                line = (FMGLine)geom;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsCircle(FMGeom geom)
        {
            FMGCircle circle;
            try
            {
                circle = (FMGCircle)geom;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static List<GeomSegment> TurnODToGeometry(FMLatheTool t, ref double tool_diam, ref double tool_len, tagFMTurretType turret_type, out List<GeomSegment> holder_segments)
        {
            List<GeomSegment> tool_segments;
            FMGeoms geoms = null;
            IFMGCurve cutter, holder;
            FMGeoms crv_pieces;
            bool is_prev_piece_circle = false,
                 is_diam_mode; /* Value of Options -> Turning input mode menu */
            int orient_coef_x = 1, orient_coef_z = 1;
            int rot_x, rot_y, rot_z;

            holder_segments = null;
            if (t == null) return null;

            ComputeTurningToolOrientation(t.HolderOrientation, turret_type, out rot_x, out rot_y, out rot_z);    

            tool_segments = new List<GeomSegment>();

            is_diam_mode = (FCToNCSIMUL.Application.TurningInputMode == tagFMTurningInputMode.eTIM_DIAMETER);

            geoms = t.GetCopyOfHolderCurve(t.Metric);

            holder = (IFMGCurve)geoms.Item(1);
            cutter = (IFMGCurve)geoms.Item(2);

            crv_pieces = (FMGeoms)cutter.ToGeometry(t.Metric);
            if (crv_pieces != null)
            {
                for (int i = 1; i <= crv_pieces.Count; i++)
                    tool_segments.Add(CurveSegmentToGeomSegment((FMGeom)crv_pieces.Item(i), i, ref is_prev_piece_circle, is_diam_mode, t.Metric, t.DefaultHolder.Metric, orient_coef_x, orient_coef_z, true));
            }

            holder_segments = new List<GeomSegment>();
            crv_pieces = (FMGeoms)holder.ToGeometry(t.Metric);
            if (crv_pieces != null)
            {
                for (int i = 1; i <= crv_pieces.Count; i++)
                    holder_segments.Add(CurveSegmentToGeomSegment((FMGeom)crv_pieces.Item(i), i, ref is_prev_piece_circle, is_diam_mode, t.Metric, t.DefaultHolder.Metric, orient_coef_x, orient_coef_z, false));
            }

            return tool_segments;
        }

        private static GeomSegment CurveSegmentToGeomSegment(FMGeom crv_piece, int crv_index,
                    ref bool is_prev_piece_circle, bool is_diam_mode,
                    bool is_tool_metric, bool is_holder_metric,
                    int orient_coef_x, int orient_coef_z, 
                    bool is_cut_segment)
        {
            GeomSegment crv_def = null;
            FMGCircle circle;
            FMGLine line;
            double x1, y1, z1,
                   x2, y2, z2,
                   xc, yc, zc;
            Point pt1, pt2, center;
            bool temp;

            if (IsLine(crv_piece))
            {
                line = (FMGLine)crv_piece;
                line.GetEndpoints(out x1, out y1, out z1, out x2, out y2, out z2, out temp, out temp);
                /* Line coords can either be in XYZ, Radius or Diameter mode. 
                 * If they're in diameter mode, we need to divide x coords by 2 */
                is_diam_mode = false;
                if (is_diam_mode)
                {
                    x1 = x1 / 2;
                    x2 = x2 / 2;
                }
                pt1 = new Point(Lib.FromUnitsToUnits(orient_coef_x * x1, is_holder_metric, is_tool_metric),
                                Lib.FromUnitsToUnits(orient_coef_z * z1, is_holder_metric, is_tool_metric));
                pt2 = new Point(Lib.FromUnitsToUnits(orient_coef_x * x2, is_holder_metric, is_tool_metric),
                                Lib.FromUnitsToUnits(orient_coef_z * z2, is_holder_metric, is_tool_metric));
                is_prev_piece_circle = false;
                return new GeomSegment(pt1, pt2, is_cut_segment);
            }
            else if (IsCircle(crv_piece))
            {
                circle = (FMGCircle)crv_piece;
                circle.GetEndpoints(out x1, out y1, out z1, out x2, out y2, out z2, out temp);
                circle.GetCenter(out xc, out yc, out zc);
                /* Arcs are always in XYZ mode, so we don't need to worry about
                 * dividing X values by 2 */
                /* Compute direction of the arc. Using sign of j component 
                 * of the cross product of 2 vectors*/
                pt1 = new Point(Lib.FromUnitsToUnits(orient_coef_x * x1, is_holder_metric, is_tool_metric), 
                                Lib.FromUnitsToUnits(orient_coef_z * z1, is_holder_metric, is_tool_metric));
                pt2 = new Point(Lib.FromUnitsToUnits(orient_coef_x * x2, is_holder_metric, is_tool_metric),
                                Lib.FromUnitsToUnits(orient_coef_z * z2, is_holder_metric, is_tool_metric));
                center = new Point(Lib.FromUnitsToUnits(orient_coef_x * xc, is_holder_metric, is_tool_metric),  
                                   Lib.FromUnitsToUnits(orient_coef_z * zc, is_holder_metric, is_tool_metric));
                if ((z1 - zc) * (x2 - xc) - (x1 - xc) * (z2 - zc) > 0)
                    crv_def = new GeomSegment(pt1, pt2, center, true, is_cut_segment);
                else
                    crv_def = new GeomSegment(pt1, pt2, center, false, is_cut_segment);
                is_prev_piece_circle = true;
            }
            else
                is_prev_piece_circle = false;

            return crv_def;
        }


        private static void ComputeTurningToolOrientation(tagFMTurnHolderOrientation holder_orient,
                        tagFMTurretType turret_type,
                        out int rot_x, out int rot_y, out int rot_z)
        {
            bool temp;
            tagFMTurretIDType turret;
            bool is_skip_orientation = false;
            FCToNCSIMUL.Application.GetTurnTurretInfo(turret_type, out temp, out turret, out temp, out temp, out temp, out temp);
            if (turret == tagFMTurretIDType.eTIDT_MainLower)
                is_skip_orientation = Variables.lmss.is_milling_head;
            else if (turret == tagFMTurretIDType.eTIDT_MainUpper)
                is_skip_orientation = Variables.umss.is_milling_head;
            else if (turret == tagFMTurretIDType.eTIDT_SubLower)
                is_skip_orientation = Variables.lsss.is_milling_head;
            else if (turret == tagFMTurretIDType.eTIDT_SubUpper)
                is_skip_orientation = Variables.usss.is_milling_head;

            if (!is_skip_orientation)
            {
                switch (holder_orient)
                {
                    case tagFMTurnHolderOrientation.eTHO_SW:
                        rot_x = 180;
                        rot_y = -90;
                        rot_z = 0;
                        break;
                    case tagFMTurnHolderOrientation.eTHO_WS:
                        rot_x = 0;
                        rot_y = 0;
                        rot_z = 0;
                        break;
                    case tagFMTurnHolderOrientation.eTHO_NW:
                        rot_x = 0;
                        rot_y = -90;
                        rot_z = 0;
                        break;
                    case tagFMTurnHolderOrientation.eTHO_WN:
                        rot_x = 0;
                        rot_y = 0;
                        rot_z = 180;
                        break;
                    case tagFMTurnHolderOrientation.eTHO_SE:
                        rot_x = 0;
                        rot_y = 90;
                        rot_z = 0;
                        break;
                    case tagFMTurnHolderOrientation.eTHO_ES:
                        rot_x = 180;
                        rot_y = 0;
                        rot_z = 0;
                        break;
                    case tagFMTurnHolderOrientation.eTHO_NE:
                        rot_x = 180;
                        rot_y = 90;
                        rot_z = 0;
                        break;
                    case tagFMTurnHolderOrientation.eTHO_EN:
                        rot_x = 180;
                        rot_y = 0;
                        rot_z = 180;
                        break;
                    default:
                        rot_x = rot_y = rot_z = 0;
                        break;
                }
            }
            else
                rot_x = rot_y = rot_z = 0;
        }
        
        private static List<GeomSegment> ColletHolderToGeometry(FMToolHolder h)
        {
            List<GeomSegment> holder_segments;
            Point pt1, pt2;

            if (h == null) return null;

            holder_segments = new List<GeomSegment>();

            if (h.Chamfer)
            {
                pt2 = new Point(h.TipDiameter / 4, 0);
                holder_segments.Add(new GeomSegment(new Point(0, 0), pt2, false));
                pt1 = pt2;
                pt2 = new Point(h.TipDiameter / 2, h.TipDiameter / 4);
                holder_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt2 = new Point(h.TipDiameter / 2, 0);
                holder_segments.Add(new GeomSegment(new Point(0, 0), pt2, false));
            }
            pt1 = pt2;
            pt2 = new Point(h.TipDiameter / 2, h.TipLength);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(h.StepDiameter / 2, h.TipLength);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(h.StepDiameter / 2, h.Length - (h.TipLength + h.FlangeLength));
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            double r = h.FlangeLength / 5.0;
            double r_mid = h.FlangeLength / 2.0;
            double z1 = r * Lib.TanDegrees(30.0);
            double z2 = r_mid - ((r / 2.0) + z1 + r);
            double x2 = z2 * Lib.TanDegrees(60);
            double ridge_rad = h.Diameter / 2.0;
            double ridge_start_x = ridge_rad - x2;
            double ridge_start_z = h.Length - h.FlangeLength;

            pt1 = new Point(h.StepDiameter / 2, h.Length - (h.TipLength + h.FlangeLength));
            pt2 = new Point(ridge_start_x, ridge_start_z);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(ridge_rad, ridge_start_z + z2);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(ridge_rad, ridge_start_z + z2 + r);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(ridge_rad - r, ridge_start_z + z2 + r + z1);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(ridge_rad - r, ridge_start_z + z2 + r + z1 + r);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(ridge_rad, ridge_start_z + z2 + r + z1 + r + z1);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(ridge_rad, ridge_start_z + z2 + r + z1 + r + z1 + r);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(ridge_start_x, h.Length);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            pt1 = pt2;
            pt2 = new Point(0, h.Length);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            return holder_segments;
        }

        private static List<GeomSegment> ThreadMillToGeometry(FMThreadMill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;
            double pitch, half_pitch;
            double tooth_depth, cur_y;
            int num_of_pitches;
            
            if (t == null) return null;
            tool_diam = t.BodyDiameter;
            tool_len = t.OverallLength;

            num_of_pitches = (int)(t.CutterLength / t.MaxPitch);
            pitch = t.MaxPitch;
            half_pitch = pitch / 2;
            tooth_depth = half_pitch / Lib.TanDegrees(30);

            tool_segments = new List<GeomSegment>();

            if (t.Taper == 0)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.CutterDiameter / 2 - tooth_depth, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                cur_y = 0;
                while (cur_y + pitch <= t.CutterLength)
                {
                    pt1 = pt2;
                    pt2 = new Point(t.CutterDiameter / 2, cur_y + half_pitch);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    cur_y += pitch;
                    pt2 = new Point(t.CutterDiameter / 2 - tooth_depth, cur_y);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
                if (cur_y + pitch > t.CutterLength)
                {
                    pt1 = pt2;
                    pt2 = new Point(t.CutterDiameter / 2 - tooth_depth, t.CutterLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.CutterLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                double x;
                pt1 = new Point(0, 0);
                x = t.CutterDiameter / 2 - (num_of_pitches - 1) * pitch * Lib.TanDegrees(t.Taper);
                pt2 = new Point(x - tooth_depth, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                for (int i = 1; i <= num_of_pitches; i++)
                {
                    x = t.CutterDiameter / 2 - (num_of_pitches - i) * pitch * Lib.TanDegrees(t.Taper);
                    pt1 = pt2;
                    pt2 = new Point(x, (i - 0.5) * pitch);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(x - tooth_depth, i * pitch);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
                if (pt2.y < t.CutterLength)
                {
                    pt1 = pt2;
                    pt2 = new Point(pt1.x + (t.CutterLength - num_of_pitches*pitch)*Lib.TanDegrees(t.Taper), t.CutterLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.CutterLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }

            return tool_segments;
        }

        private static List<GeomSegment> TapToGeometry(FMTap t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;
            double pitch, half_pitch;
            double tooth_depth, cur_y;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            if (t.Metric)
                pitch = t.thread;
            else
                pitch = 1 / t.thread;
            half_pitch = pitch / 2;
            tooth_depth = half_pitch / Lib.TanDegrees(30);

            tool_segments = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point((t.Diameter - 0.8 * pitch) / 2, 0);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            cur_y = 2 * pitch;
            pt2 = new Point(t.Diameter / 2, cur_y);
            tool_segments.Add(new GeomSegment(pt1, pt2, true));
            while (cur_y + pitch <= t.Length)
            {
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2 - tooth_depth, cur_y + half_pitch);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                cur_y += pitch;
                pt2 = new Point(t.Diameter / 2, cur_y);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
            }
            if (cur_y + pitch > t.Length)
            {
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
            }
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.OverallLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));

            return tool_segments;
        }

        private static List<GeomSegment> EndMillToGeometry(FMEndMill t, ref double tool_diam, ref double tool_len)
        {
            List<GeomSegment> tool_segments;
            Point pt1, pt2, center;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();
            if (t.EndRadius == 0) /* Flat end mill */
            {
                if (t.DiameterAtBottom)
                {
                    pt2 = new Point(t.Diameter / 2, 0);
                    tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 + t.CutterLength * Lib.TanDegrees(t.Taper), t.CutterLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
                else
                {
                    pt2 = new Point(t.Diameter / 2 - t.CutterLength * Lib.TanDegrees(t.Taper), 0);
                    tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.CutterLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
            }
            else
            {
                if (t.IsBallEnd) /* Ball end (diameter at bottom greyed out) */
                {
                    if (t.Taper == 0)
                    {
                        pt2 = new Point(t.Diameter / 2, t.Diameter / 2);
                        center = new Point(0, t.Diameter / 2);
                        tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.CutterLength);
                        tool_segments.Add(new GeomSegment(pt1, pt2, true));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                        tool_segments.Add(new GeomSegment(pt1, pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                        tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    }
                    else
                    {
                        pt2 = new Point(t.EndRadius / 2, t.EndRadius / 2);
                        center = new Point(0, t.EndRadius / 2);
                        tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.EndRadius + (t.Diameter / 2 - t.EndRadius) * Lib.TanDegrees(t.Taper));
                        tool_segments.Add(new GeomSegment(pt1, pt2, true));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.EndRadius + (t.Diameter / 2 - t.EndRadius) * Lib.TanDegrees(t.Taper));
                        tool_segments.Add(new GeomSegment(pt1, pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                        tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    }
                }
                else /* Bull nose */
                {
                    if (t.Taper == 0)
                    {
                        pt2 = new Point(t.Diameter / 2 - t.EndRadius, 0);
                        tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.EndRadius);
                        center = new Point(t.Diameter / 2 - t.EndRadius, t.EndRadius);
                        tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.CutterLength);
                        tool_segments.Add(new GeomSegment(pt1, pt2, true));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                        tool_segments.Add(new GeomSegment(pt1, pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                        tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    }
                    else
                    {
                        if (t.DiameterAtBottom)
                        {
                            pt2 = new Point(t.Diameter / 2 - t.EndRadius, 0);
                            tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2, t.EndRadius);
                            center = new Point(t.Diameter / 2 - t.EndRadius, t.EndRadius);
                            tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2 + (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), t.CutterLength);
                            tool_segments.Add(new GeomSegment(pt1, pt2, true));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                            tool_segments.Add(new GeomSegment(pt1, pt2, false));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                            tool_segments.Add(new GeomSegment(pt1, pt2, false));
                        }
                        else
                        {
                            pt2 = new Point(t.Diameter / 2 - t.EndRadius - (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), 0);
                            tool_segments.Add(new GeomSegment(new Point(0, 0), pt2, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2 - (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), t.EndRadius);
                            center = new Point(t.Diameter / 2 - t.EndRadius - (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), t.EndRadius);
                            tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2, t.CutterLength);
                            tool_segments.Add(new GeomSegment(pt1, pt2, true));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                            tool_segments.Add(new GeomSegment(pt1, pt2, false));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                            tool_segments.Add(new GeomSegment(pt1, pt2, false));
                        }
                    }
                }
            }

            return tool_segments;
        }

        private static List<GeomSegment> ChamferMillToGeometry(FMChamferMill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.InnerDiameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point(t.InnerDiameter / 2, 0);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.OuterDiameter / 2, (t.OuterDiameter - t.InnerDiameter) / 2 / Lib.TanDegrees(t.angle));
            tool_segments.Add(new GeomSegment(pt1, pt2, true));
            pt1 = pt2;
            pt2 = new Point(t.OuterDiameter / 2, t.OverallLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));

            return tool_segments;
        }

        private static List<GeomSegment> CounterBoreToGeometry(FMCounterBore t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point(t.PilotDiameter / 2, 0);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.PilotDiameter / 2, t.PilotLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.PilotLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, true));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.PilotLength + t.Length);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.OverallLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));

            return tool_segments;
        }

        private static List<GeomSegment> CounterSinkToGeometry(FMCounterSink t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.BodyDiameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            if (t.FlatDiameter == 0)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.BodyDiameter / 2, t.BodyDiameter / 2 / Lib.TanDegrees(t.angle / 2));
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.ExposedLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.FlatDiameter / 2, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, (t.BodyDiameter - t.FlatDiameter) / 2 / Lib.TanDegrees(t.angle / 2));
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.ExposedLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }

            return tool_segments;
        }

        private static List<GeomSegment> FaceMillToGeometry(FMFaceMill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2, center;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.Height;

            tool_segments = new List<GeomSegment>();

            if (t.CornerIsChamfer)
            {
                double r_e = t.EffectiveDiameter / 2;
                double r = t.Diameter / 2;
                if (t.TipRadius == 0)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(r_e, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(r, r - r_e);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(r, t.Height);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
                else
                {
                    double a = Lib.TanDegrees(22.5) * t.TipRadius;
                    double a1 = Math.Sin(Lib.Degrees2Radians(45)) * a;

                    pt1 = new Point(0, 0);
                    pt2 = new Point(r_e - a, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(r_e + a1, a1);
                    center = new Point(r_e - a, t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(r - a1, r - r_e - a1);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(r, r - r_e + a);
                    center = new Point(r - t.TipRadius, r - r_e + a);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(r, t.Height);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
            }
            else
            {
                if (t.CornerRadius == 0)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.Diameter / 2, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.Height);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
                else
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.Diameter / 2 - t.CornerRadius, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.CornerRadius);
                    center = new Point(t.Diameter / 2 - t.CornerRadius, t.CornerRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.Height);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                }
            }
            return tool_segments;
        }

        private static List<GeomSegment> PlungeMillToGeometry(FMPlungeMill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2, center;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            if (t.IsCenterCutting)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.CutterLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2 - t.InsertTipRadius, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.InsertTipRadius);
                center = new Point(t.Diameter / 2 - t.InsertTipRadius, t.InsertTipRadius);
                tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.InsertLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.CutterLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            pt1 = pt2;
            pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.ShankDiameter / 2, t.ExposedLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
           
            return tool_segments;
        }

        private static List<GeomSegment> ReamToGeometry(FMReam t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point(t.Diameter / 2, 0);
            tool_segments.Add(new GeomSegment(pt1, pt2, true));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.ExposedLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.OverallLength);
            tool_segments.Add(new GeomSegment(pt1, pt2, false));

            return tool_segments;
        }

        private static List<GeomSegment> RoundingMillToGeometry(FMRoundingMill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2, center;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.InnerDiameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            if ((t.OuterDiameter - t.InnerDiameter) / 2 == t.Radius)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.InnerDiameter / 2, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.Radius);
                center = new Point(t.InnerDiameter / 2 + t.Radius, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, center, true, true));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.ExposedLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.InnerDiameter / 2, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.InnerDiameter / 2 + t.Radius, t.Radius);
                center = new Point(t.InnerDiameter / 2 + t.Radius, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, center, true, true));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.Radius);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.ExposedLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }

            return tool_segments;
        }

        private static List<GeomSegment> SideMillToGeometry(FMSideMill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2, center;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            if (t.IsSlittingSaw) /* Slitting saw */
            {
                if (t.TipRadius == 0)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.ArborDiameter / 2, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength + t.CutterWidth);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength + t.CutterWidth);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
                else if (t.TipRadius != t.Diameter / 2)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.ArborDiameter / 2, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength + t.CutterWidth);
                    center = new Point(0, t.ArborTipLength + t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
                else
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.ArborDiameter / 2, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength + t.TipRadius);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength + t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength + t.CutterWidth - t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength + t.CutterWidth);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength + t.CutterWidth - t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength + t.CutterWidth);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
            }
            else /* Not a slitting saw */
            {
                if (t.TipRadius == 0)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.Diameter / 2, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.CutterWidth);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.CutterWidth);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
                else if (Math.Abs(t.TipRadius - t.Diameter / 2) < Math.Pow(10, -5))
                {
                    pt1 = new Point(0, 0);
                    //pt2 = new Point(t.ArborDiameter / 2, t.CutterWidth);
                    pt2 = new Point(t.ArborDiameter / 2, t.TipRadius + Math.Sqrt(t.TipRadius*t.TipRadius - t.ArborDiameter*t.ArborDiameter/4));
                    center = new Point(0, t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
                else
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, 0);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.TipRadius);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.CutterWidth - t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, t.CutterWidth);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.CutterWidth - t.TipRadius);
                    tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.CutterWidth);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    tool_segments.Add(new GeomSegment(pt1, pt2, false));
                }
            }

            return tool_segments;
        }

        private static List<GeomSegment> SpotDrillToGeometry(FMSpotDrill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            if (t.IsCenterDrill)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, t.Diameter / 2 / Lib.TanDegrees(t.SpotTipAngle / 2));
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.Length + (t.BodyDiameter - t.Diameter) / 2 / Lib.TanDegrees(30));
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, t.Diameter / 2 / Lib.TanDegrees(t.SpotTipAngle / 2));
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }

            return tool_segments;
        }

        private static List<GeomSegment> TwistDrillToGeometry(FMTwistDrill t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.OverallLength;

            tool_segments = new List<GeomSegment>();

            if (t.IsInsertDrill) /* Insert twist drill */
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2 - t.angle * Lib.TanDegrees(59), 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.angle);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            else /* Twist drill */
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, t.Diameter / 2 / Lib.TanDegrees(t.angle / 2));
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.OverallLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }

            return tool_segments;
        }

        private static List<GeomSegment> BoringBarToGeometry(FMBoringBar t, ref double tool_diam, ref double tool_len)
        {
            Point pt1, pt2, center;
            List<GeomSegment> tool_segments;

            if (t == null) return null;
            tool_diam = t.Diameter;
            tool_len = t.Length;

            tool_segments = new List<GeomSegment>();

            if (t.TipRadius == 0)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.ExposedLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2 - t.TipRadius, 0);
                tool_segments.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                center = new Point(t.Diameter / 2 - t.TipRadius, t.TipRadius);
                pt2 = new Point(t.Diameter / 2, t.TipRadius);
                tool_segments.Add(new GeomSegment(pt1, pt2, center, false, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.ExposedLength);
                tool_segments.Add(new GeomSegment(pt1, pt2, false));
            }

            return tool_segments;
        }

        public static string ConstructToolHeader(string t_type, int teeth_num,
                        double flute_len)
        {
            return ConstructToolHeader(t_type, teeth_num, flute_len, -1);
        }

        public static string ConstructToolHeader(string t_type, int teeth_num,
                        double flute_len, double shank_diam)
        {
            return Lib.String2XML("Teeth", teeth_num) + Lib.EOL +
                    Lib.String2XML("Type", t_type) + Lib.EOL +
                    "<Cutter FluteLength =\"" + ToolUnits2DocUnits(flute_len) + "\"" +
                    (shank_diam >= 0 ? " ShankDiameter=\"" + ToolUnits2DocUnits(shank_diam) + "\"" : "") +
                    ">";
        }

        public static double ToolUnits2DocUnits(double value)
        {
            if (is_metric && !Variables.doc.Metric)
                value = value / 25.4;
            else if (!is_metric && Variables.doc.Metric)
                value *= 25.4;

            return Math.Round(value, 4);
        }

        private static string LatheToolProfileToString(List<GeomSegment> profile, int indentation, bool is_tool)
        {
            string tool_profile = "";

            if (profile == null) return "";
            if (profile.Count == 0) return "";

            foreach (GeomSegment segment in profile)
                tool_profile += segment.ToString(indentation) + Environment.NewLine;

            if (is_tool)
                return Lib.Indent(indentation - 1) + "BEGIN_PROFILE_EXTRUDED" + Environment.NewLine +
                        Lib.Indent(indentation) + "|HEIGHT|0.1|" + Lib.EOL +
                                    tool_profile +
                       Lib.Indent(indentation - 1) + "END";
            else
                return Lib.Indent(indentation - 1) + "BEGIN_PROFILE_EXTRUDED" + Environment.NewLine +
                                    tool_profile +
                       Lib.Indent(indentation - 1) + "END";

        }

        private static string ToolProfileToString(List<GeomSegment> profile, int indentation)
        {
            string tool_profile = "";

            if (profile == null) return "";
            if (profile.Count == 0) return "";

            foreach (GeomSegment segment in profile)
                tool_profile += segment.ToString(indentation) + Environment.NewLine;
            /* The profile must end with x=0 point */
            GeomSegment last_segment = new GeomSegment(profile[profile.Count - 1].GetPoint2(), new Point(0, profile[profile.Count - 1].GetPoint2().y), false);
            tool_profile += last_segment.ToString(indentation) + Environment.NewLine;


            return Lib.Indent(indentation - 1) + "BEGIN_PROFILE" + Environment.NewLine +
                                tool_profile +
                   Lib.Indent(indentation - 1) + "END";
        }
    }
}
