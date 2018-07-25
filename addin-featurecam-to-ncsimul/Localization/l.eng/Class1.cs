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

namespace StringTable_Local
{
    public class StringTable
    {
        Dictionary<string, string> strings = null;

        public StringTable()
        {
            this.strings = new Dictionary<string, string>()
            {
                {
                "FeatureCAM file directory:",
                ""
                },
                {
                "Save NCSIMUL files to FeatureCAM file directory",
                ""
                },
                {
                "Save NCSIMUL files to a different directory",
                ""
                },
                {
                "Browse...",
                ""
                },
                {
                "Create subdirectory for NCSIMUL files",
                ""
                },
                {
                "Include in the NCSIMUL project subdirectory name:",
                ""
                },
                {
                "FeatureCAM file name",
                ""
                },
                {
                "FeatureCAM project title",
                ""
                },
                {
                "FeatureCAM postprocessor name",
                ""
                },
                {
                "NCSIMUL machine name",
                ""
                },
                {
                "Preview directory path for output files",
                ""
                },
                {
                "Select NCSIMUL machine file:",
                ""
                },
                {
                "Select solids to be exported as clamps:",
                ""
                },
                {
                "Post uses to identify tool:",
                ""
                },
                {
                "Tool number",
                ""
                },
                {
                "Tool ID",
                ""
                },
                {
                "Machine Zero offset from setup UCS:",
                ""
                },
                {
                "Individual fixture offset",
                ""
                },
                {
                "DATUM shift and rotation",
                ""
                },
                {
                "The stock in the project is not indexed. NCSIMUL can only simulate one setup at a time in such cases. Select setup you want to verify in NCSIMUL:",
                ""
                },
                {
                "Machine turret information:",
                ""
                },
                {
                "Upper turret, Main Spindle side:",
                ""
                },
                {
                "Upper turret, Sub Spindle side:",
                ""
                },
                {
                "Lower turret, Main Spindle side:",
                ""
                },
                {
                "Lower turret, Sub Spindle side:",
                ""
                },
                {
                "Milling head",
                ""
                },
                {
                "Turret",
                ""
                },
                {
                "Export",
                ""
                },
                {
                "Cancel/Exit",
                ""
                },
                {
                "Help",
                ""
                },
                {
                "No files are open",
                ""
                },
                {
                "Program failed to generate setup information.",
                ""
                },
                {
                "Failed to save nc code",
                ""
                },
                {
                "Output written to:",
                ""
                },
                {
                "Exception in ",
                ""
                },
                {
                "Error occured while exporting stock to .stl file: ",
                ""
                },
                {
                "Exception occured: ",
                ""
                },
                {
                "Error occured while exporting clamp to .stl file: ",
                ""
                },
                {
                "Cannot export data to NCSIMUL: there are errors in the document and nc code cannot be generated.",
                ""
                },
                {
                "Warning: Tools info was exported, but information for following tool(s) {0} was not exported completely, because the tool group(s) are unsupported by this addin.",
                ""
                },
                {
                "You don't have license for FeatureCAM to NCSIMUL module. Please contact your dealer for more information.",
                ""
                },
                {
                "Failed to find NCSIMUL on this computer. Cannot continue.",
                ""
                },
                {
                "Failed to add toolbar button for FeatureCAM to NCSIMUL macro",
                ""
                },
                {
                "Failed to remove toolbar button for FeatureCAM to NCSIMUL macro",
                ""
                },
                {
                "You need to select an item in the Order list",
                ""
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                ""
                },
                {
                "NCSIMUL machine file has to be set",
                ""
                },
                {
                "If NCSIMUL machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                ""
                },
                {
                "Setup name (non-indexed parts)",
                ""
                },
                {
                "Select order:",
                ""
                },
                {
                "Up",
                ""
                },
                {
                "Down",
                ""
                },
                {
                "OK",
                ""
                },
                {
                "Apply",
                ""
                },
                {
                "Cancel",
                ""
                },
                {
                "Subdirectory Options...",
                ""
                },
                {
                "Preview",
                ""
                },
                {
                "Z:",
                "Z"
                },
                {
                "Y:",
                "Y"
                },
                {
                "X:",
                "X"
                },
                {
                "Active document is 5-axis part with 'NC Code Reference Point' set to 'Each setup's own fixture'. Select whether post uses:",
                ""
                },
                {
                "Save Options",
                ""
                },
                {
                "FeatureCAM to NCSIMUL",
                ""
                },
                {
                "Export tool radius as radius compensation",
                ""
                },
                {
                "Export tool length as length compensation",
                ""
                }
            };
        }

        public Dictionary<string, string> GetAll()
        {
            return this.strings;
        }
    }

}