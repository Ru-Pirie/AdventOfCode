using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Day_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("../../input.txt");

            Stack<string>[] points = {new Stack<string>(), new Stack<string>(), new Stack<string>(), new Stack<string>(), new Stack<string>(), new Stack<string>(), new Stack<string>(), new Stack<string>(), new Stack<string>() };

            int i = 0;
            bool running = true;
            while (running)
            {
                if (input[i][1] == '1') running = false;
                else
                {
                    for (int j = 1; j < input[i].Length; j+=4)
                    {
                        if (input[i][j] != ' ')
                        {
                            points[(j - 1) / 4].Push(input[i][j].ToString());
                        }
                    }
                }
                i++;
            }

            for (int j = 0; j < 9; j++) points[j] = FlipStack(points[j]);

            for (int j = 10; j < input.Length; j++)
            {
                int loop = int.Parse(input[j].Split(' ')[1]);
                int from = int.Parse(input[j].Split(' ')[3]) - 1;
                int top = int.Parse(input[j].Split(' ')[5]) - 1;

                var workingStack = new Stack<string>();

                for (int k = 0; k < loop; k++) workingStack.Push(points[from].Pop());
                
                // For P1 enable this line
                //workingStack = FlipStack(workingStack);

                foreach (var element in workingStack) points[top].Push(element);
            }

            for (int j = 0; j < 9; j++) Console.Write(points[j].Peek());

            Console.ReadLine();
        }

        public static Stack<string> FlipStack(Stack<string> input)
        {
            Stack<string> flippedStack = new Stack<string>();
            while (input.Count > 0)
            {
                flippedStack.Push(input.Pop());
            }

            return flippedStack;
        }
    }
}
