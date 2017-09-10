
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    /// <summary>
    /// This interface defines Collidable behavior
    /// </summary>
    public interface ICollidable
    {

        /// <summary>
        /// Event for collision
        /// </summary>
        event Action<ICollidable> Collision;

        int Points { get; set; }//worth of the element in points

        /// <summary>
        /// Triggers the Collision event
        /// </summary>
        void Collide();
    }
}
