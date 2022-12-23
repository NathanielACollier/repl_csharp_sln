
<# 
This script will generate the icns file from a png on macos
 see: https://decovar.dev/blog/2018/10/09/macos-convert-png-to-icns/
 see: https://pypi.org/project/generate-iconset/
 Need this
 pip install generate-iconset
 Run this script with
 pwsh ./macos_generateIcon.ps1
#>

& generate-iconset ./Icon.png --use-sips

