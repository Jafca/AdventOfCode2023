using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC2023.Days
{
    internal class Day6
    {
        private const int _day = 6;

        public static string A()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var times = new Regex(@"\d+").Matches(input[0]).Select(m => int.Parse(m.Value)).ToList();
            var distances = new Regex(@"\d+").Matches(input[1]).Select(m => int.Parse(m.Value)).ToList();
            var wins = new List<int>(new int[times.Count()]);

            for (int i = 0; i < times.Count(); i++)
            {
                var distance = distances[i];
                for (int held = 0; held < times[i]; held++)
                    if (held * (times[i] - held) > distance)
                        wins[i]++;
            }

            return wins.Aggregate((x, y) => x * y).ToString();
        }

        public static string B()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var times = new Regex(@"\d+").Matches(input[0]).Select(m => m.Value).ToList();
            var distances = new Regex(@"\d+").Matches(input[1]).Select(m => m.Value).ToList();
            var sum = 0;

            var time = BigInteger.Parse(string.Join("",times));
            var distance = BigInteger.Parse(string.Join("", distances));
            for (int held = 0; held < time; held++)
                if (held * (time - held) > distance)
                    sum++;

            return sum.ToString();
        }
    }
}
