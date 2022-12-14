using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2022_Day_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool part2 = false;

            string[] input = File.ReadAllLines("../../input.txt");

            long countA = 0;

            List<List<int>> rX = new List<List<int>>();
            List<List<int>> rY = new List<List<int>>();

            for (int i = 0; i < input.Length; i++)
            {
                rX.Add(new List<int>());
                rY.Add(new List<int>());
                string[] pairs = input[i].Split(new [] { " -> " }, StringSplitOptions.None);
                for (int j = 0; j < pairs.Length; j++)
                {
                    rX[i].Add(int.Parse(pairs[j].Split(',')[0].Trim()));
                    rY[i].Add(int.Parse(pairs[j].Split(',')[1].Trim()));
                }
            }

            char[,] sandGrid = new char[1000, 1000];

            int lowest = 0;

            for (int i = 0; i < rX.Count; i++)
            {
                for (int j = 1; j < rX[i].Count; j++)
                {
                    if (rX[i][j] == rX[i][j - 1])
                    {
                        int start = rY[i][j] > rY[i][j - 1] ? rY[i][j - 1] : rY[i][j];
                        int end = rY[i][j] < rY[i][j - 1] ? rY[i][j - 1] : rY[i][j];
                        int constantX = rX[i][j];

                        for (int k = start; k <= end; k++)
                        {
                            sandGrid[k, constantX] = '#';
                            if (k > lowest) lowest = k;
                        }
                    }
                    else
                    {
                        int start = rX[i][j] > rX[i][j - 1] ? rX[i][j - 1] : rX[i][j];
                        int end = rX[i][j] < rX[i][j - 1] ? rX[i][j - 1] : rX[i][j];
                        int constantY = rY[i][j];

                        if (constantY > lowest) lowest = constantY;

                        for (int k = start; k <= end; k++)
                        {
                            sandGrid[constantY, k] = '#';
                        }
                    }
                }
            }

            if (part2)
            {
                for (int i = 0; i < 1000; i++)
                {
                    sandGrid[lowest + 2, i] = '#';
                }
            }

            bool running = true;

            List<(int, int)> sandPath = new List<(int, int)>();

            while (running)
            {
                int sX = 500;
                int sY = 0;

                // Full up
                if (sandGrid[sY, sX] == 'o')
                {
                    running = false;
                    countA--;
                }

                bool falling = true;
                while (falling)
                {
                    // VOID
                    if (sY >= lowest && !part2)
                    {
                        running = false;
                        falling = false;
                    }
                    if (sandGrid[sY + 1, sX] == '\0')
                    {
                        sY++;
                    } else if (sandGrid[sY + 1, sX - 1] == '\0')
                    {
                        sX--;
                        sY++;
                    } else if (sandGrid[sY + 1, sX + 1] == '\0')
                    {
                        sX++;
                        sY++;
                    }
                    else
                    {
                        sandGrid[sY, sX] = 'o';
                        sandPath.Add((sY, sX));
                        countA++;
                        falling = false;
                    }
                }
            }

            Console.WriteLine($"Count: {countA}");
            Console.ReadLine();

            // Visualizer to see set console char size to 6
            Console.CursorVisible = false;
            for (int y = 0; y < lowest; y++)
            {
                bool skip = false;
                for (int x = 150; x < y + 500; x++)
                {
                    if (sandGrid[y, x] != '\0')
                    {

                        if (sandGrid[y, x] == '#')
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(sandGrid[y, x]);
                            Console.ResetColor();
                        }
                        else if (sandGrid[y, x] == 'o')
                        {
                            Console.Write(' ');
                        }
                        else
                        {
                            Console.Write(sandGrid[y, x]);
                        }
                    }
                    else
                    {
                        if (!skip) Console.Write(' ');
                    }
                }   
                Console.WriteLine();
            }

            for (int i = 0; i < sandPath.Count; i++)
            {
                Console.SetCursorPosition(sandPath[i].Item2 - 150, sandPath[i].Item1 + 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('o');
                // Waste some cpu cycles
                for (int j = 0; j < 200; j++) { Console.ResetColor(); }
            }

            Console.ReadLine();
        }
    }
}
