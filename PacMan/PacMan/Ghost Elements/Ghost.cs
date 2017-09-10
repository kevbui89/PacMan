using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PacMan
{
    public enum GhostName { Blinky, Speedy, Inky, Clyde }
    /// <summary>
    /// Authors : Danny Manzato-Tates, Jacob Riendeau, Kevin Bui
    /// </summary>
    public class Ghost : ICollidable, IMoveable
    {
        /// <summary>
        /// Variables for the Ghost class
        /// </summary>
        private Pacman pacman;//used to update the target
        private Vector2 target;//used in chasing mode
        private Pen pen;//tiles in which the ghosts are contained when they are penned 
        private Maze maze;
        private Direction direction;
        public static Vector2 ReleasePosition;//used for release
        private Vector2 position;//ghosts position
        private GhostName name;
        public Ghost GhostAssistant { get; set; }
        //pending type for GUI
        private Color colour;
        public Color Colour { get { return this.colour; } set { this.colour = value; } }
        public Vector2 PacmanPosition { get { return pacman.Position; } }
        private IGhostState currentState;

        //Events
        public event Action PacmanDied;
        public event Action<ICollidable> Collision;

        public Vector2 GhostTarget { get { return currentState.Target; } }
        //Properties
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public Direction Direction { get { return this.direction; } set { this.direction = value; } }
        public GhostState CurrentState { get; private set; }
        public int Points { get; set; }
        public Vector2 HomePosition { get; set; }
        public GhostName Name { get { return this.name; } }
        /// <summary>
        /// Constructor instantiates the gamestate, the location, the target, the ghoststate and color
        /// of the ghost.
        /// </summary>
        public Ghost(GameState g, int x, int y, GhostState start, Color colour, GhostName name)
        {
            this.colour = colour;
            this.pen = g.Pen;
            this.maze = g.Maze;
            this.pacman = g.Pacman;
            this.position = new Vector2(x, y);
            this.name = name;

            ChangeState(start);
        }

        /// <summary>
        /// Puts all the ghosts into Pen.
        /// </summary>
        public void Reset()
        {
            pen.AddToPen(this);
        }

        /// <summary>
        /// Changes the state of a ghost
        /// </summary>
        /// <param name="state">The state in which the ghost is changed to</param>
        public void ChangeState(GhostState state)
        {
            this.CurrentState = state;
            switch (state)
            {
                case GhostState.Scared:
                    currentState = new Scared(this, this.maze);
                    break;
                case GhostState.Chase:
                    switch (Name)
                    {
                        case GhostName.Blinky:
                            this.currentState = new Chase(this, maze, pacman);
                            break;
                        case GhostName.Speedy:
                            this.currentState = new Ambush(this, maze, pacman);
                            break;
                        case GhostName.Inky:
                            this.currentState = new Predict(this, GhostAssistant, maze, pacman);
                            break;
                        case GhostName.Clyde:
                            this.currentState = new Proximity(this, maze, pacman);
                            break;
                    }
                    break;
                case GhostState.Released:
                    position = ReleasePosition;
                    ChangeState(GhostState.Chase);
                    break;
                case GhostState.Zombie:
                    currentState = new Zombie(this, this.maze, pen.Entrance);
                    break;
            }
        }

        /// <summary>
        /// Method that moves the ghost towards pacman
        /// </summary>
        public void Move()
        {
            //Changes Target positions once it passes the previous target
            if (target == position)
            {
                target = pacman.Position;
            }
            //Use the state objec to decide how to move
            currentState.Move();
        }

        /// <summary>
        /// Check whether the collision with pacman was scared or chase
        /// </summary>
        public void Collide()
        {
            if (CurrentState == GhostState.Scared)
            {
                OnCollision(this);
                ChangeState(GhostState.Zombie);
            }
            else if (CurrentState == GhostState.Chase)
            {
                OnPacmanDied();
            }
        }

        /// <summary>
        /// Event handler for pacman's death
        /// </summary>
        protected void OnPacmanDied()
        { 
            PacmanDied?.Invoke();
        }

        /// <summary>
        /// Event handler for collision
        /// </summary>
        /// <param name="i">The ICollidable that collides with the ghost</param>
        protected void OnCollision(ICollidable i)
        {

            Collision?.Invoke(i);
        }
    }
}
