using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2022_Day_3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            long value1 = 0;
            long value2 = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char[] inputChars = input[i].ToCharArray();

                value1 += GetPriority(SubArray(inputChars, inputChars.Length / 2, inputChars.Length / 2)
                    .Intersect(SubArray(inputChars, 0, inputChars.Length / 2)).First());
            }
            
            for (int i = 0; i < input.Length; i+=3)
            {
                value2 += GetPriority(input[i].ToCharArray()
                    .Intersect(input[i + 1].ToCharArray().Intersect(input[i + 2].ToCharArray())).First());
            }

            Console.WriteLine(value1);
            Console.WriteLine(value2);
            Console.ReadLine();
        }

        public static int GetPriority(char letter)
        {
            int returnValue = 0;
            
            if (letter.ToString().ToLower() != letter.ToString()) returnValue += 26;
            returnValue += letter.ToString().ToLower()[0] - 'a' + 1;

            return returnValue;
        }

        public static char[] SubArray(char[] array, int offset, int length)
        {
            char[] result = new char[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

    }
}
