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

namespace FeatureCAMExporter
{
    public class TurretInfo
    {
        public FeatureCAM.tagFMTurretIDType type;
        public bool available;
        public bool b_axis;
        public string subsystem;

        public TurretInfo(FeatureCAM.tagFMTurretIDType turret_type, bool turret_available, bool turret_supports_b_axis)
        {
            this.type = turret_type;
            this.available = turret_available;
            this.b_axis = turret_supports_b_axis;
            this.subsystem = "";
        }

        public TurretInfo(FeatureCAM.tagFMTurretIDType turret_type, bool turret_available, 
                          bool turret_supports_b_axis, string vericut_subsystem)
        {
            this.type = turret_type;
            this.available = turret_available;
            this.b_axis = turret_supports_b_axis;
            this.subsystem = vericut_subsystem;
        }
    }
}
