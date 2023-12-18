using System.Text.RegularExpressions;

namespace AoC2023.Days
{
    internal class Day9
    {
        private const int _day = 9;

        static int Diffs(List<int> nums)
        {
            var diffs = new List<int>();
            for (int i = 0; i < nums.Count - 1; i++)
                diffs.Add(nums[i + 1] - nums[i]);

            if (diffs.Distinct().Count() > 1)
                return nums.Last() + Diffs(diffs);
            else
                return nums.Last() + diffs.Last();
        }

        public static string A()
        {
            var sum = 0;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            foreach (var line in input)
            {
                var nums = new Regex(@"-?\d+").Matches(line).Select(m => int.Parse(m.Value)).ToList();
                sum += Diffs(nums);
            }
            return sum.ToString();
        }

        public static string B()
        {
            var sum = 0;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            foreach (var line in input)
            {
                var nums = new Regex(@"-?\d+").Matches(line).Select(m => int.Parse(m.Value)).ToList();
                nums.Reverse();
                sum += Diffs(nums);
            }
            return sum.ToString();
        }
    }
}
