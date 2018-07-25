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
using FeatureCAMExporter;

namespace FeatureCAMToVericut
{
    class VericutTool : FeatureCAMTool
    {
        
        protected VericutTool() { }

        public static string ToXML(FeatureCAMTool fc_tool, bool is_doc_metric)
        {
            StringBuilder tool_info = new StringBuilder();

            try
            {
                if (fc_tool == null) return "";

                if (Variables.are_all_setups_milling) fc_tool.turret_prefix = "";
                if (fc_tool.cutter_geom != null)
                {
                    if (fc_tool.cutter_geom.Count > 0)
                    {
                        if (Variables.doc_options.tool_turret_id_prefix)
                            fc_tool.optional_id += fc_tool.turret_prefix;
                        if (Variables.doc_options.tool_id_option == eToolOptions.eTO_PositionOnly)
                            fc_tool.optional_id += fc_tool.tool_number.ToString("00");
                        else if (Variables.doc_options.tool_id_option == eToolOptions.eTO_PositionAndName)
                            fc_tool.optional_id += String.Format("{0}_{1}", fc_tool.tool_number.ToString("00"), fc_tool.name);
                        else if (Variables.doc_options.tool_id_option == eToolOptions.eTO_IDOnly)
                            fc_tool.optional_id += fc_tool.id;

                        if (fc_tool.is_milling_tool)
                            tool_info = MillingToolToXML(fc_tool, is_doc_metric);
                        else
                            tool_info = TurningToolToXML(fc_tool, is_doc_metric);
                    }
                }
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "ToXML");
            }

            return tool_info.ToString();
        }

