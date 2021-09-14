# Sonic Adventure DC-HD Image Builder

Version 14/09/2021

This repository hosts a program that applies a set of patches (mods) to the Dreamcast version of Sonic Adventure to introduce various fixes and improvements.

The modded version is meant to be run on the [flycast emulator](https://flyinghead.github.io/flycast-builds), although *some* (not all!) mods will also work on other emulators and original Dreamcast hardware.

**LIST OF MODS**

- The game runs at 60 FPS during gameplay without framerate-related glitches.
- Widescreen hack without model clipping or HUD stretching.
- All FMVs can be skipped by pressing Start.
- Cutscenes can be skipped by holding B before a cutscene loads.
- If you cannot run in a straight line with your controller/keyboard, there is a patch to fix that.
- Minor bugfixes for some levels such as object placement, Z fighting fixes etc.
- Better level and object draw distance.
- Some mods made for the PC version of Sonic Adventure DX may have Dreamcast adaptations in the future.

**LIST OF CHEAT CODES**
- All characters unlocked in Adventure Mode.
- Easy fishing (infinite rod tension).


All mods are optional and can be toggled individually before building the image. Cheat codes can be toggled in flycast's Cheats menu.


**PREREQUISITES**

- Sonic Adventure US 1.005 GDI image.
- [flycast](https://flyinghead.github.io/flycast-builds/) or reicast to play the game.
- Dreamcast BIOS for flycast (optional?)

**As of September 2021, only flycast is compatible with the 60 FPS code and the current implementation of the widescreen hack. The cheat table is in the RetroArch .cht format used by flycast.**

**HOW TO USE**

See [this wiki page](https://github.com/PiKeyAr/sa1-dc-hd/wiki/Using-Sonic-Adventure-Image-Builder).

**CONFIGURING FLYCAST**

See [this page](https://github.com/PiKeyAr/sa1-dc-hd/wiki/Configuring-flycast-for-the-modded-image).

**CREDITS**

- [Exant](https://github.com/Exant64) for work on the hardest parts related to widescreen and skippable cutscenes, as well as general help

This program relies on the following tools and libraries:
- bin2iso by jj1odm and ISO image extractor from FamilyGuy's GDI2Data https://dcemulation.org/dumpcast/viewtopic.php?t=785
- DiscUtils https://github.com/DiscUtils/DiscUtils
- DiscUtils fork from Sappharad's GDIbuilder https://github.com/Sappharad/GDIbuilder
- Sewer56's dlang-prs https://github.com/Sewer56/dlang-prs
- SonicFreak94's pvmx https://github.com/michael-fadely/pvmx
- ini-parser https://github.com/rickyah/ini-parser
