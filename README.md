# Assignment 3 
The flocking behavior in this assignment is in the form of a swarm of bees that flies back and 
forth around the middle of the screen. Colliding with them will not kill the player, but it can 
mess up jumps, so watch out!

## 1. Fully implement the flocking algorithm as described by Craig Reynolds in his Boids application:
The code within this game contains the boids code provided by Professor Amresh, which has been
altered to act as a tight swarm of bees that move back and forth across the screen led by a 
queen bee, the target.

## 2. Demonstrate this flocking algorithm by integrating it with your game from previous assignments or developing a new game:
The bee boids are integrated into the game from assignment 1 as an obstacle to the player jumping. The bees do this by flying back and forth across the top of the screen in the middle portion of the level as collision shapes.

## 3. Use of flocking to improve design, feel, and experience:
Flocking is used in the game to act as an additional obstacle to jumping across chasms and over enemies 
in the platformer. I had initially made touching them kill the player, but this ended up making the game 
too difficult. Instead, they are just an extra layer of challenge to the current mechanics of the 
platformer. They are made to look like bees, which also adds to the overall woodsy feel of the game. 
All in all, the bee boids are a good way to add an extra, dynamic obstacle layer without making the game
too difficult, and adding another creature to the woods scene. 