        private static StringBuilder TurningToolToXML(FeatureCAMTool fc_tool, bool is_doc_metric)
        {
            string x_coord, y_coord, radius;
            StringBuilder tool_info = new StringBuilder();
            StringBuilder tool_geom = new StringBuilder();

            /* Turning tool */
            tool_info.AppendFormat("<Tool ID=\"{0}\" Units=\"{1}\">\n", fc_tool.optional_id, (is_doc_metric ? "Millimeter" : "Inch"));
            tool_info.AppendFormat("{0}\n", Utilities.String2XML("Description", fc_tool.name, 1));
            tool_info.AppendFormat("{0}\n", Utilities.String2XML("Teeth", fc_tool.num_of_flutes, 1));
            tool_info.AppendFormat("{0}\n", Utilities.String2XML("Type", "Turning", 1));

            tool_info.AppendLine(Utilities.Indent("<Cutter>", 1));
            string thickness = String.Format("Thickness=\"{0:0.####}\"", fc_tool.thickness);
            tool_info.AppendFormat(Utilities.Indent("<Sweep ID=\"{0}\" {1}>\n", 2), fc_tool.group, thickness);
            for (int i = 0; i < fc_tool.cutter_geom.Count; i++)
            {
                GeomSegment segment = fc_tool.cutter_geom[i];
                if (i == 0)
                    tool_geom.Append(segment.pt1.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                if (!segment.is_arc)
                    tool_geom.Append(segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                else
                {
                    tool_geom.Append(Utilities.Indent("<Arc>\n", 1));
                    x_coord = String.Format("<X>{0:0.####}</X>\n", Utilities.UnitsToUnits(segment.arc_center.x, fc_tool.is_metric, is_doc_metric));
                    y_coord = String.Format("<Z>{0:0.####}</Z>\n", Utilities.UnitsToUnits(segment.arc_center.y, fc_tool.is_metric, is_doc_metric));
                    radius = String.Format("<Radius>{0:0.####}</Radius>\n", Utilities.UnitsToUnits(segment.radius, fc_tool.is_metric, is_doc_metric));
                    tool_geom.AppendFormat(Utilities.Indent(x_coord, 2));
                    tool_geom.AppendFormat(Utilities.Indent(y_coord, 2));
                    tool_geom.AppendFormat(Utilities.Indent(radius, 2));
                    tool_geom.AppendFormat(Utilities.Indent("<Direction>Shortest</Direction>\n", 2));
                    tool_geom.Append(Utilities.Indent("</Arc>\n", 1));
                    tool_geom.Append(segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                }
            }
            tool_info.Append(Utilities.Indent(tool_geom.ToString(), 2));
            tool_info.AppendFormat("{0}\n", (Utilities.String2XML("NoSpin", "0", 3)));
            tool_info.Append(Utilities.Indent("</Sweep>\n", 2));
            tool_info.Append(Utilities.Indent("</Cutter>\n", 1));

            /* Holder */
            tool_info.Append(Utilities.Indent("<Holder>\n", 1));
            if (fc_tool.holder_stl_fpath != null && fc_tool.holder_stl_fpath != "") //holder was exported to an .stl file
            {
                tool_info.Append(Utilities.Indent(String.Format("<Model ID=\"holder_{0}\" Type=\"STL\">\n", fc_tool.name), 2));
				tool_info.AppendFormat(Utilities.Indent("<FileName>{0}</FileName>\n", 3), fc_tool.holder_stl_fpath);
                tool_info.Append(Utilities.Indent("<NoSpin>1</NoSpin>\n", 3));
                tool_info.Append(Utilities.Indent("<Origin>\n", 3));
                tool_info.Append(Utilities.Indent("<X>0</X>\n", 4));
                tool_info.Append(Utilities.Indent("<Y>0</Y>\n", 4));
                tool_info.Append(Utilities.Indent("<Z>0</Z>\n", 4));
                tool_info.Append(Utilities.Indent("</Origin>\n", 3));
                tool_info.Append(Utilities.Indent("<Rotation>\n", 3));
                tool_info.Append(Utilities.Indent("<X>0</X>\n", 4));
                tool_info.Append(Utilities.Indent("<Y>0</Y>\n", 4));
                tool_info.Append(Utilities.Indent("<Z>0</Z>\n", 4));
                tool_info.Append(Utilities.Indent("</Rotation>\n", 3));
                tool_info.Append(Utilities.Indent("<Color>255</Color>\n", 3));
                tool_info.Append(Utilities.Indent("</Model>\n", 2));
            }
            else
            {
                tool_info.Append(Utilities.Indent(String.Format("<Sweep ID=\"holder_{0}\" Thickness=\"-1\">\n", fc_tool.name), 2));
                tool_geom = new StringBuilder();
                for (int i = 0; i < fc_tool.holder_geom.Count; i++)
                {
                    GeomSegment segment = fc_tool.holder_geom[i];
                    if (i == 0)
                        tool_geom.Append(segment.pt1.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                    if (!segment.is_arc)
                        tool_geom.Append(segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                    else
                    {
                        tool_geom.Append(Utilities.Indent("<Arc>\n", 1));
                        x_coord = String.Format("<X>{0:0.####}</X>\n", Utilities.UnitsToUnits(segment.arc_center.x, fc_tool.is_metric, is_doc_metric));
                        y_coord = String.Format("<Z>{0:0.####}</Z>\n", Utilities.UnitsToUnits(segment.arc_center.y, fc_tool.is_metric, is_doc_metric));
                        radius = String.Format("<Radius>{0:0.####}</Radius>\n", Utilities.UnitsToUnits(segment.radius, fc_tool.is_metric, is_doc_metric));
                        tool_geom.AppendFormat(Utilities.Indent(x_coord, 2));
                        tool_geom.AppendFormat(Utilities.Indent(y_coord, 2));
                        tool_geom.AppendFormat(Utilities.Indent(radius, 2));
                        tool_geom.AppendFormat(Utilities.Indent("<Direction>Shortest</Direction>\n", 2));
                        tool_geom.Append(Utilities.Indent("</Arc>\n", 1));
                        tool_geom.Append(segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                    }
                }
                tool_info.Append(Utilities.Indent(tool_geom.ToString(), 2));
                tool_info.AppendFormat("{0}\n", (Utilities.String2XML("NoSpin", "0", 3)));
                tool_info.Append(Utilities.Indent("</Sweep>\n", 2));
            }
            tool_info.Append(Utilities.Indent("</Holder>\n", 1));

            tool_info.AppendFormat("{0}\n", Utilities.Indent(
                                    Utilities.String2XML("GagePoint",
                                        Utilities.String2XML("X", 0) + "\n" +
                                        Utilities.String2XML("Y", 0) + "\n" +
                                        Utilities.String2XML("Z", Utilities.UnitsToUnits(fc_tool.exposed_length + fc_tool.holder_length, fc_tool.is_metric, is_doc_metric)))
                                    , 1));
            tool_info.AppendFormat("{0}\n", Utilities.Indent(
                                    String.Format("<DrivenPoint ID=\"{0}\">\n", fc_tool.len_offset) + 
                                        Utilities.String2XML("X", Utilities.UnitsToUnits(fc_tool.prog_pt_x, fc_tool.is_metric, is_doc_metric), 1) + "\n" +
                                        Utilities.String2XML("Y", 0, 1) + "\n" +
                                        Utilities.String2XML("Z", Utilities.UnitsToUnits(fc_tool.prog_pt_z, fc_tool.is_metric, is_doc_metric), 1) + "\n" +
                                    String.Format("</DrivenPoint>\n")
                                    , 1));
            tool_info.Append(Utilities.Indent(
                                Utilities.String2XML("Orientation",
                                    Utilities.String2XML("X", 0) + "\n" +
                                    Utilities.String2XML("Y", 0) + "\n" +
                                    Utilities.String2XML("Z", 0))
                                , 1));
            tool_info.AppendLine("");
            tool_info.Append("</Tool>");

            return tool_info;
        }

        private static StringBuilder MillingToolToXML(FeatureCAMTool fc_tool, bool is_doc_metric)
        {
            string x_coord, y_coord, radius,
                   flute_len, shank_dia,
                   last_pt_str, pt_str;
            StringBuilder tool_info = new StringBuilder();
            StringBuilder tool_geom = new StringBuilder();

            tool_info.AppendFormat("<Tool ID=\"{0}\" Units=\"{1}\">\n", fc_tool.optional_id, (is_doc_metric ? "Millimeter" : "Inch"));
            tool_info.AppendFormat("{0}\n", Utilities.String2XML("Description", fc_tool.name, 1));
            tool_info.AppendFormat("{0}\n", Utilities.String2XML("Teeth", fc_tool.num_of_flutes, 1));
            tool_info.AppendFormat("{0}\n", Utilities.String2XML("Type", "Milling", 1));

            flute_len = String.Format("FluteLength =\"{0:0.####}\"", Utilities.UnitsToUnits(fc_tool.cutter_len, fc_tool.is_metric, is_doc_metric));
            if (fc_tool.shank_dia > 0)
                shank_dia = String.Format("ShankDiameter=\"{0:0.####}\"", Utilities.UnitsToUnits(fc_tool.shank_dia, fc_tool.is_metric, is_doc_metric));
            else
                shank_dia = "";
            tool_info.AppendFormat(Utilities.Indent("<Cutter {0} {1}>\n", 1), flute_len, shank_dia);
            tool_info.AppendFormat(Utilities.Indent("<SOR ID=\"{0}\">\n", 2), fc_tool.group);
            last_pt_str = "";
            for (int i = 0; i < fc_tool.cutter_geom.Count; i++)
            {
                GeomSegment segment = fc_tool.cutter_geom[i];
                if (i == 0)
                {
                    pt_str = segment.pt1.ToVericutString(fc_tool.is_metric, is_doc_metric, 1);
                    if (pt_str != last_pt_str)
                    {
                        tool_geom.Append(pt_str);
                        last_pt_str = pt_str;
                    }
                }
                if (!segment.is_arc)
                {
                    pt_str = segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1);
                    if (pt_str != last_pt_str)
                    {
                        tool_geom.Append(pt_str);
                        last_pt_str = pt_str;
                    }
                }
                else
                {
                    tool_geom.Append(Utilities.Indent("<Arc>\n", 1));
                    x_coord = String.Format("<X>{0:0.####}</X>\n", Utilities.UnitsToUnits(segment.arc_center.x, fc_tool.is_metric, is_doc_metric));
                    y_coord = String.Format("<Z>{0:0.####}</Z>\n", Utilities.UnitsToUnits(segment.arc_center.y, fc_tool.is_metric, is_doc_metric));
                    radius = String.Format("<Radius>{0:0.####}</Radius>\n", Utilities.UnitsToUnits(segment.radius, fc_tool.is_metric, is_doc_metric));
                    tool_geom.AppendFormat(Utilities.Indent(x_coord, 2));
                    tool_geom.AppendFormat(Utilities.Indent(y_coord, 2));
                    tool_geom.AppendFormat(Utilities.Indent(radius, 2));
                    tool_geom.AppendFormat(Utilities.Indent("<Direction>Shortest</Direction>\n", 2));
                    tool_geom.Append(Utilities.Indent("</Arc>\n", 1));
                    pt_str = segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1);
                    tool_geom.Append(pt_str);
                    last_pt_str = pt_str;
                }
            }
            for (int i = 1; i < fc_tool.shank_geom.Count; i++)
            {
                GeomSegment segment = fc_tool.shank_geom[i];
                if (!segment.is_arc)
                {
                    pt_str = segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1);
                    if (pt_str != last_pt_str)
                    {
                        tool_geom.Append(pt_str);
                        last_pt_str = pt_str;
                    }
                }
                else
                {
                    tool_geom.Append(Utilities.Indent("<Arc>\n", 1));
                    x_coord = String.Format("<X>{0:0.####}</X>\n", Utilities.UnitsToUnits(segment.arc_center.x, fc_tool.is_metric, is_doc_metric));
                    y_coord = String.Format("<Z>{0:0.####}</Z>\n", Utilities.UnitsToUnits(segment.arc_center.y, fc_tool.is_metric, is_doc_metric));
                    radius = String.Format("<Radius>{0:0.####}</Radius>\n", Utilities.UnitsToUnits(segment.radius, fc_tool.is_metric, is_doc_metric));
                    tool_geom.AppendFormat(Utilities.Indent(x_coord, 2));
                    tool_geom.AppendFormat(Utilities.Indent(y_coord, 2));
                    tool_geom.AppendFormat(Utilities.Indent(radius, 2));
                    tool_geom.AppendFormat(Utilities.Indent("<Direction>Shortest</Direction>\n", 2));
                    tool_geom.Append(Utilities.Indent("</Arc>\n", 1));
                    pt_str = segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1);
                    tool_geom.Append(pt_str);
                    last_pt_str = pt_str;
                }
            }

            tool_info.Append(Utilities.Indent(tool_geom.ToString(), 2));
            tool_info.Append(Utilities.Indent("</SOR>\n", 2));
            tool_info.Append(Utilities.Indent("</Cutter>\n", 1));
            tool_info.Append(Utilities.Indent("<Holder>\n", 1));
            if (fc_tool.holder_stl_fpath != null && fc_tool.holder_stl_fpath != "") //holder was exported to an .stl file
            {
                tool_info.Append(Utilities.Indent(String.Format("<Model ID=\"holder_{0}\" Type=\"STL\">\n", fc_tool.name), 2));
                tool_info.AppendFormat(Utilities.Indent("<FileName>{0}</FileName>\n", 3), fc_tool.holder_stl_fpath);
                tool_info.Append(Utilities.Indent("<NoSpin>1</NoSpin>\n", 3));
                tool_info.Append(Utilities.Indent("<Origin>\n", 3));
                tool_info.Append(Utilities.Indent("<X>0</X>\n", 4));
                tool_info.Append(Utilities.Indent("<Y>0</Y>\n", 4));
                tool_info.Append(Utilities.Indent(String.Format("<Z>{0}</Z>\n", Utilities.UnitsToUnits(fc_tool.exposed_length, fc_tool.is_metric, is_doc_metric)), 4));
                tool_info.Append(Utilities.Indent("</Origin>\n", 3));
                tool_info.Append(Utilities.Indent("<Color>255</Color>\n", 3));
                tool_info.Append(Utilities.Indent("</Model>\n", 2));
            }
            else
                if (fc_tool.holder_geom.Count > 0)
                {
                    StringBuilder holder_geom = new StringBuilder();
                    for (int i = 0; i < fc_tool.holder_geom.Count; i++)
                    {
                        GeomSegment segment = fc_tool.holder_geom[i];
                        if (i == 0)
                        {
                            holder_geom.Append(segment.pt1.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                        }
                        if (!segment.is_arc)
                        {
                            holder_geom.Append(segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                        }
                        else
                        {
                            holder_geom.Append(Utilities.Indent("<Arc>\n", 1));
                            x_coord = String.Format("<X>{0:0.####}</X>\n", Utilities.UnitsToUnits(segment.arc_center.x, fc_tool.is_metric, is_doc_metric));
                            y_coord = String.Format("<Z>{0:0.####}</Z>\n", Utilities.UnitsToUnits(segment.arc_center.y, fc_tool.is_metric, is_doc_metric));
                            radius = String.Format("<Radius>{0:0.####}</Radius>\n", Utilities.UnitsToUnits(segment.radius, fc_tool.is_metric, is_doc_metric));
                            holder_geom.AppendFormat(Utilities.Indent(x_coord, 2));
                            holder_geom.AppendFormat(Utilities.Indent(y_coord, 2));
                            holder_geom.AppendFormat(Utilities.Indent(radius, 2));
                            holder_geom.AppendFormat(Utilities.Indent("<Direction>Shortest</Direction>\n", 2));
                            holder_geom.Append(Utilities.Indent("</Arc>\n", 1));
                            holder_geom.Append(segment.pt2.ToVericutString(fc_tool.is_metric, is_doc_metric, 1));
                        }
                    }
                    tool_info.AppendFormat(Utilities.Indent("<SOR ID=\"holder_{0}\">\n", 2), fc_tool.name);
                    tool_info.Append(Utilities.Indent(holder_geom.ToString(), 2));
                    tool_info.AppendLine(Utilities.Indent(
                            Utilities.String2XML("Origin",
                                Utilities.String2XML("X", 0) + "\n" +
                                Utilities.String2XML("Y", 0) + "\n" +
                                Utilities.String2XML("Z", Utilities.UnitsToUnits(fc_tool.exposed_length, fc_tool.is_metric, is_doc_metric)))
                            , 3));
                    tool_info.Append(Utilities.Indent("</SOR>\n", 2));
                }
            tool_info.Append(Utilities.Indent("</Holder>\n", 1));
            tool_info.Append(Utilities.Indent(
                                    Utilities.String2XML("GagePoint",
                                        Utilities.String2XML("X", 0) + "\n" +
                                        Utilities.String2XML("Y", 0) + "\n" +
                                        Utilities.String2XML("Z", Utilities.UnitsToUnits(fc_tool.exposed_length + fc_tool.holder_length, fc_tool.is_metric, is_doc_metric)))
                                    , 1));
            tool_info.AppendLine("");
            tool_info.Append("</Tool>");

            return tool_info;
        }




    }
}
