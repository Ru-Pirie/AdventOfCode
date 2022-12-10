using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Day_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            long partA = 0;
            List<string> workingList = new List<string>();

            long cycleCount = 1;
            long settingX = 1;
            int spriteIndex = 1;


            bool secondTimeRound = false;

            for (int i = 0; i < input.Length; i++)
            {
                if ((cycleCount - 20) % 40 == 0) partA += cycleCount * settingX;

                if (input[i].Split(' ')[0].Trim() == "addx")
                {
                    if (secondTimeRound)
                    {
                        settingX += int.Parse(input[i].Split(' ')[1]);
                        secondTimeRound = false;
                    }
                    else
                    {
                        secondTimeRound = true;
                        i--;
                    }
                }

                if ((spriteIndex % 40) - 1 == settingX || settingX == (spriteIndex % 40) || (spriteIndex % 40) + 1 == settingX)
                    workingList.Add("#");
                else workingList.Add(".");
                spriteIndex++;

                cycleCount++;
            }

            Console.WriteLine(partA);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#");
            for (int i = 0; i < workingList.Count; i++)
            {
                if (workingList[i] == "#") Console.ForegroundColor = ConsoleColor.Green;
                else Console.ForegroundColor = ConsoleColor.DarkRed;

                if ((i + 1) % 40 == 0) Console.WriteLine();
                Console.Write(workingList[i]);
            }
            Console.ReadLine();
        }
    }
}
