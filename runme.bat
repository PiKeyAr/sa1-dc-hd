@echo off
cls
echo Step 1 - Extract GDI file
pause
mkdir data
bin2iso image\track03.bin data\track03.iso
copy extract.exe data\
cd data
extract track03.iso
del track03.iso
del extract.exe
cd ..
echo ...
echo ...
echo Step 2 - Patch files
pause
patcher -d
echo ...
echo ...
echo Step 3 - Build track03.bin
pause
buildgdi -data data -ip ip.bin -output image -raw
echo ...
echo ...
echo Finished!
pause