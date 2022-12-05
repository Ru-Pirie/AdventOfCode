using System;
using System.Collections.Generic;
using System.IO;

namespace _2022_Day_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            List<long> listOfValues = new List<long>();

            long part1 = 0;
            long part2 = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string first = input[i].Split(' ')[0];
                string second = input[i].Split(' ')[1];

                if ((first == "C" && second == "X") || (first == "A" && second == "Y") ||
                    (first == "B" && second == "Z"))
                {
                    part1 += 6;
                    if (second == "X") part1 += 1;
                    else if (second == "Y") part1 += 2;
                    else if (second == "Z") part1 += 3;
                }
                else if ((first == "A" && second == "X") || (first == "B" && second == "Y") ||
                         (first == "C" && second == "Z"))
                {
                    part1 += 3;
                    if (second == "X") part1 += 1;
                    else if (second == "Y") part1 += 2;
                    else if (second == "Z") part1 += 3;
                }
                else
                {
                    if (second == "X") part1 += 1;
                    else if (second == "Y") part1 += 2;
                    else if (second == "Z") part1 += 3;
                }

                Dictionary<string, int> looseDictionary = new Dictionary<string, int>();
                looseDictionary.Add("A", 3);
                looseDictionary.Add("B", 1);
                looseDictionary.Add("C", 2);

                Dictionary<string, int> drawDictionary = new Dictionary<string, int>();
                drawDictionary.Add("A", 1);
                drawDictionary.Add("B", 2);
                drawDictionary.Add("C", 3);

                Dictionary<string, int> winDictionary = new Dictionary<string, int>();
                winDictionary.Add("A", 2);
                winDictionary.Add("B", 3);
                winDictionary.Add("C", 1);


                if (second == "X")
                {
                    part2 += looseDictionary[first];
                }
                else if (second == "Y")
                {
                    part2 += drawDictionary[first];
                    part2 += 3;
                }
                else if (second == "Z")
                {
                    part2 += winDictionary[first];
                    part2 += 6;
                }
            }



            Console.WriteLine(part1);
            Console.WriteLine(part2);
            Console.ReadLine();
        }
    }
}
