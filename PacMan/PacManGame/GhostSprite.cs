using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using PacMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    /// <summary>
    /// Authors : Jacob Riendeau, Kevin Bui
    /// </summary>
    class GhostSprite : DrawableGameComponent
    {
        //Game Elements
        private SpriteBatch spriteBatch;
        private Texture2D ghostImage;
        private Texture2D eyeImage;
        private Game1 game;
        private GhostPack ghostPack;
        private SoundEffect scared;
        private Boolean isScared;
        private SoundEffectInstance scaredInstance;
        //Movement Counters
        private int threshold;
        private int counter;
        private int originalthreshold;
        private int scaredthreshold;

        private int spriteSize = 16;

        /// <summary>
        /// Constructor of ghostSprite
        /// </summary>
        public GhostSprite(Game1 game) : base(game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        public override void Initialize()
        {
            counter = 0;
            originalthreshold = 8;
            threshold = 8;
            scaredthreshold = 15;
            isScared = false;
            base.Initialize();
        }

        /// <summary>
        /// Loads the content of the game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ghostImage = game.Content.Load<Texture2D>("ghost");
            eyeImage = game.Content.Load<Texture2D>("ghosteye");
            scared = game.Content.Load<SoundEffect>("ghostscared");
            scaredInstance = scared.CreateInstance();
            this.ghostPack = game.PacManGame.Ghostpack;
            ghostPack.Fear += Scared;
            ghostPack.FearOff += UnScared;
            game.PacManGame.Score.Pause += Scared;
            base.LoadContent();
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            if (counter > threshold)
            {
                ghostPack.Move();
                counter = 0;
            }
            else { counter++; }

            base.Update(gameTime);
        }

        /// <summary>
        /// Plays the scared sound effect and slows the ghostpack speed
        /// </summary>
        /// <param name="c">The collidable</param>
        public void Scared()
        {
            threshold = scaredthreshold;
            isScared = true;
            scaredInstance.Stop();
            scaredInstance = scared.CreateInstance();
            scaredInstance.Play();

        }

        public void Scared(Boolean b)
        {
            if (b && isScared)
            {
                scaredInstance.Pause();
            }
            else if (!b && isScared)
            {
                scaredInstance.Resume();
            }
        }

        /// <summary>
        /// Puts the ghost speed back to normal
        /// </summary>
        public void UnScared()
        {
            threshold = originalthreshold;
            isScared = false;
        }

        /// <summary>
        /// Draws the spritebatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Ghost g in ghostPack)
            {
                if (g.CurrentState == GhostState.Scared)
                {
                    spriteBatch.Draw(ghostImage, new Rectangle((int)g.Position.X * spriteSize, (int)g.Position.Y * spriteSize, spriteSize, spriteSize), Color.Blue);
                }
                else if (g.CurrentState == GhostState.Zombie)
                {
                    spriteBatch.Draw(ghostImage, new Rectangle((int)g.Position.X * spriteSize, (int)g.Position.Y * spriteSize, spriteSize, spriteSize), Color.Black);
                }
                else
                {
                    spriteBatch.Draw(ghostImage, new Rectangle((int)g.Position.X * spriteSize, (int)g.Position.Y * spriteSize, spriteSize, spriteSize), g.Colour);
                }

                spriteBatch.Draw(eyeImage, new Rectangle((int)g.Position.X * spriteSize, (int)g.Position.Y * spriteSize, spriteSize, spriteSize), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
