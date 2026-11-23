CIS267_Homework02_EndlessRunner
Forest Run
Description:
	Forest run is a 2D endless runner game that uses 3 ground tiles to endlessly generate ground for the player to “run” on. The goal is to get as far as possible while collecting coins to beat the high score. Collectables and obstacles move from right to left (towards the player) at a consistent speed. Hitting an obstacle results in the end of the game, where the player can choose to play again or return to the main menu.

How to play:
	Press “play” on the start screen to begin. Player will start running automatically. When obstacles approach the player, press “space” to jump over them. When coins approach, jump at them to collect them and gain a higher score.
-	Obstacles
   o	Obstacles spawn randomly throughout the level and move toward the player from the right side of the screen
   o	The game uses multiple obstacle types to create variety
-	Collectables
   o	Coins randomly spawn throughout game
   o	Player can collect coins by touching them, each coin collected adds 10 points to the score
   o	Coins can appear at different heights
-	Scoring system
   o	Total score = distance points + coin points
   o	Distance points = total distance travel, rounded to the nearest whole number
   o	Coin points = number of coins collected times 10
-	Game over and restarting
   o	When the player hits an obstacle, the game stops and the game over screen appears.
   o	Choosing play again reloads the game and immediately starts a new round
-	High scores
   o	The game keeps track of the player’s 5 highest scores
   o	High scores are stored using unity’s PlayerPrefs system, which allows the scores to remain
   o	The high score screen displays the top 5 scores in descending order

Struggles and Challenges:
One of my initial problems was that I had to remake the project once due to weird initial compiling issues and some conflicts with windows defender, which slowed down my progress a lot at first. 
I also struggled with figuring out how to work with sprite sheets and make good animations + transitions in unity.
I struggled with the coin collectable and movement because the coin would not move towards the player, which made it impossible to collect. 
I had to rework my script that handled spawn positions and timing of obstacles + the coin collectable in order to ensure the player wasn’t faced with an impossible jump or odd placement. 
Working with parallax on the foreground and background was relatively simple, but it took a lot of time to sort out and layer correctly inside unity.

References:
I created my sprites (character, obstacles, coin) in piskelapp.com and used spritepilot.com to help with sprite sheet grids and previewing animations. Some of my sprites were just redrawn from pictures I found while gathering ideas for my game, so not all of them are original. I also used a base character to help with seamless animations of the player character. I found the forest background on itch.io (https://edermunizz.itch.io/free-pixel-art-forest). 
