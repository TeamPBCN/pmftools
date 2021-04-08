# mps2pmf
 MPS to PMF converter.

# Building
On Linux / Unix / macOS, run
``` shell
g++ -Os mps2pmf.cpp -o mps2pmf
```
On Windows, run
``` shell
g++ -Os mps2pmf.cpp -o mps2pmf.exe
```

# Usage
``` shell
mps2pmf -i <MPS File> -o <PMF File> -m <Minutes> -s <Seconds> [--icon]
```

To get the duration of an MPS file, you can use FFMpeg:
``` shell
ffmpeg -i <MPS File>
```

# Credits
This tool is ported from piccahoe's PMF Creater.