using Microsoft.Xna.Framework;
using PacMan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string level1 = File.ReadAllText("../../../PacMan/levelsPen.csv");
            GameState g = GameState.Parse(level1);

            Ghost gh = new Ghost(g, 14, 15, GhostState.Chase, new Color(255, 255, 255), GhostName.Blinky);
            gh.Direction = Direction.Left;
            //make the ghost move
            //Assert.AreEqual(gh.Position, new Vector2(14, 15));
            gh.Move();
            gh.Move();
            gh.Move();
            gh.Move();
            //Assert.AreEqual(gh.Position, new Vector2(12, 17));

            //then reset the ghost to the pen
            gh.Reset();
            Console.WriteLine(gh.Position);

            for (int i = 0; i < g.Maze.Height; i++)
            {
                for (int j = 0; j < g.Maze.Length; j++)
                {
                    Console.Write(g.Maze[j, i]+ "," +  g.Maze[j,i]?.Position);
                }
                Console.WriteLine();
            }


            /*string s = File.ReadAllText("../../../PacMan/levelsPen.csv");
            GameState g = GameState.Parse(s);

            List<Tile> t2 = g.Maze.GetAvailableNeighbours(new Vector2(15, 4), Direction.Left);
            foreach(Tile t in t2)
            {
                Console.WriteLine(t.Position);
            }*/

            /*bool triggered = false;

            g.Maze.PacmanWon += (x) => triggered = true;

            //collide with all the pellets and energizer to check win
            for (int i = 0; i < g.Maze.Size; i++)
            {
                for (int j = 0; j < g.Maze.Size; j++)
                {
                    if (g.Maze[j, i] is PacMan.Path)
                    {
                        g.Maze[j, i].Collide();
                    }
                }
            }

            g.Maze.CheckMembersLeft();

            Console.WriteLine(triggered);*/

            /*GameState g = GameState.Parse("../../../PacMan/levelsPen.csv");
            Vector2 v = g.Pacman.Position;
            Console.WriteLine(g.Pacman.Position + " initial");
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Left);
            g.Pacman.Move(Direction.Left);
            //g.Maze[21, 3].Collide();
            g.Pacman.Move(Direction.Left);
            g.Pacman.Move(Direction.Left);

            Console.WriteLine(g.Pacman.Position + " after");
            Console.WriteLine(g.Score.Lives + ", " + g.Score.Score);

            foreach(var gh in g.Ghostpack)
            {
                Console.WriteLine(gh.Position +  ", " + gh.CurrentState);
            }*/

            /*GameState g = GameState.Parse("../../../PacMan/levelsPen.csv");

            Console.WriteLine(g.Pacman.Position);

            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Down);
            g.Pacman.Move(Direction.Down);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Up);
            g.Pacman.Move(Direction.Right);
            g.Pacman.Move(Direction.Right);

            Console.WriteLine(g.Pacman.Position);
            Console.WriteLine(g.Score.Score);*/

            /*GameState g = GameState.Parse("../../../PacMan/levelsPen.csv");
            Console.WriteLine(g.Pacman.Position);
            g.Pacman.Move(Direction.Left);
            Console.WriteLine(g.Pacman.Position);

            g.Pacman.Move(Direction.Left);
            Console.WriteLine(g.Pacman.Position);
            g.Pacman.Move(Direction.Right);

            Console.WriteLine(g.Pacman.Position);
            Console.WriteLine(g.Score.Score);*/



            /*for(int i=0; i<g.Maze.Size; i++)
            {
                for(int j=0; j<g.Maze.Size; j++)
                {
                    Console.Write(g.Maze[j,i]);
                }

                Console.WriteLine();
            }*/

        }
    }
}
