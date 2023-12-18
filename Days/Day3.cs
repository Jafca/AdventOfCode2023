using System.Text.RegularExpressions;

namespace AoC2023.Days
{
    internal class Day3
    {
        private const int _day = 3;

        public static string A()
        {
            var sum = 0;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    var x = input[row][col];
                    if (char.IsDigit(x))
                    {
                        var num = x.ToString();
                        for (int col2 = col + 1; col2 < input[row].Length; col2++)
                        {
                            var y = input[row][col2];
                            if (char.IsDigit(y))
                                num += y;
                            else
                                break;
                        }

                        var start = col - 1;
                        var len = num.Length + 2;

                        if (start < 0)
                        {
                            len -= 1;
                            start = 0;
                        }
                        else if (start + len > input[row].Length)
                        {
                            len -= 1;
                        }

                        var s =
                            input[Math.Max(0, row - 1)].Substring(Math.Max(0, start), len) +
                            input[row].Substring(Math.Max(0, start), len) +
                            input[Math.Min(row + 1, input.Count - 1)].Substring(Math.Max(0, start), len);


                        if (new Regex(@"[^\.\d]").Matches(s).Any())
                            sum += int.Parse(num);

                        col += num.Length;
                    }
                }
            }
            return sum.ToString();
        }

        public static string B()
        {
            var sum = 0;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    var x = input[row][col];
                    if (x == '*')
                    {
                        var gearPos = new List<int> { col - 1, col, col + 1 };
                        var nearby = new List<string>();

                        var nums = new Regex(@"\d+").Matches(input[row]).Select(m => m.Value);
                        var start = 0;
                        foreach (var num in nums)
                        {
                            var numberPos = Enumerable.Range(input[row].IndexOf(num, start), num.Length);
                            if (numberPos.Intersect(gearPos).Any())
                                nearby.Add(num);

                            start = numberPos.Last() + 1;
                        }

                        if (row != 0)
                        {
                            nums = new Regex(@"\d+").Matches(input[row - 1]).Select(m => m.Value);
                            start = 0;
                            foreach (var num in nums)
                            {
                                var numberPos = Enumerable.Range(input[row - 1].IndexOf(num, start), num.Length);
                                if (numberPos.Intersect(gearPos).Any())
                                    nearby.Add(num);

                                start = numberPos.Last() + 1;
                            }
                        }

                        if (row != input.Count - 1)
                        {
                            nums = new Regex(@"\d+").Matches(input[row + 1]).Select(m => m.Value);
                            start = 0;
                            foreach (var num in nums)
                            {
                                var numberPos = Enumerable.Range(input[row + 1].IndexOf(num, start), num.Length);
                                if (numberPos.Intersect(gearPos).Any())
                                    nearby.Add(num);

                                start = numberPos.Last() + 1;
                            }
                        }

                        if (nearby.Count == 2)
                            sum += int.Parse(nearby[0]) * int.Parse(nearby[1]);

                    }
                }
            }
            return sum.ToString();
        }
    }
}
