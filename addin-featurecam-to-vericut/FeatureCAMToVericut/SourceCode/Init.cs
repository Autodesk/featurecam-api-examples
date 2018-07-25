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
using FeatureCAMExporter;
using FeatureCAM;

namespace FeatureCAMToVericut
{
    class Init
    {
        public static void InitializeVariables()
        {
            FMStock stock;

            try
            {
                LogFile.Write("Initialize variables");
                if (FCToVericut.Application != null)
                    Variables.doc = (FMDocument)FCToVericut.Application.ActiveDocument;
                if (Variables.doc == null)
                {
                    Variables.fname_no_ext = "";
                    Variables.doc_options.output_dirpath = "";
                    Variables.prev_doc_name = "";

                    return;
                }
                stock = (FMStock)Variables.doc.Stock;

                Variables.is_single_program = 
                    ((stock.IndexType == tagFMIndexType.eIT_None && stock.SingleProgramWithProgramStop)
                    ||
                    (stock.IndexType != tagFMIndexType.eIT_None && stock.ToolDominant && stock.SingleProgram)
                    ||
                    (stock.IndexType != tagFMIndexType.eIT_None && stock.ToolDominant)
                    ||
                    (stock.IndexType == tagFMIndexType.eIT_4thAxisZ && stock.SingleProgram));
                Variables.doc_options.combine_setups = 
                    (Variables.is_single_program
                    ||
                    (Variables.doc_options.combine_setups != -1 ? (Variables.doc_options.combine_setups == 1) : false)
                    ? 1 : 0);
                LogFile.Write(String.Format("Single program: {0}", Variables.is_single_program));

                Variables.fname_no_ext = Variables.doc.PartName;
                //if (Variables.output_dirpath == "")
                if (!Variables.doc_options.read_from_file)
                    Variables.doc_options.output_dirpath = Variables.doc.path;

                if (Variables.doc_options.combine_setups == -1)
                    Variables.doc_options.combine_setups = (Variables.is_single_program ? 1 : 0);

                Variables.all_ucss = FCExporter.InitializeAllUCS(Variables.doc.UCSs);
                Variables.all_fixture_ids = FCExporter.InitializeAllFixtureIDs(Variables.doc.Setups);
                Variables.all_solids = FCExporter.InitializeAllSolids(Variables.doc.Solids);
                Variables.setups_info = FCExporter.InitializeAllSetups(Variables.doc.Setups, Variables.doc.Solids, Variables.doc.UCSs,
                                                                       Variables.all_ucss, Variables.doc_options.all_setup_options,
                                                                       Variables.doc_options.combine_setups,
                                                                       ref Variables.are_all_setups_milling);                
            }
            catch (Exception Ex)
            {
                LogFile.WriteException(Ex, "InitializeVariables");
            }
            finally
            {
                stock = null;
            }
        }

    }
}
