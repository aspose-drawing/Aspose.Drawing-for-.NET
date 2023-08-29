set OUT=out
if not exist "%OUT%" mkdir %OUT%

set BG_OUT=RooftopClouds_out
if not exist "%BG_OUT%" mkdir %BG_OUT%

set BG2_OUT=StarrySky_out
if not exist "%BG2_OUT%" mkdir %BG2_OUT%

ffmpeg -i RooftopClouds.mp4 ./%BG_OUT%/%%05d.png
ffmpeg -i StarrySky.mp4 ./%BG2_OUT%/%%05d.png