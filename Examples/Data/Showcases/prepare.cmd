set OUT="CarBody/out"
if not exist "%OUT%" mkdir %OUT%

set OUT2="CelticHeart/out"
if not exist "%OUT2%" mkdir %OUT2%

set BG_OUT="CelticHeart/RooftopClouds_out"
if not exist "%BG_OUT%" mkdir %BG_OUT%

set BG2_OUT="CelticHeart/StarrySky_out"
if not exist "%BG2_OUT%" mkdir %BG2_OUT%

start "Extract frames from RooftopClouds.mp4" ffmpeg -i CelticHeart/RooftopClouds.mp4 ./%BG_OUT%/%%05d.png
start "Extract frames from StarrySky.mp4" ffmpeg -i CelticHeart/StarrySky.mp4 ./%BG2_OUT%/%%05d.png
