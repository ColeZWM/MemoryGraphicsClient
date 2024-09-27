WORK IN PROGRESS!

About:
			MemoryGraphicsClient is a software designed to be able to take a image as an input, and output a true/false value depending if the image(or something similar) is on the screen.
			This software is also capable of executing further commands if an object or image is recognized on the screen (for example: is the searchbar is recognized on the screen, move 
			mouse to search bar location and type a URL).

			MemoryGraphicsClient was origionaly made with 3D environments in mind. Although it is still a work in progress, MemoryGrpahicsClient can recognize the same object/image in real 3D 
			Environments regardless or lighting, fog, or other such environmental variables (provided enough example images are provided of the object/image being recognized). This program can
			also handle 2D object recognition, although it may take a bit more tinkering to get that to work :)

			Current/ExampleUses: 
								FTC (First Tech Challenge) - image rendering paired with openCV to identify game objects durring autonomous and their positions without the use of april tags
								Bots - Create bots to recognize images/objects on screen and carry out a command (if dirtBlock == true { mine();})
								FaceID/Recognition - recognize peoples faces for use in security programs
								And alot of other stuff too!

IMPORTANT:
			Although MemoryGraphicsClient is able to recognize and define 2D objects/images, it is designed for 3D objects/images. When creating/defining a coreID for an object or image,
			make sure all comparision values are on the same face (same depth value). If errors still occur (coreID.NaN) try using comparision values that are only on the back side of the 
			image (see lines 782 - 794 in GraphicsCore.cs for more info)

			MemoryGraphicsClient Cannot run if another program that utilizes screen sharing capabilities are running! (ex, discord screenshare, zoom, googleMeets, ect..)

			
HOW TO USE: 
			1. Upload all images in PNG or Bitmap format to a folder of your choice/creation inside the ObjectImgs folder

			2. go to dataManage.cs and create a string array and put all filePaths of all images your just uploaded into there

			3. in dataManage.cs, go to line 267 and change the parameter of the getDominateColors(string s) from the example array, to your array (frorm step 2)

			4. create a static double array and a static int array in dataManage.cs. This is were you will store the coreID's and simPixel values for the images you just uploaded
			   In addition, create a static string array with the messages to be sent if an object/image is on screen. (see lines 213 - 224 in GraphicsCore.cs for more info)

			5. Go to line 268 in GraphicsCore.cs and change the parameters to reference the arrays you have just declared. Make sure all 3 arrays have the same size 

			6. Go to lines 70, 593, 934, 936, and 938 and change the directory paths depending on where you saved/downloaded the files

			7. Go to line 306 in GraphicsCore.cs and change the string array parameter to the string array with all of your filepaths that you created earlier in step 2

			8. When you have the object/image you want to get the simPix and CoreID Values for on screen, run the program, make sure the console dose not block the view of the 
			   object/image on screen. Next, input the values returned into their respective arrays (the ones you made in step 4), and make sure the indexes of both simPixels, 
			   CoreID, And the response when object is found are all the same.

			9. (OPTIONAL), if you want to change/alter the coreID that is used to define your object/image, go to lines 284 - 302 in dataManage.cs and change the bL, cL, vL, 
			   and how they are processed to output the result of valOut as you see fit. The "shrinkFactor" parameter is how many cycles the program will go through before 
			   returning the coreID. If you want more information, see the PolygonalRendering class in GraphicsCore.cs. Change the shrinkFactor parameter as you see fit.

			10. run the program for your intended purpose! dataManage.cs contains examples for you to follow/take inspiration from if you need further help. The example detects 
				all 22Point font lime green letters of the English Alphabet when written in google docs! 