# Brain-Games-2.0
*Updated as of 26th of Feb.*
## World API
  * environment (this is basically the scene)
    * contains:
       - Maze
        * Maze API should be procedurally generated
       - User
    * attributes
       - static (basically this is just a rigid body)
## Obstacle API
  * should be an abstract class
  * each "rule" -- probably a class that implements obstacle needs to be hand made


## User API
  * attributes
    * position
  * actions (methods)
    * movement
      possibilities
      * 1 joystick to location
      * 2 joysticks for rotation and translation
      * headset for rotation, joystick for translation
    * selecting
      * ray gun
      * grabbing

## Module interactions
   obstacle, maze, user
