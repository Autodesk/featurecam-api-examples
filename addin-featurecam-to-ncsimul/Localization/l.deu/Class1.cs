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
                "FeatureCAM Dateiverzeichnis:"
                },
                {
                "Save NCSIMUL files to FeatureCAM file directory",
                "Speichern im FeatureCAM Dateiverzeichnis"
                },
                {
                "Save NCSIMUL files to a different directory",
                "Speichern in einem anderen Dateiverzeichnis"
                },
                {
                "Browse...",
                "Suchen..."
                },
                {
                "Create subdirectory for NCSIMUL files",
                "Unterverzeichnis für NCSIMUL Dateien"
                },
                {
                "Include in the NCSIMUL project subdirectory name:",
                "Verzeichnisname inkl:"
                },
                {
                "FeatureCAM file name",
                "FeatureCAM Dateiname"
                },
                {
                "FeatureCAM project title",
                "FeatureCAM Projekt Titel"
                },
                {
                "FeatureCAM postprocessor name",
                "FeatureCAM Postprocessorname"
                },
                {
                "NCSIMUL machine name",
                "NCSIMUL Machinenname"
                },
                {
                "Preview directory path for output files",
                "Vorschau Ausgabeverzeichnis"
                },
                {
                "Select NCSIMUL machine file:",
                "Auswahl NCSIMUL Maschine:"
                },
                {
                "Select solids to be exported as clamps:",
                "Auswahl der Spannmittel:"
                },
                {
                "Post uses to identify tool:",
                "Werkzeugausgabe in Post:"
                },
                {
                "Tool number",
                "Werkzeug Nummer"
                },
                {
                "Tool ID",
                "Werkzeug ID"
                },
                {
                "Machine Zero offset from setup UCS:",
                "Maschinen Nullpunkt zur Aufspannung:"
                },
                {
                "Individual fixture offset",
                "Einzelne Nullpunkte"
                },
                {
                "DATUM shift and rotation",
                "Nullpunkt Verschiebung"
                },
                {
                "The stock in the project is not indexed. NCSIMUL can only simulate one setup at a time in such cases. Select setup you want to verify in NCSIMUL:",
                "Das Rohteil im Projekt wurde nicht indiziert. NCSIMUL kann in solchen Fälle nur ein Setup auf einmal simulieren. Wählen Sie das Setup aus, dass Sie in NCSIMUL prüfen möchten:"
                },
                {
                "Machine turret information:",
                "Revolver Information:"
                },
                {
                "Upper turret, Main Spindle side:",
                "Oberer Revolver, Hauptspindel:"
                },
                {
                "Upper turret, Sub Spindle side:",
                "Oberer Revolver, Hauptspindel:"
                },
                {
                "Lower turret, Main Spindle side:",
                "Unterer Revolver, Hauptspindel:"
                },
                {
                "Lower turret, Sub Spindle side:",
                "Unterer Revolver, Haupspindel:"
                },
                {
                "Milling head",
                "Fraeskopf"
                },
                {
                "Turret",
                "Revolver"
                },
                {
                "Export",
                "Export"
                },
                {
                "Cancel/Exit",
                "Abbruch"
                },
                {
                "Help",
                "Hilfe"
                },
                {
                "No files are open",
                "Keine Datei offen"
                },
                {
                "Program failed to generate setup information.",
                "Fehler beim Erstellen der Setup Information."
                },
                {
                "Failed to save nc code",
                "Fehler bei NC-Speichern"
                },
                {
                "Output written to:",
                "Gespeichert nach:"
                },
                {
                "Exception in ",
                "Fehler in "
                },
                {
                "Error occured while exporting stock to .stl file: ",
                "Fehler beim Erstellen des Rohteils"
                },
                {
                "Exception occured: ",
                "Fehler aufgetreten"
                },
                {
                "Error occured while exporting clamp to .stl file: ",
                "Fehler beim Erstellen der Spannmittel"
                },
                {
                "Cannot export data to NCSIMUL: there are errors in the document and nc code cannot be generated.",
                "Export nach NCSIMUl nicht möglich: NC-Programm kann nicht generiert werden."
                },
                {
                "Warning: Tools info was exported, but information for following tool(s) {0} was not exported completely, because the tool group(s) are unsupported by this addin.",
                "Warnung: Informationen für folgende Werkzeuge {0} wurden nicht vollständig exportiert, da Werkzeuggruppe nicht unterstützt wird."
                },
                {
                "You don't have license for FeatureCAM to NCSIMUL module. Please contact your dealer for more information.",
                "Keine Lizenz gefunden."
                },
                {
                "Failed to find NCSIMUL on this computer. Cannot continue.",
                "NCSIMUL nicht gefunden."
                },
                {
                "Failed to add toolbar button for FeatureCAM to NCSIMUL macro",
                "Fehler beim Erstellen der NCSIMUL Symbolleiste"
                },
                {
                "Failed to remove toolbar button for FeatureCAM to NCSIMUL macro",
                "Fehler beim Entfernen der NCSIMUL Symbolleiste"
                },
                {
                "You need to select an item in the Order list",
                "Bitte Auswahl treffen"
                },
                {
                "Setup name (non-indexed parts)",
                "Setupname (nicht-indizierter Bauteile)"
                },
                {
                "Select order:",
                "Reihenfolge:"
                },
                {
                "Up",
                "Rauf"
                },
                {
                "Down",
                "Runter"
                },
                {
                "OK",
                "OK"
                },
                {
                "Apply",
                "Bestätigen"
                },
                {
                "Cancel",
                "Abbruch"
                },
                {
                "Preview",
                "Vorschau"
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
                "Output directory path must be set. Please fix it and try again.",
                "Ausgabe Verzeichniss muss gesetzt werden."
                },
                {
                "NCSIMUL machine file has to be set",
                "NCSIMUL Maschine waehlen"
                },
                {
                "If NCSIMUL machine file path is set, file must exist. File {0} doesn't exist. Please select existing file and try again.",
                "NCSIMUL Pfad wurde festgelegt ohne Datei. Datei {0} nicht vorhanden. Bitte auswaehlen und nochmals versuchen."
                },
                {
                "Active document is 5-axis part with 'NC Code Reference Point' set to 'Each setup's own fixture'. Select whether post uses:",
                "Dokument ist 5-Achs Positionierung mit 'Pivot Punkt...' aendern auf 'Eigene Aufspannungen..'. Gemaess dem Postprozessor:"
                },
                {
                "Subdirectory Options...",
                "Optionen des Unterverzeichnis..."
                },
                {
                "Subdirectory Options",
                "Optionen des Unterverzeichnis"
                },
                {
                "Save Options",
                "Speicheroptionen"
                },
                {
                "FeatureCAM to NCSIMUL",
                "FeatureCAM zu NCSIMUL"
                },
                {
                "Export tool radius as radius compensation",
                "Ausgabe Werkzeug Radiuskompensation"
                },
                {
                "Export tool length as length compensation",
                "Ausgabe Werkzeug Laengenkompensation"
                }
            };
        }

        public Dictionary<string, string> GetAll()
        {
            return this.strings;
        }
    }

}
