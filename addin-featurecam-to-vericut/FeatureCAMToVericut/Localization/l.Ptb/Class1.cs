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
                "Você não possui uma licença para o módulo FeatureCAM to VERICUT ou a licença não foi ativada no diálogo Opções de Avaliação. Por favor verifique o diálogo Opções de Avaliação e entre em contato com o seu fornecedor para maiores informações."
                },
                {
                "Abort export: Failed to save NC code. Check for errors in the Op List.",
                "Exportação abortada: Falha ao salvar código NC. Verifique quanto a erros na Lista de Op."
                },
                {
                "Exported completed.",
                "Exportação concluída."
                },
                {
                "Failed to export tools",
                "Falha ao exportar ferramentas"
                },
                {
                "Cannot save selected options to the file. File needs to be saved first.",
                "Não é possível salvar opções selecionadas para o arquivo. Arquivo precisa ser salvo primeiro."
                },
                {
                "No files are open",
                "Nenhum arquivo está aberto"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.",
                "Aviso: Este dll add-in foi compilado com v{0} do FeatureCAM tlb."
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.",
                "Aviso: Este dll add-in foi compilado com v{0} do FeatureCAM tlb, e não deve ser executado com versões mais antigas do FeatureCAM."
                },
                {
                "FeatureCAM to VERICUT",
                "FeatureCAM to VERICUT"
                },
                {
                "Failed to read the Machine File from the Project file.",
                "Falha ao ler o Arquivo de Máquina do arquivo de Projeto."
                },
                {
                "Yes",
                "Sim"
                },
                {
                "No",
                "Não"
                },
                {
                "Check solids to export as Fixtures and select Attach components:",
                "Marque sólidos para exportar como Fixações e selecione Componentes de conexão:"
                },
                {
                "Select solids selected in the project",
                "Selecionar sólidos selecionados no projeto"
                },
                {
                "Attach solids selected in the list to:",
                "Conectar sólidos selecionados na lista a:"
                },
                {
                "OK",
                "OK"
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
                "Selecionar opções de exportação para sólidos selecionados"
                },
                {
                "Fixture Export Options",
                "Opções de Exportação de Fixação"
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
                "Conectar a"
                },
                {
                "Main",
                "Princ."
                },
                {
                "Sub",
                "Sec."
                },
                {
                "Solids selected in the lists are entities of:",
                "Sólidos selecionados nas listas são entidades de:"
                },
                {
                "Spindle",
                "Fuso"
                },
                {
                "Milling head",
                "Cabeçote de fresam."
                },
                {
                "Turret",
                "Torre"
                },
                {
                "Lower turret, Sub Spindle side",
                "Torre inf, Lado contrafuso"
                },
                {
                "Upper turret, Sub Spindle side",
                "Torre sup, Lado contrafuso"
                },
                {
                "Lower turret, Main Spindle side",
                "Torre inf, Lado fuso princ."
                },
                {
                "Upper turret, Main Spindle side",
                "Torre sup, Lado fuso princ."
                },
                {
                "Machine Turret Info",
                "Info de Torre de Máquina"
                },
                {
                "Turret/Spindle",
                "Torre/Fuso"
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
                "Exportar sólido de Peça (arquivo .stl)"
                },
                {
                "Export Part solid (.stl file). There are no solids in the project, so you can only select file.",
                "Exportar sólido de Peça (arquivo .stl). Não existem sólidos no projeto, você somente pode selecionar arquivo."
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "Caminho de diretório de saída deve ser definido. Corrija e tente novamente."
                },
                {
                "Output file name must be set. Please fix it and try again.",
                "Nome de arquivo de saída deve ser definido. Corrija e tente novamente."
                },
                {
                "If exporting VERICUT project file, template file path must be set. Please fix it and try again.",
                "Ao exportar arquivo de projeto VERICUT, caminho de arquivo template deve ser definido. Corrija e tente novamente."
                },
                {
                "If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Ao exportar arquivo de projeto VERICUT, caminho de arquivo template deve existir. Arquivo {0} não existe. Selecione um arquivo existente e tente novamente."
                },
                {
                "If VERICUT control file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Se o caminho de arquivo de controle VERICUT estiver definido, arquivo precisa existir. Arquivo {0} não existe. Selecione um arquivo existente e tente novamente."
                },
                {
                "If VERICUT machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Se o caminho de arquivo de máquina VERICUT estiver definido, arquivo precisa existir. Arquivo {0} não existe. Selecione um arquivo existente e tente novamente."
                },
                {
                "If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again.",
                "Ao usar SCU para especificar o local da fixação inferior, SCU precisa ser selecionado. Selecione um SCU existente e tente novamente."
                },
                {
                "Select output directory",
                "Selecionar diretório de saída"
                },
                {
                "VCPROJECT file (*.vcproject)|*.vcproject",
                "Arquivo VCPROJECT (*.vcproject)|*.vcproject"
                },
                {
                "Select template VERICUT project",
                "Selecionar projeto template VERICUT"
                },
                {
                "More than one solid is selected in the part. Please select only one solid and try again.",
                "Mais de um sólido está selecionado na peça. Selecione apenas um sólido e tente novamente."
                },
                {
                "Select template VERICUT project for the setup",
                "Selecione o projeto template VERICUT para o setup"
                },
                {
                "Vericut path {0} is invalid. The file doesn't exist.",
                "Caminho Vericut {0} é inválido. O arquivo não existe."
                },
                {
                "Vericut project {0} doesn't exist. Can't open it in VERICUT.",
                "Projeto Vericut {0} não existe. Não é possível abrir no VERICUT."
                },
                {
                "Export Tools",
                "Exportar Ferramentas"
                },
                {
                "Export NC Program",
                "Exportar Programa NC"
                },
                {
                "Browse...",
                "Procurar..."
                },
                {
                "Exported project will be based upon this VERICUT template (leave the field blank, if you don't want to export the project):",
                "Projeto exportado será baseado neste template VERICUT (deixe o campo vazio caso não deseje exportar o projeto):"
                },
                {
                "Help",
                "Ajuda"
                },
                {
                "Select output directory:",
                "Selecionar diretório de saída:"
                },
                {
                "Establish UCSs:",
                "Estabelecer SCUs:"
                },
                {
                "Settings for setup:",
                "Configurações p/ setup:"
                },
                {
                "UCSs...",
                "SCUs..."
                },
                {
                "Tool Options...",
                "Opções de Ferramenta..."
                },
                {
                "Fixtures...",
                "Fixações..."
                },
                {
                "Export solids as clamps (fixtures):",
                "Exportar sólidos como fixações:"
                },
                {
                "Stock and Design...",
                "Bloco e Design..."
                },
                {
                "Export initial stock and target part (design) solids:",
                "Exportar sólidos de bloco inicial e peça alvo (design):"
                },
                {
                "Establish work offsets:",
                "Estabelecer offsets de trabalho:"
                },
                {
                "Exported setup properties will be based upon this VERICUT template (leave the field blank to use project template selected above):",
                "Propriedades de setup exportadas serão baseadas neste template VERICUT (deixe o campo vazio para utilizar o template de projeto selecionado acima):"
                },
                {
                "Work Offsets...",
                "Offsets de Trabalho..."
                },
                {
                "Machine turret information...",
                "Informação de torre de máquina..."
                },
                {
                "Export and Open in Vericut",
                "Exportar e Abrir no Vericut"
                },
                {
                "Combine setups",
                "Combinar setups"
                },
                {
                "Select UCS to use for Cut Stock Transition:",
                "Selecionar SCU para uso para Transição Bloco Cortado:"
                },
                {
                "File",
                "Arquivo"
                },
                {
                "Export and open in VERICUT",
                "Exportar e abrir no VERICUT"
                },
                {
                "Exit",
                "Sair"
                },
                {
                "Options",
                "Opções"
                },
                {
                "VERICUT...",
                "VERICUT..."
                },
                {
                "Tool...",
                "Ferramenta..."
                },
                {
                "Save settings",
                "Salvar configurações"
                },
                {
                "You have to select whether to export selected solids as clamps or design.",
                "Você precisa escolher entre exportar sólidos selecionados como fixações ou design."
                },
                {
                "You have to select attach component.",
                "Você precisa selecionar componente de conexão."
                },
                {
                "Select solids:",
                "Selecionar sólidos:"
                },
                {
                "Export as:",
                "Exportar como:"
                },
                {
                "Attach to:",
                "Conectar a:"
                },
                {
                "Check solids selected in the project",
                "Marcar sólidos selecionados no projeto"
                },
                {
                "Solid Selection",
                "Seleção de Sólido"
                },
                {
                "Attach component:",
                "Componente de conexão:"
                },
                {
                "Export Stock solid (.stl file)",
                "Exportar sólido Bloco (arquivo .stl)"
                },
                {
                "Export Design/Part solid (.stl file)",
                "Exportar sólido de Design/Peça (arquivo .stl)"
                },
                {
                "Solid name:",
                "Nome sólido:"
                },
                {
                "Stock and Design Export Settings",
                "Configurações de Exportação de Bloco e Design"
                },
                {
                "Attach components:",
                "Componentes de conexão:"
                },
                {
                "Main spindle:",
                "Fuso principal:"
                },
                {
                "Sub spindle:",
                "Contrafuso:"
                },
                {
                "Select one of the following options to use for identifying tools in VERICUT:",
                "Selecione uma das seguintes opções para identificar ferramentas no VERICUT:"
                },
                {
                "Tool numbers (positions in the crib)",
                "Números de ferramenta (posições na base)"
                },
                {
                "Tool numbers and names",
                "Números e nomes de ferramenta"
                },
                {
                "Tool IDs",
                "IDs de Ferramenta"
                },
                {
                "Prefix tool ids with turret identifier (for multi-turret parts)",
                "Prefixar ids de ferr. c/ identificador de torre (p/ peças multi-torre)"
                },
                {
                "Tool Export Options",
                "Opções de Exportação de Ferramenta"
                },
                {
                "Select component to attach UCSs to:",
                "Selecionar componente ao qual conectar SCUs:"
                },
                {
                "Select UCS to use as an attach point:",
                "Selecionar SCU para usar como ponto de conexão:"
                },
                {
                "UCSs",
                "SCUs"
                },
                {
                "Select component to attach UCS to:",
                "Selecionar componente ao qual conectar SCU:"
                },
                {
                "VERICUT batch file (*.bat)|*.bat",
                "Arquivo de lote VERICUT (*.bat)|*.bat"
                },
                {
                "Select VERICUT batch file",
                "Selecionar arquivo de lote VERICUT"
                },
                {
                "Select location of VERICUT batch file:",
                "Selecione o local do arquivo de lote VERICUT:"
                },
                {
                "VERICUT Options",
                "Opções VERICUT"
                },
                {
                "Work Offsets",
                "Offsets de Trabalho"
                },
                {
                "Program Zero",
                "Zero do Programa"
                },
                {
                "'To' CSYS Origin:",
                "'P/' Origem SistCoord:"
                },
                {
                "Register:",
                "Registro:"
                },
                {
                "Offset Name:",
                "Nome do Offset:"
                },
                {
                "Subsystem:",
                "Subsistema:"
                },
                {
                "'From' Component:",
                "'Do' Componente:"
                },
                {
                "Add new offset",
                "Adicionar novo offset"
                },
                {
                "Work offsets:",
                "Offsets de trabalho:"
                },
                {
                "Add/delete work offset:",
                "Adic./apagar offset de trab.:"
                },
                {
                "Modify selected offset",
                "Modificar offset selecionado"
                },
                {
                "Delete selected offset",
                "Apagar offset selecionado"
                },
                {
                "Add Work Offset",
                "Adic. Offset de Trabalho"
                },
                {
                "Table name",
                "Nome da mesa"
                },
                {
                "Register",
                "Registro"
                },
                {
                "'From' component",
                "'Do' componente"
                },
                {
                "'To' UCS",
                "'Para' SCU"
                }
            };
        }
    }
}
