# Sonic Adventure DC-HD patch

Version 16/08/2021

This repository hosts a set of patches for the Dreamcast version of Sonic Adventure with various fixes and improvements.

**LIST OF PATCHES**

- The game runs at 60 FPS during gameplay without framerate-related glitches (hopefully)
- All FMVs can be skipped by pressing Start
- Code to disable all cutscenes
- If you cannot run in a straight line with your controller/keyboard, there is an optional patch to fix that
- Less choppy ocean in Emerald Coast (similar to Dreamcast Conversion for SADX PC)

**PREREQUISITES**

- Sonic Adventure US 1.005 GDI image
- [flycast](https://flyinghead.github.io/flycast-builds/) or reicast to play the game (the 60 FPS hack doesn't work properly in other emulators as of August 2021)

**INSTRUCTIONS**

1) Download the latest version and extract it somewhere.
2) Open `patch.ini` and uncomment the running straight and/or cutscene codes if you need them.
3) Put your Sonic Adventure GDI file (with BIN files) in the `image` folder.
4) Run the file `runme.bat` and follow the instructions.
5) The GDI file in the `image` folder will be the patched version of the game.

**There is no backup!** After the process finishes, you GDI in the image folder will be modified. Make a backup in advance.

**CREDITS**

This patcher uses the following tools/libraries:
- bin2iso and extract from FamilyGuy's GDI2Data https://dcemulation.org/dumpcast/viewtopic.php?t=785
- buildgdi from Sappharad's GDIbuilder https://github.com/Sappharad/GDIbuilder
- FraGag's PRS compressor https://github.com/FraGag/prs.net
- ini-parser https://github.com/rickyah/ini-parser
- Simple patcher (uses all of the above) https://github.com/PiKeyAr/dcpatcher
- Special thanks to [Exant](https://github.com/Exant64) for creating SH4 assembly to fix walking NPCs at 60 FPS

**UPDATES AND DISCUSSION**

This repository is primarily meant for research and discussion related to Sonic Adventure (Dreamcast) and emulating it on PC with various patches and enhancements.

The following goals are considered for this project:

**Priority fixes**
1) 60 FPS patch + fixes for glitches that happen when the game is running at 60 FPS (mostly done)
2) Fix for "controller drift" that happens when playing with the keyboard or a controller with a less sensitive analog stick than the original DC one (done)
3) Skippable FMVs (done)
4) A code or an ingame toggle to disable cutscenes (functional but could be better)
5) A proper widescreen patch without clipping or HUD stretching (not started yet)


**Enhancements and other fixes (only ideas for now)**
1) A texture pack with HD menu textures based on [SADX HD GUI 2](https://github.com/PiKeyAr/sadx-hd-gui)
2) Restoration of unused but functional SET objects similar to the "Extra SET layouts" option in the [Fixes, Adds and Beta Restores mod](https://github.com/supercoolsonic/Fixes_Adds_BetaRestores)
3) Increased level and object draw distance
4) Various minor fixes related to object placement, missing collision etc. that involve small edits to the landtable or SET items
5) Better quality FMVs

Unless specified otherwise, all patches and codes shared here are for Sonic Adventure 1.005 (US, animated title screen).