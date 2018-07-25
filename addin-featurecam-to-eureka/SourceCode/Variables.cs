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

namespace FeatureCAMToEUREKA
{
    class UCS
    {
        public FeatureCAM.FMUcs ucs;
        public string name;
        public double x, y, z,
                      i, j, k;

        public UCS() { }

        public UCS(FeatureCAM.FMUcs fc_ucs)
        {
            this.ucs = fc_ucs;
            this.name = fc_ucs.Name;
            this.ucs.GetLocation(out x, out y, out z);
            this.x = Math.Round(x, 4);
            this.y = Math.Round(y, 4);
            this.z = Math.Round(z, 4);

            ComputeEulerAngles();
        }

        private void ComputeEulerAngles()
        {
            double
                x1, y1, z1,
                x2, y2, z2,
                x3, y3, z3,
                i1, j1, k1,
                j1_rad;

            this.ucs.GetVectors(out x1, out x2, out x3, out y1, out y2, out y3, out z1, out z2, out z3);

            if (z1 != 1 && z1 != -1)
            {
                j1_rad = -Math.Asin(x3);
                j1 = Lib.Radians2Degrees(j1_rad);
                i1 = Lib.Radians2Degrees(Math.Atan2(y3 / Math.Cos(j1_rad), z3 / Math.Cos(j1_rad)));
                k1 = Lib.Radians2Degrees(Math.Atan2(x2 / Math.Cos(j1_rad), x1 / Math.Cos(j1_rad)));
                this.i = i1;
                this.j = j1;
                this.k = k1;
            }
            else
            {
                this.k = 0;
                if (z1 == -1)
                {
                    this.j = 90;
                    this.i = k + Lib.Radians2Degrees(Math.Atan2(x2, x3));
                }
                else
                {
                    this.j = -90;
                    this.i = -k + Lib.Radians2Degrees(Math.Atan2(-x2, -x3));
                }
            }
            this.i = Math.Round(this.i, 4);
            this.j = Math.Round(this.j, 4);
            this.k = Math.Round(this.k, 4);
        }
    }

    class SetupInfo
    {
        public List<UCS> ucss;
        public string
            tool_fpath,
            nc_fpath,
            name,
            first_ucs_name;
        public int num_features;
        public bool enabled;

        public SetupInfo() { }

        public SetupInfo(FeatureCAM.FMSetup setup)
        {
            this.name = setup.Name;
            this.enabled = setup.Enabled;
            this.num_features = setup.Features.Count;
            this.tool_fpath = "";
            this.nc_fpath = "";
            if (this.ucss == null) this.ucss = new List<UCS>();
            this.ucss.Add(new UCS(setup.ucs));
            this.first_ucs_name = setup.ucs.Name;
        }
    }

    public class SolidInfo
    {
        public FeatureCAM.FMSolid solid;
        public bool is_export;

        public SolidInfo(FMSolid solid_obj, bool export_solid)
        {
            solid = solid_obj;
            is_export = export_solid;
        }
    }

    static class Variables
    {
        public static string prog_name = "FeatureCAM to EUREKA";
        public static FeatureCAM.FMDocument doc = null;
        public static FeatureCAM.FMStock stock = null;

        public static Eureka.Application eureka_app = null;
        public static Eureka.Project eureka_proj = null;

        public static string output_dirpath = "";
        public static string prev_doc_name = "";
        public static int tool_identification = 0;

        public static List<SolidInfo> clamps;
        public static List<string> clamp_fpaths;
        
        public static string eureka_path = "",
                             eureka_template_files_dir = "";
        public static double offset_x, offset_y, offset_z;
        public static FeatureCAM.FMPoint offset_pt = null;
        public static string
            eureka_template_fpath = "",
            stock_fpath = "";
        public static string
           output_msg = "",
           unsupported_tool_names = "";
        public static bool
            is_export_project = true;
        public static List<string> setup_names = null;
        public static bool
            is_single_program;
        public static int selected_setup_id;
        public static bool orig_single_stock = false;

        public static string warning_msg;
        public static List<SetupInfo> setups_info;
        public static int num_enabled_setups;
        public static bool use_DATUM = false;


        public static void Cleanup()
        {
            doc = null;

            warning_msg = "";
            if (setups_info != null)
            {
                for (int i = 0; i < setups_info.Count; i++)
                    setups_info[i] = null;
                setups_info.Clear();
            }
            output_msg = "";
            num_enabled_setups = 0;
        }
    }
}

