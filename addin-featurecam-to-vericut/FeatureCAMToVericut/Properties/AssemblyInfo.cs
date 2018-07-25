// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FeatureCAMToVericut;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("FeatureCAM to VERICUT addin")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Autodesk")]
[assembly: AssemblyProduct("FeatureCAM to VERICUT addin")]
[assembly: AssemblyCopyright("Copyright (c) 2017 Autodesk, Inc. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("7c3ef7d3-b36b-4926-a78e-c84feae731a3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version - major & minor are arbitrary? increment the minor if you make a fix
//      Build Number  - using the last two as which version of FC was it compiled against.
//      Revision      - i.e. currently 7.5.  build num must match major tlb # see tlb_header.h or interop.featurecam.dll
//                        (featurecam version 24.0)  - 23.5/6 are on 7.3'ish?
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.3.7.6")]
[assembly: AssemblyFileVersion("3.3.7.6")]

