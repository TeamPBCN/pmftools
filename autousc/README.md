# autousc
 This is an experimental tool for automating UMD Stream Composer multiplex works using UI Automation technology.

# Setup
Before using this tool, you must search and download "UMD Stream Composer" from the Internet and put the program folder to the same place as autousc.exe. Or you can use the "-x|--executable" option to specific the location of "UmdStreamComposer.exe".

UMD Stream Composer version `1.5 RC4` is proved working with this tool.

# Usage
``` shell
autousc --cn <Clip Name> --pn <Project Name> -a <Audio File> -v <Video File> -o <Output File> [-x Path to UmdStreamComposer.exe] [--cd Clip Description] [--pn Project Name]
```