namespace AoC2023.Days
{
    internal class Day7
    {
        private const int _day = 7;

        internal class HandBid
        {
            public string Hand;
            public int Bid;
            public int Power;
            public string Value;

            public HandBid(string hand, int bid, bool joker = false)
            {
                Hand = hand;
                Bid = bid;
                Power = GetPower(hand);
                Value = GetValue(joker);

                if (joker)
                    Power = GetJokerPower();
            }

            public int GetPower(string hand)
            {
                /* 55555    7
                 * 44441    6
                 * 33322    5
                 * 33321    4
                 * 22113    3
                 * 11234    2
                 * 12345    1
                 */

                if (hand == "")
                    return 0;

                var groups = hand.GroupBy(x => x).OrderByDescending(x => x.Count());
                if (groups.First().Count() == 5)
                    return 7;
                else if (groups.First().Count() == 4)
                    return 6;
                else if (groups.Any(x => x.Count() == 3) && groups.Any(x => x.Count() == 2))
                    return 5;
                else if (groups.First().Count() == 3)
                    return 4;
                else if (groups.GroupBy(x => x.Count() == 2).Any(x => x.Count() == 2))
                    return 3;
                else if (groups.First().Count() == 2)
                    return 2;
                else
                    return 1;
            }

            public int GetJokerPower()
            {
                if (!Hand.Contains('J') || Hand == "JJJJJ")
                    return Power;

                var common = Hand.Replace("J", "").GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                var newHand = Hand.Replace("J", "") + new string(common, Hand.Count(x => x == 'J'));
                return GetPower(newHand);
            }

            Dictionary<char, char> valueMap = new Dictionary<char, char>
            {
                { 'A', 'm' },
                { 'K', 'l' },
                { 'Q', 'k' },
                { 'J', 'j' },
                { 'T', 'i' },
                { '9', 'h' },
                { '8', 'g' },
                { '7', 'f' },
                { '6', 'e' },
                { '5', 'd' },
                { '4', 'c' },
                { '3', 'b' },
                { '2', 'a' },
            };

            Dictionary<char, char> valueMapJoker = new Dictionary<char, char>
            {
                { 'A', 'm' },
                { 'K', 'l' },
                { 'Q', 'k' },
                { 'T', 'j' },
                { '9', 'i' },
                { '8', 'h' },
                { '7', 'g' },
                { '6', 'f' },
                { '5', 'e' },
                { '4', 'd' },
                { '3', 'c' },
                { '2', 'b' },
                { 'J', 'a' },
            };

            public string GetValue(bool joker)
            {
                var value = "";
                var map = joker ? valueMapJoker : valueMap;

                foreach (var c in Hand)
                    value += map[c];

                return value;
            }
        }

        public static string A()
        {
            var sum = 0D;
            var hands = new List<HandBid>();
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                hands.Add(new HandBid(parts[0], int.Parse(parts[1])));
            }

            hands = hands.OrderBy(x => x.Power).ThenBy(x => x.Value).ToList();
            for (int i = 0; i < hands.Count; i++)
                sum += hands[i].Bid * (i + 1);

            return sum.ToString();
        }

        public static string B()
        {
            var sum = 0D;
            var hands = new List<HandBid>();
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                hands.Add(new HandBid(parts[0], int.Parse(parts[1]), true));
            }

            hands = hands.OrderBy(x => x.Power).ThenBy(x => x.Value).ToList();
            for (int i = 0; i < hands.Count; i++)
                sum += hands[i].Bid * (i + 1);

            return sum.ToString();
        }
    }
}
