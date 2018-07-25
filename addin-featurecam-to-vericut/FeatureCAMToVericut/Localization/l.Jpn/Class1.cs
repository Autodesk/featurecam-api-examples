// -----------------------------------------------------------------------
// Copyright 2018 Autodesk, Inc. All rights reserved.
// 
// Use of this software is subject to the terms of the Autodesk license
// agreement provided at the time of installation or download, or which
// otherwise accompanies this software in either electronic or hard copy form.
// -----------------------------------------------------------------------
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
                "FeatureCAM to VERICUT ﾓｼﾞｭｰﾙのﾗｲｾﾝｽがないか、試用設定ﾀﾞｲｱﾛｸﾞでこのﾓｼﾞｭｰﾙが有効に設定されていません。試用設定ﾀﾞｲｱﾛｸﾞを確認して、販売代理店までお問合せ下さい。"
                },
                {
                "Abort export: Failed to save NC code. Check for errors in the Op List.",
                "ｴｸｽﾎﾟｰﾄ中止: NC ﾃﾞｰﾀの保存に失敗しました。工程ﾘｽﾄでｴﾗｰを確認して下さい。"
                },
                {
                "Exported completed.",
                "ｴｸｽﾎﾟｰﾄが完了しました。"
                },
                {
                "Failed to export tools",
                "工具のｴｸｽﾎﾟｰﾄに失敗しました。"
                },
                {
                "Cannot save selected options to the file. File needs to be saved first.",
                "選択したｵﾌﾟｼｮﾝをﾌｧｲﾙに保存できません。先にﾌｧｲﾙを保存しなければなりません。"
                },
                {
                "No files are open",
                "開いているﾌｧｲﾙがありません。"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.",
                "警告: この dll ｱﾄﾞｲﾝﾏｸﾛは v{0} の FeatureCAM tlb に対してｺﾝﾊﾟｲﾙされています。"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.",
                "警告: この dll ｱﾄﾞｲﾝﾏｸﾛは v{0} の FeatureCAM tlb に対してｺﾝﾊﾟｲﾙされているため、古いﾊﾞｰｼﾞｮﾝの FeatureCAM での実行は適切ではありません。"
                },
                {
                "FeatureCAM to VERICUT",
                "FeatureCAM to VERICUT"
                },
                {
                "Failed to read the Machine File from the Project file.",
                "ﾌﾟﾛｼﾞｪｸﾄﾌｧｲﾙからﾏｼﾝﾌｧｲﾙを読込めませんでした。"
                },
                {
                "Yes",
                "はい"
                },
                {
                "No",
                "いいえ"
                },
                {
                "Check solids to export as Fixtures and select Attach components:",
                "冶具としてｴｸｽﾎﾟｰﾄするｿﾘｯﾄﾞを確認して、ｱﾀｯﾁｺﾝﾎﾟｰﾈﾝﾄを選択:"
                },
                {
                "Select solids selected in the project",
                "ﾌﾟﾛｼﾞｪｸﾄで選択したｿﾘｯﾄﾞを選択"
                },
                {
                "Attach solids selected in the list to:",
                "選択ｿﾘｯﾄﾞを次と関連付け:"
                },
                {
                "OK",
                "OK"
                },
                {
                "Cancel",
                "ｷｬﾝｾﾙ"
                },
                {
                "Apply",
                "適用"
                },
                {
                "Select export options for the selected solids",
                "選択ｿﾘｯﾄﾞのｴｸｽﾎﾟｰﾄｵﾌﾟｼｮﾝ"
                },
                {
                "Fixture Export Options",
                "冶具のｴｸｽﾎﾟｰﾄｵﾌﾟｼｮﾝ"
                },
                {
                "Export",
                "ｴｸｽﾎﾟｰﾄ"
                },
                {
                "Solid",
                "ｿﾘｯﾄﾞ"
                },
                {
                "Attach to",
                "次と関連付け"
                },
                {
                "Main",
                "ﾒｲﾝ"
                },
                {
                "Sub",
                "ｻﾌﾞ"
                },
                {
                "Solids selected in the lists are entities of:",
                "選択ｿﾘｯﾄﾞのｴﾝﾃｨﾃｨ:"
                },
                {
                "Spindle",
                "ｽﾋﾟﾝﾄﾞﾙ"
                },
                {
                "Milling head",
                "ﾌﾗｲｽ加工ﾍｯﾄﾞ"
                },
                {
                "Turret",
                "ﾀﾚｯﾄ"
                },
                {
                "Lower turret, Sub Spindle side",
                "下部ﾀﾚｯﾄ ｻﾌﾞｽﾋﾟﾝﾄﾞﾙ側"
                },
                {
                "Upper turret, Sub Spindle side",
                "上部ﾀﾚｯﾄ ｻﾌﾞｽﾋﾟﾝﾄﾞﾙ側"
                },
                {
                "Lower turret, Main Spindle side",
                "下部ﾀﾚｯﾄ ﾒｲﾝｽﾋﾟﾝﾄﾞﾙ側"
                },
                {
                "Upper turret, Main Spindle side",
                "上部ﾀﾚｯﾄ ﾒｲﾝｽﾋﾟﾝﾄﾞﾙ側"
                },
                {
                "Machine Turret Info",
                "ﾏｼﾝのﾀﾚｯﾄ情報"
                },
                {
                "Turret/Spindle",
                "ﾀﾚｯﾄ / ｽﾋﾟﾝﾄﾞﾙ"
                },
                {
                "Type",
                "ﾀｲﾌﾟ"
                },
                {
                "Subsystem",
                "ｻﾌﾞｼｽﾃﾑ"
                },
                {
                "Export Part solid (.stl file)",
                "ﾊﾟｰﾄｿﾘｯﾄﾞ (.stl ﾌｧｲﾙ) をｴｸｽﾎﾟｰﾄ"
                },
                {
                "Export Part solid (.stl file). There are no solids in the project, so you can only select file.",
                "ﾊﾟｰﾄｿﾘｯﾄﾞ (.stl ﾌｧｲﾙ) をｴｸｽﾎﾟｰﾄ - ﾌﾟﾛｼﾞｪｸﾄにｿﾘｯﾄﾞがないため、ﾌｧｲﾙのみ選択することができます。"
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "出力ﾃﾞｨﾚｸﾄﾘのﾊﾟｽを設定しなければなりません。訂正してから再試行して下さい。"
                },
                {
                "Output file name must be set. Please fix it and try again.",
                "出力ﾌｧｲﾙの名前を設定しなければなりません。訂正してから再試行して下さい。"
                },
                {
                "If exporting VERICUT project file, template file path must be set. Please fix it and try again.",
                " VERICUT ﾌﾟﾛｼﾞｪｸﾄﾌｧｲﾙをｴｸｽﾎﾟｰﾄする場合、ﾃﾝﾌﾟﾚｰﾄﾌｧｲﾙのﾊﾟｽを設定しなければなりません。訂正してから再試行して下さい。"
                },
                {
                "If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again.",
                " VERICUT ﾌﾟﾛｼﾞｪｸﾄﾌｧｲﾙをｴｸｽﾎﾟｰﾄする場合、ﾃﾝﾌﾟﾚｰﾄﾌｧｲﾙのﾊﾟｽが存在しなければなりません。ﾌｧｲﾙ {0} は存在しません。既存のﾌｧｲﾙを選択してから再試行して下さい。"
                },
                {
                "If VERICUT control file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "VERICUT ｺﾝﾄﾛｰﾙﾌｧｲﾙのﾊﾟｽを設定する場合、ﾌｧｲﾙが存在しなければなりません。ﾌｧｲﾙ {0} は存在しません。既存のﾌｧｲﾙを選択してから再試行して下さい。"
                },
                {
                "If VERICUT machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "VERICUT ﾏｼﾝﾌｧｲﾙのﾊﾟｽを設定する場合、ﾌｧｲﾙが存在しなければなりません。ﾌｧｲﾙ {0} は存在しません。既存のﾌｧｲﾙを選択してから再試行して下さい。"
                },
                {
                "If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again.",
                "UCS を使用して最も下のｸﾗﾝﾌﾟを指定する場合、UCS を選択しなければなりません。既存の UCS を選択してから再試行して下さい。"
                },
                {
                "Select output directory",
                "出力ﾃﾞｨﾚｸﾄﾘを選択"
                },
                {
                "VCPROJECT file (*.vcproject)|*.vcproject",
                "VCPROJECT ﾌｧｲﾙ (*.vcproject)|*.vcproject"
                },
                {
                "Select template VERICUT project",
                "ﾃﾝﾌﾟﾚｰﾄを選択 - VERICUT ﾌﾟﾛｼﾞｪｸﾄ"
                },
                {
                "More than one solid is selected in the part. Please select only one solid and try again.",
                "ﾊﾟｰﾄから複数のｿﾘｯﾄﾞが選択されています。1 つのみ選択してから再試行して下さい。"
                },
                {
                "Select template VERICUT project for the setup",
                "ﾃﾝﾌﾟﾚｰﾄを選択 - ｾｯﾄｱｯﾌﾟの VERICUT ﾌﾟﾛｼﾞｪｸﾄ"
                },
                {
                "Vericut path {0} is invalid. The file doesn't exist.",
                "VERICUT ﾊﾟｽ {0} は無効です。ﾌｧｲﾙは存在しません。"
                },
                {
                "Vericut project {0} doesn't exist. Can't open it in VERICUT.",
                "VERICUT ﾌﾟﾛｼﾞｪｸﾄ {0} は存在しません。 VERICUT で開けません。"
                },
                {
                "Export Tools",
                "工具をｴｸｽﾎﾟｰﾄ"
                },
                {
                "Export NC Program",
                "NC ﾌﾟﾛｸﾞﾗﾑをｴｸｽﾎﾟｰﾄ"
                },
                {
                "Browse...",
                "参照..."
                },
                {
                "Exported project will be based upon this VERICUT template (leave the field blank, if you don't want to export the project):",
                "ｴｸｽﾎﾟｰﾄするﾌﾟﾛｼﾞｪｸﾄの VERICUT ﾃﾝﾌﾟﾚｰﾄ (ﾌﾟﾛｼﾞｪｸﾄをｴｸｽﾎﾟｰﾄしない場合、空白のまま):"
                },
                {
                "Help",
                "ﾍﾙﾌﾟ"
                },
                {
                "Select output directory:",
                "出力ﾃﾞｨﾚｸﾄﾘを選択:"
                },
                {
                "Establish UCSs:",
                "UCS を構築:"
                },
                {
                "Settings for setup:",
                "ｾｯﾄｱｯﾌﾟの設定:"
                },
                {
                "UCSs...",
                "UCS..."
                },
                {
                "Tool Options...",
                "工具ｵﾌﾟｼｮﾝ..."
                },
                {
                "Fixtures...",
                "冶具..."
                },
                {
                "Export solids as clamps (fixtures):",
                "ｿﾘｯﾄﾞをｸﾗﾝﾌﾟ (冶具) としてｴｸｽﾎﾟｰﾄ"
                },
                {
                "Stock and Design...",
                "素材とﾃﾞｻﾞｲﾝ..."
                },
                {
                "Export initial stock and target part (design) solids:",
                "初期素材とﾀｰｹﾞｯﾄﾊﾟｰﾄ (ﾃﾞｻﾞｲﾝ) ｿﾘｯﾄﾞをｴｸｽﾎﾟｰﾄ"
                },
                {
                "Establish work offsets:",
                "ﾜｰｸｵﾌｾｯﾄを構築"
                },
                {
                "Exported setup properties will be based upon this VERICUT template (leave the field blank to use project template selected above):",
                "ｴｸｽﾎﾟｰﾄするｾｯﾄｱｯﾌﾟﾌﾟﾛﾊﾟﾃｨの VERICUT ﾃﾝﾌﾟﾚｰﾄ (前のｾｸｼｮﾝで選択したﾌﾟﾛｼﾞｪｸﾄﾃﾝﾌﾟﾚｰﾄを使用する場合、空白のまま):"
                },
                {
                "Work Offsets...",
                "ﾜｰｸｵﾌｾｯﾄ..."
                },
                {
                "Machine turret information...",
                "ﾏｼﾝのﾀﾚｯﾄ情報..."
                },
                {
                "Export and Open in Vericut",
                "ｴｸｽﾎﾟｰﾄして VERICUT で開く"
                },
                {
                "Combine setups",
                "ｾｯﾄｱｯﾌﾟを結合"
                },
                {
                "Select UCS to use for Cut Stock Transition:",
                "切削素材の移動に使用する UCS を選択:"
                },
                {
                "File",
                "ﾌｧｲﾙ"
                },
                {
                "Export and open in VERICUT",
                "ｴｸｽﾎﾟｰﾄして VERICUT で開く"
                },
                {
                "Exit",
                "終了"
                },
                {
                "Options",
                "ｵﾌﾟｼｮﾝ"
                },
                {
                "VERICUT...",
                "VERICUT..."
                },
                {
                "Tool...",
                "工具..."
                },
                {
                "Save settings",
                "設定を保存"
                },
                {
                "You have to select whether to export selected solids as clamps or design.",
                "選択ｿﾘｯﾄﾞをｸﾗﾝﾌﾟとしてｴｸｽﾎﾟｰﾄするか、ﾃﾞｻﾞｲﾝとしてｴｸｽﾎﾟｰﾄするか指定しなければなりません。"
                },
                {
                "You have to select attach component.",
                "ｱﾀｯﾁｺﾝﾎﾟｰﾈﾝﾄを選択しなければなりません。"
                },
                {
                "Select solids:",
                "ｿﾘｯﾄﾞを選択:"
                },
                {
                "Export as:",
                "次としてｴｸｽﾎﾟｰﾄ:"
                },
                {
                "Attach to:",
                "次と関連付け:"
                },
                {
                "Check solids selected in the project",
                "ﾌﾟﾛｼﾞｪｸﾄで選択したｿﾘｯﾄﾞを確認"
                },
                {
                "Solid Selection",
                "ｿﾘｯﾄﾞ選択"
                },
                {
                "Attach component:",
                "ｱﾀｯﾁｺﾝﾎﾟｰﾈﾝﾄ:"
                },
                {
                "Export Stock solid (.stl file)",
                "素材ｿﾘｯﾄﾞ (.stl ﾌｧｲﾙ) をｴｸｽﾎﾟｰﾄ"
                },
                {
                "Export Design/Part solid (.stl file)",
                "ﾃﾞｻﾞｲﾝ / ﾊﾟｰﾄｿﾘｯﾄﾞ (.stl ﾌｧｲﾙ) をｴｸｽﾎﾟｰﾄ"
                },
                {
                "Solid name:",
                "ｿﾘｯﾄﾞ名:"
                },
                {
                "Stock and Design Export Settings",
                "素材とﾃﾞｻﾞｲﾝのｴｸｽﾎﾟｰﾄ設定"
                },
                {
                "Attach components:",
                "ｱﾀｯﾁｺﾝﾎﾟｰﾈﾝﾄ:"
                },
                {
                "Main spindle:",
                "ﾒｲﾝｽﾋﾟﾝﾄﾞﾙ:"
                },
                {
                "Sub spindle:",
                "ｻﾌﾞｽﾋﾟﾝﾄﾞﾙ:"
                },
                {
                "Select one of the following options to use for identifying tools in VERICUT:",
                "VERICUT での工具識別ｵﾌﾟｼｮﾝを選択:"
                },
                {
                "Tool numbers (positions in the crib)",
                "工具番号 (工具箱内での位置 )"
                },
                {
                "Tool numbers and names",
                "工具番号と工具名"
                },
                {
                "Tool IDs",
                "工具 ID"
                },
                {
                "Prefix tool ids with turret identifier (for multi-turret parts)",
                "ﾀﾚｯﾄの識別子を、工具 ID のﾌﾟﾘﾌｨｯｸｽとして使用 (ﾏﾙﾁﾀﾚｯﾄﾊﾟｰﾄ用)"
                },
                {
                "Tool Export Options",
                "工具のｴｸｽﾎﾟｰﾄｵﾌﾟｼｮﾝ"
                },
                {
                "Select component to attach UCSs to:",
                "UCS と関連付けるｺﾝﾎﾟｰﾈﾝﾄを選択:"
                },
                {
                "Select UCS to use as an attach point:",
                "ｱﾀｯﾁﾎﾟｲﾝﾄとして使用する UCS を選択:"
                },
                {
                "UCSs",
                "UCS"
                },
                {
                "Select component to attach UCS to:",
                "UCS と関連付けるｺﾝﾎﾟｰﾈﾝﾄを選択:"
                },
                {
                "VERICUT batch file (*.bat)|*.bat",
                "VERICUT ﾊﾞｯﾁﾌｧｲﾙ (*.bat)|*.bat"
                },
                {
                "Select VERICUT batch file",
                "VERICUT ﾊﾞｯﾁﾌｧｲﾙを選択"
                },
                {
                "Select location of VERICUT batch file:",
                "VERICUT ﾊﾞｯﾁﾌｧｲﾙの保存場所を選択:"
                },
                {
                "VERICUT Options",
                "VERICUT ｵﾌﾟｼｮﾝ"
                },
                {
                "Work Offsets",
                "ﾜｰｸｵﾌｾｯﾄ"
                },
                {
                "Program Zero",
                "ﾌﾟﾛｸﾞﾗﾑ原点"
                },
                {
                "'To' CSYS Origin:",
                "「対象」座標系原点:"
                },
                {
                "Register:",
                "登録:"
                },
                {
                "Offset Name:",
                "ｵﾌｾｯﾄ名:"
                },
                {
                "Subsystem:",
                "ｻﾌﾞｼｽﾃﾑ:"
                },
                {
                "'From' Component:",
                "「基準」ｺﾝﾎﾟｰﾈﾝﾄ:"
                },
                {
                "Add new offset",
                "新規ｵﾌｾｯﾄを作成"
                },
                {
                "Work offsets:",
                "ﾜｰｸｵﾌｾｯﾄ:"
                },
                {
                "Add/delete work offset:",
                "ﾜｰｸｵﾌｾｯﾄを追加 / 削除:"
                },
                {
                "Modify selected offset",
                "選択ｵﾌｾｯﾄを編集"
                },
                {
                "Delete selected offset",
                "選択ｵﾌｾｯﾄを削除"
                },
                {
                "Add Work Offset",
                "ﾜｰｸｵﾌｾｯﾄを追加"
                },
                {
                "Table name",
                "ﾃｰﾌﾞﾙ名"
                },
                {
                "Register",
                "登録"
                },
                {
                "'From' component",
                "「基準」ｺﾝﾎﾟｰﾈﾝﾄ"
                },
                {
                "'To' UCS",
                "「対象」UCS"
                }
            };
        }
    }
}
