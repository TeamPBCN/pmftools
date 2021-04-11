@echo off
set FF=tools\ffmpeg
set FFFLAGS=-hide_banner -loglevel error -y
set PD=tools\psmfdump

if exist input\pmf\ (
  for %%f in (input\pmf\*.pmf) do call :pmf2mp4 %%f
) else (
  echo Please put your *.pmf files into input\pmf
)
rd /s /q obj
goto :EOF

:pmf2mp4
echo Processing file %1...
if exist obj\ (
  rd /s /q obj
)
md "obj" 2>nul
md "output\mp4" 2>nul
%PD% %1 -a obj\%~n1.oma -v obj\%~n1.264
%FF% %FFFLAGS% -i obj\%~n1.264 -i obj\%~n1.oma -map 0 -map 1 -s 480x272 output\mp4\%~n1.mp4
exit /b