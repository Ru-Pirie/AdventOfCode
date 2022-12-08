using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Day_7
{
    internal class Program
    {
        public static long GetDirSize(Dictionary<string, long> entries, Dictionary<string, List<string>> children, string toGetDir)
        {
            long newSize = entries[toGetDir];

            for (int i = 0; i < children[toGetDir].Count; i++)
            {
                List<string> split = toGetDir.Split('/').ToList();
                for (int j = 0; j < split.Count; j++) if (split[j] == "") split.RemoveAt(j);
                newSize += GetDirSize(entries, children, (toGetDir == "/" ? "" : "/") + string.Join("/", split) + "/" + children[toGetDir][i] + "/");
            }

            return newSize;
        }

        public static string ParseDirChain(string current, List<string> chain)
        {
            chain.Reverse();
            string working = current;
            List<string> stackOfThings = new List<string>(working.Trim().Split('/'));
            for (int i = 0; i < chain.Count; i++)
            {
                switch (chain[i].Split(' ')[1].Trim())
                {
                    case "..":
                        while (stackOfThings[stackOfThings.Count - 1] == "") stackOfThings.RemoveAt(stackOfThings.Count - 1);
                        stackOfThings.RemoveAt(stackOfThings.Count - 1);
                        break;
                    default:
                        stackOfThings.Add(chain[i].Split(' ')[1]);
                        break;
                }
            }

            string result = "/";
            for (int i = 0; i < stackOfThings.Count; i++) if (stackOfThings[i] != "") result += stackOfThings[i].Trim() + "/";

            return result;
        }

        static void Main(string[] args)
        {
            string raw = File.ReadAllText("../../input.txt");

            List<(string, List<string>)> commandsList = new List<(string, List<string>)>();

            string[] input = raw.Split('$');
            
            for (int i = 1; i < input.Length; i++)
            {
                List<string> contents = input[i].Split('\n').ToList();
                string command = contents[0].Split('\r')[0];
                contents.RemoveAt(0);
                var temp = new List<string>();

                foreach (var response in contents) if (response != "") temp.Add(response.Split('\r')[0].Trim());
                
                commandsList.Add((command.Trim(), temp));
            }

            Dictionary<string, long> dirSizes = new Dictionary<string, long>();
            Dictionary<string, List<string>> dirChildren = new Dictionary<string, List<string>>();

            string currentLevel = "/";

            for (int i = 0; i < commandsList.Count; i++)
            {
                if (commandsList[i].Item1 == "ls")
                {
                    int backI = 1;
                    string prev = commandsList[i - backI].Item1;

                    List<string> dirChain = new List<string>();

                    while (prev.StartsWith("cd") && i - backI > 0)
                    {
                        dirChain.Add(commandsList[i - backI].Item1);
                        backI++;
                        prev = commandsList[i - backI].Item1;
                    }

                    string dir = ParseDirChain(currentLevel, dirChain);
                    long tot = 0;
                    long working;

                    List<string> children = new List<string>();

                    for (int j = 0; j < commandsList[i].Item2.Count; j++)
                    {
                        if (long.TryParse(commandsList[i].Item2[j].Split(' ')[0], out working)) tot += working;
                        else children.Add(commandsList[i].Item2[j].Split(' ')[1].Trim());
                    }

                    dirSizes.Add(dir, tot);
                    dirChildren.Add(dir, children);
                    currentLevel = dir;
                }
            }

            foreach (var key in dirChildren.Keys) dirSizes[key] = GetDirSize(dirSizes, dirChildren, key);
            long total = 0;

            // Part 1
            foreach (var key in dirSizes.Keys) if (dirSizes[key] <= 100000) total += dirSizes[key];
            Console.WriteLine(total);
            
            // Part 2
            long someValue = 70000000 - dirSizes["/"];
            long smallest = long.MaxValue;
            foreach (var key in dirSizes.Keys) if (someValue + dirSizes[key] > 30000000 && dirSizes[key] < smallest) smallest = dirSizes[key];
            Console.WriteLine(smallest);

            Console.ReadLine();
        }
    }
}
