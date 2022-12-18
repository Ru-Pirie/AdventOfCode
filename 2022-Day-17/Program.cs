using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _2022_Day_17
{
    internal class Program
    {
        public static string[][] blocks = {
            new[] {
                "@@@@"
            },
            new[] {
                " @ ",
                "@@@",
                " @ "
            },
            new[] {
                "@@@",
                "  @",
                "  @"
            },
            new[] {
                "@",
                "@",
                "@",
                "@"
            },
            new[] {
                "@@",
                "@@"
            }
        };

        public static int block = 0;
        public static string[] current = blocks[block];

        public static Dictionary<(int X, int Y), int> map = new Dictionary<(int X, int Y), int>();

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            string tape = string.Join("", input);
            int index = 0;
            long repeats = 0;

            int maxY = 0;
            int dispMax = 0;

            bool inPlay = false;

            var (blockX, blockY) = (0, 0);

            long nth = 0;

            var revisit = new Dictionary<(int Tape, int Shape), (long Rocks, long Height)>();


            long found1 = 0;
            long found2 = 0;

            while (found1 == 0 || found2 == 0)
            {
                if (!inPlay)
                {
                    blockX = 2;
                    blockY = (map.Count > 1 ? maxY : -1) + 4;
                    dispMax = blockY + blocks[block].Length;
                    current = blocks[block];
                    inPlay = true;
                    nth++;
                    if (nth == 2023)
                    {
                        found1 = maxY + 1;
                    }
                }

                int next = blockX;
                if (tape[index] == '<')
                {
                    next--;
                }
                else
                {
                    next++;
                }
                if (0 <= next && next + current[0].Length <= 7 && CheckCollision(next, blockY))
                {
                    blockX = next;
                }
                index = (index + 1) % tape.Length;
                if (index == 0) repeats++;
                next = blockY - 1;
                if (0 <= next && CheckCollision(blockX, next))
                {
                    blockY = next;
                }
                else
                {
                    for (int y = 0; y < blocks[block].Length; y++)
                    {
                        for (int x = 0; x < blocks[block][y].Length; x++)
                        {
                            bool place = blocks[block][y][x] == '@';
                            if (place) map[(blockX + x, blockY + y)] = block;
                            maxY = Math.Max(maxY, blockY + y);
                        }
                    }
                    block = (block + 1) % blocks.Length;
                    inPlay = false;

                    if (revisit.ContainsKey((index, block)) && found2 == 0)
                    {
                        var last = revisit[(index, block)];
                        long cycle = nth - last.Rocks;
                        long adds = maxY + 1 - last.Height;
                        long remaining = 1000000000000 - nth - 1;
                        long combo = (remaining / (cycle) + 1);
                        if (nth + combo * cycle == 1000000000000)
                            found2 = maxY + 1 + combo * adds;
                    }
                    else
                    {
                        revisit[(index, block)] = (nth, maxY + 1);
                    }
                }
            }
            Console.WriteLine($"Part 1: {found1}");
            Console.WriteLine($"Part 2: {found2}");
            Console.ReadKey();
        }

        public static bool CheckCollision(int bx, int by)
        {
            bool ok = true;
            for (int y = by; y < by + current.Length; y++)
            {
                for (int x = bx; x < bx + current[y - by].Length; x++)
                {
                    int read = map.ContainsKey((x, y)) ? map[(x, y)] : -1;
                    if (by <= y && y <= by + current.Length - 1 && bx <= x && x <= bx + current[y - by].Length - 1)
                    {
                        if (current[y - by][x - bx] == '@' && read != -1)
                        {
                            ok = false;
                        }
                    }
                }
            }
            return ok;
        }
    }
}
