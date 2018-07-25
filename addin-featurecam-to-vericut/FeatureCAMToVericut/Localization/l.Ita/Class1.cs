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
                "Non si possiede una licenza per il modulo FeatureCAM to VERICUT o non è stata attivata nella finestra di dialogo Opzioni Demo. Si prega di controllare la finestra di dialogo Opzioni Demo e di contattare il proprio rivenditore per maggiori informazioni."
                },
                {
                "Abort export: Failed to save NC code. Check for errors in the Op List.",
                "Interrompi esportazione: Salvataggio codice NC non riuscito. Verifica errori nella Lista Op."
                },
                {
                "Exported completed.",
                "Esportazione completata."
                },
                {
                "Failed to export tools",
                "Esportazione utensili non riuscita."
                },
                {
                "Cannot save selected options to the file. File needs to be saved first.",
                "Impossibile salvare le opzioni selezionate nel file. I file devono prima essere salvati."
                },
                {
                "No files are open",
                "Non c'è nessun file aperto"
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.",
                "Attenzione: Questo modulo aggiuntivo dll è stato compilato rispetto alla v{0} di FeatureCAM tlb."
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.",
                "Attenzione: Questo modulo aggiuntivo dll è stato compilato rispetto alla v{0} di FeatureCAM tlb e non deve essere eseguito con versioni più vecchie di FeatureCAM."
                },
                {
                "FeatureCAM to VERICUT",
                "FeatureCAM to VERICUT"
                },
                {
                "Failed to read the Machine File from the Project file.",
                "Lettura File Macchina dal file di Progetto non riuscita."
                },
                {
                "Yes",
                "Si"
                },
                {
                "No",
                "No"
                },
                {
                "Check solids to export as Fixtures and select Attach components:",
                "Controlla i solidi da esportare come Origini e seleziona Attacca componenti:"
                },
                {
                "Select solids selected in the project",
                "Seleziona i solidi selezionati nel progetto"
                },
                {
                "Attach solids selected in the list to:",
                "Attacca i solidi selezionati nell'elenco a:"
                },
                {
                "OK",
                "OK"
                },
                {
                "Cancel",
                "Annulla"
                },
                {
                "Apply",
                "Applica"
                },
                {
                "Select export options for the selected solids",
                "Seleziona le opzioni di esportazione per i solidi selezionati"
                },
                {
                "Fixture Export Options",
                "Opzioni Esportazione Origini"
                },
                {
                "Export",
                "Esporta"
                },
                {
                "Solid",
                "Solido"
                },
                {
                "Attach to",
                "Attacca a"
                },
                {
                "Main",
                "Principale"
                },
                {
                "Sub",
                "Secondario"
                },
                {
                "Solids selected in the lists are entities of:",
                "I solidi selezionati negli elenchi sono entità di:"
                },
                {
                "Spindle",
                "Mandrino"
                },
                {
                "Milling head",
                "Testa di fresatura"
                },
                {
                "Turret",
                "Torretta"
                },
                {
                "Lower turret, Sub Spindle side",
                "Torretta Inferiore, lato Contromandrino"
                },
                {
                "Upper turret, Sub Spindle side",
                "Torretta Superiore, lato Contromandrino"
                },
                {
                "Lower turret, Main Spindle side",
                "Torretta Inferiore, lato Mandrino Principale"
                },
                {
                "Upper turret, Main Spindle side",
                "Torretta superiore, lato Mandrino Principale"
                },
                {
                "Machine Turret Info",
                "Informazioni Torretta Macchina"
                },
                {
                "Turret/Spindle",
                "Torretta/Mandrino"
                },
                {
                "Type",
                "Tipo"
                },
                {
                "Subsystem",
                "Sottosistema"
                },
                {
                "Export Part solid (.stl file)",
                "Esporta solido Parte (file .stl)"
                },
                {
                "Export Part solid (.stl file). There are no solids in the project, so you can only select file.",
                "Esporta solido Parte (file .stl). Non esistono solidi nel progetto, perciò si possono solo selezionare i file."
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "Deve essere impostata la directory di output del percorso. Si prega di sistemare e provare di nuovo."
                },
                {
                "Output file name must be set. Please fix it and try again.",
                "Deve essere impostato il nome del file. Si prega di sistemare e provare di nuovo."
                },
                {
                "If exporting VERICUT project file, template file path must be set. Please fix it and try again.",
                "Se si sta esportando il file di progetto VERICUT, deve essere impostato il percorso del file template. Si prega di sistemare e provare di nuovo."
                },
                {
                "If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Se si sta esportando il file di progetto VERICUT, deve essere impostato il percorso del file template. Il file {0} non esiste. Si prega di selezionare il file esistente e provare di nuovo. "
                },
                {
                "If VERICUT control file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Se è impostato il percorso del file di controllo VERICUT, il file deve esistere. Il file {0} non esiste. Si prega di selezionare il file esistente e provare di nuovo."
                },
                {
                "If VERICUT machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Se è impostato il percorso del file macchina VERICUT, il file deve esistere. Il file {0} non esiste. Si prega di selezionare il file esistente e provare di nuovo. "
                },
                {
                "If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again.",
                "Se si sta usando il UCS per specificare la posizione della staffa più in basso, UCS deve essere selezionato. Si prega di selezionare il UCS esistente e provare di nuovo."
                },
                {
                "Select output directory",
                "Seleziona la directory di output"
                },
                {
                "VCPROJECT file (*.vcproject)|*.vcproject",
                "File VCPROJECT (*.vcproject)|*.vcproject"
                },
                {
                "Select template VERICUT project",
                "Seleziona il template del progetto VERICUT"
                },
                {
                "More than one solid is selected in the part. Please select only one solid and try again.",
                "È selezionato più di un solido nella parte. Si prega di selezionare solo un solido e provare di nuovo."
                },
                {
                "Select template VERICUT project for the setup",
                "Selezionare il template del progetto VERICUT per il setup"
                },
                {
                "Vericut path {0} is invalid. The file doesn't exist.",
                "Il percorso Vericut {0} non è valido. Il file non esiste."
                },
                {
                "Vericut project {0} doesn't exist. Can't open it in VERICUT.",
                "Il progetto Vericut {0} non esiste. Impossibile aprirlo in VERICUT."
                },
                {
                "Export Tools",
                "Esporta Utensili"
                },
                {
                "Export NC Program",
                "Esporta Programma NC"
                },
                {
                "Browse...",
                "Sfoglia..."
                },
                {
                "Exported project will be based upon this VERICUT template (leave the field blank, if you don't want to export the project):",
                "Il progetto esportato si baserà su questo template VERICUT (lascia il campo in bianco, se non vuoi esportare il progetto):"
                },
                {
                "Help",
                "Aiuto"
                },
                {
                "Select output directory:",
                "Seleziona la directory di output:"
                },
                {
                "Establish UCSs:",
                "Stabilisci UCS:"
                },
                {
                "Settings for setup:",
                "Impostazioni per il setup:"
                },
                {
                "UCSs...",
                "UCS..."
                },
                {
                "Tool Options...",
                "Opzioni Utensili..."
                },
                {
                "Fixtures...",
                "Origini..."
                },
                {
                "Export solids as clamps (fixtures):",
                "Esporta solidi come staffe (origini):"
                },
                {
                "Stock and Design...",
                "Grezzo e Progetto..."
                },
                {
                "Export initial stock and target part (design) solids:",
                "Esporta grezzo iniziale e solidi parte di arrivo (progetto):"
                },
                {
                "Establish work offsets:",
                "Stabilisci gli offset di lavoro:"
                },
                {
                "Exported setup properties will be based upon this VERICUT template (leave the field blank to use project template selected above):",
                "Le proprietà del setup esportato si baseranno su questo template VERICUT (lascia il campo in bianco per usare il template del progetto selezionato sopra):"
                },
                {
                "Work Offsets...",
                "Offset di lavoro..."
                },
                {
                "Machine turret information...",
                "Informazioni torretta macchina..."
                },
                {
                "Export and Open in Vericut",
                "Esporta e Apri in Vericut"
                },
                {
                "Combine setups",
                "Combina setup"
                },
                {
                "Select UCS to use for Cut Stock Transition:",
                "Seleziona UCS per Transizione Grezzo di Taglio"
                },
                {
                "File",
                "File"
                },
                {
                "Export and open in VERICUT",
                "Esporta e apri in VERICUT"
                },
                {
                "Exit",
                "Esci"
                },
                {
                "Options",
                "Opzioni"
                },
                {
                "VERICUT...",
                "VERICUT..."
                },
                {
                "Tool...",
                "Utensile..."
                },
                {
                "Save settings",
                "Salva impostazioni"
                },
                {
                "You have to select whether to export selected solids as clamps or design.",
                "Devi selezionare se esportare i solidi selezionati come staffe o progetto."
                },
                {
                "You have to select attach component.",
                "Devi selezionare attacca componente."
                },
                {
                "Select solids:",
                "Seleziona solidi:"
                },
                {
                "Export as:",
                "Esporta come:"
                },
                {
                "Attach to:",
                "Attacca a:"
                },
                {
                "Check solids selected in the project",
                "Controlla i solidi selezionati nel progetto"
                },
                {
                "Solid Selection",
                "Selezione Solidi"
                },
                {
                "Attach component:",
                "Attacca componente:"
                },
                {
                "Export Stock solid (.stl file)",
                "Esporta solido Grezzo (file .stl)"
                },
                {
                "Export Design/Part solid (.stl file)",
                "Esporta solido Progetto/Parte (file .stl)"
                },
                {
                "Solid name:",
                "Nome solido:"
                },
                {
                "Stock and Design Export Settings",
                "Impostazioni di Esportazione Grezzo e Progetto"
                },
                {
                "Attach components:",
                "Attacca componenti:"
                },
                {
                "Main spindle:",
                "Mandrino principale:"
                },
                {
                "Sub spindle:",
                "Contromandrino:"
                },
                {
                "Select one of the following options to use for identifying tools in VERICUT:",
                "Seleziona una delle opzioni seguenti da usare per identificare gli utensili in VERICUT:"
                },
                {
                "Tool numbers (positions in the crib)",
                "Numeri utensile (posizioni nel magazzino)"
                },
                {
                "Tool numbers and names",
                "Nomi e numeri utensile"
                },
                {
                "Tool IDs",
                "ID Utensile"
                },
                {
                "Prefix tool ids with turret identifier (for multi-turret parts)",
                "ID prefisso utensile con identificatore torretta (per parti con più torrette)"
                },
                {
                "Tool Export Options",
                "Opzioni di Esportazione Utensili"
                },
                {
                "Select component to attach UCSs to:",
                "Seleziona il componente a cui attaccare gli UCS:"
                },
                {
                "Select UCS to use as an attach point:",
                "Seleziona UCS da usare come punto di attacco:"
                },
                {
                "UCSs",
                "UCS"
                },
                {
                "Select component to attach UCS to:",
                "Seleziona il componente a cui attaccare il UCS:"
                },
                {
                "VERICUT batch file (*.bat)|*.bat",
                "File batch VERICUT (*.bat)|*.bat"
                },
                {
                "Select VERICUT batch file",
                "Seleziona il file batch VERICUT"
                },
                {
                "Select location of VERICUT batch file:",
                "Seleziona la posizione del file batch VERICUT:"
                },
                {
                "VERICUT Options",
                "Opzioni VERICUT"
                },
                {
                "Work Offsets",
                "Offset di lavoro"
                },
                {
                "Program Zero",
                "Zero Programma"
                },
                {
                "'To' CSYS Origin:",
                "\"A\" origine CSYS:"
                },
                {
                "Register:",
                "Registro:"
                },
                {
                "Offset Name:",
                "Nome Offset:"
                },
                {
                "Subsystem:",
                "Sottosistema:"
                },
                {
                "'From' Component:",
                "\"Da\" Componente:"
                },
                {
                "Add new offset",
                "Aggiungi nuovo offset"
                },
                {
                "Work offsets:",
                "Offset di lavoro:"
                },
                {
                "Add/delete work offset:",
                "Aggiungi/annulla offset di lavoro:"
                },
                {
                "Modify selected offset",
                "Modifica offset selezionato"
                },
                {
                "Delete selected offset",
                "Elimina offset selezionato"
                },
                {
                "Add Work Offset",
                "Aggiungi Offset di Lavoro"
                },
                {
                "Table name",
                "Nome tavola"
                },
                {
                "Register",
                "Registro"
                },
                {
                "'From' component",
                "\"Da\" componente"
                },
                {
                "'To' UCS",
                "\"A\" UCS"
                }
            };
        }
    }
}
