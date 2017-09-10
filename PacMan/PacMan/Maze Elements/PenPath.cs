using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PacMan
{
    /// <summary>
    /// Authors : Danny Manzato-Tates, Jacob Riendeau, Kevin Bui
    /// </summary>
    public class PenPath: Tile
    {
        /// <summary>
        /// Variable for contained member
        /// </summary>
        private ICollidable member;

        public override ICollidable Member //member which is ICollidable contained within the tile
        {
            get { return member; }
            set { member = value; }
        }

        /// <summary>
        /// Constructor instantiates the position in the base
        /// </summary>
        public PenPath(int x, int y) : base(x, y)
        {
            this.member = null;
        }

        /// <summary>
        /// The element defines whether anything can enter the tile.
        /// </summary>
        /// <returns>Returns true</returns>
        public override bool CanEnter()
        {
            return true;
        }

        /// <summary>
        /// Calls the member's collide method and deletes the meber afterwards
        /// </summary>
        public override void Collide() { }

        /// <summary>
        /// The element defines whether there is a member within it.
        /// </summary>
        /// <returns>boolean</returns>
        public override bool isEmpty()
        {
                return true;
        }
    }
}
