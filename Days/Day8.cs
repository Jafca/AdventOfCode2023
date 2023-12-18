using System.Text.RegularExpressions;

namespace AoC2023.Days
{
    internal class Day8
    {
        private const int _day = 8;

        public static string A()
        {
            var count = 0D;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var directions = input[0];

            var nodes = new Dictionary<string, Tuple<string, string>>();
            for (int i = 2; i < input.Count; i++)
            {
                var nodeParts = new Regex(@"\w+").Matches(input[i]).Select(m => m.Value).ToList();
                nodes.Add(nodeParts[0], Tuple.Create(nodeParts[1], nodeParts[2]));
            }

            var dirI = 0;
            var current = "AAA";
            while (current != "ZZZ")
            {
                if (dirI >= directions.Length)
                    dirI = 0;

                if (directions[dirI] == 'L')
                    current = nodes[current].Item1;
                else
                    current = nodes[current].Item2;

                dirI++;
                count++;
            }

            return count.ToString();
        }

        public static string B()
        {
            var count = 0;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var directions = input[0];

            var nodes = new Dictionary<string, Tuple<string, string>>();
            var startingNodes = new List<string>();
            for (int i = 2; i < input.Count; i++)
            {
                var nodeParts = new Regex(@"\w+").Matches(input[i]).Select(m => m.Value).ToList();
                nodes.Add(nodeParts[0], Tuple.Create(nodeParts[1], nodeParts[2]));
                if (nodeParts[0][2] == 'A')
                    startingNodes.Add(nodeParts[0]);
            }

            var targetCount = startingNodes.Count;
            var ends = new List<long>(new long[targetCount]);

            var dirI = 0;
            var endsFound = 0;
            while (endsFound != targetCount)
            {
                if (dirI >= directions.Length)
                    dirI = 0;

                for (int i = 0; i < startingNodes.Count; i++)
                {
                    if (directions[dirI] == 'L')
                        startingNodes[i] = nodes[startingNodes[i]].Item1;
                    else
                        startingNodes[i] = nodes[startingNodes[i]].Item2;

                    if (startingNodes[i][2] == 'Z')
                    {
                        ends[i] = count + 1;
                        endsFound++;
                    }
                }

                dirI++;
                count++;
            }

            return LCM(ends.ToArray()).ToString();
        }

        static long LCM(long[] numbers) => numbers.Aggregate(LCMPair);
        static long LCMPair(long a, long b) => Math.Abs(a * b) / GCD(a, b);
        static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);
    }
}
