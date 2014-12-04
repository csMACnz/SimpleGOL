using System;
using System.Linq;
using System.Threading;

namespace SimpleGOL
{
    public class Program
    {
        private const int Width = 10;
        private const int Height = 10;
        public static void Main(string[] args)
        {
            var world = new bool[Height, Width];
            //uncomment a single group for oscillators

            /* Blinker */
            //world[5, 4] = true;
            //world[5, 5] = true;
            //world[5, 6] = true;

            /* Toad */
            //world[5, 4] = true;
            //world[5, 5] = true;
            //world[5, 6] = true;
            //world[6, 3] = true;
            //world[6, 4] = true;
            //world[6, 5] = true;

            /* Beacon */
            //world[3, 3] = true;
            //world[3, 4] = true;
            //world[4, 3] = true;
            //world[5, 6] = true;
            //world[6, 5] = true;
            //world[6, 6] = true;

            /* Random */
            var random = new Random((int)DateTime.Now.Ticks % int.MaxValue);
            foreach (int row in Enumerable.Range(0, Height))
            {
                foreach (int col in Enumerable.Range(0, Width))
                {
                    world[row, col] = random.Next(2) == 1;
                }
            }


            while (true)
            {
                var oldWorld = world;
                world = new bool[Height, Width];
                foreach (int row in Enumerable.Range(0, Height))
                {
                    foreach (int col in Enumerable.Range(0, Width))
                    {

                        var neighbourLiveCount = 0;
                        var offsets = new[] {-1, 0, 1};
                        foreach (int rowOffset in offsets)
                        {
                            foreach (int colOffset in offsets)
                            {
                                if (rowOffset == 0 && colOffset == 0) continue;
                                var neighbourRow = row + rowOffset;
                                var neightbourColumn = col + colOffset;
                                if (neightbourColumn >= 0 &&
                                    neightbourColumn < Width &&
                                    neighbourRow >= 0 &&
                                    neighbourRow < Height &&
                                    oldWorld[neighbourRow, neightbourColumn]
                                    )
                                {
                                    neighbourLiveCount++;
                                }
                            }
                        }
                        bool newState;
                        var currentState = oldWorld[row, col];
                        if (currentState)
                        {
                            newState = neighbourLiveCount == 2 || neighbourLiveCount == 3;
                        }
                        else
                        {
                            newState = neighbourLiveCount == 3;
                        }
                        world[row, col] = newState;
                    }
                }

                Console.Clear();
                foreach (int row in Enumerable.Range(0, Height))
                {
                    foreach (int col in Enumerable.Range(0, Width))
                    {
                        var isAlive = world[row, col];
                        Console.Write(isAlive ? "[#]" : "[ ]");
                    }
                    Console.WriteLine("");
                }
                Thread.Sleep(600);
            }
        }
    }
}
