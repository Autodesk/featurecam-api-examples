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
    public class SetupInfo
    {
        public bool enabled;
        public bool sub_spindle;
        public string fixture_id,
                      name,
                      first_ucs_name;
        public int num_features;
        public SetupOptions options;
        public List<UCS> ucss;
        public string attach_ucs;
        public string attach_ucs_subspindle;
        public string attach_ucss_to;
        public string attach_ucss_to_subspindle;
        public string attach_stock_to;
        public string attach_stock_to_subspindle;
        public List<string> nc_fpaths;
        public string tool_list;
        public string tool_fpath;
        public List<SolidInfo> clamps;
        public List<SolidInfo> part;
        public SolidInfo stock;
        public string stock_fpath;
        public List<FeatureCAMTool> tools;
        public double setup_solid_x,
                      setup_solid_y,
                      setup_solid_z;
        public double setup_solid_i,
                      setup_solid_j,
                      setup_solid_k;
        public double sub_setup_solid_x, /* for subspindle */
                      sub_setup_solid_y,
                      sub_setup_solid_z;
        public double sub_setup_solid_i,
                      sub_setup_solid_j,
                      sub_setup_solid_k;
        public List<WorkOffset> work_offsets;
        public List<string> attach_components,
                            subsystems;

        public SetupInfo() 
        {
            fixture_id = "";
            name = "";
            first_ucs_name = "";
            num_features = 0;
            options = null;
            ucss = null;
            attach_ucs = "";
            attach_ucss_to = "";
            attach_ucs_subspindle = "";
            attach_ucss_to_subspindle = "";
            attach_stock_to = "";
            attach_stock_to_subspindle = "";
            nc_fpaths = null;
            tool_fpath = "";
            clamps = null;
            part = null;
            stock = null;
            stock_fpath = "";
            tools = null;
            setup_solid_x = setup_solid_y = setup_solid_z = 
            setup_solid_i = setup_solid_j = setup_solid_k = 0;
            sub_setup_solid_x = sub_setup_solid_y = sub_setup_solid_z =
            sub_setup_solid_i = sub_setup_solid_j = sub_setup_solid_k = 0;
            work_offsets = null;
            attach_components = null;
            subsystems = null;
        }

        public SetupInfo(FeatureCAM.FMSetup setup)
        {
            this.name = setup.Name;
            this.enabled = setup.Enabled;
            this.num_features = setup.Features.Count;
            this.fixture_id = setup.FixtureID;
            this.tool_fpath = "";
            this.nc_fpaths = new List<string>();
            if (this.ucss == null) this.ucss = new List<UCS>();
            this.ucss.Add(new UCS(setup.ucs));
            this.first_ucs_name = setup.ucs.Name;
            this.attach_components = new List<string>();
            this.attach_stock_to = "";
            this.attach_stock_to_subspindle = "";
            this.attach_ucs = "";
            this.attach_ucs_subspindle = "";
            this.attach_ucss_to = "";
            this.attach_ucss_to_subspindle = "";
        }

        public SetupInfo(SetupOptions options, FeatureCAM.FMSetup setup, FeatureCAM.FMSolids fm_solids, List<UCS> doc_ucss)
        {
            this.name = setup.Name;
            this.enabled = setup.Enabled;
            this.num_features = setup.Features.Count;
            this.fixture_id = setup.FixtureID;
            this.tool_fpath = "";
            this.nc_fpaths = new List<string>();
            if (this.ucss == null) this.ucss = new List<UCS>();
            this.ucss.Add(new UCS(setup.ucs));
            this.first_ucs_name = setup.ucs.Name;
            this.attach_ucs = "";
            this.attach_ucs_subspindle = "";
            this.sub_spindle = (setup.Spindle == FeatureCAM.tagFMSetupSpindleType.eSST_SubSpindle ? true : false);
            if (options == null)
            {
                this.options = new SetupOptions();
                this.options.is_subspindle = this.sub_spindle;
                this.options.is_export = this.enabled;
                this.options.is_export_nc = this.enabled;
                this.options.is_export_stock = this.enabled;
                this.options.is_export_tools = this.enabled;
                this.options.setup_name = this.name;
                this.options.clamps = new List<SolidInfo>();
                setup.Activate();
                this.clamps = new List<SolidInfo>();
                this.options.parts = null;
                if (fm_solids != null)
                    foreach (FeatureCAM.FMSolid solid in fm_solids)
                    {
                        if (solid.UseAsClamp)
                        {
                            this.clamps.Add(new SolidInfo(solid.Name, ""));
                        }
                        else if (solid.UseAsPartCompareTarget)
                        {
                            if (this.part == null) this.part = new List<SolidInfo>();
                            this.part.Add(new SolidInfo(solid.Name, ""));
                        }
                    }
                this.options.is_subspindle = this.sub_spindle;
                this.options.ucs_attach = "";
                this.options.attach_ucss_to = "";
                this.options.ucs_attach_subspindle = "";
                this.options.attach_ucss_to_subspindle = "";
                this.options.attach_stock_to = "";
                this.options.attach_stock_to_subspindle = "";
                this.setup_solid_x = this.setup_solid_y = this.setup_solid_z = 0;
                this.sub_setup_solid_x = this.sub_setup_solid_y = this.sub_setup_solid_z = 0;
                this.attach_stock_to = "";
                this.attach_stock_to_subspindle = "";
                this.attach_ucss_to = "";
                this.attach_ucss_to_subspindle = "";
                this.options.is_subspindle = this.sub_spindle;
                this.options.template_fpath = "";
            }
            else
            {
                this.options = options;
                if (fm_solids != null)
                {
                    InitializeClamps(options, fm_solids);
                    if (options.parts != null)
                    {
                        foreach (SolidInfo part in options.parts)
                        {
                            FeatureCAM.FMSolid doc_part = (FeatureCAM.FMSolid)fm_solids.Item(part.name);
                            if (doc_part != null)
                            {
                                if (this.part == null) this.part = new List<SolidInfo>();
                                this.part.Add(part);
                            }
                        }
                    }
                    else
                    {
                        foreach (FeatureCAM.FMSolid solid in fm_solids)
                        {
                            if (solid.UseAsPartCompareTarget)
                            {
                                if (this.part == null) this.part = new List<SolidInfo>();
                                this.part.Add(new SolidInfo(solid.Name, ""));
                            }
                        }
                    }
                }
                this.sub_spindle = options.is_subspindle;
                this.attach_ucss_to = options.attach_ucss_to;
                this.attach_ucss_to_subspindle = options.attach_ucss_to_subspindle;
                if (options.ucs_attach != "" && IsUCSInDoc(doc_ucss, options.ucs_attach))
                    this.attach_ucs = options.ucs_attach;
                if (options.ucs_attach_subspindle != "" && IsUCSInDoc(doc_ucss, options.ucs_attach_subspindle))
                    this.attach_ucs_subspindle = options.ucs_attach_subspindle;
                this.attach_stock_to = options.attach_stock_to;
                this.attach_stock_to_subspindle = options.attach_stock_to_subspindle;
                if (options.fixture != "")
                    this.fixture_id = options.fixture;
                else
                    this.fixture_id = setup.FixtureID;
                if (options.offsets != null)
                    this.work_offsets = options.offsets;
            }
        }

        private bool IsUCSInDoc(List<UCS> doc_ucss, string ucs_name)
        {
            if (doc_ucss == null) return false;

            foreach (UCS ucs in doc_ucss)
                if (ucs.name.Equals(ucs_name)) return true;

            return false;
        }

        private void InitializeClamps(SetupOptions options, FeatureCAM.FMSolids fm_solids)
        {
            if (options.clamps == null) return;
            if (options.clamps.Count == 0) return;

            FeatureCAM.FMSolid temp_clamp = null;
            foreach (SolidInfo clamp in options.clamps)
            {
                temp_clamp = (FeatureCAM.FMSolid)fm_solids.Item(clamp.name);
                if (temp_clamp != null)
                {
                    if (this.clamps == null) this.clamps = new List<SolidInfo>();
                    this.clamps.Add(clamp); //adding clamp obj as it has settings such as export clamp or not
                }
            }
        }

        public SolidInfo FindSolidInAllSolids(string solid_name, List<SolidInfo> all_solids)
        {
            if (all_solids == null) return null;

            foreach (SolidInfo solid in all_solids)
                if (solid.name == solid_name) return solid;

            return null;
        }

        public void IsSolidAPartOrClampForSetup(string name, out bool is_part, out bool is_clamp, out string attach_to)
        {
            bool main_spindle;

            IsSolidAPartOrClampForSetup(name, out is_part, out is_clamp, out attach_to, out main_spindle);
        }

        public void IsSolidAPartOrClampForSetup(string name, out bool is_part, out bool is_clamp, out string attach_to, out bool main_spindle)
        {
            is_part = false;
            is_clamp = false;
            attach_to = "";
            main_spindle = true;

            if (this.part != null)
                foreach (SolidInfo part in this.part)
                {
                    if (part.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        is_part = true;
                        attach_to = part.attach_to;
                    }
                }
            if (this.clamps != null)
                foreach (SolidInfo clamp in this.clamps)
                {
                    if (clamp.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        is_clamp = true;
                        attach_to = clamp.attach_to;
                        main_spindle = clamp.main_spindle;
                    }
                }
        }

    }
}
