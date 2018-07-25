// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FeatureCAMExporter
{
    [Serializable]
    public class SetupOptions
    {
        [XmlElement("SetupName")]
        public string setup_name;
        [XmlElement("Subspindle")]
        public bool is_subspindle;
        [XmlElement("Template")]
        public string template_fpath;
        [XmlElement("Export")]
        public bool is_export;
        [XmlElement("ExportNC")]
        public bool is_export_nc;
        [XmlElement("ExportTools")]
        public bool is_export_tools;
        [XmlElement("ExportStock")]
        public bool is_export_stock;
        [XmlElement("UCSAttach")]
        public string ucs_attach;
        [XmlElement("AttachUCSTo")]
        public string attach_ucss_to;
        [XmlElement("UCSAttachSubspindle")]
        public string ucs_attach_subspindle;
        [XmlElement("AttachUCSToSubspindle")]
        public string attach_ucss_to_subspindle;
        [XmlElement("AttachStockTo")]
        public string attach_stock_to;
        [XmlElement("AttachStockToSubspindle")]
        public string attach_stock_to_subspindle;
        [XmlElement("FixtureID")]
        public string fixture;
        [XmlArray("Parts")]
        public List<SolidInfo> parts;
        [XmlArray("Clamps")]
        public List<SolidInfo> clamps;
        [XmlArray("WorkOffsets")]
        public List<WorkOffset> offsets;


        public SetupOptions() 
        {
            this.setup_name = "";
            this.template_fpath = "";
            this.is_export = true;
            this.is_export_nc = true;
            this.is_export_tools = true;
            this.is_export_stock = true;
            this.clamps = null;
            this.parts = null;
            this.ucs_attach = "";
            this.attach_ucss_to = "";
            this.ucs_attach_subspindle = "";
            this.attach_ucss_to_subspindle = "";
            this.attach_stock_to = "";
            this.attach_stock_to_subspindle = "";
            this.fixture = "";
            this.offsets = new List<WorkOffset>();
            this.is_subspindle = false;
        }
    }
}
