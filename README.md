# Ludo
## Introduction
This is a 2D Ludo program developed in Unity Engine. In the game, the user has the option to play local multiplayer or against computer opponents. The computer opponents can operate at three difficulty levels. Additionally, the program includes a settings menu where various graphical settings can be applied. The program also contains its own graphical elements, which were created using Adobe Illustrator.
## Explaining each part of my project
### Main menu and submenu
The main menu includes "play", "options", "rules", and "exit" buttons. When clicking on the "options" button, a panel appears where the user can select their preferred resolution. Additionally, the user can decide whether they want to use the game in full-screen mode. When the user clicks the "play" button, they proceed to a submenu.

In this submenu, the user can specify the username they want to play with. The program will warn the user and not allow them to proceed if they choose a name that has already been used by another player. Furthermore, the user can select the color of the figure they want to play with and set the difficulty level of the computer opponents they want to play against.
### Game 
At the beginning of the game, a game board greets us in the middle of the screen. To the left of it, there is a panel displaying the results of the last 12 rolls during the game. To the right of the game board, there is also a panel showing the name of the next player and a dice. In the top right corner of the screen, there is a menu button, which can be accessed not only by clicking on it but also by pressing the "escape" key. When the game ends, the program congratulates the winning user. Additionally, it provides the option to restart the game with the existing settings, return to the main menu, or close the program.
### Figures
The datas of individual figures is stored using the “Figure”class. After the dice roll is completed, the game highlights the figures that can be moved by the user, and then by clicking on the figure, it can be moved. If the figure moves to a position where an enemy figure is located, it captures the enemy piece. If the figure moves to a position where a friendly figure is located, it receives protection and its appearance changes.


### Computer opponents
At **easy** difficulty level, the program monitors whether:
-  Can it bring a figure into play
-  Can it capture an enemy figure
-  Can the figure enter a garden path or reach the goal

At **meduim** difficulty level, the program monitors:
- All of the previously listed functions
- It checks whether the figure is in danger

At **hard** difficulty level, the program monitors:
-  All of the previously listed functions
-  It checks after the move whether the piece would be in danger
-  It checks if it can move to the same spot as a friendly piece

**Video Demo:**


