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
    class Scatter : IGhostState
    {
        /// <summary>
        /// Variables used in the for the ghost to be able to chase
        /// </summary>
        private Ghost ghost;//used to change position
        private Maze maze;//used to acces available neighbours
        private Vector2 target;//home of the ghost 
        public Vector2 Target { get { return target; } }

        /// <summary>
        /// Constructor instantiates the ghost, maze, target and pacman.
        /// </summary>
        public Scatter(Ghost ghost, Maze maze, Vector2 target)
        {
            this.ghost = ghost;
            this.maze = maze;
            this.target = target;
        }

        /// <summary>
        /// Calculates the shortest distance for the ghosts to catch pacman
        /// </summary>
        public void Move()
        {
            Tile current = maze[(int)ghost.Position.X, (int)ghost.Position.Y];
            List<Tile> places = maze.GetAvailableNeighbours(ghost.Position, ghost.Direction);
            int num = places.Count;
            if (num == 0)
                throw new Exception("Nowhere to go");

            Tile shortestDistance = places[0];
            for (int i = 1; i < places.Count(); i++)
            {
                if (places[i].GetDistance(target) < shortestDistance.GetDistance(target))
                {
                    shortestDistance = places[i];
                }
            }

            if (shortestDistance.Position.X == ghost.Position.X + 1)
                ghost.Direction = Direction.Right;
            else if (shortestDistance.Position.X == ghost.Position.X - 1)
                ghost.Direction = Direction.Left;
            else if (shortestDistance.Position.Y == ghost.Position.Y - 1)
                ghost.Direction = Direction.Up;
            else
                ghost.Direction = Direction.Down;

            ghost.Position = shortestDistance.Position;
        }
    }
}
