using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Day_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string[] input = File.ReadAllLines("../../input.txt");

            //Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            //List<long> valuesOf = new List<long>();

            //long temp = 0;
            //long temp2 = 0;

            //List<int> aList = new List<int>();
            //List<int> bList = new List<int>();

            //for (int i = 0; i < input.Length; i++)
            //{
            //    string elfOne = input[i].Split(',')[0];
            //    string elfTwo = input[i].Split(',')[1];

            //    aList = new List<int>();
            //    for (int j = int.Parse(elfOne.Split('-')[0]); j <= int.Parse(elfOne.Split('-')[1]); j++)
            //    {
            //        aList.Add(j);
            //    }

            //    bList = new List<int>();
            //    for (int j = int.Parse(elfTwo.Split('-')[0]); j <= int.Parse(elfTwo.Split('-')[1]); j++)
            //    {
            //        bList.Add(j);
            //    }

            //    bool all = true;
            //    bool all2 = false;
            //    foreach (var element in bList)
            //    {
            //        if (!aList.Contains(element)) all = false;
            //        if (aList.Contains(element)) all2 = true;
            //    }

            //    if (all) temp++;
            //    if (all2) temp2++;

            //    if (!all)
            //    {
            //        all = true;
            //        foreach (var element in aList)
            //        {
            //            if (!bList.Contains(element))
            //            {
            //                all = false;
            //            }
            //        }
            //        if (all) temp++;
            //    }

            //    if (!all2)
            //    {
            //        all2 = false;
            //        foreach (var element in bList)
            //        {
            //            if (aList.Contains(element))
            //            {
            //                all2 = true;
            //            }
            //        }
            //        if (all2) temp2++;
            //    }

            //}

            //Console.WriteLine(temp);
            //Console.WriteLine(temp2);
            //Console.ReadLine();
            string[] input = File.ReadAllLines("../../input.txt");

            long partA = 0;
            long partB = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string elfOne = input[i].Split(',')[0];
                string elfTwo = input[i].Split(',')[1];
                int AA = int.Parse(elfOne.Split('-')[0]);
                int AB = int.Parse(elfOne.Split('-')[1]);
                int BA = int.Parse(elfTwo.Split('-')[0]);
                int BB = int.Parse(elfTwo.Split('-')[1]);

                if ((AA <= BA && AB >= BB) || (BA <= AA && BB >= AB)) partA++;
                if ((AA <= BA && AB >= BA) || (AB >= BB && AA <= BB) || (BA <= AA && BB >= AA) || (BB >= AB && BA <= AB)) partB++;
            }

            Console.WriteLine(partA);
            Console.WriteLine(partB);
            Console.ReadLine();
        }
    }
}
