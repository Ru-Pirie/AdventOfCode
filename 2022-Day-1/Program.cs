using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2022_Day_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            List<long> valuesOfEnergy = new List<long>();

            long temp = 0;
            
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != "")
                {
                    temp += int.Parse(input[i]); 
                }
                else
                {

                    valuesOfEnergy.Add(temp);
                    temp = 0;
                }
            }

            valuesOfEnergy.Sort();

            //Console.WriteLine(value);
            Console.WriteLine(valuesOfEnergy[valuesOfEnergy.Count - 1] + valuesOfEnergy[valuesOfEnergy.Count - 2] + valuesOfEnergy[valuesOfEnergy.Count -3]);
            Console.WriteLine(valuesOfEnergy[valuesOfEnergy.Count - 1]);
            Console.ReadLine();
        }
    }
}
