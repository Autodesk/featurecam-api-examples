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
                "Либо у вас нет лицензии на модуль FeatureCAM to VERICUT, либо он не активирован в диалоге Параметры демо-версии. Проверьте диалог и свяжитесь с вашим поставщиком для дополнительной информации."
                },
                {
                "Abort export: Failed to save NC code. Check for errors in the Op List.",
                "Экспорт прерван: Не удалось сохранить код УП. Проверьте ошибки в списке Операции."
                },
                {
                "Exported completed.",
                "Экспорт завершен."
                },
                {
                "Failed to export tools",
                "Не удалось экспортировать инструменты"
                },
                {
                "Cannot save selected options to the file. File needs to be saved first.",
                "Не удается сохранить выбранные опции в файл. Сначала сохраните сам файл."
                },
                {
                "No files are open",
                "Нет открытых файлов"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.",
                "Внимание: DLL этого дополнения был скомпилирован для v{0} FeatureCAM tlb."
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.",
                "Внимание: DLL этого дополнения был скомпилирован для v{0} FeatureCAM tlb, и не должен запускаться с более старыми версиями FeatureCAM."
                },
                {
                "FeatureCAM to VERICUT",
                "FeatureCAM to VERICUT"
                },
                {
                "Failed to read the Machine File from the Project file.",
                "Не удалось прочесть файл станка из файла проекта."
                },
                {
                "Yes",
                "Да"
                },
                {
                "No",
                "Нет"
                },
                {
                "Check solids to export as Fixtures and select Attach components:",
                "Отметьте тела для экспорта в качестве зажимов и выберите компоненты привязки:"
                },
                {
                "Select solids selected in the project",
                "Выберите тела, выбранные в проекте"
                },
                {
                "Attach solids selected in the list to:",
                "Привязать тела, выбранные в списке, к:"
                },
                {
                "OK",
                "OK"
                },
                {
                "Cancel",
                "Отмена"
                },
                {
                "Apply",
                "Применить"
                },
                {
                "Select export options for the selected solids",
                "Выберите опции экспорта для выбранных тел"
                },
                {
                "Fixture Export Options",
                "Опции экспорта зажимов"
                },
                {
                "Export",
                "Экспорт"
                },
                {
                "Solid",
                "Тело"
                },
                {
                "Attach to",
                "Привязать к"
                },
                {
                "Main",
                "Шпиндель"
                },
                {
                "Sub",
                "Противошпиндель"
                },
                {
                "Solids selected in the lists are entities of:",
                "Тела, выбранные в списках, относятся к:"
                },
                {
                "Spindle",
                "Шпиндель"
                },
                {
                "Milling head",
                "Фрезерная головка"
                },
                {
                "Turret",
                "Револьверная головка"
                },
                {
                "Lower turret, Sub Spindle side",
                "Нижняя РГ, сторона Противошпинделя"
                },
                {
                "Upper turret, Sub Spindle side",
                "Верхняя РГ, сторона Противошпинделя"
                },
                {
                "Lower turret, Main Spindle side",
                "Нижняя РГ, сторона Шпинделя"
                },
                {
                "Upper turret, Main Spindle side",
                "Верхняя РГ, сторона Шпинделя"
                },
                {
                "Machine Turret Info",
                "Информация о револьверной головке станка"
                },
                {
                "Turret/Spindle",
                "Револьверная головка/Шпиндель"
                },
                {
                "Type",
                "Тип"
                },
                {
                "Subsystem",
                "Подсистема"
                },
                {
                "Export Part solid (.stl file)",
                "Экспорт тела детали (.stl файл)"
                },
                {
                "Export Part solid (.stl file). There are no solids in the project, so you can only select file.",
                "Экспорт тела детали (.stl файл). В проекте нет тел, поэтому можно выбрать только файл."
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "Должен быть задан путь к директории вывода. Исправьте и попробуйте снова."
                },
                {
                "Output file name must be set. Please fix it and try again.",
                "Должно быть задано имя файла вывода. Исправьте и попробуйте снова."
                },
                {
                "If exporting VERICUT project file, template file path must be set. Please fix it and try again.",
                "При экспорте файла проекта VERICUT должен быть задан путь к файлу шаблона. Исправьте и попробуйте снова."
                },
                {
                "If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again.",
                "При экспорте файла проекта VERICUT файл шаблона должен существовать. Файл {0} не существует. Выберите существующий файл и попробуйте снова."
                },
                {
                "If VERICUT control file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Если путь к управляющему файлу VERICUT задан, то файл должен существовать. Файл {0} не существует. Выберите существующий файл и попробуйте снова."
                },
                {
                "If VERICUT machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Если путь к файлу станка VERICUT задан, то файл должен существовать. Файл {0} не существует. Выберите существующий файл и попробуйте снова."
                },
                {
                "If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again.",
                "При использовании ЛСК для задания положения самого нижнего зажима ЛСК должна быть выбрана. Выберите существующую ЛСК и попробуйте снова."
                },
                {
                "Select output directory",
                "Выберите директорию вывода:"
                },
                {
                "VCPROJECT file (*.vcproject)|*.vcproject",
                "VCPROJECT файл (*.vcproject)|*.vcproject"
                },
                {
                "Select template VERICUT project",
                "Выберите проект-шаблон VERICUT"
                },
                {
                "More than one solid is selected in the part. Please select only one solid and try again.",
                "В детали выбрано больше одного тела. Выберите только одно тело и попробуйте снова."
                },
                {
                "Select template VERICUT project for the setup",
                "Выберите проект-шаблон VERICUT для установа"
                },
                {
                "Vericut path {0} is invalid. The file doesn't exist.",
                "Путь к Vericut {0} некорректен. Файл не существует."
                },
                {
                "Vericut project {0} doesn't exist. Can't open it in VERICUT.",
                "Проект Vericut {0} не существует. Невозможно открыть его в VERICUT."
                },
                {
                "Export Tools",
                "Экспорт инструментов"
                },
                {
                "Export NC Program",
                "Экспорт управляющей программы"
                },
                {
                "Browse...",
                "Обзор..."
                },
                {
                "Exported project will be based upon this VERICUT template (leave the field blank, if you don't want to export the project):",
                "Экспортируемый проект будет основан на этом шаблоне VERICUT (оставьте это поле пустым, если не хотите экспортировать проект):"
                },
                {
                "Help",
                "Справка"
                },
                {
                "Select output directory:",
                "Выберите директорию вывода:"
                },
                {
                "Establish UCSs:",
                "Установить ЛСК:"
                },
                {
                "Settings for setup:",
                "Параметры для установа:"
                },
                {
                "UCSs...",
                "ЛСК..."
                },
                {
                "Tool Options...",
                "Параметры инструмента..."
                },
                {
                "Fixtures...",
                "Зажимы..."
                },
                {
                "Export solids as clamps (fixtures):",
                "Экспорт тел как зажимов (приспособлений):"
                },
                {
                "Stock and Design...",
                "Заготовка и дизайн..."
                },
                {
                "Export initial stock and target part (design) solids:",
                "Экспорт тел заготовки и конечной детали (дизайна):"
                },
                {
                "Establish work offsets:",
                "Установить смещения детали:"
                },
                {
                "Exported setup properties will be based upon this VERICUT template (leave the field blank to use project template selected above):",
                "Экспортируемые свойства установа будут основаны на этом шаблоне VERICUT (оставьте это поле пустым, если не хотите экспортировать проект):"
                },
                {
                "Work Offsets...",
                "Смещения детали..."
                },
                {
                "Machine turret information...",
                "Информация о револьверной головке станка..."
                },
                {
                "Export and Open in Vericut",
                "Экспорт и открыть в Vericut"
                },
                {
                "Combine setups",
                "Комбинировать установы"
                },
                {
                "Select UCS to use for Cut Stock Transition:",
                "Выберите ЛСК для переноса резания заготовки:"
                },
                {
                "File",
                "Файл"
                },
                {
                "Export and open in VERICUT",
                "Экспортировать и открыть в VERICUT"
                },
                {
                "Exit",
                "Выход"
                },
                {
                "Options",
                "Опции"
                },
                {
                "VERICUT...",
                "VERICUT..."
                },
                {
                "Tool...",
                "Инструмент..."
                },
                {
                "Save settings",
                "Сохранить настройки"
                },
                {
                "You have to select whether to export selected solids as clamps or design.",
                "Нужно выбрать, как экспортировать выбранные тела, как зажимы или как дизайн."
                },
                {
                "You have to select attach component.",
                "Нужно выбрать компонент привязки."
                },
                {
                "Select solids:",
                "Выберите тела:"
                },
                {
                "Export as:",
                "Экспортировать как:"
                },
                {
                "Attach to:",
                "Привязать к:"
                },
                {
                "Check solids selected in the project",
                "Отметьте тела, выбранные в проекте"
                },
                {
                "Solid Selection",
                "Выбор тел"
                },
                {
                "Attach component:",
                "Компонент привязки:"
                },
                {
                "Export Stock solid (.stl file)",
                "Экспорт тела заготовки (.stl файл)"
                },
                {
                "Export Design/Part solid (.stl file)",
                "Экспорт тела дизайна/детали (.stl file)"
                },
                {
                "Solid name:",
                "Имя тела:"
                },
                {
                "Stock and Design Export Settings",
                "Параметры экспорта заготовки и дизайна"
                },
                {
                "Attach components:",
                "Компоненты привязки:"
                },
                {
                "Main spindle:",
                "Шпиндель:"
                },
                {
                "Sub spindle:",
                "Противошпиндель:"
                },
                {
                "Select one of the following options to use for identifying tools in VERICUT:",
                "Выберите одну из следующих опций для использования при идентификации инструментов в VERICUT:"
                },
                {
                "Tool numbers (positions in the crib)",
                "Номера инструментов (положения в наборе)"
                },
                {
                "Tool numbers and names",
                "Номера и имена инструментов"
                },
                {
                "Tool IDs",
                "ID инструментов"
                },
                {
                "Prefix tool ids with turret identifier (for multi-turret parts)",
                "Добавлять к ID инструмента идентификатор револьверной головки (для деталей с несколькими РГ)"
                },
                {
                "Tool Export Options",
                "Опции экспорта инструмента"
                },
                {
                "Select component to attach UCSs to:",
                "Выберите компоненты для привязки ЛСК:"
                },
                {
                "Select UCS to use as an attach point:",
                "Выберите ЛСК - точку привязки:"
                },
                {
                "UCSs",
                "ЛСК"
                },
                {
                "Select component to attach UCS to:",
                "Выберите компонент для привязки ЛСК:"
                },
                {
                "VERICUT batch file (*.bat)|*.bat",
                "VERICUT пакетный файл (*.bat)|*.bat"
                },
                {
                "Select VERICUT batch file",
                "Выберите пакетный файл VERICUT"
                },
                {
                "Select location of VERICUT batch file:",
                "Выберите расположение командного файла VERICUT:"
                },
                {
                "VERICUT Options",
                "Опции VERICUT"
                },
                {
                "Work Offsets",
                "Смещения детали"
                },
                {
                "Program Zero",
                "Ноль программы"
                },
                {
                "'To' CSYS Origin:",
                "Центр CSYS 'К':"
                },
                {
                "Register:",
                "Регистр:"
                },
                {
                "Offset Name:",
                "Имя смещения:"
                },
                {
                "Subsystem:",
                "Подсистема:"
                },
                {
                "'From' Component:",
                "Компонент 'От':"
                },
                {
                "Add new offset",
                "Добавить новое смещение"
                },
                {
                "Work offsets:",
                "Смещения детали:"
                },
                {
                "Add/delete work offset:",
                "Добавить/удалить смещение детали:"
                },
                {
                "Modify selected offset",
                "Изменить выбранное смещение"
                },
                {
                "Delete selected offset",
                "Удалить выбранное смещение"
                },
                {
                "Add Work Offset",
                "Добавить смещение детали"
                },
                {
                "Table name",
                "Имя стола"
                },
                {
                "Register",
                "Регистр"
                },
                {
                "'From' component",
                "Компонент 'От'"
                },
                {
                "'To' UCS",
                "ЛСК 'К'"
                }
            };
        }
    }
}
