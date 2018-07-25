// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace FeatureCAMExporter
{
    public class Point
    {
        public double x, y;

        public Point() { }

        public Point(double X, double Y)
        {
            x = X;
            y = Y;
        }
        
        public string ToVericutString(bool metric, bool is_doc_metric, int indentation)
        {
            string x_coord, y_coord;
            StringBuilder pt;

            x_coord = String.Format("<X>{0:0.####}</X>", Utilities.UnitsToUnits(x, metric, is_doc_metric));
            y_coord = String.Format("<Z>{0:0.####}</Z>", Utilities.UnitsToUnits(y, metric, is_doc_metric));
            pt = new StringBuilder();
            pt.AppendFormat("{0}\n", Utilities.Indent("<Pt>", indentation));
            pt.AppendFormat("{0}\n", Utilities.Indent(x_coord, indentation + 1));
            pt.AppendFormat("{0}\n", Utilities.Indent(y_coord, indentation + 1));
            pt.AppendFormat("{0}\n", Utilities.Indent("</Pt>", indentation));

            return pt.ToString();
        }
    };

    public class GeomSegment
    {
        public Point pt1,
               pt2,
               arc_center;
        public double radius;
        public bool is_arc,
               is_arc_cw,
               is_cut;

        public GeomSegment(double x1, double y1, double x2, double y2)
        {
            is_arc = false;
            pt1 = new Point(x1, y1);
            pt2 = new Point(x2, y2);
        }

        public GeomSegment(Point P1, Point P2)
        {
            is_arc = false;
            pt1 = P1;
            pt2 = P2;
        }

        public GeomSegment(Point P1, Point P2, bool Is_Cutting_Segment)
        {
            is_arc = false;
            pt1 = P1;
            pt2 = P2;
            is_cut = Is_Cutting_Segment;
        }

        public GeomSegment(Point P1, Point P2, Point Center, double Radius, bool Is_CW, bool Is_Cutting_Segment)
        {
            is_arc = true;
            pt1 = P1;
            pt2 = P2;
            arc_center = Center;
            radius = Radius;
            is_arc_cw = Is_CW;
            is_cut = Is_Cutting_Segment;
        }

        public GeomSegment(Point P1, Point P2, Point Center, double Radius, bool Is_CW)
        {
            is_arc = true;
            pt1 = P1;
            pt2 = P2;
            arc_center = Center;
            radius = Radius;
            is_arc_cw = Is_CW;
        }

        public Point GetPoint1()
        {
            return pt1;
        }

        public Point GetPoint2()
        {
            return pt2;
        }
    }
}
