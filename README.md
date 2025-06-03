# ComfyUI EXIF Reader
This is a super simple and small utility I wrote to view the EXIF data for images generated from Comfy UI.

## Dependencies
You will need to download the "exiftool" by Phil Harvey - Find it here: https://exiftool.org/

## Usage
* Clone this repository
* Update the `appsettings.json` file to point to the path where you've just installed or downloaded the exiftool
* Build the solution
* Launch the app!

When the app launches, drag any generated PNG image onto the box in the top-right corner and the ComfyUI parameters or workflow will be displayed on the grid below and the image itself will appear on the left.

## Example

Here's an example of an image I generated. I've highlighted things I find most important:

![image](https://github.com/user-attachments/assets/f1421f0b-139c-4996-9d11-dfb064df43ee)

## Bonus - Use as "Send To" or "Open With"

#### Open With
On Windows 11, right-click on a generated image and select "Open With" > "Choose another app", then browse to the compiled EXE.
Now when you right-click on any image and select "Open With", you can select the EXIF reader directly from the right-click menu.

#### Send To
You can also include this application in the "Send To" group in Windows Explorer:
* Press "Copy" on the EXE file from the BIN\Debug output directory of the solution.
* Open the Run Dialog ([WIN] + [R]) or Windows Explorer and navigate to `shell:sendto`.
* Right-click and select "Paste Shortcut"
Now you will have a new option on the "Send To" list for ExifReader (or whatever you choose to call your shortcut)
