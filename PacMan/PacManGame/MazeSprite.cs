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
    class MazeSprite : DrawableGameComponent
    {
        private Game1 game;
        private SpriteBatch spriteBatch;
        private Texture2D empty;

        //maze walls
        private Texture2D wall;
        private Texture2D vertical;
        private Texture2D closedhorizontal;
        private Texture2D connectorhorizontal;
        private Texture2D closedvertical;
        private Texture2D connectorvertical;
        private Texture2D cornerbot;
        private Texture2D cornertop;
        private Texture2D emptywall;

        //test
        private Texture2D energizerAnimation;

        //maze items
        private Texture2D pellet;
        private Texture2D energizer;

        private Maze maze;
        private int frame;
        private int animationcounter;

        private int spriteSize = 16;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The game</param>
        public MazeSprite(Game1 game) : base(game)
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

            //maze walls
            empty = game.Content.Load<Texture2D>("empty");
            wall = game.Content.Load<Texture2D>("wall");
            vertical = game.Content.Load<Texture2D>("wallvertical");
            closedhorizontal = game.Content.Load<Texture2D>("closedhorizontal");
            connectorhorizontal = game.Content.Load<Texture2D>("connectorhorizontal");
            closedvertical = game.Content.Load<Texture2D>("closedvertical");
            connectorvertical = game.Content.Load<Texture2D>("connectorvertical");
            cornerbot = game.Content.Load<Texture2D>("cornerbot");
            cornertop = game.Content.Load<Texture2D>("cornertop");
            emptywall = game.Content.Load<Texture2D>("emptywall");
            energizerAnimation = game.Content.Load<Texture2D>("energizeranimation");

            //game items
            pellet = game.Content.Load<Texture2D>("pellet");
            energizer = game.Content.Load<Texture2D>("energizer");

            this.maze = game.PacManGame.Maze;
            base.LoadContent();
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            game.PacManGame.Maze.CheckMembersLeft();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the sprites
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //for loop that draws all the maze and items inside it
            for (int i = 0; i < maze.Height; i++)
            {

                for (int j = 0; j < maze.Length; j++)
                {
                    if (maze[i, j] is Wall)
                    {
                        switch (((Wall)maze[i, j]).Type)
                        {
                            case WallType.Horizontal:
                                spriteBatch.Draw(wall,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.Vertical:
                                spriteBatch.Draw(vertical,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.ClosedR:
                                spriteBatch.Draw(closedhorizontal,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.ClosedL:
                                spriteBatch.Draw(closedhorizontal,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), null, Color.White, 0,
                                    new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                                break;
                            case WallType.ConnectorR:
                                spriteBatch.Draw(connectorhorizontal,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.ConnectorL:
                                spriteBatch.Draw(connectorhorizontal,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), null, Color.White, 0,
                                    new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                                break;
                            case WallType.ClosedD:
                                spriteBatch.Draw(closedvertical,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.ClosedU:
                                spriteBatch.Draw(closedvertical,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), null, Color.White, 0,
                                    new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                                break;
                            case WallType.ConnectorD:
                                spriteBatch.Draw(connectorvertical,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.ConnectorU:
                                spriteBatch.Draw(connectorvertical,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), null, Color.White, 0,
                                    new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                                break;
                            case WallType.CornerUR:
                                spriteBatch.Draw(cornertop,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.CornerUL:
                                spriteBatch.Draw(cornertop,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), null, Color.White, 0,
                                    new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                                break;
                            case WallType.CornerDR:
                                spriteBatch.Draw(cornerbot,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;
                            case WallType.CornerDL:
                                spriteBatch.Draw(cornerbot,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), null, Color.White, 0,
                                    new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                                break;
                            case WallType.EmtpyWall:
                                spriteBatch.Draw(emptywall,
                                    new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                                break;

                        }

                    }

                    else if (maze[i, j] is Tile)
                    {
                        if (maze[i, j].Member is Pellet)
                            spriteBatch.Draw(pellet, new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                        else if (maze[i, j].Member is Energizer)
                        {
                            if (frame == 0)
                            {
                                spriteBatch.Draw(energizer, new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);
                            }
                            else if (frame == 1)
                            {
                                spriteBatch.Draw(energizerAnimation, new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize),
                                    new Rectangle(i * 32, (j * 32) * frame, spriteSize, spriteSize), Color.White);
                            }
                        }

                        else
                            spriteBatch.Draw(empty, new Rectangle(i * spriteSize, j * spriteSize, spriteSize, spriteSize), Color.White);

                    }
                }
            }

            if (animationcounter > 10)
            {
                frame++;
                animationcounter = 0;
            }
            else if (!game.Paused)
                animationcounter++;

            if (frame > 1)
                frame = 0;

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
