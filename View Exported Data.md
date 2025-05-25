# Access exported data on Quest Devices

## Get into the headset file directory

1. Connect headset  
2. Open SideQuest app, and make sure the circle in the top left is green - which means the connection is valid  
3. Go to Settings  
4. Toggle MTP (Media Transfer Protocol)  
5. Open the headset folder under My Computer  

![MTP Toggle](https://raw.githubusercontent.com/TAU-XR/TAUXR-OpenTemplate/main/Media/mtp.png)


## Get the data files

1. navigate to Android/data  
2. search for a folder with your **project name**.  
   1. Every folder of a TAUXR project will start with `com.TAUXRStudio.XXXX`  
   2. Instead of `XXXX` try to search for your project name.  
3. Enter `files` folder - files should be inside  
4. Find your project files and copy to PC

# Second way

### On Quest

---

1. Connect the headset to the computer.  
   1. Look inside the headset and approve connection.  
2. Go to **My Computer,** enter “Quest 2/Pro”  
   1. If you don’t see the Quest 2/Pro device, try:  
      1. Disconnect headset.  
      2. Restart headset.  
      3. Connect headset.  
      4. Look inside headset and allow connection to PC. sometimes you have to soft restart (short press) again for it to appear.  
      5. Connect again.  
3. In your PC, navigate to this path: `This PC\Quest 2\Internal shared storage\Android\data`  
4. search for a folder with your **project name**.  
   1. Every folder of a TAUXR project will start with `com.TAUXRStudio.XXXX`  
   2. Instead of `XXXX` try to search for your project name.  
5. Enter `files` folder  
6. Files should be inside.

### Not Working?

---

1. Sometimes the headset is not recognized by the PC. → Restart and it will probably work.
