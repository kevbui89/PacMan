using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PacMan
{
    /// <summary>
    /// Authors : Danny Manzato-Tates, Jacob Riendeau, Kevin Bui
    /// </summary>
    public enum Direction { Left, Right, Up, Down }//Represent direction in which entity is moving

    public enum GhostState { Scared, Chase, Released, Zombie, Scatter }//Represents the state of the ghost

    /// <summary>
    /// The GameState represents all the business classes
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Variables contain the state of the game.
        /// </summary>
        public Pacman Pacman { get { return this.pacman; } }
        private Pacman pacman;

        public GhostPack Ghostpack { get { return this.ghostpack; } }
        private GhostPack ghostpack;

        public Maze Maze { get { return this.maze; } }
        private Maze maze;

        public Pen Pen { get { return this.pen; } }
        private Pen pen;

        public ScoreAndLives Score { get { return this.score; } }
        private ScoreAndLives score;


        /// <summary>
        /// Static method that returns a mapped gamestate instance by reading a specified file
        /// </summary>
        /// <param name="string">File path of the game file</param>
        /// <returns>GameState instance</returns>
        public static GameState Parse(string fileContent)
        {
            GameState g = new GameState();
            Maze maze = new Maze();
            g.maze = maze;
            Pen pen = new Pen();
            g.pen = pen;
            GhostPack gp = new GhostPack();
            g.ghostpack = gp;
            g.score = new ScoreAndLives(g);
            g.score.Lives = 3;
            Pacman pac = new Pacman(g);
            g.pacman = pac;

            g.Pacman.Position = new Vector2(11, 17);
            Ghost assistant = null;
            Ghost gh;
            string[][] parse = getElements(fileContent);

            Tile[,] array = new Tile[parse[0].Length, parse.Length];

            for (int y = 0; y < parse.Length; y++)
            {
                for (int x = 0; x < parse[0].Length; x++)
                {
                    switch (parse[y][x])
                    {
                        case "ew":
                            array[x, y] = new Wall(x, y, WallType.EmtpyWall);
                            break;
                        case "w":
                            array[x, y] = new Wall(x, y, WallType.Horizontal);
                            break;
                        case "ww":
                            array[x, y] = new Wall(x, y, WallType.Vertical);
                            break;
                        case "cur":
                            array[x, y] = new Wall(x, y, WallType.CornerUR);
                            break;
                        case "cul":
                            array[x, y] = new Wall(x, y, WallType.CornerUL);
                            break;
                        case "cdr":
                            array[x, y] = new Wall(x, y, WallType.CornerDR);
                            break;
                        case "cdl":
                            array[x, y] = new Wall(x, y, WallType.CornerDL);
                            break;
                        case "cr":
                            array[x, y] = new Wall(x, y, WallType.ClosedR);
                            break;
                        case "cl":
                            array[x, y] = new Wall(x, y, WallType.ClosedL);
                            break;
                        case "cu":
                            array[x, y] = new Wall(x, y, WallType.ClosedU);
                            break;
                        case "cd":
                            array[x, y] = new Wall(x, y, WallType.ClosedD);
                            break;
                        case "tr":
                            array[x, y] = new Wall(x, y, WallType.ConnectorR);
                            break;
                        case "tl":
                            array[x, y] = new Wall(x, y, WallType.ConnectorL);
                            break;
                        case "td":
                            array[x, y] = new Wall(x, y, WallType.ConnectorD);
                            break;
                        case "tu":
                            array[x, y] = new Wall(x, y, WallType.ConnectorU);
                            break;
                        case "p":
                            Pellet p = new Pellet(10);
                            p.Collision += g.score.IncrementScore;
                            array[x, y] = new Path(x, y, p);
                            break;
                        case "e":
                            Energizer e = new Energizer(g.Ghostpack, 100);
                            e.Collision += g.score.IncrementScore;
                            array[x, y] = new Path(x, y, e);
                            break;
                        case "m":
                            array[x, y] = new Path(x, y, null);
                            break;
                        case "x":
                            array[x, y] = new PenPath(x, y);
                            g.pen.AddTile(array[x, y]);
                            break;
                        case "P":
                            array[x, y] = new Path(x, y, null);
                            g.pacman.Position = new Vector2(x, y);
                            g.pacman.initPosition = new Vector2(x, y);
                            break;
                        case "1":

                            gh = new Ghost(g, x, y, GhostState.Chase, new Color(255, 0, 0), GhostName.Blinky);
                            assistant = gh;
                            pen.Entrance = new Vector2(x, y);
                            gh.Points = 200;
                            Ghost.ReleasePosition = new Vector2(x, y);
                            gh.Collision += g.score.IncrementScore;
                            gh.PacmanDied += g.score.DeadPacman;
                            g.ghostpack.Add(gh);
                            array[x, y] = new Path(x, y, null);
                            break;
                        case "2":
                            gh = new Ghost(g, x, y, GhostState.Chase, new Color(255, 192, 203), GhostName.Speedy);
                            gh.Points = 200;
                            gh.Collision += g.score.IncrementScore;
                            gh.PacmanDied += g.score.DeadPacman;
                            g.ghostpack.Add(gh);
                            array[x, y] = new Path(x, y, null);
                            g.pen.AddTile(array[x, y]);
                            g.pen.AddToPen(gh);
                            break;
                        case "3":
                            gh = new Ghost(g, x, y, GhostState.Chase, new Color(64, 224, 208), GhostName.Inky);
                            gh.GhostAssistant = assistant;
                            gh.Points = 200;
                            gh.Collision += g.score.IncrementScore;
                            gh.PacmanDied += g.score.DeadPacman;
                            g.ghostpack.Add(gh);
                            array[x, y] = new Path(x, y, null);
                            g.pen.AddTile(array[x, y]);
                            g.pen.AddToPen(gh);
                            break;
                        case "4":
                            gh = new Ghost(g, x, y, GhostState.Chase, new Color(255, 165, 0), GhostName.Clyde);

                            gh.Points = 200;
                            gh.Collision += g.score.IncrementScore;
                            gh.PacmanDied += g.score.DeadPacman;
                            g.ghostpack.Add(gh);
                            array[x, y] = new Path(x, y, null);
                            g.pen.AddTile(array[x, y]);
                            g.pen.AddToPen(gh);
                            break;
                    }
                }
            }
            g.maze.SetTiles(array);

            //sets home positions for scatter mode
            foreach (Ghost ghost in g.ghostpack)
            {
                switch (ghost.Name)
                {
                    case GhostName.Blinky:
                        ghost.HomePosition = new Vector2(g.maze.Length - 2, 1);
                        break;
                    case GhostName.Speedy:
                        ghost.HomePosition = new Vector2(1, 1);
                        break;
                    case GhostName.Inky:
                        ghost.HomePosition = new Vector2(g.maze.Length - 2, g.maze.Height - 2);
                        break;
                    case GhostName.Clyde:
                        ghost.HomePosition = new Vector2(1, g.maze.Height - 2);
                        break;
                }
            }
            g.maze.PacmanWon += g.Score.IncrementScore;
            g.pacman.SubToGhosts();
            g.Score.Pause += g.Ghostpack.Pause;
            g.Score.Pause += g.Pen.Pause;
            return g;
        }

        /// <summary>
        /// Method that parse the .csv file into a jagged array
        /// </summary>
        /// <param name="filePath">The file path of the maze</param>
        private static string[][] getElements(string fileContent)
        {
            string[] stringLines = Regex.Split(fileContent, @"\r\n");
            string[][] parseStr = new string[stringLines.Length][];
            for (int i = 0; i < stringLines.Length; i++)
            {
                parseStr[i] = stringLines[i].Split(',');
            }

            return parseStr;
        }
    }
}
