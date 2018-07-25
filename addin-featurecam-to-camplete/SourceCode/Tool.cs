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
using FeatureCAM;

namespace FeatureCAMToCAMplete
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

        public string ToString(bool metric)
        {
            return Math.Round(Lib.FromUnitsToUnits(x, metric, true), 4) + " " + Math.Round(Lib.FromUnitsToUnits(y, metric, true), 4);
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

        public string ToString(int indentation, bool metric)
        {
            if (!is_arc) /* line */
                return Lib.Indent(indentation) + "<LINE>" +
                                        "<START>" + pt1.ToString(metric) + "</START><END>" + pt2.ToString(metric) + "</END>" +
                       "</LINE>";
            else
                return Lib.Indent(indentation) + "<ARC DIR=\"" + (is_arc_cw ? "CW" : "CCW") + "\">" + Environment.NewLine +
                        Lib.Indent(indentation + 1) + "<START>" + pt1.ToString(metric) + "</START>" + Environment.NewLine +
                        Lib.Indent(indentation + 1) + "<END>" + pt2.ToString(metric) + "</END>" + Environment.NewLine +
                        Lib.Indent(indentation + 1) + "<CENTER>" + arc_center.ToString(metric) + "</CENTER>" + Environment.NewLine +
                        Lib.Indent(indentation) + "</ARC>";
        }
    }

    class Tool
    {
        static bool is_metric;

        protected Tool() { }


        public static string ToString(FeatureCAM.FMToolMap2 toolmap, List<string> partline_features)
        {
            List<GeomSegment> cutter = null, shank = null;
            FMToolHolder holder;
            string tool_desc = "";
            string tool_name = "";
            int pot_number = -1; //is this spot in the holder 
            int tool_num = -1;
            int rad_comp = -1; //DiamC/number of the radius compensation
            int comp_1st_len = -1; //LenC/Number of the 1st length compensation
            int comp_2nd_len = 0; //LenC2/Number of the 2nd length compensation 
            string tool_type = "";
            double tool_diam = 0, tool_len = 0;

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
                tool_diam = 0;
                tool_len = 0;
                cutter = null;
                shank = null;
                switch (toolmap.Tool.ToolGroup)
                {
                    case tagFMToolGroup.eTG_EndMill:
                        tool_type = "MILL";
                        EndMillToGeometry((FMEndMill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_ChamferMill:
                        tool_type = "MILL";
                        ChamferMillToGeometry((FMChamferMill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_CounterBore:
                        tool_type = "MILL";
                        CounterBoreToGeometry((FMCounterBore)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_CounterSink:
                        tool_type = "MILL";
                        CounterSinkToGeometry((FMCounterSink)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_FaceMill:
                        tool_type = "MILL";
                        FaceMillToGeometry((FMFaceMill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_PlungeRough:
                        tool_type = "MILL";
                        PlungeMillToGeometry((FMPlungeMill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_Ream:
                        tool_type = "MILL";
                        ReamToGeometry((FMReam)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_RoundingMill:
                        tool_type = "MILL";
                        RoundingMillToGeometry((FMRoundingMill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_SideMill:
                        tool_type = "MILL";
                        SideMillToGeometry((FMSideMill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_SpotDrill:
                        tool_type = "DRILL";
                        SpotDrillToGeometry((FMSpotDrill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_Tap:
                        tool_type = "MILL";
                        TapToGeometry((FMTap)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_ThreadMill:
                        tool_type = "MILL";
                        ThreadMillToGeometry((FMThreadMill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_TwistDrill:
                        tool_type = "DRILL";
                        TwistDrillToGeometry((FMTwistDrill)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    case tagFMToolGroup.eTG_BoringBar:
                        tool_type = "MILL";
                        BoringBarToGeometry((FMBoringBar)toolmap.Tool, ref tool_diam, ref tool_len, ref cutter, ref shank);
                        break;
                    default:
                        Variables.warning_msg = "Unsupported tool type";
                        Variables.unsupported_tool_names += (!String.IsNullOrEmpty(Variables.unsupported_tool_names) ? "; " : "") +
                                                            toolmap.Tool.Name;
                        break;
                }

                holder = GetHolder(toolmap);

                string tool_id = "";
                if (Variables.tool_identification == 0) /* tool number */
                    tool_id = toolmap.Tool.Name;
                else
                    tool_id = (!String.IsNullOrEmpty(toolmap.ToolID) ? toolmap.ToolID.ToString() : toolmap.ToolNumber.ToString());
                tool_desc = "<TOOL " +
                                   "CODE=\"" + tool_num + "\" " +
                                   "ID=\"" + tool_id.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;") + "\" " + 
                                   "TYPE=\"" + tool_type + "\" " +
                                   "VER=\"6\" " + 
                                   "TOLERANCE=\"-1\" " +
                                   "UNITS=\"MM\" " +
                                   "OFFSETZ=\"" + Lib.FromUnitsToUnits(tool_len, toolmap.Tool.Metric, true) + "\" " +
                                   "DIAMETER=\"" + Lib.FromUnitsToUnits(tool_diam, toolmap.Tool.Metric, true) + "\" " +
                                   ">" + Environment.NewLine +
                                   Lib.tab + "<CUTTER TYPE=\"CUSTOM_REVOLUTED\">" + Lib.EOL +
                                                ToolProfileToString(cutter, 2, toolmap.Tool.Metric) + Lib.EOL +
                                   Lib.tab + "</CUTTER>" + Lib.EOL +
                                   Lib.tab + "<SHANK TYPE=\"CUSTOM_REVOLUTED\">" + Lib.EOL +
                                                ToolProfileToString(shank, 2, toolmap.Tool.Metric) + Lib.EOL +
                                   Lib.tab + "</SHANK>" + Lib.EOL +
                                   HolderToString(toolmap, holder) + Lib.EOL +
                               "</TOOL>";

            }
            catch //Exception Ex)
            { }

            return tool_desc;
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

            holder_str = Lib.tab + "<HOLDER TYPE=\"CUSTOM_REVOLUTED\" VER=\"2\" POSX=\"0\" POSY=\"0\" POSZ=\"" + Lib.FromUnitsToUnits(toolmap.Tool.ExposedLength, toolmap.Tool.Metric, true) + "\" RZ=\"0\" RY=\"0\" RX=\"0\">" + Lib.EOL +
                         Lib.double_tab + "<CODE>FIRST HOLDER</CODE>" + Lib.EOL +
                         ToolProfileToString(holder_geom, 3, holder.Metric) + Lib.EOL +
                         Lib.tab + "</HOLDER>";

            return holder_str;
        }

        private static FMToolHolder GetHolder(FeatureCAM.FMToolMap2 toolmap)
        {
            FMToolCrib tc;
            FMToolHolder holder = null;

            tc = Variables.doc.get_ActiveToolCrib();

            string holder_name = toolmap.Tool.Holder;

            if (String.IsNullOrEmpty(holder_name))
            {
                holder = (FMToolHolder)toolmap.Tool.DefaultHolder;
            }
            else
            {
                foreach (FMToolHolder tc_holder in tc.ToolHolders)
                {
                    if (tc_holder != null)
                        if (holder_name.Equals(tc_holder.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            holder = tc_holder;
                            Console.WriteLine("Found " + tc_holder.Name);
                        }
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

            pt2 = new Point(h.Diameter / 2, 0);
            holder_segments.Add(new GeomSegment(new Point(0, 0), pt2, false));

            pt1 = pt2;
            pt2 = new Point(h.Diameter / 2, 
                            h.Length);
            holder_segments.Add(new GeomSegment(pt1, pt2, false));

            return holder_segments;
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

        private static void ThreadMillToGeometry(FMThreadMill t, 
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;
            double pitch, half_pitch;
            double tooth_depth, cur_y;
            int num_of_pitches;

            if (t == null) return;

            diam = t.BodyDiameter;
            overall_length = t.OverallLength;

            num_of_pitches = (int)(t.CutterLength / t.MaxPitch);
            pitch = t.MaxPitch;
            half_pitch = pitch / 2;
            tooth_depth = half_pitch / Lib.TanDegrees(30);

            cutter = new List<GeomSegment>();

            if (t.Taper == 0)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.CutterDiameter / 2 - tooth_depth, 0);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                cur_y = 0;
                while (cur_y + pitch <= t.CutterLength)
                {
                    pt1 = pt2;
                    pt2 = new Point(t.CutterDiameter / 2, cur_y + half_pitch);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    cur_y += pitch;
                    pt2 = new Point(t.CutterDiameter / 2 - tooth_depth, cur_y);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                }
                if (cur_y + pitch > t.CutterLength)
                {
                    pt1 = pt2;
                    pt2 = new Point(t.CutterDiameter / 2 - tooth_depth, t.CutterLength);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                }
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.CutterLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                double x;
                pt1 = new Point(0, 0);
                x = t.CutterDiameter / 2 - (num_of_pitches - 1) * pitch * Lib.TanDegrees(t.Taper);
                pt2 = new Point(x - tooth_depth, 0);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                for (int i = 1; i <= num_of_pitches; i++)
                {
                    x = t.CutterDiameter / 2 - (num_of_pitches - i) * pitch * Lib.TanDegrees(t.Taper);
                    pt1 = pt2;
                    pt2 = new Point(x, (i - 0.5) * pitch);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(x - tooth_depth, i * pitch);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                }
                if (pt2.y < t.CutterLength)
                {
                    pt1 = pt2;
                    pt2 = new Point(pt1.x + (t.CutterLength - num_of_pitches*pitch)*Lib.TanDegrees(t.Taper), t.CutterLength);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                }
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.CutterLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
        }

        private static void TapToGeometry(FMTap t, 
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;
            double pitch, half_pitch;
            double tooth_depth, cur_y;

            if (t == null) return;

            diam = t.Diameter;
            overall_length = t.OverallLength;

            if (t.Metric)
                pitch = t.thread;
            else
                pitch = 1 / t.thread;
            half_pitch = pitch / 2;
            tooth_depth = half_pitch / Lib.TanDegrees(30);

            cutter = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point((t.Diameter - 0.8 * pitch) / 2, 0);
            cutter.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            cur_y = 2 * pitch;
            pt2 = new Point(t.Diameter / 2, cur_y);
            cutter.Add(new GeomSegment(pt1, pt2, true));
            while (cur_y + pitch <= t.Length)
            {
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2 - tooth_depth, cur_y + half_pitch);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                cur_y += pitch;
                pt2 = new Point(t.Diameter / 2, cur_y);
                cutter.Add(new GeomSegment(pt1, pt2, true));
            }
            if (cur_y + pitch > t.Length)
            {
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                cutter.Add(new GeomSegment(pt1, pt2, true));
            }
            shank = new List<GeomSegment>();
            shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.OverallLength);
            shank.Add(new GeomSegment(pt1, pt2, false));
        }

        private static void EndMillToGeometry(FMEndMill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2, center;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();
            if (t.EndRadius == 0) /* Flat end mill */
            {
                if (t.DiameterAtBottom)
                {
                    pt2 = new Point(t.Diameter / 2, 0);
                    cutter.Add(new GeomSegment(new Point(0, 0), pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 + t.CutterLength * Lib.TanDegrees(t.Taper), t.CutterLength);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                }
                else
                {
                    pt2 = new Point(t.Diameter / 2 - t.CutterLength * Lib.TanDegrees(t.Taper), 0);
                    cutter.Add(new GeomSegment(new Point(0, 0), pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.CutterLength);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
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
                        cutter.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.CutterLength);
                        cutter.Add(new GeomSegment(pt1, pt2, true));
                        shank = new List<GeomSegment>();
                        shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                        shank.Add(new GeomSegment(pt1, pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                        shank.Add(new GeomSegment(pt1, pt2, false));
                    }
                    else
                    {
                        pt2 = new Point(t.EndRadius / 2, t.EndRadius / 2);
                        center = new Point(0, t.EndRadius / 2);
                        cutter.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.EndRadius + (t.Diameter / 2 - t.EndRadius) * Lib.TanDegrees(t.Taper));
                        cutter.Add(new GeomSegment(pt1, pt2, true));
                        shank = new List<GeomSegment>();
                        shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.EndRadius + (t.Diameter / 2 - t.EndRadius) * Lib.TanDegrees(t.Taper));
                        shank.Add(new GeomSegment(pt1, pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                        shank.Add(new GeomSegment(pt1, pt2, false));
                    }
                }
                else /* Bull nose */
                {
                    if (t.Taper == 0)
                    {
                        pt2 = new Point(t.Diameter / 2 - t.EndRadius, 0);
                        cutter.Add(new GeomSegment(new Point(0, 0), pt2, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.EndRadius);
                        center = new Point(t.Diameter / 2 - t.EndRadius, t.EndRadius);
                        cutter.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                        pt1 = pt2;
                        pt2 = new Point(t.Diameter / 2, t.CutterLength);
                        cutter.Add(new GeomSegment(pt1, pt2, true));
                        shank = new List<GeomSegment>();
                        shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                        shank.Add(new GeomSegment(pt1, pt2, false));
                        pt1 = pt2;
                        pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                        shank.Add(new GeomSegment(pt1, pt2, false));
                    }
                    else
                    {
                        if (t.DiameterAtBottom)
                        {
                            pt2 = new Point(t.Diameter / 2 - t.EndRadius, 0);
                            cutter.Add(new GeomSegment(new Point(0, 0), pt2, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2, t.EndRadius);
                            center = new Point(t.Diameter / 2 - t.EndRadius, t.EndRadius);
                            cutter.Add(new GeomSegment(new Point(0, 0), pt2, center, false, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2 + (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), t.CutterLength);
                            cutter.Add(new GeomSegment(pt1, pt2, true));
                            shank = new List<GeomSegment>();
                            shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                            shank.Add(new GeomSegment(pt1, pt2, false));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                            shank.Add(new GeomSegment(pt1, pt2, false));
                        }
                        else
                        {
                            pt2 = new Point(t.Diameter / 2 - t.EndRadius - (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), 0);
                            cutter.Add(new GeomSegment(new Point(0, 0), pt2, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2 - (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), t.EndRadius);
                            center = new Point(t.Diameter / 2 - t.EndRadius - (t.CutterLength - t.EndRadius) * Lib.TanDegrees(t.Taper), t.EndRadius);
                            cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                            pt1 = pt2;
                            pt2 = new Point(t.Diameter / 2, t.CutterLength);
                            cutter.Add(new GeomSegment(pt1, pt2, true));
                            shank = new List<GeomSegment>();
                            shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
                            shank.Add(new GeomSegment(pt1, pt2, false));
                            pt1 = pt2;
                            pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
                            shank.Add(new GeomSegment(pt1, pt2, false));
                        }
                    }
                }
            }
        }

        private static void ChamferMillToGeometry(FMChamferMill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;

            if (t == null) return;
            diam = t.InnerDiameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point(t.InnerDiameter / 2, 0);
            cutter.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.OuterDiameter / 2, (t.OuterDiameter - t.InnerDiameter) / 2 / Lib.TanDegrees(t.angle));
            cutter.Add(new GeomSegment(pt1, pt2, true));
            shank = new List<GeomSegment>();
            shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.OuterDiameter / 2, t.OverallLength);
            shank.Add(new GeomSegment(pt1, pt2, false));
        }

        private static void CounterBoreToGeometry(FMCounterBore t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point(t.PilotDiameter / 2, 0);
            cutter.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.PilotDiameter / 2, t.PilotLength);
            cutter.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.PilotLength);
            cutter.Add(new GeomSegment(pt1, pt2, true));
            shank = new List<GeomSegment>();
            shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.PilotLength + t.Length);
            shank.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.OverallLength);
            shank.Add(new GeomSegment(pt1, pt2, false));
        }

        private static void CounterSinkToGeometry(FMCounterSink t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;

            if (t == null) return;

            diam = t.BodyDiameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            if (t.FlatDiameter == 0)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.BodyDiameter / 2, t.BodyDiameter / 2 / Lib.TanDegrees(t.angle / 2));
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.FlatDiameter / 2, 0);
                cutter.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, (t.BodyDiameter - t.FlatDiameter) / 2 / Lib.TanDegrees(t.angle / 2));
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
        }

        private static void FaceMillToGeometry(FMFaceMill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2, center;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.ExposedLength;

            cutter = new List<GeomSegment>();

            if (t.CornerRadius == 0)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, 0);
                cutter.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Height);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2 - t.CornerRadius, 0);
                cutter.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.CornerRadius);
                center = new Point(t.Diameter / 2 - t.CornerRadius, t.CornerRadius);
                cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Height);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
        }

        private static void PlungeMillToGeometry(FMPlungeMill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2, center;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            if (t.IsCenterCutting)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, 0);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.CutterLength);
                cutter.Add(new GeomSegment(pt1, pt2, true));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2 - t.InsertTipRadius, 0);
                cutter.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.InsertTipRadius);
                center = new Point(t.Diameter / 2 - t.InsertTipRadius, t.InsertTipRadius);
                cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.InsertLength);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.CutterLength);
                cutter.Add(new GeomSegment(pt1, pt2, true));
            }
            shank = new List<GeomSegment>();
            shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.ShankDiameter / 2, t.CutterLength);
            shank.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.ShankDiameter / 2, t.ExposedLength);
            shank.Add(new GeomSegment(pt1, pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.ShankDiameter / 2, t.OverallLength);
            shank.Add(new GeomSegment(pt1, pt2, false));
        }

        private static void ReamToGeometry(FMReam t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            pt1 = new Point(0, 0);
            pt2 = new Point(t.Diameter / 2, 0);
            cutter.Add(new GeomSegment(pt1, pt2, true));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.ExposedLength);
            cutter.Add(new GeomSegment(pt1, pt2, false));
            shank = new List<GeomSegment>();
            shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
            pt1 = pt2;
            pt2 = new Point(t.Diameter / 2, t.OverallLength);
            shank.Add(new GeomSegment(pt1, pt2, false));

        }

        private static void RoundingMillToGeometry(FMRoundingMill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2, center;

            if (t == null) return;
            diam = t.InnerDiameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            if ((t.OuterDiameter - t.InnerDiameter) / 2 == t.Radius)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.InnerDiameter / 2, 0);
                cutter.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.Radius);
                center = new Point(t.InnerDiameter / 2 + t.Radius, 0);
                cutter.Add(new GeomSegment(pt1, pt2, center, true, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.InnerDiameter / 2, 0);
                cutter.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.InnerDiameter / 2 + t.Radius, t.Radius);
                center = new Point(t.InnerDiameter / 2 + t.Radius, 0);
                cutter.Add(new GeomSegment(pt1, pt2, center, true, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.Radius);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.OuterDiameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
        }

        private static void SideMillToGeometry(FMSideMill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2, center;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            if (t.IsSlittingSaw) /* Slitting saw */
            {
                if (t.TipRadius == 0)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.ArborDiameter / 2, 0);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength + t.CutterWidth);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength + t.CutterWidth);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                }
                else if (t.TipRadius != t.Diameter / 2)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.ArborDiameter / 2, 0);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength + t.CutterWidth);
                    center = new Point(0, t.ArborTipLength + t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                }
                else
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.ArborDiameter / 2, 0);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength + t.TipRadius);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength + t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.ArborTipLength + t.CutterWidth - t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength + t.CutterWidth);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.ArborTipLength + t.CutterWidth - t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ArborTipLength + t.CutterWidth);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                }
            }
            else /* Not a slitting saw */
            {
                if (t.TipRadius == 0)
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.Diameter / 2, 0);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.CutterWidth);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.CutterWidth);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                }
                else if (Math.Abs(t.TipRadius - t.Diameter / 2) < Math.Pow(10, -5))
                {
                    pt1 = new Point(0, 0);
                    //pt2 = new Point(t.ArborDiameter / 2, t.CutterWidth);
                    pt2 = new Point(t.ArborDiameter / 2, t.TipRadius + Math.Sqrt(t.TipRadius*t.TipRadius - t.ArborDiameter*t.ArborDiameter/4));
                    center = new Point(0, t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                }
                else
                {
                    pt1 = new Point(0, 0);
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, 0);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.TipRadius);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2, t.CutterWidth - t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, true));
                    pt1 = pt2;
                    pt2 = new Point(t.Diameter / 2 - t.TipRadius, t.CutterWidth);
                    center = new Point(t.Diameter / 2 - t.TipRadius, t.CutterWidth - t.TipRadius);
                    cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.CutterWidth);
                    cutter.Add(new GeomSegment(pt1, pt2, false));
                    shank = new List<GeomSegment>();
                    shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.ExposedLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                    pt1 = pt2;
                    pt2 = new Point(t.ArborDiameter / 2, t.OverallLength);
                    shank.Add(new GeomSegment(pt1, pt2, false));
                }
            }
        }

        private static void SpotDrillToGeometry(FMSpotDrill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            if (t.IsCenterDrill)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, t.Diameter / 2 / Lib.TanDegrees(t.SpotTipAngle / 2));
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.Length + (t.BodyDiameter - t.Diameter) / 2 / Lib.TanDegrees(30));
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.BodyDiameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, t.Diameter / 2 / Lib.TanDegrees(t.SpotTipAngle / 2));
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
        }

        private static void TwistDrillToGeometry(FMTwistDrill t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.OverallLength;

            cutter = new List<GeomSegment>();

            if (t.IsInsertDrill) /* Insert twist drill */
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2 - t.angle * Lib.TanDegrees(59), 0);
                cutter.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.angle);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
            else /* Twist drill */
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, t.Diameter / 2 / Lib.TanDegrees(t.angle / 2));
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.OverallLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
        }

        private static void BoringBarToGeometry(FMBoringBar t,
                            ref double diam, ref double overall_length,
                            ref List<GeomSegment> cutter, ref List<GeomSegment> shank)
        {
            Point pt1, pt2, center;

            if (t == null) return;
            diam = t.Diameter;
            overall_length = t.ExposedLength;

            cutter = new List<GeomSegment>();

            if (t.TipRadius == 0)
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2, 0);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
            else
            {
                pt1 = new Point(0, 0);
                pt2 = new Point(t.Diameter / 2 - t.TipRadius, 0);
                cutter.Add(new GeomSegment(pt1, pt2, true));
                pt1 = pt2;
                center = new Point(t.Diameter / 2 - t.TipRadius, t.TipRadius);
                pt2 = new Point(t.Diameter / 2, t.TipRadius);
                cutter.Add(new GeomSegment(pt1, pt2, center, false, true));
                shank = new List<GeomSegment>();
                shank.Add(new GeomSegment(new Point(0, pt2.y), pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.Length);
                shank.Add(new GeomSegment(pt1, pt2, false));
                pt1 = pt2;
                pt2 = new Point(t.Diameter / 2, t.ExposedLength);
                shank.Add(new GeomSegment(pt1, pt2, false));
            }
        }

        public static string ConstructToolHeader(string t_type, int teeth_num,
                        double flute_len)
        {
            return ConstructToolHeader(t_type, teeth_num, flute_len, -1);
        }

        private static string ConstructToolHeader(string t_type, int teeth_num,
                        double flute_len, double shank_diam)
        {
            return Lib.String2Xml("Teeth", teeth_num) + Lib.EOL +
                    Lib.String2Xml("Type", t_type) + Lib.EOL +
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

        private static string ToolProfileToString(List<GeomSegment> profile, int indentation, bool metric)
        {
            string tool_profile = "";

            if (profile == null) return "";
            if (profile.Count == 0) return "";

            foreach (GeomSegment segment in profile)
                tool_profile += segment.ToString(indentation + 1, metric) + Environment.NewLine;
            /* The profile must end with x=0 point */
            GeomSegment last_segment = new GeomSegment(profile[profile.Count - 1].GetPoint2(), new Point(0, profile[profile.Count - 1].GetPoint2().y), false);
            tool_profile += last_segment.ToString(indentation + 1, metric) + Environment.NewLine;

            return Lib.Indent(indentation) + "<PROFILE>" + Lib.EOL +
                                tool_profile +
                   Lib.Indent(indentation) + "</PROFILE>";
        }
    }
}