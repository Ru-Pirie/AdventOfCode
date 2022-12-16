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
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            long countA = 0;

            Dictionary<string, long> rates = new Dictionary<string, long>();
            // open = true
            Dictionary<string, bool> states = new Dictionary<string, bool>();
            Dictionary<string, List<string>> leads = new Dictionary<string, List<string>>();

            List<Node> graph = new List<Node>();

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

                Node working = new Node();
                working.Name = valve;
                working.Rate = flowRate;
                working.Visited = false;
                working.CameFrom = new List<Node>();
                working.Connections = new List<Node>();

                graph.Add(working);

                rates.Add(valve, flowRate);
                states.Add(valve, false);
                leads.Add(valve, connections);
            }

            long maxRate = graph.Max(x => x.Rate) + 1;

            for (int i = 0; i < graph.Count; i++)
            {
                foreach (string s in leads[graph[i].Name])
                {
                    graph[i].Connections.Add(graph.Where(x => x.Name == s).First());
                }

                graph[i].Rate = maxRate - graph[i].Rate;
            }



            Console.WriteLine($"P1: {countA}");
            Console.ReadLine();

            // int maxTime = 30;
            // int currentTime = 0;
            //
            // string current = "AA";
            //
            // while (currentTime < maxTime)
            // {
            //     foreach (var valve in states.Keys) if (states[valve]) countA += rates[valve];
            //     
            //     if (rates[current] > 0)
            //     {
            //         states[current] = true;
            //
            //     }
            //
            //     currentTime++;
            //     Console.WriteLine(currentTime);
            // }


        }
    }

    class Node
    {
        public string Name;
        public long Rate;
        public List<Node> Connections;
        public List<Node> CameFrom;
        public bool Visited;
        public long MinDistance;
    }
}
