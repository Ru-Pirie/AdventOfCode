using System;
using System.Collections.Generic;
using System.IO;

namespace _2022_Day_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            (long, long) head = (0, 0);
            (long, long)[] tails =
            {
                (0, 0),
                (0, 0),
                (0, 0),
                (0, 0),
                (0, 0),
                (0, 0),
                (0, 0),
                (0, 0),
                (0, 0)
            };

            List<(long, long)> partAVisits = new List<(long, long)> { (0, 0) };
            List<(long, long)> partBVisits = new List<(long, long)> { (0, 0) };

            for (int i = 0; i < input.Length; i++)
            {
                char direction = input[i].Split(' ')[0][0];
                int moves = int.Parse(input[i].Split(' ')[1].Trim());

                for (int j = 0; j < moves; j++)
                {
                    switch (direction)
                    {
                        case 'R':
                            head = (head.Item1, head.Item2 + 1);
                            break;
                        case 'L':
                            head = (head.Item1, head.Item2 - 1);
                            break;
                        case 'U':
                            head = (head.Item1 + 1, head.Item2);
                            break;
                        case 'D':
                            head = (head.Item1 - 1, head.Item2);
                            break;
                    }

                    tails[0] = Move(head, tails[0]);

                    for (int k = 1; k < tails.Length; k++)
                    {
                        tails[k] = Move(tails[k - 1], tails[k]);
                    }

                    if (!partAVisits.Contains(tails[0])) partAVisits.Add(tails[0]);
                    if (!partBVisits.Contains(tails[8])) partBVisits.Add(tails[8]);
                }
            }

            Console.WriteLine(partAVisits.Count);
            Console.WriteLine(partBVisits.Count);
            Console.ReadLine();
        }

        public static (long, long) Move((long, long) a, (long, long) b)
        {
            long dx = Math.Abs(a.Item1 - b.Item1);
            long dy = Math.Abs(a.Item2 - b.Item2);

            if (dx <= 1 && dy <= 1) return b;
            if (dx >= 2 && dy >= 2)
            {
                long newX = b.Item1 < a.Item1 ? (a.Item1 - 1) : (a.Item1 + 1);
                long newY = b.Item2 < a.Item2 ? (a.Item2 - 1) : (a.Item2 + 1);
                b = (newX, newY);
            }
            else if (dx >= 2)
            {
                long newX = b.Item1 < a.Item1 ? (a.Item1 - 1) : (a.Item1 + 1);
                b = (newX, a.Item2);
            } else if (dy >= 2)
            {
                long newY = b.Item2 < a.Item2 ? (a.Item2 - 1) : (a.Item2 + 1);
                b = (a.Item1, newY);
            }

            return b;
        } 
    }
}
