// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using FeatureCAM;
using System.IO;

namespace FeatureCAMExporter
{
    public class FeatureCAMTool
    {
        private string output_dir;
        public List<GeomSegment> cutter_geom,
                                 shank_geom,
                                 holder_geom;
        public string group,
                      name,
                      id,
                      holder_stl_fpath,
                      turret_prefix,
                      optional_id;
        public int tool_number,
                   num_of_flutes,
                   len_offset;
        public double cutter_len,
                      shank_dia,
                      diam,
                      overall_length,
                      exposed_length,
                      thickness,
                      prog_pt_x,
                      prog_pt_z,
                      holder_length;
        public bool is_metric,
                    is_milling_tool,
                    is_doc_metric;
        public tagFMTurretType turr_type;

        private static double EPS = 0.000001;

        public FeatureCAMTool() { }

        public FeatureCAMTool(FMToolMap2 toolmap, FMTool tool, 
                              tagFMTurretIDType turret_id, tagFMTurningInputMode turning_input_mode, string files_output_dir, bool is_doc_metric)
        {
            if (tool == null) return;

            this.output_dir = Path.Combine(files_output_dir, "Holders");
            this.is_doc_metric = is_doc_metric;
            if (!Directory.Exists(this.output_dir))
                Directory.CreateDirectory(this.output_dir);
            this.holder_stl_fpath = Path.Combine(output_dir, "holder_" + tool.Name.Replace(":", "_").Replace("|", "_") + ".stl");

            is_metric = tool.Metric;
            tool_number = toolmap.ToolNumber;
            name = tool.Name;
            exposed_length = tool.ExposedLength;
            turr_type = toolmap.turret;
            //if (tool_id != "")
            //    id = tool_id;
            //else
                id = toolmap.ToolNumber.ToString("00");
            switch (turret_id)
            {
                case tagFMTurretIDType.eTIDT_MainLower:
                    this.turret_prefix = "Main_Lower_";
                    break;
                case tagFMTurretIDType.eTIDT_MainUpper:
                    this.turret_prefix = "Main_Upper_";
                    break;
                case tagFMTurretIDType.eTIDT_SubLower:
                    this.turret_prefix = "Sub_Lower_";
                    break;
                case tagFMTurretIDType.eTIDT_SubUpper:
                    this.turret_prefix = "Sub_Upper_";
                    break;
                default:
                    this.turret_prefix = "";
                    break;
            }

            cutter_geom = new List<GeomSegment>();
            shank_geom = new List<GeomSegment>();
            holder_geom = new List<GeomSegment>();
            FMToolHolder tool_holder = (FMToolHolder)tool.DefaultHolder;
            if (tool_holder != null)
                holder_length = Utilities.UnitsToUnits(tool_holder.Length, tool_holder.Metric, tool.Metric);
            switch (tool.ToolGroup)
            {
                case tagFMToolGroup.eTG_EndMill:
                    is_milling_tool = true;
                    EndMill((FMEndMill)tool);
                    break;
                case tagFMToolGroup.eTG_ChamferMill:
                    is_milling_tool = true;
                    ChamferMill((FMChamferMill)tool);
                    break;
                case tagFMToolGroup.eTG_CounterBore:
                    is_milling_tool = true;
                    CounterBore((FMCounterBore)tool);
                    break;
                case tagFMToolGroup.eTG_CounterSink:
                    is_milling_tool = true;
                    CounterSink((FMCounterSink)tool);
                    break;
                case tagFMToolGroup.eTG_SpotDrill:
                    is_milling_tool = true;
                    SpotDrill((FMSpotDrill)tool);
                    break;
                case tagFMToolGroup.eTG_TwistDrill:
                    is_milling_tool = true;
                    TwistDrill((FMTwistDrill)tool);
                    break;
                case tagFMToolGroup.eTG_FaceMill:
                    is_milling_tool = true;
                    FaceMill((FMFaceMill)tool);
                    break;
                case tagFMToolGroup.eTG_Ream:
                    is_milling_tool = true;
                    Ream((FMReam)tool);
                    break;
                case tagFMToolGroup.eTG_Tap:
                    is_milling_tool = true;
                    Tap((FMTap)tool);
                    break;
                case tagFMToolGroup.eTG_ThreadMill:
                    is_milling_tool = true;
                    ThreadMill((FMThreadMill)tool);
                    break;
                case tagFMToolGroup.eTG_RoundingMill:
                    is_milling_tool = true;
                    RoundingMill((FMRoundingMill)tool);
                    break;
                case tagFMToolGroup.eTG_SideMill:
                    is_milling_tool = true;
                    SideMill((FMSideMill)tool);
                    break;
                case tagFMToolGroup.eTG_PlungeRough:
                    is_milling_tool = true;
                    PlungeMill((FMPlungeMill)tool);
                    break;
                case tagFMToolGroup.eTG_BoringBar:
                    is_milling_tool = true;
                    BoringBar((FMBoringBar)tool);
                    break;
                case tagFMToolGroup.eTG_TurnOD:
                case tagFMToolGroup.eTG_TurnID:
                case tagFMToolGroup.eTG_TurnGroove:
                    is_milling_tool = false;
                    this.len_offset = (toolmap.LengthOffsetRegister > 0 ? toolmap.LengthOffsetRegister : toolmap.ToolNumber);
                    TurnOD((FMLatheTool)tool, toolmap.turret, turret_id, turning_input_mode);
                    break;
                case tagFMToolGroup.eTG_TurnThread:
                    is_milling_tool = false;
                    TurnThread((FMThreadTool)tool, toolmap.turret, turret_id, turning_input_mode);
                    break;
                default:
                    break;
            }
        }

