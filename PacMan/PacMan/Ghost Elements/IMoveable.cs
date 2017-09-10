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
    interface IMoveable
    {
        
        Direction Direction { get; set; }//defines in which direction the implemented object is going

        Vector2 Position { get; set; }//the position of the implemented object can be update

        /// <summary>
        /// Move should update the position according to specific criterea
        /// </summary>
        void Move();
    }
}
