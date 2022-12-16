using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;

namespace _2022_Day_16
{
    internal class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("../../input.txt");

            long countA = 0;

            Dictionary<string, long> rates = new Dictionary<string, long>();
            Dictionary<string, List<string>> leads = new Dictionary<string, List<string>>();

            for (int i = 0; i < input.Length; i++)
            {
                string valve = input[i].Split(' ')[1];
                long flowRate = int.Parse(input[i].Split('=')[1].Split(';')[0]);
                List<string> connections = new List<string>();
                foreach (string split in input[i]
                             .Split(new[] { "to valve" }, StringSplitOptions.None)[1].Split((',')))
                {
                    connections.Add(split.Split(' ')[1].Trim());
                }

                rates.Add(valve, flowRate);
                leads.Add(valve, connections);
            }

            Dictionary<string, Dictionary<string, long>> distances = new Dictionary<string, Dictionary<string, long>>();

            foreach (var key in rates.Keys)
            {
                Dictionary<string, long> paths = new Dictionary<string, long>();
                paths[key] = 0;

                Dictionary<string, bool> visited = new Dictionary<string, bool>();
                foreach (string tempKey in rates.Keys) visited.Add(tempKey, false);

                Queue<string> queue = new Queue<string>();
                queue.Enqueue(key);

                while (queue.Count > 0)
                {
                    string current = queue.Dequeue();

                    foreach (string point in leads[current])
                    {
                        long cost = paths[current] + 1;
                        if (!paths.ContainsKey(point)) paths.Add(point, cost);
                        else if (paths[point] > cost) paths[point] = cost;
                        if (!visited[point]) queue.Enqueue(point);
                    }

                    visited[current] = true;
                }

                distances.Add(key, paths);
            }
            
            Console.WriteLine($"P1: {countA}");
            Console.ReadLine();

        }

        // I started optimising this and lost faith
    }
}
