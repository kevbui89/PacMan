using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    /// <summary>
    /// Authors : Danny Manzato-Tates, Jacob Riendeau, Kevin Bui
    /// </summary>
    public enum Items { Empty, Pellet, Energizer}
    public class Pacman
    {
        /// <summary>
        /// Variables and indexer
        /// </summary>
        private GameState controller;
        private Maze maze;
        public Vector2 Position { get { return pos; } set { pos = value; } }
        private Vector2 pos;
        public Direction PacmanDirection { get; set; }
        public Vector2 initPosition;

        /// <summary>
        /// Constructor that instantiates the controller and maze
        /// </summary>
        /// <param name="controller">The controller to be instantianted</param>
        public Pacman(GameState controller)
        {
            this.controller = controller;
            this.maze = this.controller.Maze;
            this.PacmanDirection = Direction.Right;
            //added event handlers to reset pacman position
            this.controller.Score.GameOver += (x) => pos = initPosition;
            this.controller.Maze.PacmanWon += (x) => pos = initPosition;
            
        }

        public void SubToGhosts()
        {
            foreach (var g in this.controller.Ghostpack)
            {
                g.PacmanDied += () => { pos = initPosition; };
            }   
        }


        /// <summary>
        /// Checks if pacman can move in a given direction and checks for
        /// collisions
        /// </summary>
        /// <param name="dir">The direction to move in</param>
        public void Move (Direction dir)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;
            PacmanDirection = dir;
            switch (dir)
            {
                case Direction.Up:
                    if(maze[x,y- 1].CanEnter())
                    {
                        pos = new Vector2(Position.X, Position.Y-1);
                        CheckCollisions();
                    }
                    break;
                case Direction.Down:
                    if (maze[x, y + 1].CanEnter())
                    {
                        pos = new Vector2(Position.X, Position.Y + 1);
                        CheckCollisions();
                    }
                    break;
                case Direction.Left:
                    if (maze[x - 1, y].CanEnter())
                    {
                        pos = new Vector2(Position.X - 1, Position.Y);
                        CheckCollisions();
                    }
                    break;
                case Direction.Right:
                    if (maze[x + 1, y].CanEnter())
                    {
                        pos = new Vector2(Position.X + 1, Position.Y);
                        CheckCollisions();
                    }
                    break;
            }
        }
        
        /// <summary>
        /// Checks for collisions
        /// </summary>
        public void CheckCollisions()
        {

            controller.Ghostpack.CheckCollideGhosts(Position);

            maze[(int)pos.X, (int)pos.Y].Collide(); 
        }

        
        
    }
}
