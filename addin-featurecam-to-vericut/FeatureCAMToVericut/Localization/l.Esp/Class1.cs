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
                "Puede que no se tenga la licencia para el módulo de FeatureCAM a VERICUT o no esté activado en el cuadro de diálogo de Opciones de Evaluación. Por favor comprobar el cuadro de diálogo Opciones de Evaluación y póngase en contacto con su distribuidor para más información."
                },
                {
                "Abort export: Failed to save NC code. Check for errors in the Op List.",
                "Cancelar exportación: Fallo al guardar el código CN. Revisar en busca de errores en la Lista Op."
                },
                {
                "Exported completed.",
                "Exportación completada."
                },
                {
                "Failed to export tools",
                "Fallo al exportar las herramientas"
                },
                {
                "Cannot save selected options to the file. File needs to be saved first.",
                "Imposible guardar las opciones seleccionadas en el fichero. Primero es necesario guardar el fichero."
                },
                {
                "No files are open",
                "Ningún fichero abierto"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.",
                "Aviso: Este complemento dll se compiló con la v{0} de FeatureCAM tlb."
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.",
                "Aviso: Este complemento dll se compiló con la v{0} de FeatureCAM tlb, y no debería ejecutarse con versiones más antiguas de FeatureCAM."
                },
                {
                "FeatureCAM to VERICUT",
                "FeatureCAM a VERICUT"
                },
                {
                "Failed to read the Machine File from the Project file.",
                "Fallo al leer el Fichero de Máquina desde el fichero de Proyecto."
                },
                {
                "Yes",
                "Sí"
                },
                {
                "No",
                "No"
                },
                {
                "Check solids to export as Fixtures and select Attach components:",
                "Comprobar los sólidos a exportar como Fijaciones y seleccionar Agregar componentes:"
                },
                {
                "Select solids selected in the project",
                "Seleccionar los sólidos seleccionados en el proyecto"
                },
                {
                "Attach solids selected in the list to:",
                "Agregar los sólidos seleccionados en la lista a:"
                },
                {
                "OK",
                "Aceptar"
                },
                {
                "Cancel",
                "Cancelar"
                },
                {
                "Apply",
                "Aplicar"
                },
                {
                "Select export options for the selected solids",
                "Seleccionar las opciones de exportación para los sólidos seleccionados"
                },
                {
                "Fixture Export Options",
                "Opciones de Exportación de Fijaciones"
                },
                {
                "Export",
                "Exportar"
                },
                {
                "Solid",
                "Sólido"
                },
                {
                "Attach to",
                "Agregar a"
                },
                {
                "Main",
                "Principal"
                },
                {
                "Sub",
                "Sub"
                },
                {
                "Solids selected in the lists are entities of:",
                "Los sólidos seleccionados en las listas son entidades de:"
                },
                {
                "Spindle",
                "Husillo"
                },
                {
                "Milling head",
                "Cabezal de fresado"
                },
                {
                "Turret",
                "Torreta"
                },
                {
                "Lower turret, Sub Spindle side",
                "Torreta inferior, lateral Subhusillo"
                },
                {
                "Upper turret, Sub Spindle side",
                "Torreta superior, lateral Subhusillo"
                },
                {
                "Lower turret, Main Spindle side",
                "Torreta inferior, lateral Husillo Principal"
                },
                {
                "Upper turret, Main Spindle side",
                "Torreta superior, lateral Husillo Principal"
                },
                {
                "Machine Turret Info",
                "Información de la Torreta de la Máquina"
                },
                {
                "Turret/Spindle",
                "Torreta/Husillo"
                },
                {
                "Type",
                "Tipo"
                },
                {
                "Subsystem",
                "Subsistema"
                },
                {
                "Export Part solid (.stl file)",
                "Exportar sólido Pieza (fichero .stl)"
                },
                {
                "Export Part solid (.stl file). There are no solids in the project, so you can only select file.",
                "Exportar sólido Pieza (fichero .stl). No hay sólidos en el proyecto, así que solo se puede seleccionar el fichero."
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "Tiene que configurarse la ruta del directorio de salida. Por favor hacerlo e intentarlo de nuevo."
                },
                {
                "Output file name must be set. Please fix it and try again.",
                "Tiene que configurarse el nombre del fichero de salida. Por favor hacerlo e intentarlo de nuevo."
                },
                {
                "If exporting VERICUT project file, template file path must be set. Please fix it and try again.",
                "Al exportar el fichero del proyecto de VERICUT, la ruta del fichero plantilla tiene que estar configurada. Por favor hacerlo e intentarlo de nuevo."
                },
                {
                "If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Al exportar el fichero del proyecto de VERICUT, la ruta del fichero plantilla tiene que existir. El fichero {0} no existe. Por favor seleccionar el fichero existente e intentarlo de nuevo."
                },
                {
                "If VERICUT control file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Si la ruta del fichero de control de VERICUT está configurada, el fichero tiene que existir. El fichero {0} no existe. Por favor seleccionar el fichero existente e intentarlo de nuevo."
                },
                {
                "If VERICUT machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Si la ruta del fichero de máquina de VERICUT está configurada, el fichero tiene que existir. El fichero {0} no existe. Por favor seleccionar el fichero existente e intentarlo de nuevo."
                },
                {
                "If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again.",
                "Si se está usando el SCU para especificar la posición de la abrazadera inferior, el SCU tiene que estar seleccionado. Por favor seleccionar el SCU existente e intentarlo de nuevo."
                },
                {
                "Select output directory",
                "Seleccionar el directorio de salida"
                },
                {
                "VCPROJECT file (*.vcproject)|*.vcproject",
                "Fichero VCPROJECT (*.vcproject)|*.vcproject"
                },
                {
                "Select template VERICUT project",
                "Seleccionar el proyecto de VERICUT de plantilla"
                },
                {
                "More than one solid is selected in the part. Please select only one solid and try again.",
                "Se ha seleccionado más de un sólido en la pieza. Por favor seleccionar solo un sólido e intentarlo de nuevo."
                },
                {
                "Select template VERICUT project for the setup",
                "Seleccionar el proyecto de VERICUT de plantilla para la configuración"
                },
                {
                "Vericut path {0} is invalid. The file doesn't exist.",
                "La ruta de Vericut {0} no es válida. El fichero no existe."
                },
                {
                "Vericut project {0} doesn't exist. Can't open it in VERICUT.",
                "El proyecto de Vericut {0} no existe. Imposible abrirlo en VERICUT."
                },
                {
                "Export Tools",
                "Exportar Herramientas"
                },
                {
                "Export NC Program",
                "Exportar Programa CN"
                },
                {
                "Browse...",
                "Buscar..."
                },
                {
                "Exported project will be based upon this VERICUT template (leave the field blank, if you don't want to export the project):",
                "El proyecto exportado se basará en esta plantilla de VERICUT (dejar el campo vacío, si no se desea exportar el proyecto):"
                },
                {
                "Help",
                "Ayuda"
                },
                {
                "Select output directory:",
                "Seleccionar el directorio de salida:"
                },
                {
                "Establish UCSs:",
                "Establecer SCUs:"
                },
                {
                "Settings for setup:",
                "Configuraciones para la configuración:"
                },
                {
                "UCSs...",
                "SCUs..."
                },
                {
                "Tool Options...",
                "Opciones de Herramienta..."
                },
                {
                "Fixtures...",
                "Fijaciones..."
                },
                {
                "Export solids as clamps (fixtures):",
                "Exportar sólidos como abrazaderas (fijaciones):"
                },
                {
                "Stock and Design...",
                "Resto y Diseño..."
                },
                {
                "Export initial stock and target part (design) solids:",
                "Exportar el resto inicial y los sólidos (diseño) de la pieza de destino:"
                },
                {
                "Establish work offsets:",
                "Establecer offsets de trabajo:"
                },
                {
                "Exported setup properties will be based upon this VERICUT template (leave the field blank to use project template selected above):",
                "Las propiedades de la configuración exportada se basarán en esta plantilla de VERICUT (dejar el campo vacío para usar la plantilla del proyecto seleccionada arriba):"
                },
                {
                "Work Offsets...",
                "Offsets de Trabajo... "
                },
                {
                "Machine turret information...",
                "Información de la torreta de la máquina..."
                },
                {
                "Export and Open in Vericut",
                "Exportar y Abrir en Vericut"
                },
                {
                "Combine setups",
                "Combinar configuraciones"
                },
                {
                "Select UCS to use for Cut Stock Transition:",
                "Seleccionar SCU para usar en Transición del Resto Cortado:"
                },
                {
                "File",
                "Fichero"
                },
                {
                "Export and open in VERICUT",
                "Exportar y abrir en VERICUT "
                },
                {
                "Exit",
                "Salir"
                },
                {
                "Options",
                "Opciones"
                },
                {
                "VERICUT...",
                "VERICUT..."
                },
                {
                "Tool...",
                "Herramienta..."
                },
                {
                "Save settings",
                "Guardar configuraciones"
                },
                {
                "You have to select whether to export selected solids as clamps or design.",
                "Se tiene que seleccionar si exportar los sólidos seleccionados como abrazaderas o diseño."
                },
                {
                "You have to select attach component.",
                "Se tiene seleccionar agregar componente."
                },
                {
                "Select solids:",
                "Seleccionar sólidos:"
                },
                {
                "Export as:",
                "Exportar como:"
                },
                {
                "Attach to:",
                "Agregar a:"
                },
                {
                "Check solids selected in the project",
                "Comprobar los sólidos seleccionados en el proyecto"
                },
                {
                "Solid Selection",
                "Selección de Sólidos"
                },
                {
                "Attach component:",
                "Agregar componente:"
                },
                {
                "Export Stock solid (.stl file)",
                "Exportar sólido Resto (fichero .stl)"
                },
                {
                "Export Design/Part solid (.stl file)",
                "Exportar sólido Diseño/Pieza (fichero .stl)"
                },
                {
                "Solid name:",
                "Nombre del sólido:"
                },
                {
                "Stock and Design Export Settings",
                "Configuraciones de Exportación de Resto y Diseño..."
                },
                {
                "Attach components:",
                "Agregar componentes:"
                },
                {
                "Main spindle:",
                "Husillo principal:"
                },
                {
                "Sub spindle:",
                "Subhusillo:"
                },
                {
                "Select one of the following options to use for identifying tools in VERICUT:",
                "Seleccionar una de las siguientes opciones a usar para identificar herramientas en VERICUT:"
                },
                {
                "Tool numbers (positions in the crib)",
                "Números de herramienta (posiciones en el almacén de herramientas)"
                },
                {
                "Tool numbers and names",
                "Números de herramienta y nombres"
                },
                {
                "Tool IDs",
                "IDs de Herramienta"
                },
                {
                "Prefix tool ids with turret identifier (for multi-turret parts)",
                "Prefijar ids de herramienta con el identificador de torreta (para piezas multitorreta)"
                },
                {
                "Tool Export Options",
                "Opciones de Exportación de Herramienta"
                },
                {
                "Select component to attach UCSs to:",
                "Seleccionar el componente al que agregar los SCUs:"
                },
                {
                "Select UCS to use as an attach point:",
                "Seleccionar SCU a usar como un punto de sujeción:"
                },
                {
                "UCSs",
                "SCUs"
                },
                {
                "Select component to attach UCS to:",
                "Seleccionar el componente al que agregar el SCU:"
                },
                {
                "VERICUT batch file (*.bat)|*.bat",
                "Fichero batch de VERICUT  (*.bat)|*.bat"
                },
                {
                "Select VERICUT batch file",
                "Seleccionar fichero batch de VERICUT"
                },
                {
                "Select location of VERICUT batch file:",
                "Seleccionar la posición del fichero batch de VERICUT:"
                },
                {
                "VERICUT Options",
                "Opciones de VERICUT"
                },
                {
                "Work Offsets",
                "Offsets de Trabajo"
                },
                {
                "Program Zero",
                "Programa Cero"
                },
                {
                "'To' CSYS Origin:",
                "'Hasta' Origen CSYS:"
                },
                {
                "Register:",
                "Registro:"
                },
                {
                "Offset Name:",
                "Nombre del Offset:"
                },
                {
                "Subsystem:",
                "Subsistema:"
                },
                {
                "'From' Component:",
                "'Desde' Componente:"
                },
                {
                "Add new offset",
                "Añadir nuevo offset"
                },
                {
                "Work offsets:",
                "Offsets de trabajo:"
                },
                {
                "Add/delete work offset:",
                "Añadir/borrar offset de trabajo:"
                },
                {
                "Modify selected offset",
                "Modificar offset seleccionado"
                },
                {
                "Delete selected offset",
                "Borrar offset seleccionado"
                },
                {
                "Add Work Offset",
                "Añadir Offset de Trabajo"
                },
                {
                "Table name",
                "Nombre de tabla"
                },
                {
                "Register",
                "Registro"
                },
                {
                "'From' component",
                "'Desde' componente"
                },
                {
                "'To' UCS",
                "'Hasta' SCU"
                }
            };
        }
    }
}
