@echo off
setlocal

rem Set the source directory path to the directory where this script is located
set "source_path=%~dp0"

rem Define the file containing target paths
set "distribution_file=%~dp0distribution_target.txt"

rem Check if distribution file exists
if not exist "%distribution_file%" (
    echo Distribution file not found: %distribution_file%
    pause
    exit /b 1
)

rem Loop through each line in the distribution file and copy files
for /f "usebackq delims=" %%A in ("%distribution_file%") do (
    echo Copying files to: %%A
    robocopy "%source_path%\" "%%A" "*.exe" /is /r:0 /w:0
	robocopy "%source_path%\" "%%A" "*.lnk" /is /r:0 /w:0
)

echo All operations completed.
pause