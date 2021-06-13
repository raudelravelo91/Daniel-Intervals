using System;
using System.Collections.Generic;
using System.Linq;

namespace Daniel_Intervals
{
    class Program
    {
        static void Main(string[] args)
        {
            var intervals = new List<Interval>()
            {
                // start, end, money
                new Interval(2,5,3),
                new Interval(5,8,4),
                new Interval(7,10,8),
                new Interval(8,10,5)
            };
            int start = 1;
            int end = 10;

            var sol = new Solution();
            Console.WriteLine(sol.FindMaxPay(intervals, start, end));
        }
    }

    public class Solution
    {
        public int FindMaxPay(List<Interval> intervals, int start, int end)
        {
            intervals = intervals.Where(inter => inter.start >= start && inter.end <= end).ToList(); //ignore out of range
            intervals.Sort(); //sort by end date
            int[] bestSol = new int[intervals.Count];
            bestSol[0] = intervals[0].money;

            for (int i = 1; i < intervals.Count; i++)
            {
                bestSol[i] = bestSol[i - 1];

                int currentStart = intervals[i].start;
                for(int j = i-1; j >= 0; j--) //this is the n^2 sol but you can do a lower bound here to find this value and make it nlogn
                {
                    int previousEnd = intervals[j].end;
                    if(previousEnd < currentStart)
                    {
                        int tempSol = bestSol[j] + intervals[i].money;//possible best solution for position i
                        bestSol[i] = Math.Max(bestSol[i], tempSol);//update if bigger than current at position i
                    }
                }
            }

            return bestSol[bestSol.Length - 1];
        }
    }


    public class Interval:IComparable<Interval>
    {
        public int start, end, money;

        public Interval(int start, int end, int money)
        {
            this.start = start;
            this.end = end;
            this.money = money;
        }

        public int CompareTo(Interval other)
        {
            return end.CompareTo(other.end);
        }

        public override string ToString()
        {
            return $"[{start},{end}] ${money}";
        }
    }
}
