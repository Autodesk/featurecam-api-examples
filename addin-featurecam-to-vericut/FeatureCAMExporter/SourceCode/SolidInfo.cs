// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FeatureCAM;
using System.Xml.Serialization;

namespace FeatureCAMExporter
{
    [Serializable]
    public class SolidInfo
    {
        [XmlElement("Name")]
        public string name;
        [XmlElement("AttachComponent")]
        public string attach_to;
        [XmlIgnore]
        public string fpath;
        [XmlElement("Mainspindle")]
        public bool main_spindle;

        public SolidInfo() 
        {
            name = "";
            attach_to = "";
            fpath = "";
            main_spindle = true;
        }

        public SolidInfo(string solid_name)
        {
            name = solid_name;
            attach_to = "";
            fpath = "";
            main_spindle = true;
        }

        public SolidInfo(string solid_name, string attach_component)
        {
            name = solid_name;
            attach_to = attach_component;
            fpath = "";
            main_spindle = true;
        }

        public SolidInfo(string solid_name, string attach_component, bool is_main_spindle)
        {
            name = solid_name;
            attach_to = attach_component;
            fpath = "";
            main_spindle = is_main_spindle;
        }
    }

}
