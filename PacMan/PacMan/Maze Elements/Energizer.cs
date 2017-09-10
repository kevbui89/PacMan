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
    public class Energizer : ICollidable
    {
        private int points;
        public int Points { get { return points; } set { this.points = value; } }//value of the energizer for the score
        GhostPack ghosts;//used to trigger their scared mode when event is raise.

        public event Action<ICollidable> Collision;//raised when collision happens with pacman

        /// <summary>
        /// Constructor instantiates the ghosts, the points 
        /// </summary>
        public Energizer(GhostPack ghosts, int point)
        {
            this.ghosts = ghosts;
            this.points = point;
        }

        /// <summary>
        /// Triggers the Collision event and scares the ghost
        /// </summary>
        public void Collide()
        {
            onCollision(this);
            this.ghosts.ScareGhosts();
        }

        /// <summary>
        /// Event raise for collision
        /// </summary>
        protected void onCollision(ICollidable ic)
        {
            Collision?.Invoke(this);
        } 
    }
}
