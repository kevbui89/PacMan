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
    class Predict : IGhostState
    {
        private Pacman pacman;
        private Ghost assistant;
        private Maze maze;
        private Vector2 target;
        private Ghost ghost;
        public Vector2 Target { get { return target; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public Predict(Ghost g, Ghost gg, Maze m, Pacman pacman)
        {
            this.target = pacman.Position;
            this.pacman = pacman;
            this.assistant = gg;
            this.maze = m;
            this.ghost = g;
        }

        /// <summary>
        /// Finds pacman in the maze
        /// </summary>
        private Vector2 findTarget()
        {
            Vector2 p = findPacman();
            Vector2 c = assistant.Position - p;
            c.X = MathHelper.Clamp(c.X * 2, 1, maze.Length - 2);
            c.Y = MathHelper.Clamp(c.Y * 2, 1, maze.Height - 2);
            Boolean goodpos = true;
            if (maze[(int)c.X, (int)c.Y] is Wall || maze[(int)c.X, (int)c.Y] is PenPath)
                goodpos = false;
            if (!goodpos)
                findAdjacentTarget();


            return p;
        }

        /// <summary>
        /// Finds the nearest path to ambush pacman
        /// </summary>
        public Vector2 findAdjacentTarget()
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

            return shortestDistance.Position;
        }

        /// <summary>
        /// Finds pacman
        /// </summary>
        private Vector2 findPacman()
        {
            int counter = 0;
            switch (pacman.PacmanDirection)
            {
                case Direction.Right:
                    while (counter < 2)
                    {
                        target = new Vector2(MathHelper.Clamp(pacman.Position.X + 4 - counter, 1, maze.Length - 2), pacman.Position.Y);
                        if (maze[(int)target.X, (int)target.Y] is Wall || maze[(int)target.X, (int)target.Y] is PenPath)
                        { counter++; }
                        else { return target; }
                    }
                    return pacman.Position;
                case Direction.Left:
                    while (counter < 2)
                    {
                        target = new Vector2(MathHelper.Clamp(pacman.Position.X - 4 + counter, 1, maze.Length - 2), pacman.Position.Y);
                        if (maze[(int)target.X, (int)target.Y] is Wall || maze[(int)target.X, (int)target.Y] is PenPath) { counter++; }
                        else { return target; }
                    }
                    return pacman.Position;
                case Direction.Up:
                    while (counter < 2)
                    {
                        target = new Vector2(pacman.Position.Y, MathHelper.Clamp(pacman.Position.Y - 4 + counter, 1, maze.Height - 2));
                        if (maze[(int)target.X, (int)target.Y] is Wall || maze[(int)target.X, (int)target.Y] is PenPath) { counter++; }
                        else { return target; }
                    }
                    return pacman.Position;
                case Direction.Down:
                    while (counter < 2)
                    {
                        target = new Vector2(pacman.Position.Y, MathHelper.Clamp(pacman.Position.Y + 4 - counter, 1, maze.Height - 2));
                        if (maze[(int)target.X, (int)target.Y] is Wall || maze[(int)target.X, (int)target.Y] is PenPath) { counter++; }
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

