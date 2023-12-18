namespace AoC2023.Days
{
    internal class Day1
    {
        private const int _day = 1;

        public static string A()
        {
            var sum = 0;
            string line;
            using (var sr = new StreamReader($"../../../Inputs/Day{_day}.txt"))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    var digits = line.Where(char.IsDigit);
                    sum += int.Parse($"{digits.First()}{digits.Last()}");

                    line = sr.ReadLine();
                }
            }
            return sum.ToString();
        }

        public static string B()
        {
            var nums = new Dictionary<string, int>
            {
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
            };
            var sum = 0;
            string line;
            using (var sr = new StreamReader($"../../../Inputs/Day{_day}.txt"))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    var a = nums.Keys.Where(line.Contains).OrderBy(line.IndexOf).First();
                    var b = nums.Keys.Where(line.Contains).OrderBy(line.LastIndexOf).Last();
                    sum += int.Parse($"{nums[a]}{nums[b]}");

                    line = sr.ReadLine();
                }
            }

            return sum.ToString();
        }
    }
}
