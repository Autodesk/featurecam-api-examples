// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace FeatureCAMExporter
{
    public class UCS
    {
        public FeatureCAM.FMUcs ucs;
        public string name;
        public double x, y, z,
                      i, j, k;
        private double
                x1, y1, z1,
                x2, y2, z2,
                x3, y3, z3;

        public UCS() { }

        public UCS(FeatureCAM.FMUcs fc_ucs)
        {
            this.ucs = fc_ucs;
            this.name = fc_ucs.Name;
            this.ucs.GetLocation(out x, out y, out z);
            this.ucs.GetVectors(out x1, out x2, out x3, out y1, out y2, out y3, out z1, out z2, out z3);

            ComputeEulerAngles(this.x1, this.y1, this.z1, this.x2, this.y2, this.z2, this.x3, this.y3, this.z3,
                               out this.i, out this.j, out this.k);
        }

        public void ComputeCoordinatesInRelationToSetup(FeatureCAM.FMSetup base_fm_setup,
                out double x_rel, out double y_rel, out double z_rel,
                out double i_rel, out double j_rel, out double k_rel)
        {
            double x1, y1, z1,
                   x2, y2, z2,
                   x3, y3, z3;

            x_rel = this.x; y_rel = this.y; z_rel = this.z;
            x1 = this.x1; y1 = this.y1; z1 = this.z1;
            x2 = this.x2; y2 = this.y2; z2 = this.z2;
            x3 = this.x3; y3 = this.y3; z3 = this.z3;

            base_fm_setup.MapWorldToSetup(ref x_rel, ref y_rel, ref z_rel);
            base_fm_setup.MapVectorWorldToSetup(ref x1, ref x2, ref x3);
            base_fm_setup.MapVectorWorldToSetup(ref y1, ref y2, ref y3);
            base_fm_setup.MapVectorWorldToSetup(ref z1, ref z2, ref z3);

            ComputeEulerAngles(x1, y1, z1, x2, y2, z2, x3, y3, z3,
                               out i_rel, out j_rel, out k_rel);
        }

        private void ComputeEulerAngles(double x1, double y1, double z1,
                                        double x2, double y2, double z2,
                                        double x3, double y3, double z3,
                                        out double i, out double j, out double k)
        {
            double j1_rad,
                   i1, j1, k1;

            if (z1 != 1 && z1 != -1)
            {
                j1_rad = -Math.Asin(x3);
                j1 = Utilities.Radians2Degrees(j1_rad);
                i1 = Utilities.Radians2Degrees(Math.Atan2(y3 / Math.Cos(j1_rad), z3 / Math.Cos(j1_rad)));
                k1 = Utilities.Radians2Degrees(Math.Atan2(x2 / Math.Cos(j1_rad), x1 / Math.Cos(j1_rad)));
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
                    i = k + Utilities.Radians2Degrees(Math.Atan2(x2, x3));
                }
                else
                {
                    j = -90;
                    i = -k + Utilities.Radians2Degrees(Math.Atan2(-x2, -x3));
                }
            }
            i = Math.Round(i, 4);
            j = Math.Round(j, 4);
            k = Math.Round(k, 4);
        }

    }
}
