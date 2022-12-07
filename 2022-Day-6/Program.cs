using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Day_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            long partA = 0;
            long partB = 0;

            Queue<char> working = new Queue<char>();

            for (int i = 0; i < input[0].Length; i++)
            {
                // change to 4 for p1 
                if (working.Count < 14) working.Enqueue(input[0][i]);
                else if (working.All(x => working.Count(y => x == y) == 1))
                {
                    partA = i;
                    break;
                }
                else
                {
                    working.Dequeue();
                    working.Enqueue(input[0][i]);
                }
            }

            Console.WriteLine(partA);
            //Console.WriteLine(partB);
            Console.ReadLine();
        }
    }
}
