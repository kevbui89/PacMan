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
using System.Timers;

namespace PacManGame
{
    /// <summary>
    /// Authors : Jacob Riendeau, Kevin Bui
    /// </summary>
    class PacManSprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D pacmanHorizontal;
        private Texture2D pacmanVertical;

        private Game1 game;
        private Pacman pacman;

        private KeyboardState oldState;

        //threshold counter
        private int counter;
        private int threshold;

        //move counters
        private int movecounter;
        private int movethreshold;

        //animation counters
        private int frame;
        private int animationcounter;

        //sounds
        private SoundEffect eatEnergizer;
        private SoundEffect eatGhost;
        private SoundEffect eatPellet;
        private SoundEffect death;

        //size of the sprites
        private int spriteSize = 16;

        //test
        private int counterSound = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The game</param>
        public PacManSprite(Game1 game) : base(game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        public override void Initialize()
        {
            oldState = Keyboard.GetState();
            threshold = 10;
            movecounter = 0;
            movethreshold = 8;
            frame = 0;
            base.Initialize();
        }

        /// <summary>
        /// Loads the content of the game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pacmanHorizontal = game.Content.Load<Texture2D>("pacman");
            pacmanVertical = game.Content.Load<Texture2D>("pacman2");

            //sound
            eatEnergizer = game.Content.Load<SoundEffect>("pacmaneatfruit");
            eatGhost = game.Content.Load<SoundEffect>("pacmaneatghost");
            eatPellet = game.Content.Load<SoundEffect>("pacmanchomp");

            this.pacman = game.PacManGame.Pacman;
            game.PacManGame.Score.Eats += eat;
            base.LoadContent();
        }

        /// <summary>
        /// Plays a sound depending on what is eaten
        /// </summary>
        /// <param name="c">The Icollidable object hit</param>
        public void eat(ICollidable c)
        {
            counterSound++;
            if (c is Energizer)
            {
                eatEnergizer.Play();
            }
            else if (c is Ghost)
            {
                eatGhost.Play();
                
            }
            //counterSound set to 4 because the sound is about 1 second
            //and pacman eats about 4 pellets in 1 second
            else if (c is Pellet && counterSound >= 4)
            {
                eatPellet.Play();
                counterSound = 0;
            }

        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            //If statements to move pacman
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Right))
            {

                if (!oldState.IsKeyDown(Keys.Right))
                {
                    pacman.PacmanDirection = Direction.Right;
                    counter = 0;

                }
                else
                {
                    counter++;
                    if (counter > threshold)
                        pacman.PacmanDirection = Direction.Right;
                }
            }
            //Left
            if (newState.IsKeyDown(Keys.Left))
            {
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    pacman.PacmanDirection = Direction.Left;
                    counter = 0;
                }
                else
                {
                    counter++;
                    if (counter > threshold)
                        pacman.PacmanDirection = Direction.Left;
                }
            }
            //Up
            if (newState.IsKeyDown(Keys.Up))
            {
                if (!oldState.IsKeyDown(Keys.Up))
                {
                    pacman.PacmanDirection = Direction.Up;
                    counter = 0;
                }
                else
                {
                    counter++;
                    if (counter > threshold)
                        pacman.PacmanDirection = Direction.Up;
                }
            }
            //Down
            if (newState.IsKeyDown(Keys.Down))
            {
                if (!oldState.IsKeyDown(Keys.Down))
                {
                    pacman.PacmanDirection = Direction.Down;
                    counter = 0;
                }
                else
                {
                    counter++;
                    if (counter > threshold)
                        pacman.PacmanDirection = Direction.Down;
                }
            }
            oldState = newState;
            if (movecounter > movethreshold)
            {
                pacman.Move(pacman.PacmanDirection);
                movecounter = 0;
            }
            else { movecounter++; }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the spritebatches
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (pacman.PacmanDirection) {
                case Direction.Right:
                     spriteBatch.Draw(pacmanHorizontal,
                       new Rectangle((int)pacman.Position.X * spriteSize, (int)pacman.Position.Y * spriteSize, spriteSize, spriteSize),
                     new Rectangle(0, spriteSize * frame, spriteSize, spriteSize), Color.White);
               
                    break;
                case Direction.Left:
                    spriteBatch.Draw(pacmanHorizontal,
                        new Rectangle((int)pacman.Position.X * spriteSize, (int)pacman.Position.Y * spriteSize, spriteSize, spriteSize),
                        new Rectangle(0, spriteSize * frame, spriteSize, spriteSize), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                    break;
                case Direction.Up:
                    spriteBatch.Draw(pacmanVertical,
                        new Rectangle((int)pacman.Position.X * spriteSize, (int)pacman.Position.Y * spriteSize, spriteSize, spriteSize),
                        new Rectangle(spriteSize * frame, 0, spriteSize, spriteSize), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                    break;
                case Direction.Down:
                    spriteBatch.Draw(pacmanVertical,
                        new Rectangle((int)pacman.Position.X * spriteSize, (int)pacman.Position.Y * spriteSize, spriteSize, spriteSize),
                        new Rectangle(spriteSize * frame, 0, spriteSize, spriteSize), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                    break;
            }

            //animates pacman
            if (animationcounter > 5)
            {
                frame++;
                animationcounter = 0;
            }
            else if (!game.Paused)
                animationcounter++;

            if (frame > 2)
                frame = 0;

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
