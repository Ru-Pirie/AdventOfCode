using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2022_Day_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            int[,] grid = new int[input.Length,input[0].Length];

            (int,int) start = (0,0);
            (int,int) end = (0, 0);
            
            for (int y = 0; y < input.Length; y++)
            {
                char[] squares = input[y].ToCharArray();
                for (int x = 0; x < squares.Length; x++)
                {
                    if (squares[x] == 'S' || squares[x] == 'E')
                    {
                        if (squares[x] == 'S') start = (x, y);
                        else end = (x, y);
                        grid[y, x] = squares[x] == 'S' ? 0 : 25;
                    }
                    else grid[y, x] = Math.Abs((int)'a' - (int)squares[x]);
                }
            }

            Graph<(int, int)> myGraph = new Graph<(int, int)>();

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    (int, int) parent = (x,y);
                    myGraph.AddNode(parent);
                    
                    if (x - 1 >= 0)
                    {
                        if (grid[y, x - 1] - 1 == grid[y, x] || grid[y, x - 1] == grid[y, x] || grid[y, x - 1] + 1== grid[y, x])
                        {
                            myGraph.AddConnection(parent, (x - 1,y));
                        }
                    }
                    if (y - 1 >= 0)
                    {
                        if (grid[y - 1, x] - 1 == grid[y, x] || grid[y - 1, x] == grid[y, x] || grid[y - 1, x] + 1 == grid[y, x])
                        {
                            myGraph.AddConnection(parent, (x, y - 1));
                        }
                    }
                    if (x + 1 < grid.GetLength(1))
                    {
                        if (grid[y, x + 1] - 1 == grid[y, x] || grid[y, x + 1] == grid[y, x] || grid[y, x + 1] + 1 == grid[y, x])
                        {
                            myGraph.AddConnection(parent, (x + 1, y));
                        }
                    }
                    if (y + 1 < grid.GetLength(0))
                    {
                        if (grid[y + 1, x] - 1 == grid[y, x] || grid[y + 1, x] == grid[y, x] || grid[y + 1, x] + 1== grid[y, x])
                        {
                            myGraph.AddConnection(parent, (x, y + 1));
                        }
                    }
                }
            }

            myGraph.GetNode((0, 0));

            Traversal<(int, int)> myTraversal = new Traversal<(int, int)>(myGraph);
            var result = myTraversal.Dijkstra(start);

            var path = RebuildPath<(int, int)>(result, end).ToList();

            Console.WriteLine(string.Join(", ", path));
            Console.WriteLine(path.Count);
            Console.ReadLine();
        }

        public static T[] RebuildPath<T>(Dictionary<T, T> prev, T goal)
        {
            if (prev == null) return new T[1];
            List<T> sequence = new List<T>();
            T u = goal;

            while (prev.ContainsKey(u))
            {
                sequence.Insert(0, u);
                u = prev[u];
            }

            return sequence.ToArray();
        }

        public static (int, int)[] GetNeighbors(int[,] grid, (int, int) node)
        {
            List<(int, int)> viableNodes = new List<(int, int)>();
            List<(int, int)> nodes = new List<(int, int)>();
            if (node.Item1 > 0) nodes.Add((node.Item1 - 1, node.Item2));
            if (node.Item2 > 0) nodes.Add((node.Item1, node.Item2 - 1));
            if (node.Item1 < grid.GetLength(0) - 1) nodes.Add((node.Item1 + 1, node.Item2));
            if (node.Item2 < grid.GetLength(1) - 1) nodes.Add((node.Item1, node.Item2 + 1));

            int value = grid[node.Item1, node.Item2];
            foreach ((int, int) child in nodes)
            {
                if (value + 1 == grid[child.Item1, child.Item2]) viableNodes.Add(child);
            }

            return viableNodes.ToArray();
        }
    }
}
