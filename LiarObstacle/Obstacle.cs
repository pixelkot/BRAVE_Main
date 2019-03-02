/* Interface for every Obstacle in the maze to derive from.
 	 Authors: Ahmet Oguzlu
 	 Updated: 03/01/2019 */

interface Obstacle {

	/* Retruns the int corresponding to the current state of the game
	  -1 - hasn't started
	  0 - ongoing
	  1 - user won
	  2 - user lost */
	int gameState();
}
