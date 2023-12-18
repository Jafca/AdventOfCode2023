using System.Text.RegularExpressions;

namespace AoC2023.Days
{
    internal class Day2
    {
        private const int _day = 2;

        public static string A()
        {
            var sum = 0;
            var lineNo = 1;
            string line;
            using (var sr = new StreamReader($"../../../Inputs/Day{_day}.txt"))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    var valid = true;

                    var patternR = new Regex(@"\d+ r");
                    var matchR = patternR.Matches(line);
                    foreach (var m in matchR)
                        if (int.Parse(m.ToString().Substring(0, m.ToString().IndexOf(' '))) > 12)
                            valid = false;

                    if (valid)
                    {
                        var patternG = new Regex(@"\d+ g");
                        var matchG = patternG.Matches(line);
                        foreach (var m in matchG)
                            if (int.Parse(m.ToString().Substring(0, m.ToString().IndexOf(' '))) > 13)
                                valid = false;
                    }

                    if (valid)
                    {
                        var patternB = new Regex(@"\d+ b");
                        var matchB = patternB.Matches(line);
                        foreach (var m in matchB)
                            if (int.Parse(m.ToString().Substring(0, m.ToString().IndexOf(' '))) > 14)
                                valid = false;
                    }

                    if (valid)
                        sum += lineNo;

                    lineNo++;
                    line = sr.ReadLine();
                }
            }
            return sum.ToString();
        }

        public static string B()
        {
            var sum = 0;
            string line;
            using (var sr = new StreamReader($"../../../Inputs/Day{_day}.txt"))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    sum +=
                        new Regex(@"(\d+) r").Matches(line)
                        .Max(m => int.Parse(m.Groups[1].ToString())) *
                        new Regex(@"(\d+) g").Matches(line)
                        .Max(m => int.Parse(m.Groups[1].ToString())) *
                        new Regex(@"(\d+) b").Matches(line)
                        .Max(m => int.Parse(m.Groups[1].ToString()));

                    line = sr.ReadLine();
                }
            }
            return sum.ToString();
        }
    }
}
