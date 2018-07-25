# FeatureCAM API Examples
Welcome to the Autodesk FeatureCAM API Examples repository.   Many API examples ship with the FeatureCAM product, but this repo provides the source code for a set of more advanced plugins which are included with the application only in compiled form or not included directly at all.  These plugins are translators that use the FeatureCAM API to export data from FeatureCAM to third party applications such as Vericut or NCSimul.

The FeatureCAM development team is not actively maintaining or enhancing these plugins.  The source code is being made available to third parties who would like to do their own enhancements, or who would like to use these examples as templates for creating interfaces to other products.  Contributions to this repo will be reviewed and considered for inclusion (see below), but please be aware that these plugins have low development priority.

To learn more about FeatureCAM, visit the forum [here](https://forums.autodesk.com/t5/featurecam-forum/bd-p/276) or the product website [here](https://www.autodesk.com/products/featurecam/overview).

The repository for the FeatureCAM API Examples is hosted at:
https://github.com/Autodesk/featurecam-api-examples

To Contribute please see [Contribute.md](Contribute.md). 

The extension is distributed under the BSD 3-clause license. See [LICENSE.txt](LICENSE.txt).

## Get started
There are just a few simple steps for building one of these plugins:

1) Clone the repo

2) For the plugin you want to build, open the solution in Visual Studio

3) Build

If you are building the Eureka plugin, Eureka must be installed on the machine.

## Running the Plugins 
There are two approaches for running the plugin once you have compiled it.  There may be a few additional notes in the README for each plugin.

### From the FeatureCAM Install Folder
- Copy the dll that you built into the "addins" folder of the FeatureCAM installation you will be running (you may want to make a copy of the original dll before doing this).
- Select the “Add-Ins” tab on the FeatureCAM ribbon.
- Press the “Manage...” button.
- Click the “Library” button.
- Select the plugin from the Exports section.
- Click “Load” and “OK”
- You should get an icon for your plugin in the Addins ribbon tab.

### From the Build Folder
- Check the "addins" folder of the FeatureCAM installation to see if there is a sub-folder corresponding to your plugin.  This may contain icons or localization that needs to be in the same location as the dll when you load it.   You can either copy this folder or reproduce it in your build tree.
- Select the “Add-Ins” tab on the FeatureCAM ribbon.
- Press the “Manage...” button.
- If the plugin you are developing already appears in the list, remove it using the red cross.
- Select the “Browse for file location” button.
- Navigate to the location where the dll was built and select it.
- You should get an icon for your plugin in the Addins ribbon tab.

## Contributions
In order to clarify the intellectual property license granted with Contributions from any person or entity, Autodesk must have a Contributor License Agreement ("CLA") on file that has been signed by each Contributor to this Open Source Project (the “Project”), indicating agreement to the license terms. This license is for your protection as a Contributor to the Project as well as the protection of Autodesk and the other Project users; it does not change your rights to use your own Contributions for any other purpose. There is no need to fill out the agreement until you actually have a contribution ready. Once you have a contribution you simply fill out and sign the applicable agreement (see the contributor folder in the repository) and send it to us at the address in the agreement.

## Trademarks

The license does not grant permission to use the trade names, trademarks, service marks, or product names of Autodesk, except as required for reasonable and customary use in describing the origin of the work and reproducing the content of any notice file. Autodesk, the Autodesk logo, Inventor HSM, HSMWorks, HSMXpress, Fusion 360, FeatureCAM, PartMarker, and PowerMILL are registered trademarks or trademarks of Autodesk, Inc., and/or its subsidiaries and/or affiliates in the USA and/or other countries. All other brand names, product names, or trademarks belong to their respective holders. Autodesk is not responsible for typographical or graphical errors that may appear in this document.
