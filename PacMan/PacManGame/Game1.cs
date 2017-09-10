using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using PacMan;
using System.IO;

namespace PacManGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GhostSprite ghostSprite;
        private MazeSprite mazeSprite;
        private ScoreSprite scoreSprite;
        private PacManSprite pacmanSprite;
        private GameState gameState;
        public GameState PacManGame { get { return this.gameState; } }

        private KeyboardState oldState;
        private int counter;
        private int threshold;

        public bool Paused { get; private set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 450;
            graphics.PreferredBackBufferWidth = 368;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            string level1 = File.ReadAllText("../../../../../PacMan/levelsPen.csv");
            this.gameState = GameState.Parse(level1);
            this.mazeSprite = new MazeSprite(this);
            this.ghostSprite = new GhostSprite(this);
            this.pacmanSprite = new PacManSprite(this);
            this.scoreSprite = new ScoreSprite(this);
            Components.Add(mazeSprite);
            Components.Add(ghostSprite);
            Components.Add(pacmanSprite);
            Components.Add(scoreSprite);
            base.Initialize();

            //events
            this.PacManGame.Maze.PacmanWon += Maze_PacmanWon;
            this.PacManGame.Score.GameOver += Score_GameOver;
        }

        /// <summary>
        /// Removes the ghostsprite and pacman sprite when 
        /// pacman loses 3 lives and the gameover event is triggered.
        /// </summary>
        private void Score_GameOver(string obj)
        {
            Components.Remove(ghostSprite);
            Components.Remove(pacmanSprite);
        }

        /// <summary>
        /// Removes the ghostsprite and pacman sprite when 
        /// pacman wins the game and the pacmanwon event is triggered.
        /// </summary>
        private void Maze_PacmanWon(ICollidable obj)
        {
            Components.Remove(ghostSprite);
            Components.Remove(pacmanSprite);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //backgroundMusic = Content.Load<SoundEffect>("pacman_beginning");
            //backgroundMusic.Play();
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() { }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
            {
                Pause();
            }

            if ((PacManGame.Score.Lives == -1 && newState.IsKeyDown(Keys.Enter)) ||
                PacManGame.Maze.gameWon() == true && newState.IsKeyDown(Keys.Enter))
            {
                Reset();
            }
            oldState = newState;
            if (!Paused)
            {
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Exits the game when called.
        /// </summary>
        private void Reset()
        {
            Exit();
        }

        /// <summary>
        /// Pauses the game
        /// </summary>
        private void Pause()
        {
            Paused = !Paused;
            PacManGame.Score.PauseGame();
        }
    }
}
