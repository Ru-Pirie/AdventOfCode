using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2022_Day_15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool part2 = false;

            string[] input = File.ReadAllLines("../../input.txt");

            long countA = 0;

            List<(long, long)> sensorPosition = new List<(long, long)>();
            List<(long, long)> beaconPosition = new List<(long, long)>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] parts = input[i].Split('=');
                long Sx = long.Parse(parts[1].Trim().Split(',')[0].Trim());
                long Sy = long.Parse(parts[2].Trim().Split(':')[0].Trim());

                long Bx = long.Parse(parts[3].Trim().Split(',')[0].Trim());
                long By = long.Parse(parts[4].Trim().Split(':')[0].Trim());

                sensorPosition.Add((Sx, Sy));
                beaconPosition.Add((Bx, By));
            }


            long lookupRow = 2000000;
            List<(long, long)> beaconOffset = new List<(long, long)>();
            List<(long, long)> lengthPairs = new List<(long, long)>();

            for (int i = 0; i < sensorPosition.Count; i++)
            {
                long xWidth = ((Math.Abs(sensorPosition[i].Item1 - beaconPosition[i].Item1) 
                    + (Math.Abs(sensorPosition[i].Item2 - beaconPosition[i].Item2))) * 2) + 1;
                long yOffset = Math.Abs(sensorPosition[i].Item2 - lookupRow);
                long widthAtLine = xWidth - (yOffset * 2);

                if (beaconPosition[i].Item2 == lookupRow && !beaconOffset.Contains(beaconPosition[i]))
                    beaconOffset.Add(beaconPosition[i]); 

                if ((xWidth - 1)/ 2 > yOffset)
                {
                    long xStart = sensorPosition[i].Item1 - ((widthAtLine - 1) / 2);
                    long xEnd = sensorPosition[i].Item1 + ((widthAtLine - 1) / 2);

                    lengthPairs.Add((xStart, xEnd));
                }
            }

            for (int i = -9999999; i < 9999999; i++)
            {
                for (int j = 0; j < lengthPairs.Count; j++)
                {
                    if (lengthPairs[j].Item1 <= i && i <= lengthPairs[j].Item2)
                    {
                        countA++;
                        break;
                    }
                }
            }

            Console.WriteLine($"P1: {countA - beaconOffset.Count}");

            (int, int)[] directions = { (-1, -1), (-1, 1), (1, -1), (1, 1) };
            bool found = false;

            for (int i = 0; i < sensorPosition.Count; i++)
            {
                long dist = Math.Abs(sensorPosition[i].Item1 - beaconPosition[i].Item1)
                    + Math.Abs(sensorPosition[i].Item2 - beaconPosition[i].Item2);

                for (long dX = 0; dX <= dist + 2; dX++)
                {
                    long dY = (dist + 1) - dX;
                    foreach (var direction in directions)
                    {
                        long newX = sensorPosition[i].Item1 + (dX + direction.Item1);
                        long newY = sensorPosition[i].Item2 + (dY + direction.Item2);

                        if (!(0 <= newX && newX <= 4000000 && 0 <= newY && newY <= 4000000)) continue;
                        if (CheckIfValid(newX, newY, sensorPosition, beaconPosition) && !found)
                        {
                            Console.WriteLine($"P2: {newX * 4000000 + newY}");
                            found = true;
                            break;
                        } 
                    }

                    if (found) break;
                }

                if (found) break;
            }

            Console.ReadLine();
        }

        public static bool CheckIfValid(long x, long y, List<(long, long)> sensors, List<(long, long)> beacons)
        {
            for (int i = 0; i < sensors.Count; i++)
            {
                long sensorBeaconDiff = Math.Abs(sensors[i].Item1 - beacons[i].Item1)
                                         + Math.Abs(sensors[i].Item2 - beacons[i].Item2);
                long diffXY = Math.Abs(x - sensors[i].Item1) + Math.Abs(y - sensors[i].Item2);
                if (diffXY <= sensorBeaconDiff) return false;
            }
            return true;
        }
    }
}
