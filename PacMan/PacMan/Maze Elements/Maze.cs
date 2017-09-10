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
    public class Maze
    {
        /// <summary>
        /// Variable and event
        /// </summary>
        private Tile[,] maze;
        public event Action<ICollidable> PacmanWon;

        //property Length of the Tile[,]
        public int Length { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// No parameter constructor
        /// </summary>
        public Maze() { }

        /// <summary>
        /// Sets the maze and size
        /// </summary>
        /// <param name="maze">The maze array</param>
        public void SetTiles(Tile[,] maze)
        {
            this.maze = maze;
            this.Length = maze.GetLength(0);
            this.Height = maze.GetLength(1);
        }

        /// <summary>
        /// Indexer with a getter and setter
        /// </summary>
        /// <param name="x">The X coordinate on the maze</param>
        /// <param name="y">The Y coordinate on the maze</param>
        public Tile this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Length || y < 0 || y >= Height)
                    throw new ArgumentOutOfRangeException("The indeces are out of range (" + x + ", " + y + ")");
                return maze[x, y];
            }
            set
            {
                maze[x, y] = value;
            }
        }

        /// <summary>
        /// Method that gets all available neighouring Tiles of the current position if it can enter
        /// and adds it into a List
        /// </summary>
        /// <param name="position">The current vector</param>
        /// <param name="dir">The current direction</param>
        public List<Tile> GetAvailableNeighbours(Vector2 position, Direction dir)
        {
            List<Tile> t = new List<Tile>(); 
            switch (dir)
            {
                case Direction.Left:
                    if (maze[(int)position.X - 1, (int)position.Y].CanEnter())
                        t.Add(maze[(int)position.X - 1, (int)position.Y]);
                    if (maze[(int)position.X, (int)position.Y - 1].CanEnter())
                        t.Add(maze[(int)position.X, (int)position.Y - 1]);
                    if (maze[(int)position.X, (int)position.Y + 1].CanEnter())
                        t.Add(maze[(int)position.X, (int)position.Y + 1]);
                    break;
                case Direction.Right:
                    if (maze[(int)position.X + 1, (int)position.Y].CanEnter())
                        t.Add(maze[(int)position.X + 1, (int)position.Y]);
                    if (maze[(int)position.X, (int)position.Y - 1].CanEnter())
                        t.Add(maze[(int)position.X, (int)position.Y - 1]);
                    if (maze[(int)position.X, (int)position.Y + 1].CanEnter())
                        t.Add(maze[(int)position.X, (int)position.Y + 1]);
                    break;
                case Direction.Up:
                    if (maze[(int)position.X + 1, (int)position.Y].CanEnter())
                        t.Add(maze[(int)position.X + 1, (int)position.Y]);
                    if (maze[(int)position.X, (int)position.Y - 1].CanEnter())
                        t.Add(maze[(int)position.X, (int)position.Y - 1]);
                    if (maze[(int)position.X - 1, (int)position.Y].CanEnter())
                        t.Add(maze[(int)position.X - 1, (int)position.Y]);
                    break;
                case Direction.Down:
                    if (maze[(int)position.X + 1, (int)position.Y].CanEnter())
                        t.Add(maze[(int)position.X + 1, (int)position.Y]);
                    if (maze[(int)position.X, (int)position.Y + 1].CanEnter())
                        t.Add(maze[(int)position.X, (int)position.Y + 1]);
                    if (maze[(int)position.X - 1, (int)position.Y].CanEnter())
                        t.Add(maze[(int)position.X - 1, (int)position.Y]);
                    break; 
            }
            return t;
        }

        /// <summary>
        /// Checks if all the tiles are empty, if it is the case, calls 
        /// the event PacmanWon
        /// </summary>
        public void CheckMembersLeft()
        {
            if (areMembersNull())
            {
                OnPacmanWon();
            }
        }

        /// <summary>
        /// Checks if all the tiles are empty.
        /// </summary>
        /// <returns>A boolean value whether the members are null or not</returns>
        public bool gameWon()
        {
            if (areMembersNull())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Event raise when Pacman wins
        /// </summary>
        protected void OnPacmanWon()
        {
            //temporary for this phase
            PacmanWon?.Invoke(null);
            Console.WriteLine("Won");
        }

        /// <summary>
        /// Helper method that checks if all the Tiles are empty
        /// </summary>
        private bool areMembersNull()
        {
            foreach (Tile t in maze)
            {
                if (t is Path)
                {
                    if (!t.isEmpty())
                    {
                        Console.WriteLine(t.Position + " is not null" );
                        return false;
                    }
                }
            }
            return true;
        }  
    }
}
