@echo off
for %%a in (D:\BERT\BertScout2019Data\Documents\2019*.json) do %LOCALAPPDATA%\Android\sdk\platform-tools\adb.exe push "%%a" /storage/sdcard0/Documents/
pause