        public void EndMill(FMEndMill tool)
        {
            double x1, y1, x2, y2, xc, yc;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }
            group = "Unsupported End mill";
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Flutes;
            cutter_len = tool.CutterLength;
            shank_dia = tool.ShankDiameter;

            if (tool.EndRadius < EPS) /* Flat end mill */
            {
                group = "Flat endmill";
                if (tool.DiameterAtBottom)
                {
                    x1 = y1 = 0;
                    x2 = tool.Diameter / 2;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2 + tool.CutterLength * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                    y2 = tool.CutterLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                    shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ShankDiameter / 2;
                    y2 = tool.CutterLength;
                    shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ShankDiameter / 2;
                    y2 = tool.ExposedLength;
                    shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                }
                else
                {
                    x1 = y1 = 0;
                    x2 = tool.Diameter / 2 - tool.CutterLength * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    x1 = x2;
                    y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.CutterLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                    shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ShankDiameter / 2;
                    y2 = tool.CutterLength;
                    shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ShankDiameter / 2;
                    y2 = tool.ExposedLength;
                    shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                }
            }
            else
            {
                if (tool.IsBallEnd) /* Ball end (diameter at bottom greyed out) */
                {
                    group = "Ball end";
                    if (tool.Taper < EPS)
                    {
                        x1 = y1 = 0;
                        x2 = tool.Diameter / 2;
                        y2 = tool.Diameter / 2;
                        xc = 0;
                        yc = tool.Diameter / 2;
                        cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.Diameter / 2, false, true));
                        x1 = x2; y1 = y2;
                        x2 = tool.Diameter / 2;
                        y2 = tool.CutterLength;
                        cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                        shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                        x1 = x2; y1 = y2;
                        x2 = tool.ShankDiameter / 2;
                        y2 = tool.CutterLength;
                        shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                        x1 = x2; y1 = y2;
                        x2 = tool.ShankDiameter / 2;
                        y2 = tool.ExposedLength;
                        shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    }
                    else
                    {
                        x1 = y1 = 0;
                        x2 = tool.EndRadius / 2;
                        y2 = tool.EndRadius / 2;
                        xc = 0;
                        yc = tool.EndRadius / 2;
                        cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.EndRadius / 2, false, true));
                        x1 = x2; y1 = y2;
                        x2 = tool.Diameter / 2;
                        y2 = tool.EndRadius + (tool.Diameter / 2 - tool.EndRadius) * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                        cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                        shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                        x1 = x2; y1 = y2;
                        x2 = tool.ShankDiameter / 2;
                        y2 = tool.EndRadius + (tool.Diameter / 2 - tool.EndRadius) * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                        shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                        x1 = x2; y1 = y2;
                        x2 = tool.ShankDiameter / 2;
                        y2 = tool.ExposedLength;
                        shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    }
                }
                else /* Bull nose */
                {
                    group = "Bull nose";
                    if (tool.Taper < EPS)
                    {
                        x1 = y1 = 0;
                        x2 = tool.Diameter / 2 - tool.EndRadius;
                        y2 = 0;
                        cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                        x1 = x2; y1 = y2;
                        x2 = tool.Diameter / 2;
                        y2 = tool.EndRadius;
                        xc = tool.Diameter / 2 - tool.EndRadius;
                        yc = tool.EndRadius;
                        cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.EndRadius, false, true));
                        x1 = x2; y1 = y2;
                        x2 = tool.Diameter / 2;
                        y2 = tool.CutterLength;
                        cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                        shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                        x1 = x2; y1 = y2;
                        x2 = tool.ShankDiameter / 2;
                        y2 = tool.CutterLength;
                        shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                        x1 = x2; y1 = y2;
                        x2 = tool.ShankDiameter / 2;
                        y2 = tool.ExposedLength;
                        shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    }
                    else
                    {
                        if (tool.DiameterAtBottom)
                        {
                            x1 = y1 = 0;
                            x2 = tool.Diameter / 2 - tool.EndRadius;
                            y2 = 0;
                            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                            x1 = x2; y1 = y2;
                            x2 = tool.Diameter / 2;
                            y2 = tool.EndRadius;
                            xc = tool.Diameter / 2 - tool.EndRadius;
                            yc = tool.EndRadius;
                            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.EndRadius, false, true));
                            x1 = x2; y1 = y2;
                            x2 = tool.Diameter / 2 + (tool.CutterLength - tool.EndRadius) * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                            y2 = tool.CutterLength;
                            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                            x1 = x2; y1 = y2;
                            x2 = tool.ShankDiameter / 2;
                            y2 = tool.CutterLength;
                            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                            x1 = x2; y1 = y2;
                            x2 = tool.ShankDiameter / 2;
                            y2 = tool.ExposedLength;
                            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                        }
                        else
                        {
                            x1 = y1 = 0;
                            x2 = tool.Diameter / 2 - tool.EndRadius - (tool.CutterLength - tool.EndRadius) * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                            y2 = 0;
                            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                            x1 = x2; y1 = y2;
                            x2 = tool.Diameter / 2 - (tool.CutterLength - tool.EndRadius) * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                            y2 = tool.EndRadius;
                            xc = tool.Diameter / 2 - tool.EndRadius - (tool.CutterLength - tool.EndRadius) * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                            yc = tool.EndRadius;
                            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.EndRadius, false, true));
                            x1 = x2; y1 = y2;
                            x2 = tool.Diameter / 2;
                            y2 = tool.CutterLength;
                            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                            x1 = x2; y1 = y2;
                            x2 = tool.ShankDiameter / 2;
                            y2 = tool.CutterLength;
                            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                            x1 = x2; y1 = y2;
                            x2 = tool.ShankDiameter / 2;
                            y2 = tool.ExposedLength;
                            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                        }
                    }
                }
            }
            double holder_len = tool.OverallLength - tool.ExposedLength;
            holder_geom.Add(new GeomSegment(new Point(0, 0), new Point(tool.ShankDiameter / 2, 0), false));
            holder_geom.Add(new GeomSegment(new Point(tool.ShankDiameter / 2, 0), new Point(tool.ShankDiameter / 2, holder_len), false));
            holder_geom.Add(new GeomSegment(new Point(tool.ShankDiameter / 2, holder_len), new Point(0, holder_len), false));
        }

        public void ChamferMill(FMChamferMill tool)
        {
            double x1, y1, x2, y2, xc, yc;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            group = "Chamfer mill";
            diam = tool.InnerDiameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.NumberOfTeeth;
            cutter_len = (tool.OuterDiameter - tool.InnerDiameter) / 2 / Math.Tan(Utilities.Degrees2Radians(tool.angle));
            shank_dia = tool.ShankDiameter;

            x1 = y1 = 0;
            if (tool.TipRadius < EPS)
            {
                x2 = tool.InnerDiameter / 2;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.OuterDiameter / 2;
                y2 = (tool.OuterDiameter - tool.InnerDiameter) / 2 / Math.Tan(Utilities.Degrees2Radians(tool.angle));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            else
            {
                double a = tool.TipRadius * Math.Tan(Utilities.Degrees2Radians(tool.angle / 2));
                x2 = tool.InnerDiameter / 2 - a;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                xc = tool.InnerDiameter / 2 - a;
                yc = tool.TipRadius;
                x2 = tool.InnerDiameter / 2 + a * Math.Cos(Utilities.Degrees2Radians(tool.angle));
                y2 = a * Math.Sin(Utilities.Degrees2Radians(tool.angle));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, false));
                x1 = x2; y1 = y2;
                x2 = tool.OuterDiameter / 2;
                y2 = (tool.OuterDiameter - tool.InnerDiameter) / 2 / Math.Tan(Utilities.Degrees2Radians(tool.angle));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.OuterDiameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }

        public void CounterBore(FMCounterBore tool)
        {
            double x1, y1, x2, y2;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            group = "Counter bore";
            diam = tool.Diameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Flutes;
            cutter_len = tool.Length + tool.PilotLength;
            shank_dia = tool.ShankDiameter;

            x1 = y1 = 0;
            x2 = tool.PilotDiameter / 2;
            y2 = 0;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.PilotDiameter / 2;
            y2 = tool.PilotLength;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.Diameter / 2;
            y2 = tool.PilotLength;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            x1 = x2; y1 = y2;
            x2 = tool.Diameter / 2;
            y2 = tool.PilotLength + tool.Length;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.PilotLength + tool.Length;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }
        
        public void SpotDrill(FMSpotDrill tool)
        {
            double x1, y1, x2, y2, dy;
            /* Here is the rough diagram of the tool. Assuming angle = 118 degrees
             *     |
             *     |
             *     / ( BD/2, CL ) |
             *    /               | dy 
             *   /                |
             *   | ( D/2, CL - (BD - D)/2/(tan(118/2)) )
             *   |
             *   / ( D/2, (D/2)/tan(angle/2)) )
             *  /
             * / 
             * (0, 0)
             */

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.OverallLength;
            num_of_flutes = 1;
            cutter_len = tool.Length;
            shank_dia = tool.BodyDiameter;

            if (tool.IsCenterDrill)
            {
                group = "Center drill";
                x1 = y1 = 0;
                x2 = tool.Diameter / 2;
                y2 = tool.Diameter / 2 / Math.Tan(Utilities.Degrees2Radians(118 / 2));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                dy = (tool.BodyDiameter - tool.Diameter) / 2 / Math.Tan(Utilities.Degrees2Radians(60 / 2));
                cutter_len += dy;
                x1 = x2; y1 = y2;
                x2 = tool.BodyDiameter / 2;
                y2 = tool.Length + dy;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.BodyDiameter / 2;
                y2 = tool.ExposedLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));

                double holder_len = tool.OverallLength - tool.ExposedLength;
                double holder_dia = tool.BodyDiameter / 2;
                holder_geom.Add(new GeomSegment(new Point(0, 0), new Point(holder_dia, 0), false));
                holder_geom.Add(new GeomSegment(new Point(holder_dia, 0), new Point(holder_dia, holder_len), false));
                holder_geom.Add(new GeomSegment(new Point(holder_dia, holder_len), new Point(0, holder_len), false));
            }
            else
            {
                group = "Spot drill";
                x1 = y1 = 0;
                x2 = tool.Diameter / 2;
                y2 = tool.Diameter / 2 / Math.Tan(Utilities.Degrees2Radians(tool.SpotTipAngle / 2));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

                shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.ExposedLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));

                double holder_len = tool.OverallLength - tool.ExposedLength;
                double holder_dia = tool.Diameter / 2;
                holder_geom.Add(new GeomSegment(new Point(0, 0), new Point(holder_dia, 0), false));
                holder_geom.Add(new GeomSegment(new Point(holder_dia, 0), new Point(holder_dia, holder_len), false));
                holder_geom.Add(new GeomSegment(new Point(holder_dia, holder_len), new Point(0, holder_len), false));
            }
        }

        public void TwistDrill(FMTwistDrill tool)
        {
            double x1, y1, x2, y2;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.OverallLength;
            num_of_flutes = 1;
            cutter_len = tool.Length;
            shank_dia = tool.ShankDiameter;

            if (tool.IsInsertDrill) /* Insert twist drill */
            {
                group = "Insert twist drill";
                x1 = y1 = 0;
                x2 = tool.Diameter / 2 - tool.angle * Math.Tan(Utilities.Degrees2Radians(59));
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.angle;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            else /* Twist drill */
            {
                group = "Twist drill";
                x1 = y1 = 0;
                x2 = tool.Diameter / 2;
                y2 = tool.Diameter / 2 / Math.Tan(Utilities.Degrees2Radians(tool.angle / 2));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.Length;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));

            double holder_len = tool.OverallLength - tool.ExposedLength;
            double holder_dia = tool.ShankDiameter / 2;
            holder_geom.Add(new GeomSegment(new Point(0, 0), new Point(holder_dia, 0), false));
            holder_geom.Add(new GeomSegment(new Point(holder_dia, 0), new Point(holder_dia, holder_len), false));
            holder_geom.Add(new GeomSegment(new Point(holder_dia, holder_len), new Point(0, holder_len), false));
        }

        public void CounterSink(FMCounterSink tool)
        {
            double x1, y1, x2, y2;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.BodyDiameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Flutes;
            cutter_len = tool.BodyDiameter / 2 / Math.Tan(Utilities.Degrees2Radians(tool.angle));
            shank_dia = tool.ShankDiameter;

            group = "Counter sink";

            if (tool.FlatDiameter < EPS)
            {
                x1 = y1 = 0;
                x2 = tool.BodyDiameter / 2;
                y2 = tool.BodyDiameter / 2 / Math.Tan(Utilities.Degrees2Radians(tool.angle / 2));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            else
            {
                x1 = y1 = 0;
                x2 = tool.FlatDiameter / 2;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.BodyDiameter / 2;
                y2 = (tool.BodyDiameter - tool.FlatDiameter) / 2 / Math.Tan(Utilities.Degrees2Radians(tool.angle / 2));
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }

            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.BodyDiameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }

        public void FaceMill(FMFaceMill tool)
        {
            double x1, y1, x2, y2, xc, yc;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.ExposedLength;
            num_of_flutes = tool.Teeth;
            cutter_len = tool.Height;
            shank_dia = -1;

            group = "Face mill";

            if (tool.CornerIsChamfer)
            {
                double r_e = tool.EffectiveDiameter / 2;
                double r = tool.Diameter / 2;

                x1 = y1 = 0;
                if (tool.TipRadius < EPS)
                {
                    x2 = r_e;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = r;
                    y2 = r - r_e;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = r;
                    y2 = r - tool.Height;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                }
                else
                {
                    double a = Math.Tan(Utilities.Degrees2Radians(22.5)) * tool.TipRadius;
                    double a1 = Math.Sin(Utilities.Degrees2Radians(45)) * a;

                    x2 = r_e - a;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = r_e + a1;
                    y2 = a1;
                    xc = r_e - a;
                    yc = tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                    x1 = x2; y1 = y2;
                    x2 = r - a1;
                    y2 = r - r_e - a1;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = r;
                    y2 = r - r_e + a;
                    xc = r - tool.TipRadius;
                    yc = r - r_e + a;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                    x1 = x2; y1 = y2;
                    x2 = r;
                    y2 = tool.Height;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                }
            }
            else
            {
                x1 = y1 = 0;
                if (tool.CornerRadius < EPS)
                {
                    x2 = tool.Diameter / 2;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.Height;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                }
                else
                {
                    x2 = tool.Diameter / 2 - tool.CornerRadius;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.CornerRadius;
                    xc = tool.Diameter / 2 - tool.CornerRadius;
                    yc = tool.CornerRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.CornerRadius, false, true));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.Height;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                }
            }

            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.Diameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }

        public void Ream(FMReam tool)
        {
            double x1, y1, x2, y2;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Flutes;
            cutter_len = tool.Length;
            shank_dia = tool.ShankDiameter;

            group = "Reamer";

            if (tool.Taper < EPS)
            {
                x1 = y1 = 0;
                x2 = tool.Diameter / 2;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            }
            else
            {
                x1 = y1 = 0;
                x2 = tool.Diameter / 2 - tool.Length * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            }

            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.Length;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.ExposedLength;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }

        public void Tap(FMTap tool)
        {
            double x1, y1, x2, y2;
            double pitch,
                   half_pitch,
                   tooth_depth,
                   cur_y;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Flutes;
            cutter_len = tool.Length;
            shank_dia = tool.Diameter;

            group = "Tap";

            if (tool.Metric)
                pitch = tool.thread;
            else
                pitch = 1 / tool.thread;
            half_pitch = pitch / 2;
            tooth_depth = half_pitch / Math.Tan(Utilities.Degrees2Radians(30));

            x1 = y1 = 0;
            x2 = (tool.Diameter - 0.8 * pitch) / 2;
            y2 = 0;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            cur_y = 2 * pitch;
            x2 = tool.Diameter / 2;
            y2 = cur_y;
            cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));

            while (cur_y + pitch <= tool.Length)
            {
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2 - tooth_depth;
                y2 = cur_y + half_pitch;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                cur_y += pitch;
                x2 = tool.Diameter / 2;
                y2 = cur_y;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            if (cur_y + pitch > tool.Length)
            {
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }

            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.Diameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }

        public void ThreadMill(FMThreadMill tool)
        {
            double x1, y1, x2, y2;
            double pitch,
                   half_pitch,
                   tooth_depth,
                   cur_y;
            int num_of_pitches;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.BodyDiameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Flutes;
            cutter_len = tool.CutterLength;
            shank_dia = tool.ShankDiameter;

            group = "Thread mill";

            num_of_pitches = (int)(tool.CutterLength / tool.MaxPitch);
            pitch = tool.MaxPitch;
            half_pitch = pitch / 2;
            tooth_depth = half_pitch / Math.Tan(Utilities.Degrees2Radians(30));

            if (tool.Taper < EPS)
            {
                x1 = y1 = 0;
                x2 = tool.CutterDiameter / 2 - tooth_depth;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                cur_y = 0;
                while (cur_y + pitch <= tool.CutterLength)
                {
                    x1 = x2; y1 = y2;
                    x2 = tool.CutterDiameter / 2;
                    y2 = cur_y + half_pitch;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    cur_y += pitch;
                    x2 = tool.CutterDiameter / 2 - tooth_depth;
                    y2 = cur_y;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                }
                if (cur_y + pitch > tool.CutterLength)
                {
                    x1 = x2; y1 = y2;
                    x2 = tool.CutterDiameter / 2 - tooth_depth;
                    y2 = tool.CutterLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                }
            }
            else
            {
                double x, last_y = 0, last_x = 0;
                x1 = y1 = 0;
                x = tool.CutterDiameter / 2 - (num_of_pitches - 1) * pitch * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                x2 = x - tooth_depth;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                for (int i = 1; i <= num_of_pitches; i++)
                {
                    x = tool.CutterDiameter / 2 - (num_of_pitches - i) * pitch * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                    x1 = x2; y1 = y2;
                    x2 = x;
                    y2 = (i - 0.5) * pitch;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = x - tooth_depth;
                    y2 = i * pitch;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    last_x = x - tooth_depth;
                    last_y = i * pitch;
                }
                if (y2 < tool.CutterLength)
                {
                    x1 = x2; y1 = y2;
                    x2 = x1 + (tool.CutterLength - num_of_pitches * pitch) * Math.Tan(Utilities.Degrees2Radians(tool.Taper));
                    y2 = tool.CutterLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                }
            }
            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.BodyDiameter / 2;
            y2 = tool.CutterLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.BodyDiameter / 2;
            y2 = tool.BodyLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.BodyLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }
        
        public void RoundingMill(FMRoundingMill tool) 
        {
            double x1, y1, x2, y2, xc, yc;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.InnerDiameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Teeth;
            cutter_len = tool.Radius;
            shank_dia = tool.ShankDiameter;

            group = "Rounding mill";

            double tmp = (tool.OuterDiameter - tool.InnerDiameter) / 2 - tool.Radius;
            if (Math.Abs((tool.OuterDiameter - tool.InnerDiameter) / 2 - tool.Radius) < EPS)
            {
                x1 = y1 = 0;
                x2 = tool.InnerDiameter / 2;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.OuterDiameter / 2;
                y2 = tool.Radius;
                xc = tool.InnerDiameter / 2 + tool.Radius;
                yc = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.Radius, true, true));

                shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.OuterDiameter / 2;
                y2 = tool.ExposedLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.ShankDiameter / 2;
                y2 = tool.ExposedLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.ShankDiameter / 2;
                y2 = tool.OverallLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            }
            else
            {
                x1 = y1 = 0;
                x2 = tool.InnerDiameter / 2;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.InnerDiameter / 2 + tool.Radius;
                y2 = tool.Radius;
                xc = tool.InnerDiameter / 2 + tool.Radius;
                yc = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.Radius, true, true));

                shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.OuterDiameter / 2;
                y2 = tool.Radius;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.OuterDiameter / 2;
                y2 = tool.ExposedLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.ShankDiameter / 2;
                y2 = tool.ExposedLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.ShankDiameter / 2;
                y2 = tool.OverallLength;
                shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            }
        }
 
        public void SideMill(FMSideMill tool) 
        {
            double x1, y1, x2, y2, xc, yc;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.Teeth;
            cutter_len = tool.CutterWidth;
            shank_dia = tool.ShankDiameter;

            group = "Side mill";

            if (tool.IsSlittingSaw)
            {
                /* Slitting saw */
                if (tool.TipRadius < EPS)
                {
                    x1 = y1 = 0;
                    x2 = tool.ArborDiameter / 2;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.ArborTipLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.ArborTipLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.ArborTipLength + tool.CutterWidth;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.ArborTipLength + tool.CutterWidth;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                }
                else if (tool.TipRadius != tool.Diameter / 2)
                {
                    x1 = y1 = 0;
                    x2 = tool.ArborDiameter / 2;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.ArborTipLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.ArborTipLength + tool.CutterWidth;
                    xc = 0;
                    yc = tool.ArborTipLength + tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                }
                else
                {
                    x1 = y1 = 0;
                    x2 = tool.ArborDiameter / 2;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.ArborTipLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2 - tool.TipRadius;
                    y2 = tool.ArborTipLength;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.ArborTipLength + tool.TipRadius;
                    xc = tool.Diameter / 2 - tool.TipRadius;
                    yc = tool.ArborTipLength + tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.ArborTipLength + tool.CutterWidth - tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2 - tool.TipRadius;
                    y2 = tool.ArborTipLength + tool.CutterWidth;
                    xc = tool.Diameter / 2 - tool.TipRadius;
                    yc = tool.ArborTipLength + tool.CutterWidth - tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.ArborTipLength + tool.CutterWidth;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y2), new Point(x2, y2), false));
                }
            }
            else
            {
                /* Not a slitting saw */
                if (tool.TipRadius < EPS)
                {
                    x1 = y1 = 0;
                    x2 = tool.Diameter / 2;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y2), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.CutterWidth;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y2), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.CutterWidth;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y2), new Point(x2, y2), false));
                }
                else if (tool.TipRadius - tool.Diameter / 2 < Math.Pow(10.0, -5))
                {
                    x1 = y1 = 0;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.TipRadius + Math.Sqrt(tool.TipRadius * tool.TipRadius - tool.ArborDiameter * tool.ArborDiameter / 4);
                    xc = 0;
                    yc = tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                }
                else
                {
                    x1 = y1 = 0;
                    x2 = tool.Diameter / 2 - tool.TipRadius;
                    y2 = 0;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y2), new Point(x2, y2), false));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.TipRadius;
                    xc = tool.Diameter / 2 - tool.TipRadius;
                    yc = tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2;
                    y2 = tool.CutterWidth - tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y2), new Point(x2, y2), true));
                    x1 = x2; y1 = y2;
                    x2 = tool.Diameter / 2 - tool.TipRadius;
                    y2 = tool.CutterWidth;
                    xc = tool.Diameter / 2 - tool.TipRadius;
                    yc = tool.CutterWidth - tool.TipRadius;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                    x1 = x2; y1 = y2;
                    x2 = tool.ArborDiameter / 2;
                    y2 = tool.CutterWidth;
                    cutter_geom.Add(new GeomSegment(new Point(x1, y2), new Point(x2, y2), false));
                }
            }
            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ArborDiameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }
        
        public void PlungeMill(FMPlungeMill tool) 
        {
            double x1, y1, x2, y2, xc, yc;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.OverallLength;
            num_of_flutes = tool.NumberOfInserts;
            cutter_len = tool.CutterLength;
            shank_dia = tool.ShankDiameter;

            group = "Plunge mill";

            if (tool.IsCenterCutting)
            {
                x1 = y1 = 0;
                x2 = tool.Diameter / 2;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.CutterLength;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            else
            {
                x1 = y1 = 0;
                x2 = tool.Diameter / 2 - tool.InsertTipRadius;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.InsertTipRadius;
                xc = tool.Diameter / 2 - tool.InsertTipRadius;
                yc = tool.InsertTipRadius;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.InsertTipRadius, false, true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.InsertLength;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.CutterLength;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }

            shank_geom.Add(new GeomSegment(new Point(0, y2), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.CutterLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
            x1 = x2; y1 = y2;
            x2 = tool.ShankDiameter / 2;
            y2 = tool.OverallLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }

        public void BoringBar(FMBoringBar tool)
        {
            double x1, y1, x2, y2, xc, yc;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            diam = tool.Diameter;
            overall_length = tool.ExposedLength;
            num_of_flutes = 1;
            cutter_len = tool.Length;
            shank_dia = tool.ExposedLength;

            group = "Boring bar";

            if (tool.TipRadius == 0)
            {
                x1 = y1 = 0;
                x2 = tool.Diameter / 2;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            else
            {
                x1 = y1 = 0;
                x2 = tool.Diameter / 2 - tool.TipRadius;
                y2 = 0;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.TipRadius;
                xc = tool.Diameter / 2 - tool.TipRadius;
                yc = tool.TipRadius;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), new Point(xc, yc), tool.TipRadius, false, true));
                x1 = x2; y1 = y2;
                x2 = tool.Diameter / 2;
                y2 = tool.Length;
                cutter_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), true));
            }
            x1 = x2; y1 = y2;
            x2 = tool.Diameter / 2;
            y2 = tool.ExposedLength;
            shank_geom.Add(new GeomSegment(new Point(x1, y1), new Point(x2, y2), false));
        }

        public void TurnOD(FMLatheTool tool, tagFMTurretType turret_type, tagFMTurretIDType turret_id,
                            tagFMTurningInputMode turning_input_mode)
        {
            FMGeoms geoms = null;
            IFMGCurve cutter, holder;
            FMGeoms crv_pieces;
            bool is_prev_piece_circle = false;
            int orient_coef_x = 1, orient_coef_z = 1;
            int rot_x, rot_y, rot_z;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            exposed_length = (tool.ExposedLength == 0 ? tool.HolderLength : tool.ExposedLength);
            thickness = (!tool.IsLeftHandTool ? -0.1 : 0.1);
            num_of_flutes = 2;
            prog_pt_x = tool.XProgPt;
            prog_pt_z = tool.ZProgPt;

            group = "Lathe";

            ComputeTurningToolOrientation(tool.HolderOrientation, turret_type, turret_id, false, false, false, false, out rot_x, out rot_y, out rot_z);

            geoms = tool.GetCopyOfHolderCurve(tool.Metric);

            if (geoms == null) return;

            if (geoms.Count == 1) //Holder is a solid
            {
                cutter = (IFMGCurve)geoms.Item(1);
                holder = null;
            }
            else
            {
                holder = (IFMGCurve)geoms.Item(1);
                cutter = (IFMGCurve)geoms.Item(2);
            }

            crv_pieces = (FMGeoms)cutter.ToGeometry(tool.Metric);
            if (crv_pieces != null)
            {
                for (int i = 1; i <= crv_pieces.Count; i++)
                    cutter_geom.Add(CurveSegmentToGeomSegment((FMGeom)crv_pieces.Item(i), i, ref is_prev_piece_circle, turning_input_mode, orient_coef_x, orient_coef_z));
            }
        }

        public void TurnThread(FMThreadTool tool, tagFMTurretType turret_type, tagFMTurretIDType turret_id,
                            tagFMTurningInputMode turning_input_mode)
        {
            FMGeoms geoms = null;
            IFMGCurve cutter, holder;
            FMGeoms crv_pieces;
            bool is_prev_piece_circle = false; /* Value of Options -> Turning input mode menu */
            int orient_coef_x = 1, orient_coef_z = 1;
            int rot_x, rot_y, rot_z;

            prog_pt_x = tool.XProgPt;
            prog_pt_z = tool.ZProgPt;

            if (tool == null) return;

            try
            {
                bool saved = tool.SaveHolderGeomAsSTL(this.holder_stl_fpath, this.is_doc_metric);
                if (!File.Exists(this.holder_stl_fpath))
                    this.holder_stl_fpath = "";
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "Export tool holder");
            }

            exposed_length = (tool.ExposedLength == 0 ? tool.HolderLength : tool.ExposedLength);
            thickness = (!tool.IsLeftHandTool ? -0.1 : 0.1);
            num_of_flutes = 2;

            group = "Turn Thread";

            ComputeTurningToolOrientation(tool.HolderOrientation, turret_type, turret_id, false, false, false, false, out rot_x, out rot_y, out rot_z);
            geoms = tool.GetCopyOfHolderCurve(tool.Metric);

            if (geoms.Count == 1) //Holder is a solid
            {
                cutter = (IFMGCurve)geoms.Item(1);
                holder = null;
            }
            else
            {
                holder = (IFMGCurve)geoms.Item(1);
                cutter = (IFMGCurve)geoms.Item(2);
            }

            crv_pieces = (FMGeoms)cutter.ToGeometry(tool.Metric);
            if (crv_pieces != null)
            {
                for (int i = 1; i <= crv_pieces.Count; i++)
                    cutter_geom.Add(CurveSegmentToGeomSegment((FMGeom)crv_pieces.Item(i), i, ref is_prev_piece_circle, turning_input_mode, orient_coef_x, orient_coef_z));
            }

        }


        private static void ComputeTurningToolOrientation(tagFMTurnHolderOrientation holder_orient,
                        tagFMTurretType turret_type, tagFMTurretIDType turret_id, 
                        bool is_lmss_milling_head, bool is_umss_milling_head, bool is_lsss_milling_head, bool is_usss_milling_head,
                        out int rot_x, out int rot_y, out int rot_z)
        {
            bool is_skip_orientation = false;
            if (turret_id == tagFMTurretIDType.eTIDT_MainLower)
                is_skip_orientation = is_lmss_milling_head; //Variables.lmss.is_milling_head;
            else if (turret_id == tagFMTurretIDType.eTIDT_MainUpper)
                is_skip_orientation = is_umss_milling_head; //Variables.umss.is_milling_head;
            else if (turret_id == tagFMTurretIDType.eTIDT_SubLower)
                is_skip_orientation = is_lsss_milling_head; //Variables.lsss.is_milling_head;
            else if (turret_id == tagFMTurretIDType.eTIDT_SubUpper)
                is_skip_orientation = is_usss_milling_head; //Variables.usss.is_milling_head;

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


        private static GeomSegment CurveSegmentToGeomSegment(FMGeom crv_piece, int crv_index,
                                    ref bool is_prev_piece_circle, tagFMTurningInputMode turning_input_mode,
                                    int orient_coef_x, int orient_coef_z)
        {
            GeomSegment crv_def = null;
            FMGCircle circle;
            FMGLine line;
            double x1, y1, z1,
                   x2, y2, z2,
                   xc, yc, zc;
            bool temp;

            if (IsLine(crv_piece))
            {
                line = (FMGLine)crv_piece;
                line.GetEndpoints(out x1, out y1, out z1, out x2, out y2, out z2, out temp, out temp);
                /* Line coords can either be in XYZ, Radius or Diameter mode. 
                 * If they're in diameter mode, we need to divide x coords by 2 */
                if (turning_input_mode == tagFMTurningInputMode.eTIM_DIAMETER)
                {
                    //x1 = x1 / 2;
                    //x2 = x2 / 2;
                }
                crv_def = new GeomSegment(new Point(orient_coef_x * x1, orient_coef_z * z1),
                                          new Point(orient_coef_x * x2, orient_coef_z * z2));
                is_prev_piece_circle = false;
            }
            else if (IsCircle(crv_piece))
            {
                circle = (FMGCircle)crv_piece;
                circle.GetEndpoints(out x1, out y1, out z1, out x2, out y2, out z2, out temp);
                circle.GetCenter(out xc, out yc, out zc);
                if (turning_input_mode == tagFMTurningInputMode.eTIM_DIAMETER)
                {
                    //x1 = x1 / 2;
                    //x2 = x2 / 2;
                }
                /* Arcs are always in XYZ mode, so we don't need to worry about
                  * dividing X values by 2 */
                /* Compute direction of the arc. Using sign of j component 
                 * of the cross product of 2 vectors*/
                //if ((z1 - zc) * (x2 - xc) - (x1 - xc) * (z2 - zc) > 0)
                    crv_def = new GeomSegment(new Point(x1, z1), new Point(x2, z2), 
                                              new Point(orient_coef_x * xc, orient_coef_z * zc), circle.Radius, false);
                is_prev_piece_circle = true;
            }
            else
                is_prev_piece_circle = false;

            return crv_def;
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


    }

}

