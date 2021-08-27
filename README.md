# Sonic Adventure DC-HD

Version 27/08/2021

This repository hosts a program that applies a set of patches to the Dreamcast version of Sonic Adventure to introduce various fixes and improvements.

The patched version is meant to be run on emulators, although everything other than 60 FPS and the widescreen hacks will also work on original Dreamcast hardware.

**LIST OF PATCHES**

- The game runs at 60 FPS during gameplay without framerate-related glitches (hopefully).
- Widescreen hack without model clipping or HUD stretching.
- All FMVs can be skipped by pressing Start.
- Code to disable all cutscenes.
- If you cannot run in a straight line with your controller/keyboard, there is an optional patch to fix that.
- Less choppy ocean in Emerald Coast (similar to Dreamcast Conversion for SADX PC).


All patches are optional and can be toggled individually before building the image.


**PREREQUISITES**

- Sonic Adventure US 1.005 GDI image.
- [flycast](https://flyinghead.github.io/flycast-builds/) or reicast to play the game.

**As of August 2021, only flycast is compatible with the 60 FPS code and the current implementation of the widescreen hack.**

**HOW TO USE**
1. Download the latest release and extract it somewhere.
2. Run `Patcher.exe`.
3. In the first tab, select the folder containing your SA1 GDI file. If the SA1 image is the correct version, two other tabs will appear.
![Tab1](images/tab1.png)
4. In the second tab, select the patches you would like to apply.
![Tab2](images/tab2.png)
5. In the third tab, select the output path for the patched image and click Build. Wait for the process to finish.
![Tab3](images/tab3.png)
6. Run flycast and select the patched image.
7. Load the cheat file called `SA1-DC-HD.cht` located in the same folder as the patched image.
![Cheat](images/cheat.png)
8. If you are using the widescreen hack, enable the culling fix in the cheats menu.
![Culling](images/culling.png)

**CREDITS**

This patcher uses the following tools/libraries:
- bin2iso and extract from FamilyGuy's GDI2Data https://dcemulation.org/dumpcast/viewtopic.php?t=785
- buildgdi from Sappharad's GDIbuilder https://github.com/Sappharad/GDIbuilder
- Sewer56's dlang-prs https://github.com/Sewer56/dlang-prs
- ini-parser https://github.com/rickyah/ini-parser
- Special thanks to [Exant](https://github.com/Exant64) for creating SH4 assembly to fix several issues related to 60 FPS and widescreen

**UPDATES AND DISCUSSION**

This repository is primarily meant for research and discussion related to Sonic Adventure (Dreamcast) and emulating it on PC with various patches and enhancements.

The following goals are considered for this project:

**Priority fixes**
1) 60 FPS patch + fixes for glitches that happen when the game is running at 60 FPS (mostly done)
2) Fix for "controller drift" that happens when playing with the keyboard or a controller with a less sensitive analog stick than the original DC one (done but could be better)
3) Skippable FMVs (done)
4) A proper widescreen patch without clipping or HUD stretching (functional, in progress)
5) A code or an ingame toggle to disable cutscenes (functional but could be better)

**Enhancements and other fixes (only ideas for now)**
1) A texture pack with HD menu textures based on [SADX HD GUI 2](https://github.com/PiKeyAr/sadx-hd-gui)
2) Restoration of unused but functional SET objects similar to the "Extra SET layouts" option in the [Fixes, Adds and Beta Restores mod](https://github.com/supercoolsonic/Fixes_Adds_BetaRestores)
3) Increased level and object draw distance
4) Various minor fixes related to object placement, missing collision etc. that involve small edits to the landtable or SET items
5) Better quality FMVs

Unless specified otherwise, all patches and codes shared here are for Sonic Adventure 1.005 (US, animated title screen).