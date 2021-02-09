## Documentation

This is the documentation for my Dart Augmented reality game prototype. This repository contains all of the code for this game and this readme will go through the code to explain how it works in a nutshell.

This game was made using Unity 2019.4.13f and uses the AR Foundation package along with ARKit, the iOS native augmented reality package. For rendering, the game uses the Universal Render Pipeline.


## Table of Contents
<!--ts-->
* [Documentation](#documentation)
   * [Table of Contents](#table-of-contents)
   * [Gameplay](#gameplay)
   * [Code Structure](#code-structure)
   * [Credits](#credits)
<!--te-->

------------

### Gameplay

The game is a two-player (so far) augmented reality dart game. Currently there isn't a finished game loop and a win/lose state. Before being able to play the game, it tries to find a playable dart-board. For now, the board is only generated via three images of different dart boards. When the board is loaded up, the players are ready to start playing. To shoot a dart, push the button on the bottom right corner of the screen (you will know because the button says SHOOT).  If, for some reason, the dart board gets out of line with the image, the button on the top right corner allows you to reload the screen and re-allign the phone with the board.

Here are the dart boards that the game works with:

![](https://images-na.ssl-images-amazon.com/images/I/910jYf5st5L._AC_SL_1500.jpg)

![](https://newittsproduction.blob.core.windows.net/cdn/images/products/new-design/800x800/it017801.jpg)

![](https://www.a-zdarts.com/mm5/graphics/00000001/dartboards/Gran%20Darts%20Gran%20Board%203S%20Bluetooth%20Electronic%20Dartboard%20-%20Green.jpg)

------------

### Code Structure

The code structure for this game heavily utilizes[ ScriptableObjects,](https://docs.unity3d.com/Manual/class-ScriptableObject.html " ScriptableObjects,") which basically are Unity's get out of jail free card (literally the best). They allow the developer to create a script that can be turned into various instances of itself as an asset.

A code structure that utilizes ScriptableObjects prevents dependencies that could otherwise break the prototype if they failed to exist. Furthermore, it allowed me to test various aspects of the prototype independently, as I was easily able to test one part of it at a time. I created ScriptableObjects that held variables and events, which were "fed" in to various scripts throughout the project and were utilized based on the context of the script I was working with. They could each be found in the utils folder in this repository.

------------

### Credits

Since some of the assets used in this project were taken from outside sources, this section provides credit to the makers of the tools, sounds, and other assets used in this prototype:
- [QuickOutline](https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488 "QuickOutline by Chris Nolet") - A Unity asset that adds a solid outline to any object.
- [Chalk Stick Font](https://www.fontspace.com/chalk-stick-font-f32098 " Chalk Stick Font")
- Sounds found on [Zapsplat](https://www.zapsplat.com "Zapsplat") and [Youtube](https://www.youtube.com/ "Youtube")
