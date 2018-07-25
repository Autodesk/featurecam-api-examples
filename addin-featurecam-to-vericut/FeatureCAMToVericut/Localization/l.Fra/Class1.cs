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
                "Soit vous n'avez pas de licence pour le module FeatureCAM to VERICUT, soit elle n'est pas activée dans la boite de dialogue Options d'évaluation. Veuillez vérifier vos options d'évaluation et contactez votre revendeur pour de plus amples informations. "
                },
                {
                "Abort export: Failed to save NC code. Check for errors in the Op List.",
                "Annuler d'exportation : Echec de sauvegarde du code CN. Vérifiez les erreurs dans la Liste Opé. "
                },
                {
                "Exported completed.",
                "Exportation terminée. "
                },
                {
                "Failed to export tools",
                "Echec d'exportation des outils. "
                },
                {
                "Cannot save selected options to the file. File needs to be saved first.",
                "Impossible d'enregistrer les options sélectionnées dans le fichier. Le fichier doit d'abord être enregistré. "
                },
                {
                "No files are open",
                "Aucun fichier n'est ouvert. "
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb.",
                "Attention : Cet add-in dll a été compilé avec {0} de FeatureCAM tlb. "
                },
                {
                "Warning: This dll add-in was compiled against v{0} of FeatureCAM tlb, and should not be run with older versions of FeatureCAM.",
                "Attention : Cet add-in dll a été compilé avec v{0} de FeatureCAM tlb et ne devrait pas être lancé avec des version plus anciennes de FeatureCAM. "
                },
                {
                "FeatureCAM to VERICUT",
                "FeatureCAM to VERICUT"
                },
                {
                "Failed to read the Machine File from the Project file.",
                "Echec de lecture du fichier machine depuis le fichier projet. "
                },
                {
                "Yes",
                "Oui"
                },
                {
                "No",
                "Non"
                },
                {
                "Check solids to export as Fixtures and select Attach components:",
                "Vérifier les solides à exporter comme brides et sélectionner Composants d'attache : "
                },
                {
                "Select solids selected in the project",
                "Sélectionner les solides sélectionnés dans le projet"
                },
                {
                "Attach solids selected in the list to:",
                "Attacher les solides sélectionnés dans la liste à : "
                },
                {
                "OK",
                "OK"
                },
                {
                "Cancel",
                "Annuler"
                },
                {
                "Apply",
                "Appliquer"
                },
                {
                "Select export options for the selected solids",
                "Sélectionner les options d'exportation pour les solides sélectionnés"
                },
                {
                "Fixture Export Options",
                "Options d'exportation de posage"
                },
                {
                "Export",
                "Exporter"
                },
                {
                "Solid",
                "Solide"
                },
                {
                "Attach to",
                "Attacher à "
                },
                {
                "Main",
                "Principal"
                },
                {
                "Sub",
                "Contre"
                },
                {
                "Solids selected in the lists are entities of:",
                "Solides sélectionnés dans la liste sont entités de : "
                },
                {
                "Spindle",
                "Broche"
                },
                {
                "Milling head",
                "Tête de fraisage"
                },
                {
                "Turret",
                "Tourelle"
                },
                {
                "Lower turret, Sub Spindle side",
                "Tourelle inférieure, Contre-broche"
                },
                {
                "Upper turret, Sub Spindle side",
                "Tourelle supérieure, Contre-broche"
                },
                {
                "Lower turret, Main Spindle side",
                "Tourelle inférieure, Broche principale"
                },
                {
                "Upper turret, Main Spindle side",
                "Tourelle supérieure, Broche principale"
                },
                {
                "Machine Turret Info",
                "Informations de tourelle machine"
                },
                {
                "Turret/Spindle",
                "Tourelle/Broche"
                },
                {
                "Type",
                "Type"
                },
                {
                "Subsystem",
                "Subsystem"
                },
                {
                "Export Part solid (.stl file)",
                "Exporter solide de pièce (fichier .stl) "
                },
                {
                "Export Part solid (.stl file). There are no solids in the project, so you can only select file.",
                "Exporter solide de pièce (fichier .stl). Il n'y a aucun solide dans le projet, vous ne pouvez donc que sélectionner des fichiers. "
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "Le chemin d'accès du répertoire de sortie doit être défini. Veuillez régler ce problème et réessayez. "
                },
                {
                "Output file name must be set. Please fix it and try again.",
                "Le nom du fichier de sortie doit être défini. Veuillez régler ce problème et réessayez. "
                },
                {
                "If exporting VERICUT project file, template file path must be set. Please fix it and try again.",
                "En exportation de fichier projet VERICUT, le chemin du fichier template doit être défini. Veuillez régler ce problème et réessayez. "
                },
                {
                "If exporting VERICUT project file, template file path must exist. File {0} doesn't exist. Please select existing file and try again.",
                "En exportation de fichier projet VERICUT, le chemin du fichier template doit exister. Le fichier {0} n'existe pas. Veuillez sélectionner un fichier existant et réessayez. "
                },
                {
                "If VERICUT control file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Si le chemin du fichier de contrôle VERICUT est défini, le fichier doit existé. Le fichier {0} n'existe pas. Veuillez sélectionner un fichier existant et réessayez. "
                },
                {
                "If VERICUT machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Si le chemin du fichier machine VERICUT est défini, le fichier doit existé. Le fichier {0} n'existe pas. Veuillez sélectionner un fichier existant et réessayez. "
                },
                {
                "If using UCS for specifying the location of the bottom most clamp, UCS has to be selected. Please select existing UCS and try again.",
                "Si vous utilisez un SCU pour spécifier l'emplacement de la bride inférieure, le SCU doit être sélectionné. Veuillez sélectionner un SCU existant et réessayez. "
                },
                {
                "Select output directory",
                "Sélectionner répertoire de sortie"
                },
                {
                "VCPROJECT file (*.vcproject)|*.vcproject",
                "Fichier VCPROJECT (*.vcproject)|*.vcproject"
                },
                {
                "Select template VERICUT project",
                "Sélectionner projet VERICUT template"
                },
                {
                "More than one solid is selected in the part. Please select only one solid and try again.",
                "Plus d'un solide est sélectionné dans la pièce. Veuillez ne sélectionner qu'un seul solide et réessayez. "
                },
                {
                "Select template VERICUT project for the setup",
                "Sélectionner projet VERICUT template pour le repère "
                },
                {
                "Vericut path {0} is invalid. The file doesn't exist.",
                "Le chemin Vericut {0} est invalide. Le fichier n'existe pas. "
                },
                {
                "Vericut project {0} doesn't exist. Can't open it in VERICUT.",
                "Le projet Vericut {0} n'existe pas. Impossible de l'ouvrir dans VERICUT. "
                },
                {
                "Export Tools",
                "Exporter des outils"
                },
                {
                "Export NC Program",
                "Exporter un programme CN"
                },
                {
                "Browse...",
                "Parcourir..."
                },
                {
                "Exported project will be based upon this VERICUT template (leave the field blank, if you don't want to export the project):",
                "Le projet exporté sera basé sur le template VERICUT (laisser le champ vide si vous ne voulez pas exporter le projet) : "
                },
                {
                "Help",
                "Aide"
                },
                {
                "Select output directory:",
                "Sélectionner répertoire de sortie:"
                },
                {
                "Establish UCSs:",
                "Etablir des SCUs : "
                },
                {
                "Settings for setup:",
                "Réglages pour repère : "
                },
                {
                "UCSs...",
                "SCUs..."
                },
                {
                "Tool Options...",
                "Options d'outil..."
                },
                {
                "Fixtures...",
                "Posages..."
                },
                {
                "Export solids as clamps (fixtures):",
                "Exporter solides en tant que brides (posages) : "
                },
                {
                "Stock and Design...",
                "Brut et conception..."
                },
                {
                "Export initial stock and target part (design) solids:",
                "Exporter les solides du brut initial et de pièce cible (conception) : "
                },
                {
                "Establish work offsets:",
                "Etablir les décalages de travail : "
                },
                {
                "Exported setup properties will be based upon this VERICUT template (leave the field blank to use project template selected above):",
                "Les propriétés de repère exportées seront basées sur le template VERICUT (laisser le champ vide pour utiliser le template de projet sélectionné ci-dessus) : "
                },
                {
                "Work Offsets...",
                "Décalages de travail..."
                },
                {
                "Machine turret information...",
                "Informations de tourelle machine..."
                },
                {
                "Export and Open in Vericut",
                "Exporter et ouvrir dans Vericut"
                },
                {
                "Combine setups",
                "Combiner repères"
                },
                {
                "Select UCS to use for Cut Stock Transition:",
                "Sélectionner SCU à utiliser pour la transition de coupe de brut : "
                },
                {
                "File",
                "Fichier"
                },
                {
                "Export and open in VERICUT",
                "Exporter et ouvrir dans VERICUT"
                },
                {
                "Exit",
                "Quitter"
                },
                {
                "Options",
                "Options"
                },
                {
                "VERICUT...",
                "VERICUT..."
                },
                {
                "Tool...",
                "Outil..."
                },
                {
                "Save settings",
                "Enregistrer réglages"
                },
                {
                "You have to select whether to export selected solids as clamps or design.",
                "Vous devez choisir d'exporter les solides sélectionnés en tant que brides ou conception. "
                },
                {
                "You have to select attach component.",
                "Vous devez sélectionne le composant d'attache. "
                },
                {
                "Select solids:",
                "Sélectionner solides : "
                },
                {
                "Export as:",
                "Exporter en tant que : "
                },
                {
                "Attach to:",
                "Attacher à :"
                },
                {
                "Check solids selected in the project",
                "Vérifier les solides sélectionnés dans le projet"
                },
                {
                "Solid Selection",
                "Sélection de solide"
                },
                {
                "Attach component:",
                "Composant d'attache : "
                },
                {
                "Export Stock solid (.stl file)",
                "Exporter solide de brut (fichier .stl) "
                },
                {
                "Export Design/Part solid (.stl file)",
                "Exporter solide conception/pièce (fichier .stl) "
                },
                {
                "Solid name:",
                "Nom de solide : "
                },
                {
                "Stock and Design Export Settings",
                "Réglages d'exportation de brut et conception"
                },
                {
                "Attach components:",
                "Composants d'attache : "
                },
                {
                "Main spindle:",
                "Broche principale : "
                },
                {
                "Sub spindle:",
                "Contre-broche : "
                },
                {
                "Select one of the following options to use for identifying tools in VERICUT:",
                "Sélectionner une des options suivantes à utiliser pour identifier les outils dans VERICUT : "
                },
                {
                "Tool numbers (positions in the crib)",
                "Numéros d'outil (positions dans l'armoire.) "
                },
                {
                "Tool numbers and names",
                "Numéros et noms d'outil"
                },
                {
                "Tool IDs",
                "ID d'outil"
                },
                {
                "Prefix tool ids with turret identifier (for multi-turret parts)",
                "IDs d'outils en préfixe avec identifiant (pour pièces multi-tourelles) "
                },
                {
                "Tool Export Options",
                "Options d'exportation d'outil"
                },
                {
                "Select component to attach UCSs to:",
                "Sélectionner le composant à attacher aux SCUs : "
                },
                {
                "Select UCS to use as an attach point:",
                "Sélectionner SCU à utiliser comme point d'attache : "
                },
                {
                "UCSs",
                "SCUs"
                },
                {
                "Select component to attach UCS to:",
                "Sélectionner le composant à attacher au SCU : "
                },
                {
                "VERICUT batch file (*.bat)|*.bat",
                "Fichier batch VERICUT (*.bat)|*.bat"
                },
                {
                "Select VERICUT batch file",
                "Sélectionner le fichier batch VERICUT"
                },
                {
                "Select location of VERICUT batch file:",
                "Sélectionner l'emplacement du fichier batch VERICUT : "
                },
                {
                "VERICUT Options",
                "Options de VERICUT"
                },
                {
                "Work Offsets",
                "Décalages de travail"
                },
                {
                "Program Zero",
                "Origine programme"
                },
                {
                "'To' CSYS Origin:",
                "'Vers' origine YSCS"
                },
                {
                "Register:",
                "Registre : "
                },
                {
                "Offset Name:",
                "Nom de décalage : "
                },
                {
                "Subsystem:",
                "Subsystem :"
                },
                {
                "'From' Component:",
                "'Depuis' composant : "
                },
                {
                "Add new offset",
                "Ajouter nouveau décalage"
                },
                {
                "Work offsets:",
                "Décalages de travail : "
                },
                {
                "Add/delete work offset:",
                "Ajouter/effacer décalage de travail : "
                },
                {
                "Modify selected offset",
                "Modifier décalage sélectionné"
                },
                {
                "Delete selected offset",
                "Effacer décalage sélectionné"
                },
                {
                "Add Work Offset",
                "Ajouter décalage de travail"
                },
                {
                "Table name",
                "Nom de la table"
                },
                {
                "Register",
                "Registre"
                },
                {
                "'From' component",
                "'Depuis' composant "
                },
                {
                "'To' UCS",
                "'Vers' SCU"
                }
            };
        }
    }
}
