set OUT=out
if not exist "%OUT%" mkdir %OUT%

set BG_OUT=RooftopClouds_out
if not exist "%BG_OUT%" mkdir %BG_OUT%

ffmpeg -i RooftopClouds.mp4 ./%BG_OUT%/%%05d.png