using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    /// <summary>
    /// Behavior change according to the state
    /// </summary>
    
    
    public interface IGhostState
    {
        /// <summary>
        /// The method Move updates the Ghosts position according to a specific behavior
        /// </summary>
        void Move();
        Vector2 Target { get;}
    }
}
