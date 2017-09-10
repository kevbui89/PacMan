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
    public class Pellet : ICollidable
    {
        
        private int points;//represents the value of the pellet

        /// <summary>
        /// Indexer for points
        /// </summary>
        public int Points { get { return points; } set { this.points = value; } }

        public event Action<ICollidable> Collision;

        /// <summary>
        /// Constructor instantiates the points
        /// </summary>
        public Pellet(int point)
        {
            this.points = point;
        }

        /// <summary>
        /// Triggers the Collision event
        /// </summary>
        public void Collide()
        {
            onCollision(this);
        }

        /// <summary>
        /// Event handler for collision
        /// </summary>
        protected void onCollision(ICollidable ic)
        {
            Collision?.Invoke(this);
        }
    }
}
