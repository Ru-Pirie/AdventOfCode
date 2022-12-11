using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2022_Day_11
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            long divisorBoi = 1;

            string raw = File.ReadAllText("../../input.txt");
            string[] input = raw.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            List<Monkey> monkeys = new List<Monkey>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] monkeyDetails = input[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                Monkey working = new Monkey();
                working.activity = 0;
                string[] tempItems = monkeyDetails[1].Split(':')[1].Split(',');
                working.items = new List<long>();
                foreach (string tempItem in tempItems)
                    working.items.Add(int.Parse(tempItem.Trim()));

                bool isAdding = monkeyDetails[2].Split('=')[1].Split('*').Length == 1;
                string val1 = isAdding
                    ? monkeyDetails[2].Split('=')[1].Split('+')[0].Trim()
                    : monkeyDetails[2].Split('=')[1].Split('*')[0].Trim();
                string val2 = isAdding
                    ? monkeyDetails[2].Split('=')[1].Split('+')[1].Trim()
                    : monkeyDetails[2].Split('=')[1].Split('*')[1].Trim();

                working.ChangeWorryOp1 = val1 == "old" ? -1 : int.Parse(val1);
                working.ChangeWorryOp2 = val2 == "old" ? -1 : int.Parse(val2);

                working.TestDevisor = int.Parse(monkeyDetails[3].Split(new[] { "by" }, StringSplitOptions.None)[1].Trim());

                divisorBoi *= working.TestDevisor;

                working.DoReciver =
                    int.Parse(monkeyDetails[4].Split(new[] { "monkey" }, StringSplitOptions.None)[1].Trim());
                working.NotReciver =
                    int.Parse(monkeyDetails[5].Split(new[] { "monkey" }, StringSplitOptions.None)[1].Trim());

                working.adding = isAdding;

                monkeys.Add(working);
            }

            // P1 set to 20
            // P2 set to 10000 
            for (int _ = 0; _ < 10000; _++)
            {
                for (int i = 0; i < monkeys.Count; i++)
                {
                    while (monkeys[i].items.Count > 0)
                    {
                        if (monkeys[i].adding) monkeys[i].items[0] =
                                AddWorry(
                                    (monkeys[i].ChangeWorryOp1 == -1 ? monkeys[i].items[0] : monkeys[i].ChangeWorryOp1),
                                    (monkeys[i].ChangeWorryOp2 == -1 ? monkeys[i].items[0] : monkeys[i].ChangeWorryOp2));

                        else monkeys[i].items[0] =
                                MultiplyWorry(
                                    (monkeys[i].ChangeWorryOp1 == -1 ? monkeys[i].items[0] : monkeys[i].ChangeWorryOp1),
                                    (monkeys[i].ChangeWorryOp2 == -1 ? monkeys[i].items[0] : monkeys[i].ChangeWorryOp2));


                        // Disable this for p2
                        //monkeys[i].items[0] /= 3;
                        monkeys[i].items[0] %= divisorBoi;

                        monkeys[i].activity++;

                        if (monkeys[i].items[0] % monkeys[i].TestDevisor == 0) monkeys[monkeys[i].DoReciver].items.Add(monkeys[i].items[0]);
                        else monkeys[monkeys[i].NotReciver].items.Add(monkeys[i].items[0]);

                        monkeys[i].items.RemoveAt(0);
                    }
                }

            }

            List<long> activity = monkeys.Select(x => x.activity).ToList();
            activity.Sort();
            Console.WriteLine(activity[activity.Count - 2] * activity[activity.Count - 1]);
            Console.ReadLine();
        }

        public static long AddWorry(long op1, long op2) => op1 + op2;
        public static long MultiplyWorry(long op1, long op2) => op1 * op2;

        public class Monkey
        {
            public long activity;
            public List<long> items;
            public bool adding;
            public int ChangeWorryOp1;
            public int ChangeWorryOp2;
            public int TestDevisor;
            public int NotReciver;
            public int DoReciver;
        }
    }
}
