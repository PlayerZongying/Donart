# Donart  

## Intro
Using Unity 2022.3.8f1, By Zongying Liu.[Play at Itch.io](https://playerzongying.itch.io/donart)  

Donart (Donut cart) is a racing game, where two players could add different things (toppings) to the donut track and then race.  

There are two scenes in this game, "MainMenu" and "Game". Players will first select toppings in the "MainMenu" scene to make the level, then start racing in the "game" scene.

## How to Boot  

For Unity users, please go to```./Assets/Scenes/MainMenu```, and start to play from this scene.

## Gameplay  

### 1. Make Your Level  

In "MainMenu" scene, you can select what you want in the donut track. You can choose from none to all of them.  

* Chocolate Bump: Brown static bump staying at a random position on the surface of the donut track.   

* Melon Slide: Green "accelerator" appears and rotates at a random position within the donut track. It will accelerate cars passing through it.  

* Cherry Boom: Pink booms, appearing and moving at a random position on the surface of the donut track. It will burst and impact all cars with the explosion range, and then appear somewhere else.  

![img](Misc/LevelMaking.png "Main Menu ")*Main Menu*  

Apart from selecting Toppings, you can decide also how many rounds you would like to complete in a game session by changing the "Number of Donut", and single or double player mode by selecting number of player.

Finally press "Place Your Order" to start load the game session.

### 2. Racing  

There are two cars in this racing game, a black car "Night" and a white car "Day". In the single-player mode, player will be signed with one of the car randomly, and the other one will be controlled by AI. While in the double player mode, player will control both of the two cars.

In the "Game" scene, there is a top-down view of the whole track and the view(s) of the player(s) in the lower half of the screen.  
![img](Misc/LayoutSinglePlayer.png "Single-Player Mode Layout")*Single-Player Mode Layout*  

![img](Misc/LayoutDoublePlayer.png "Double-Player Mode Layout")*Double-Player Mode Layout*

The black car for Player 1 and the white car for Player 2 will be initialized at the left and right parts inside of the donut track. 
The black car should go clockwise while the white car should go counterclockwise. 
There is also a green arrow for each car, indicating the correct direction the car should go.
![img](Misc/InGame.png "In Game Layout")*In Game layout*


The racing starts after counting down for 3 seconds. the time starts counting as well.

Since the track is a circle, the progress for each car is counted in degrees from its initial position to its current position.   

The number of completed rounds is also counted for each car. 
![img](Misc/UI.png "In Game UI")*In Game UI*

By pressing ```WASD``` and ```up, down, left, right arrow```, try your best to avoid the bumps, catch the slides, recover balance after being blown away from the booms, and complete more rounds as fast as you can.
![img](Misc/InGameWithAll.png "Chaotic but fun")*Chaotic but fun*

You can also press the ```Esc``` key to pause the game.
![img](Misc/Pause.png "Pause panel")*Pause panel*

When both players have completed a certain number of rounds (for now 3), results will be shown, and who finished faster is the winner. And then you can choose to rematch or go back to the main menu.
![img](Misc/Result.png "Result Panel")*Result Panel*

## Operation
### 1. In the main menu
Just use the mouse to click on the button.

### 2. In-game

* Single-player mode:  
Press ```WASD``` or ```up, down, left, right arrow``` to control your car.

* Double-player mode:  
For player 1 to control the black car "Night". press ```WASD```.  
For player 2 to control the white car "Day". press ```up, down, left, right arrow```.

To pause the game, press ```Esc``` button and the pause menu will be shown while the game is paused.

## Game Logic

### 1. Car
The car in this game is made based on Unity Physics with rigid body, controlled by Unity new input system, followed by a cinemachine free look camera.  

* ```carController``` calculates the suspension, steering, and acceleration.  
* ```carInputHandeler``` lerps the input from the input system.  
* ```carStatus``` determines the driving direction (clockwise or counterclockwise) and counts the progress in degree and the rounds the player has finished.   
* ```AIInput``` determines how the AI should adjust its acceleration and steering comparing to the current correct driving direction in the donut track.



### 2. Torus (the donut track)
Based on its major radius R, minor radius r, and local scale, the torus can give a position and rotation on its surface, with which the children ```BumpManager```, ```AcceleratorManager```, ```BoomManager``` could instantiate the bump (chocolate bump), accelerator (melon slide) and boom (cherry boom) correctly on the torus surface. 

The three managers mentioned above will manage the behavior of the prefabs they instantiated.

### 3. Game Manager
According to the scriptable object ```GameSettings``` which was modified previously in the main menu scene through the buttons, ```GameManager``` sets the game session with:

* Toppings that should be added to the donut track.
* The number of rounds that should be completed.
* Single-player mode or double-player mode.

Apart from that, ```GameSettings``` also determines game states by watching the status of 2 cars.

### 4. UI
Show different panels according to game states (from the ```GameManager```) and player input.  
Display important game data(from the ```GameManager```).


## Insperation

Car physics:   
https://www.youtube.com/watch?v=CdPYlj5uZeI&t=51s&ab_channel=ToyfulGames  

Math for torus:  
https://en.wikipedia.org/wiki/Torus

Button animation:  
https://www.youtube.com/watch?v=cW-E4WEogzE&ab_channel=CocoCode


## Dependency
This project uses Unity 2022.3.8f1, in URP.  
Packages installed:   
* TextMeshPro
* Input System
* Cinemachine
* Universal RP
* Shader Graph

Models are all self-made in Blender.

## Lastly
Wish you have fun in my game! - Zongying Liu
