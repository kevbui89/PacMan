using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMan;

namespace PacManGame
{
    /// <summary>
    /// Authors : Jacob Riendeau, Kevin Bui
    /// </summary>
    class ScoreSprite : DrawableGameComponent
    {
        private Game1 game;
        private ScoreAndLives sl;
        //Used for showing the score when you eat a ghost
        private List<Ghost> eatenGhost;

        private SpriteBatch spriteBatch;
        private SpriteFont font;

        private Texture2D lives;
        private Texture2D livesTitle;
        private Texture2D score;
        private Texture2D gameOver;
        private Texture2D victory;

        //sounds
        private SoundEffect death;
        private SoundEffect beginning;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The game</param>
        public ScoreSprite(Game1 game) : base(game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        public override void Initialize()
        {
            
            base.Initialize();
        }

        /// <summary>
        /// Loads the content of the game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sl = game.PacManGame.Score;
            eatenGhost = new List<Ghost>();

            font = game.Content.Load<SpriteFont>("score");
            lives = game.Content.Load<Texture2D>("livesimage");
            score = game.Content.Load<Texture2D>("scoretitle");

            //game state
            gameOver = game.Content.Load<Texture2D>("gameover");
            victory = game.Content.Load<Texture2D>("victory");
            livesTitle = game.Content.Load<Texture2D>("lives");

            //sounds
            death = game.Content.Load<SoundEffect>("pacmandeath");
            beginning = game.Content.Load<SoundEffect>("pacmanbeginning");

            beginning.Play();

            
            base.LoadContent();
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            if (sl.Lives == 0)
            {
                death.Play();
                sl.Lives = -1;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the spritebatches
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(score, new Vector2(0, 380), Color.White);
            spriteBatch.DrawString(font, "" + sl.Score, new Vector2(10, 420), Color.White);

            spriteBatch.Draw(livesTitle, new Vector2(270, 380), Color.White);
            if (sl.Lives == 3)
            {
                spriteBatch.Draw(lives, new Rectangle(275, 420, 20, 20), Color.White);
                spriteBatch.Draw(lives, new Rectangle(300, 420, 20, 20), Color.White);
                spriteBatch.Draw(lives, new Rectangle(325, 420, 20, 20), Color.White);
            }
            else if (sl.Lives == 2)
            {
                spriteBatch.Draw(lives, new Rectangle(275, 420, 20, 20), Color.White);
                spriteBatch.Draw(lives, new Rectangle(300, 420, 20, 20), Color.White);
            }
            else if (sl.Lives == 1)
            {
                spriteBatch.Draw(lives, new Rectangle(275, 420, 20, 20), Color.White);
            }

            if (sl.Lives == -1)
            {
                spriteBatch.Draw(gameOver, new Rectangle(95, 100, 180, 83), Color.White);
            }

            if (game.PacManGame.Maze.gameWon() == true && sl.Lives > 0)
            {
                spriteBatch.Draw(victory, new Rectangle(95, 100, 180, 135), Color.White);
            }

            if (game.Paused)
            {
                spriteBatch.DrawString(font, "Paused", new Vector2(130, 160), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}