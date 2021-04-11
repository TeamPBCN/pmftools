@echo off
set AC=tools\autousc
set FF=tools\ffmpeg
set FFFLAGS=-hide_banner -loglevel error -y
set MP=tools\mps2pmf

if exist input\avi\ (
  for %%f in (input\avi\*.avi) do call :avi2pmf %%f
) else (
  echo Please put your *.avi files into input\avi
)
rd /s /q obj
goto :EOF

:avi2pmf
echo Processing file %~nx1...
if exist obj\ (
  rd /s /q obj
)
md "obj" 2>nul
md "output\pmf" 2>nul
%FF% %FFFLAGS% -i %1 -ar 44100 obj\%~n1.wav
%AC% --cn %~n1 --pn %~n1 -a obj\%~n1.wav -v %1 -o obj\%~n1.MPS

ffmpeg -i obj\%~n1.MPS 2> obj\output.tmp
rem search "  Duration: HH:MM:SS.mm, start: NNNN.NNNN, bitrate: xxxx kb/s"
for /F "tokens=1,2,3,4,5,6 delims=:., " %%i in (obj\output.tmp) do (
    if "%%i"=="Duration" (
        set /A min=%%k+%%j*60
        set sec=%%l
    )
)
%MP% -i obj\%~n1.MPS -o output\pmf\%~n1.pmf -m %min% -s %sec%
exit /b