using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace _2022_Day_12
{
    internal class Program
    {
        public static int[,] grid;
        public static int gX, gY;

        private static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            (gX, gY) = (input[0].Length, input.Length);

            grid = new int[gY, gX];

            (int sX, int sY) = (0, 0);
            (int eX, int eY) = (0, 0);

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == 'S')
                    {
                        (sX, sY) = (x, y);
                        grid[y, x] = 0;
                    }
                    else if (input[y][x] == 'E')
                    {
                        (eX, eY) = (x, y);
                        grid[y, x] = (int)('z' - 'a');
                    }
                    else grid[y, x] = (int)(input[y][x] - 'a');
                }
            }

            Node startNode = new Node { X = sX, Y = sY };
            Node endNode = new Node { X = eX, Y = eY };

            Console.WriteLine("Count: ");
            Console.ReadLine();
        }

        private static int BFS(Node start, Node goal)
        {
            Queue<Node> queue = new Queue<Node>();
            Dictionary<Node, bool> explored = new Dictionary<Node, bool>();
            Dictionary<Node, Node> parents = new Dictionary<Node, Node>();

            queue.Enqueue(start);
            explored[start] = true;

            int steps = 0;

            while (queue.Count > 0)
            {
                Node temp = queue.Dequeue();

                if (goal.X == temp.X && goal.Y == temp.Y) return steps;
                //List<Node> neighbors = 
            }

            return -1;
        }

        private static List<Node> GetNeighbors(Node node)
        {
            var neighbors = new List<Node>();
            if (node.X > 0 && grid[node.X - 1, node.Y] <= grid[node.X, node.Y] + 1)
            {
                neighbors.Add(new Node { X = node.X - 1, Y = node.Y });
            }
            if (node.X < gX - 1 && grid[node.X + 1, node.Y] <= grid[node.X, node.Y] + 1)
            {
                neighbors.Add(new Node { X = node.X + 1, Y = node.Y });
            }
            if (node.Y > 0 && grid[node.X, node.Y - 1] <= grid[node.X, node.Y] + 1)
            {
                neighbors.Add(new Node { X = node.X, Y = node.Y - 1 });
            }
            if (node.Y < gY - 1 && grid[node.X, node.Y + 1] <= grid[node.X, node.Y] + 1)
            {
                neighbors.Add(new Node { X = node.X, Y = node.Y + 1 });
            }

            return neighbors;
        }
    }
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ulong Prio { get; set; }
        public ulong BestDist { get; set; }
        public long Index { get; set; }
    }
}
