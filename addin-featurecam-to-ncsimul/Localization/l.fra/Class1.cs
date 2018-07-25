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
                "Répertoire fichier FeatureCAM:"
                },
                {
                "Save NCSIMUL files to FeatureCAM file directory",
                "Enreg. fichiers NCSIMUL dans le répertoire FeatureCAM"
                },
                {
                "Save NCSIMUL files to a different directory",
                "Enreg. fichiers NCSIMUL dans un autre répertoire"
                },
                {
                "Browse...",
                "Parcourir ..."
                },
                {
                "Create subdirectory for NCSIMUL files",
                "Créer un sous répertoire pour les fichiers NCSIMUL"
                },
                {
                "Include in the NCSIMUL project subdirectory name:",
                "Inclure dans le nom du répertoire projet NCSIMUL :"
                },
                {
                "FeatureCAM file name",
                "Nom du fichier FeatureCAM"
                },
                {
                "FeatureCAM project title",
                "Titre du projet FeatureCAM"
                },
                {
                "FeatureCAM postprocessor name",
                "Nom du Postprocesseur FeatureCAM"
                },
                {
                "NCSIMUL machine name",
                "Nom de la machine NCSIMUL"
                },
                {
                "Preview directory path for output files",
                "Voir le répertoire de sortie pour les fichiers"
                },
                {
                "Select NCSIMUL machine file:",
                "Sélectionnez le fichier Machine NCSIMUL:"
                },
                {
                "Select solids to be exported as clamps:",
                "Sélectionnez les solides Bride à exporter:"
                },
                {
                "Post uses to identify tool:",
                "PostPro identifie l'outil par:"
                },
                {
                "Tool number",
                "Numéro outil"
                },
                {
                "Tool ID",
                "ID outil"
                },
                {
                "Machine Zero offset from setup UCS:",
                "Décalage Zéro Machine du SCU repère:"
                },
                {
                "Individual fixture offset",
                "Origine posage individuel"
                },
                {
                "DATUM shift and rotation",
                "Décalage et rotation Repère"
                },
                {
                "The stock in the project is not indexed. NCSIMUL can only simulate one setup at a time in such cases. Select setup you want to verify in NCSIMUL:",
                "Le brut de ce projet n'est pas indexé. NCSimul peut uniquement simuler un repère à la fois dans ce cas. Choisissez le repère à contrôler dans NCSIMUL:"
                },
                {
                "Machine turret information:",
                "Informations Tourelles Machine:"
                },
                {
                "Upper turret, Main Spindle side:",
                "Tourelle Haute, Broche Principale:"
                },
                {
                "Upper turret, Sub Spindle side:",
                "Tourelle Haute, Contre-Broche:"
                },
                {
                "Lower turret, Main Spindle side:",
                "Tourelle Basse, Broche Principale:"
                },
                {
                "Lower turret, Sub Spindle side:",
                "Tourelle Basse, Contre-Broche:"
                },
                {
                "Milling head",
                "Tête de Fraisage"
                },
                {
                "Turret",
                "Tourelle"
                },
                {
                "Export",
                "Exporter"
                },
                {
                "Cancel/Exit",
                "Annuler/Quitter"
                },
                {
                "Help",
                "Aide"
                },
                {
                "No files are open",
                "Aucun fichier ouvert"
                },
                {
                "Program failed to generate setup information.",
                "Le programme a échoué lors de la génération des informations repère."
                },
                {
                "Failed to save nc code",
                "Echec d'écriture du fichier iso"
                },
                {
                "Output written to:",
                "Fichiers écrits sous :"
                },
                {
                "Exception in ",
                "Exception dans "
                },
                {
                "Error occured while exporting stock to .stl file: ",
                "Erreur lors de l'export du brut en .stl: "
                },
                {
                "Exception occured: ",
                "Une exception s'est déclenchée : "
                },
                {
                "Error occured while exporting clamp to .stl file: ",
                "Erreur lors de l'export des brides en .stl "
                },
                {
                "Cannot export data to NCSIMUL: there are errors in the document and nc code cannot be generated.",
                "Impossible d'exporter des données vers NCSimul: il y a des erreurs dans le document et le code CN ne peut être généré."
                },
                {
                "Warning: Tools info was exported, but information for following tool(s) {0} was not exported completely, because the tool group(s) are unsupported by this addin.",
                "Avertissement: Des informations outils ont été exportées, mais des informations des outil(s) {0} n'ont pas été exportées totalement, car ce groupe d'outil n'est pas supporté par cet addin."
                },
                {
                "You don't have license for FeatureCAM to NCSIMUL module. Please contact your dealer for more information.",
                "Vous ne possèdez pas la licence FeatureCAM to NCSIMUL. Merci de contacter votre revendeur pour plus d'information."
                },
                {
                "Failed to find NCSIMUL on this computer. Cannot continue.",
                "NCSIMUL n'est pas installé sur cet ordinateur, impossible de continuer."
                },
                {
                "Failed to add toolbar button for FeatureCAM to NCSIMUL macro",
                "Echec d'ajout du bouton macro FeatureCAM to NCSIMUL"
                },
                {
                "Failed to remove toolbar button for FeatureCAM to NCSIMUL macro",
                "Echec d'enlèvement du bouton macro FeatureCAM to NCSIMUL"
                },
                {
                "You need to select an item in the Order list",
                "Veuillez sélectionner un élément de la liste"
                },
                {
                "Output directory path must be set. Please fix it and try again.",
                "Veuillez spécifier un répertoire d'enregistrement."
                },
                {
                "NCSIMUL machine file has to be set",
                "Veuillez sélectionner une machine NCSIMUL"
                },
                {
                "If NCSIMUL machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "Si le chemin de la machine NCSimul est défini, le fichier doit exister or le fichier {0} est absent. Veuillez choisir un fichier existant et réessayer."
                },
                {
                "Setup name (non-indexed parts)",
                "Nom du repere (pièce non indexée)"
                },
                {
                "Select order:",
                "Sélectionner l'ordre"
                },
                {
                "Up",
                "Haut"
                },
                {
                "Down",
                "Bas"
                },
                {
                "OK",
                "Ok"
                },
                {
                "Apply",
                "Appliquer"
                },
                {
                "Cancel",
                "Annuler"
                },
                {
                "Subdirectory Options...",
                "Options du sous-répertoire..."
                },
                {
                "Preview",
                "Aperçu"
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
                "Le document actif est une pièce 5axes avec 'Position Posage' réglé sur 'Un Repere, 1 Posage'. Sélectionnez si le postpro utilise:"
                },
                {
                "Save Options",
                "Sauver les Options"
                },
                {
                "FeatureCAM to NCSIMUL",
                "FeatureCAM vers NCSIMUL"
                },
                {
                "Export tool radius as radius compensation",
                "Exporter le rayon comme valeur de compensation"
                },
                {
                "Export tool length as length compensation",
                "Exporter la longueur comme valeur de compensation"
                }
            };
        }

        public Dictionary<string, string> GetAll()
        {
            return this.strings;
        }
    }

}