using System;
using System.IO;

namespace _2022_Day_8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            long partA = 0;
            long partB = 0;

            int[,] grid = new int[input[0].Length, input.Length];

            for (int i = 0; i < input.Length; i++) for (int j = 0; j < input[i].Length; j++) grid[i, j] = int.Parse(input[i][j].ToString());
            
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (IsVisible(x, y, grid))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(grid[y, x]);
                        Console.ResetColor();
                        partA++;
                    }
                    else
                    {
                        Console.Write(grid[y, x]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(partA);

            for (int y = 1; y < input.Length - 1; y++)
            {
                for (int x = 1; x < input[0].Length - 1; x++)
                {
                    if ((GetScenicScore(x, y, grid) > partB))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(grid[y, x]);
                        Console.ResetColor();
                        partB = GetScenicScore(x, y, grid);
                    }
                    else
                    {
                        Console.Write(grid[y, x]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(partB);
            Console.ReadLine();
        }

        public static long GetScenicScore(int x, int y, int[,] grid)
        {
            long value = 1;
            long temp = 0;
            for (int i = x - 1; i >= 0; i--)
            {
                temp++;
                if (grid[y, i] >= grid[y, x]) break;
            }

            value *= temp;
            temp = 0;
            for (int i = x + 1; i < grid.GetLength(1); i++)
            {
                temp++;
                if (grid[y, i] >= grid[y, x]) break;
            }

            value *= temp;
            temp = 0;
            for (int i = y - 1; i >= 0; i--)
            {
                temp++;
                if (grid[i, x] >= grid[y, x]) break;
            }

            value *= temp;
            temp = 0;
            for (int i = y + 1; i < grid.GetLength(0); i++)
            {
                temp++;
                if (grid[i, x] >= grid[y, x]) break;
            }

            value *= temp;
            return value;
        }

        public static bool IsVisible(int x, int y, int[,] grid)
        {
            bool collided = false;
            for (int i = 0; i < x; i++) if (grid[y, i] >= grid[y, x]) collided = true;
            if (!collided) return true;

            collided = false;
            for (int i = x + 1; i < grid.GetLength(1); i++) if (grid[y, i] >= grid[y, x]) collided = true;
            if (!collided) return true;

            collided = false;
            for (int i = 0; i < y; i++) if (grid[i, x] >= grid[y, x]) collided = true;
            if (!collided) return true;

            collided = false;
            for (int i = y + 1; i < grid.GetLength(0); i++) if (grid[i, x] >= grid[y, x]) collided = true;
            return !collided;
        }
    }
}
