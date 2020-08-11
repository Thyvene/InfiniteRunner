# InfiniteRunner

Infinite Runner is an endless runner game made with Unity engine, using a procedural algorithm to generate the map and put obstacles on it!

## Core system:
###### Implemented:
  - Procedural Algorithm to generate the map [Map Generator](https://github.com/Thyvene/InfiniteRunner/blob/master/Assets/Scripts/Helper%20Scripts/MapGenerator.cs) and [Offset](https://github.com/Thyvene/InfiniteRunner/blob/master/Assets/Scripts/Helper%20Scripts/OffScreen.cs)
  - Sound manager for the background and animations sound [Sound Manager](https://github.com/Thyvene/InfiniteRunner/blob/master/Assets/Scripts/Helper%20Scripts/SoundManager.cs)
  - Main menu controller, related to every main menu functions/interactions [Main Menu](https://github.com/Thyvene/InfiniteRunner/blob/master/Assets/Scripts/Helper%20Scripts/MainMenuController.cs)
  - Character selection system [Character Selection](https://github.com/Thyvene/InfiniteRunner/blob/master/Assets/Scripts/Helper%20Scripts/CharacterSelectScript.cs)
  - Game Data and Save/Load data system [Game Data system](https://github.com/Thyvene/InfiniteRunner/blob/master/Assets/Scripts/Helper%20Scripts/GameManager.cs)
  - Player control/interactions [Player Controller](https://github.com/Thyvene/InfiniteRunner/blob/master/Assets/Scripts/Player%20Scripts/PlayerController.cs)
###### To do/to fix:
  - An issue where game data are not loaded after the game close (so everything is lost)
  - Maybe some scripts need refactoring at certain points
  
## UI/UX:
###### Implemented:
  - Main menu
  - Pause menu
  - Character selection
  - Background music
  - Objects animations
###### To do/to fix:
  - Some buttons are not usable yet or useless
  - At the base, it's a game phone, so the display is not scaled for PC or Web (Binary will launch with a 480*800 dimension)
