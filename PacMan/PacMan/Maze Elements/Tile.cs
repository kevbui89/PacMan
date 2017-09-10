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
    /// The Tile represents 1 element in the maze which defines its behaviour and position.
    /// </summary>
    public abstract class Tile
    {
        public Vector2 Position { get; set; }//position of the Tile according to other tiles in a grid
        public abstract ICollidable Member { get; set; } //member which is ICollidable contained within the tile

        /// <summary>
        /// Constructor instantiates the position.
        /// </summary>
        public Tile(int x, int y)
        {
            Position = new Vector2(x, y);
        }

        /// <summary>
        /// The element defines whether anything can enter the tile.
        /// </summary>
        /// <returns>boolean</returns>
        public abstract bool CanEnter();

        /// <summary>
        /// Calls the member's collide method and deletes the meber afterwards
        /// </summary>
        public abstract void Collide();

        /// <summary>
        /// The element defines whether there is a member within it.
        /// </summary>
        /// <returns>boolean</returns>
        public abstract bool isEmpty();

        /// <summary>
        /// Gets the distance to the goal
        /// </summary>
        /// <returns>float distance between 2 vectors</returns>
        public float GetDistance(Vector2 goal)
        {
            if (goal == null)
                throw new ArgumentNullException();
            return Vector2.Distance(this.Position, goal);
        }
    }
}
