using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2022_Day_18
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            int min = 100;
            int max = 0;

            List<List<int>> cubes = new List<List<int>>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] bits = input[i].Split(',');
                int[] numBits = new int[3];

                for (int j = 0; j < bits.Length; j++)
                {
                    numBits[j] = int.Parse(bits[j]);

                    max = Math.Max(max, numBits[j]);
                    min = Math.Min(min, numBits[j]);
                }

                cubes.Add(numBits.ToList());
            }

            HashSet<int> sides = new HashSet<int>();
            foreach (List<int> cube in cubes)
            {
                for (int i = 0; i < 6; i++)
                {
                    int code = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        code = code * 100 + cube[j] * 2;
                        if (j == i / 2)
                        {
                            code += (i % 2 == 0 ? 1 : -1);
                        }
                    }
                    sides.Add(code);
                }
            }

            int connected = cubes.Count * 6 - sides.Count;
            int shown = sides.Count - connected;
            Console.WriteLine($"Part 1: {shown}");

            List<(int, int, int)> trace = new List<(int, int, int)>();

            HashSet<int> blocks = new HashSet<int>();
            foreach (List<int> cube in cubes)
            {
                blocks.Add(cube[0] * 10000 + cube[1] * 100 + cube[2]);
            }

            max++;
            min--;

            List<(int, int, int)> steam = new List<(int, int, int)> {
                (min, min, min),
                (min, min, max),
                (min, max, min),
                (min, max, max),
                (max, min, min),
                (max, min, max),
                (max, max, min),
                (max, max, max)
            };

            List<(int, int, int)> dirs = new List<(int, int, int)>
            {
                (-1, 0, 0),
                ( 1, 0, 0),
                ( 0,-1, 0),
                ( 0, 1, 0),
                ( 0, 0,-1),
                ( 0, 0, 1)
            };

            HashSet<int> visited = new HashSet<int>
            {
                10000 * min + 100 * min + min
            };

            HashSet<int> sides2 = new HashSet<int>();

            while (steam.Count > 0)
            {
                trace.AddRange(steam);
                var more = new List<(int, int, int)>(steam.Count);
                foreach ((int x, int y, int z) in steam)
                {
                    foreach ((int dx, int dy, int dz) in dirs)
                    {
                        if (x + dx < min || y + dy < min || z + dz < min) continue;
                        if (x + dx > max || y + dy > max || z + dz > max) continue;
                        int code = (x + dx) * 10000 + (y + dy) * 100 + (z + dz);
                        if (visited.Contains(code)) continue;
                        if (blocks.Contains(code))
                        {
                            sides2.Add((x * 2 + dx) * 10000 + (y * 2 + dy) * 100 + (z * 2 + dz));
                            continue;
                        }
                        visited.Add(code);
                        more.Add((x + dx, y + dy, z + dz));
                    }
                }
                steam = more;
            }


            Console.WriteLine($"Part 2: {sides2.Count}");
            Console.ReadLine();
        }
    }
}
