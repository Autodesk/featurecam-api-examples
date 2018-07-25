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

        public Dictionary<string, string> GetAll()
        {
            return this.strings;
        }

        public StringTable()
        {
            this.strings = new Dictionary<string, string>()
            {
                {
                "You either don't have a license for FeatureCAM to VERICUT module or it's not activated in the Evaluation Options dialog. Please check your Evaluation Options dialog and contact your dealer for more information.",
                "您要么是没有 FeatureCAM to VERICUT 模块授权，或是未在试用选项对话视窗中激活该模块。请检查试用选项对话视窗并和您的软件提供商联系，获取更多信息。"
                },
                {
                "Abort export: Failed to save NC code. Check for errors in the Op List.",
                "中断输出: 保存NC代码失败。请检查操作清单中的错误。"
                },
                {
                "Exported completed.",
                "输出完成。"
                },
                {
                "Failed to export tools",
                "输出刀具失败"
                },
                {
                "Cannot save selected options to the file. File needs to be saved first.",
                "不能保存已选选项到文件。首先需要保存文件。"
                },
                {
                "No files are open",
                "无文件打开"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.",
                "警告: 此 dll add-in 对照FeatureCAM tlb  v{0} 编译。"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.",
                "警告: 此 dll add-in 对照FeatureCAM tlb v{0} 编译，不能在旧版本 FeatureCAM 上运行。"
                },
                {
                "FeatureCAM to VERICUT",
                "FeatureCAM to VERICUT"
                },
                {
                "Failed to read the Machine File from the Project file.",
                "从项目文件读取机床文件失败。"
                },
                {
                "Yes",
                "是"
                },
                {
                "No",
                "否"
                },
                {
                "Check solids to export as Fixtures and select Attach components:",
                "检查实体输出为夹具并选取附加部件:"
                },
                {
                "Select solids selected in the project",
                "选取项目中选取的实体"
                },
                {
                "Attach solids selected in the list to:",
                "附加列表中选取的实体到:"
                },
                {
                "OK",
                "确认"
                },
                {
                "Cancel",
                "取消"
                },
                {
                "Apply",
                "应用"
                },
                {
                "Select export options for the selected solids",
                "选取已选实体的输出选项"
                },
                {
                "Fixture Export Options",
                "夹具输出选项"
                },
                {
                "Export",
                "输出"
                },
                {
                "Solid",
                "实体"
                },
                {
                "Attach to",
                "附加到"
                },
                {
                "Main",
                "主"
                },
                {
                "Sub",
                "副"
                },
                {
                "Solids selected in the lists are entities of:",
                "列表中选取的实体是...元素:"
                },
                {
                "Spindle",
                "主轴"
                },
                {
                "Milling head",
                "铣削主轴"
                },
                {
                "Turret",
                "转塔"
                },
                {
                "Lower turret, Sub Spindle side",
                "下转塔 ，副轴侧"
                },
                {
                "Upper turret, Sub Spindle side",
                "上转塔 ，副轴侧"
                },
                {
                "Lower turret, Main Spindle side",
                "下转塔，主轴侧"
                },
                {
                "Upper turret, Main Spindle side",
                "上转塔，主轴侧"
                },
                {
                "Machine Turret Info",
                "机床转塔信息"
                },
                {
                "Turret/Spindle",
                "转塔/主轴"
                },
                {
                "Type",
                "类型"
                },
                {
                "Subsystem",
                "子系统"
                },
                {
                "Export Part solid (.stl file)",
                "输出零件实体 (.stl 文件)"
                },
                {
                "Export Part solid (.stl file). There are no solids in the project, so you can only select file.",
                "输出零件实体 (.stl 文件)。项目中无实体，因此仅可选取文件。"
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "必须设置输出目录路径。请设置后再试。"
                },
                {
                "Output file name must be set. Please fix it and try again.",
                "必须设置输出文件名称。请设置后再试。"
                },
                {
                "If exporting VERICUT project file, template file path must be set. Please fix it and try again.",
                "如果输出 VERICUT 项目文件，必须设置模板文件路径。请设置后再试。"
                },
                {
                "If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again.",
                "如果输出 VERICUT 项目文件，必须存在模板文件路径。{0}文件不存在。请选取一已有文件后重试。"
                },
                {
                "If VERICUT control file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "如果设置 VERICUT 控制文件路径，必须存在该文件。{0}文件不存在。请选取一已有文件后重试。"
                },
                {
                "If VERICUT machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "如果设置 VERICUT 机床文件路径，必须存在该文件。{0}文件不存在。请选取一已有文件后重试。"
                },
                {
                "If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again.",
                "如果使用用户坐标系指定夹具最底部位置，那么必须选取用户坐标系。请选取一已有用户坐标系后重试。"
                },
                {
                "Select output directory",
                "选取输出目录"
                },
                {
                "VCPROJECT file (*.vcproject)|*.vcproject",
                "VCPROJECT 文件 (*.vcproject)|*.vcproject"
                },
                {
                "Select template VERICUT project",
                "选取模板 VERICUT 项目"
                },
                {
                "More than one solid is selected in the part. Please select only one solid and try again.",
                "在零件中选取了一个以上的实体。请仅选取一个实体，然后重试。"
                },
                {
                "Select template VERICUT project for the setup",
                "选取设置的模板 VERICUT 项目"
                },
                {
                "Vericut path {0} is invalid. The file doesn't exist.",
                "Vericut 路径 {0} 无效。文件不存在。"
                },
                {
                "Vericut project {0} doesn't exist. Can't open it in VERICUT.",
                "Vericut项目 {0} 不存在。不能在 VERICUT 中打开。"
                },
                {
                "Export Tools",
                "输出刀具"
                },
                {
                "Export NC Program",
                "输出NC程序"
                },
                {
                "Browse...",
                "浏览..."
                },
                {
                "Exported project will be based upon this VERICUT template (leave the field blank, if you don't want to export the project):",
                "输出项目将基于此 VERICUT 模板 (如果不希望输出项目，不填写此域):"
                },
                {
                "Help",
                "帮助"
                },
                {
                "Select output directory:",
                "选取输出目录:"
                },
                {
                "Establish UCSs:",
                "建立用户坐标系:"
                },
                {
                "Settings for setup:",
                "设定设置:"
                },
                {
                "UCSs...",
                "用户坐标系..."
                },
                {
                "Tool Options...",
                "刀具选项..."
                },
                {
                "Fixtures...",
                "夹具..."
                },
                {
                "Export solids as clamps (fixtures):",
                "输出实体为夹具:"
                },
                {
                "Stock and Design...",
                "毛坯和设计..."
                },
                {
                "Export initial stock and target part (design) solids:",
                "输出初始毛坯和目标零件(设计)实体:"
                },
                {
                "Establish work offsets:",
                "建立工件偏置:"
                },
                {
                "Exported setup properties will be based upon this VERICUT template (leave the field blank to use project template selected above):",
                "输出设置属性将基于此 VERICUT 模板 (不填写此域，使用上面选取的项目模板):"
                },
                {
                "Work Offsets...",
                "工件偏置..."
                },
                {
                "Machine turret information...",
                "机床转塔信息"
                },
                {
                "Export and Open in Vericut",
                "输出并在 Vericut 中打开"
                },
                {
                "Combine setups",
                "组合设置"
                },
                {
                "Select UCS to use for Cut Stock Transition:",
                "选取切削毛坯变换使用的用户坐标系:"
                },
                {
                "File",
                "文件"
                },
                {
                "Export and open in VERICUT",
                "输出并在 VERICUT 中打开"
                },
                {
                "Exit",
                "退出"
                },
                {
                "Options",
                "选项"
                },
                {
                "VERICUT...",
                "VERICUT..."
                },
                {
                "Tool...",
                "刀具..."
                },
                {
                "Save settings",
                "保存设置"
                },
                {
                "You have to select whether to export selected solids as clamps or design.",
                "需选取是输出已选实体为夹具还是为设计。"
                },
                {
                "You have to select attach component.",
                "必须选取附加部件。"
                },
                {
                "Select solids:",
                "选取实体:"
                },
                {
                "Export as:",
                "输出为:"
                },
                {
                "Attach to:",
                "附加到:"
                },
                {
                "Check solids selected in the project",
                "检查项目中选取的实体"
                },
                {
                "Solid Selection",
                "实体选取"
                },
                {
                "Attach component:",
                "附加部件:"
                },
                {
                "Export Stock solid (.stl file)",
                "输出毛坯实体 (.stl 文件)"
                },
                {
                "Export Design/Part solid (.stl file)",
                "输出设计/零件实体 (.stl 文件)"
                },
                {
                "Solid name:",
                "实体名称:"
                },
                {
                "Stock and Design Export Settings",
                "毛坯和设计输出设置"
                },
                {
                "Attach components:",
                "附加部件:"
                },
                {
                "Main spindle:",
                "主轴:"
                },
                {
                "Sub spindle:",
                "副轴:"
                },
                {
                "Select one of the following options to use for identifying tools in VERICUT:",
                "选取以下其中一个选项用来在 VERICUT 中识别刀具:"
                },
                {
                "Tool numbers (positions in the crib)",
                "刀具编号(刀库中位置)"
                },
                {
                "Tool numbers and names",
                "刀具编号和名称"
                },
                {
                "Tool IDs",
                "刀具 ID"
                },
                {
                "Prefix tool ids with turret identifier (for multi-turret parts)",
                "刀具ID前缀前包括转塔识别(多转塔零件)"
                },
                {
                "Tool Export Options",
                "刀具输出选项"
                },
                {
                "Select component to attach UCSs to:",
                "选取附加到用户坐标系的部件:"
                },
                {
                "Select UCS to use as an attach point:",
                "选取用作附加点的用户坐标系:"
                },
                {
                "UCSs",
                "用户坐标系"
                },
                {
                "Select component to attach UCS to:",
                "选取附加到用户坐标系的部件:"
                },
                {
                "VERICUT batch file (*.bat)|*.bat",
                "VERICUT 批处理文件 (*.bat)|*.bat"
                },
                {
                "Select VERICUT batch file",
                "选取 VERICUT 批处理文件"
                },
                {
                "Select location of VERICUT batch file:",
                "选取 VERICUT 批处理文件位置:"
                },
                {
                "VERICUT Options",
                "VERICUT 选项"
                },
                {
                "Work Offsets",
                "工件偏置"
                },
                {
                "Program Zero",
                "程序零点"
                },
                {
                "'To' CSYS Origin:",
                "到CSYS原点:"
                },
                {
                "Register:",
                "注册:"
                },
                {
                "Offset Name:",
                "偏置名称:"
                },
                {
                "Subsystem:",
                "子系统:"
                },
                {
                "'From' Component:",
                "自部件:"
                },
                {
                "Add new offset",
                "增加新的偏置"
                },
                {
                "Work offsets:",
                "工件偏置:"
                },
                {
                "Add/delete work offset:",
                "增加/删除工件偏置:"
                },
                {
                "Modify selected offset",
                "修改已选偏置"
                },
                {
                "Delete selected offset",
                "删除已选偏置"
                },
                {
                "Add Work Offset",
                "增加工件偏置"
                },
                {
                "Table name",
                "工作台名称"
                },
                {
                "Register",
                "注册"
                },
                {
                "'From' component",
                "自部件"
                },
                {
                "'To' UCS",
                "到用户坐标系"
                }
            };
        }
    }
}
