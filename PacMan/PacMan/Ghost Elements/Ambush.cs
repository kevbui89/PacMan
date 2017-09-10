using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    /// <summary>
    /// Authors : Jacob Riendeau, Kevin Bui
    /// </summary>
    class Ambush : IGhostState
    {
        /// <summary>
        /// Variables used in the for the ghost to be able to chase
        /// </summary>
        private Ghost ghost;//used to change position
        private Maze maze;//used to acces available neighbours
        private Vector2 target;//used to chase that target 
        private Pacman pacman;
        //private Pacman pacman; Not used.
        public Vector2 Target { get { return target; } }
        /// <summary>
        /// Constructor instantiates the ghost, maze, target and pacman.
        /// </summary>
        public Ambush(Ghost ghost, Maze maze, Pacman pacman)
        {
            this.ghost = ghost;
            this.maze = maze;
            this.pacman = pacman;
            this.target = pacman.Position;
        }
        private Vector2 findTarget()
        {
            int counter = 0;
            switch (pacman.PacmanDirection)
            {
                case Direction.Right:
                    while (counter < 4)
                    {
                        target = new Vector2(MathHelper.Clamp(pacman.Position.X + 4 - counter, 1, maze.Length - 2), pacman.Position.Y);
                        if (maze[(int)target.X , (int)target.Y ] is Wall || maze[(int)target.X , (int)target.Y ] is PenPath)
                        { counter++; }
                        else { return target; }
                    }
                    return pacman.Position;
                case Direction.Left:
                    while (counter < 4)
                    {
                        target = new Vector2(MathHelper.Clamp(pacman.Position.X - 4 + counter, 1, maze.Length - 2), pacman.Position.Y);
                        if (maze[(int)target.X, (int)target.Y] is Wall || maze[(int)target.X, (int)target.Y] is PenPath) { counter++; }
                        else { return target; }
                    }
                    return pacman.Position;
                case Direction.Up:
                    while (counter < 4)
                    {
                        target = new Vector2(pacman.Position.Y, MathHelper.Clamp(pacman.Position.Y - 4 + counter, 1, maze.Height - 2));
                        if (maze[(int)target.X , (int)target.Y] is Wall || maze[(int)target.X, (int)target.Y] is PenPath) { counter++; }
                        else { return target; }
                    }
                    return pacman.Position;
                case Direction.Down:
                    while (counter < 4)
                    {
                        target = new Vector2(pacman.Position.Y, MathHelper.Clamp(pacman.Position.Y + 4 - counter, 1, maze.Height - 2));
                        if (maze[(int)target.X , (int)target.Y ] is Wall || maze[(int)target.X, (int)target.Y] is PenPath) { counter++; }
                        else { return target; }
                    }
                    return pacman.Position;
                default: return pacman.Position;
            }
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

            if (ghost.Position == target)
            {
                target = findTarget();
            }

        }

    }
}
