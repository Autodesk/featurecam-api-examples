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
using System.Xml.Serialization;

namespace FeatureCAMExporter
{
    [Serializable]
    public class WorkOffset
    {
        [XmlElement("Name")]
        public string name;
        [XmlElement("Type")]
        public int type;
        [XmlElement("Register")]
        public string register;
        [XmlElement("Subsystem")]
        public string subsystem;
        [XmlElement("FromComponent")]
        public string from_component;
        [XmlElement("ToUCS")]
        public string to_csys_ucs_name;

        public WorkOffset()
        {
            this.type = 0;
            this.register = "";
            this.subsystem = "";
            this.from_component = "";
            this.to_csys_ucs_name = "";
        }

        public WorkOffset(int offset_type, string offset_register, string offset_subsystem,
                          string offset_from_component, string offset_to_csys)
        {
            this.type = offset_type;
            this.register = offset_register;
            this.subsystem = offset_subsystem;
            this.from_component = offset_from_component;
            this.to_csys_ucs_name = offset_to_csys;
        }
    }
}
