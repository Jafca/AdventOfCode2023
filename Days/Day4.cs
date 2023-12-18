using System.Text.RegularExpressions;

namespace AoC2023.Days
{
    internal class Day4
    {
        private const int _day = 4;

        public static string A()
        {
            var sum = 0D;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            foreach (var line in input)
            {
                var card = line.Split(':')[1].Split('|');
                var good = new Regex(@"\d+").Matches(card[0]).Select(m => int.Parse(m.Value));
                var mine = new Regex(@"\d+").Matches(card[1]).Select(m => int.Parse(m.Value));
                var wins = good.Intersect(mine).Count();
                var points = wins > 0 ? 1D : 0D;
                points *= Math.Pow(2, Math.Max(0, wins - 1));
                sum += points;
            }

            return sum.ToString();
        }

        public static string B()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var cardCount = new Dictionary<int, int>();
            for (int i = 0; i < input.Count; i++)
            {
                var cardNum = i + 1;
                if (cardCount.ContainsKey(cardNum))
                    cardCount[cardNum]++;
                else
                    cardCount.Add(cardNum, 1);

                var line = input[i];
                var card = line.Split(":")[1].Split('|');
                var good = new Regex(@"\d+").Matches(card[0]).Select(m => int.Parse(m.Value));
                var mine = new Regex(@"\d+").Matches(card[1]).Select(m => int.Parse(m.Value));

                var wins = good.Intersect(mine).Count();
                for (int x = 1; x <= wins; x++)
                {
                    if (cardCount.ContainsKey(cardNum + x))
                        cardCount[cardNum + x] += cardCount[cardNum];
                    else
                        cardCount.Add(cardNum + x, cardCount[cardNum]);
                }
            }

            return cardCount.Values.Sum().ToString();
        }
    }
}
