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
    public class ScoreAndLives
    {
        /// <summary>
        /// Property
        /// </summary>
        public int Lives { get; set; }
        public int Score { get; set; }

        public event Action<string> GameOver;
        public event Action<ICollidable> Eats;
        public event Action<Boolean> Pause;
        private Boolean paused = false;
        /// <summary>
        /// Constructor thats takes in the gamestate but does nothing with it
        /// Implementation of subscribing to events have changed to the gamestate
        /// </summary>
        [Obsolete]
        public ScoreAndLives(GameState game)
        {

        }

        /// <summary>
        /// When pacman has no more lives, triggers the event Over
        /// </summary>
        public void DeadPacman()
        {
            this.Lives--;
            if (this.Lives == 0)
            {
                onOver("dead");
            }
        }

        /// <summary>
        /// Event handler when Pacman dies
        /// </summary>
        protected void onOver(string state)
        {
            Console.WriteLine(state);
            GameOver?.Invoke(state);
        }

        /// <summary>
        /// Increments the score everytime Pacman hits a dot, Energizer or scares ghosts
        /// </summary>
        /// <param name="collidable">The ICollidable that got hit</param>
        public void IncrementScore(ICollidable collidable)
        {
            if (collidable != null)
            {
                if ((collidable is Ghost))
                {
                    //If Ghost is scared sends him to the pen.
                    if (((Ghost)collidable).CurrentState == GhostState.Scared)
                    {
                        this.Score += collidable.Points;
                        ((Ghost)collidable).ChangeState(GhostState.Zombie);
                        Eats?.Invoke(collidable);
                    }
                }
                else
                {
                    this.Score += collidable.Points;
                    Eats?.Invoke(collidable);
                }
            }
        }

        /// <summary>
        /// Calls the event to pause the game
        /// </summary>
        public void PauseGame()
        {
            paused = !paused;
            Pause?.Invoke(paused);
            
        }
    }
}
