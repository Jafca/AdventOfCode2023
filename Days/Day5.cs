using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC2023.Days
{
    internal class Day5
    {
        private const int _day = 5;

        internal class SourceDest
        {
            public BigInteger Source;
            public BigInteger Dest;
            public BigInteger Range;
        }

        public static string A()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var seed = new List<SourceDest>();
            var soil = new List<SourceDest>();
            var fertilizer = new List<SourceDest>();
            var water = new List<SourceDest>();
            var light = new List<SourceDest>();
            var temperature = new List<SourceDest>();
            var humidity = new List<SourceDest>();
            var allLists = new Dictionary<string, List<SourceDest>>()
            {
                { "seed", seed },
                { "soil", soil },
                { "fertilizer", fertilizer },
                { "water", water },
                { "light", light },
                { "temperature", temperature },
                { "humidity", humidity },
            };

            var seeds = new Regex(@"\d+").Matches(input[0]).Select(m => BigInteger.Parse(m.Value)).ToList();

            var current = "";
            for (int i = 1; i < input.Count; i++)
            {
                var line = input[i];
                switch (line)
                {
                    case "":
                        break;
                    case "seed-to-soil map:":
                        current = "seed";
                        break;
                    case "soil-to-fertilizer map:":
                        current = "soil";
                        break;
                    case "fertilizer-to-water map:":
                        current = "fertilizer";
                        break;
                    case "water-to-light map:":
                        current = "water";
                        break;
                    case "light-to-temperature map:":
                        current = "light";
                        break;
                    case "temperature-to-humidity map:":
                        current = "temperature";
                        break;
                    case "humidity-to-location map:":
                        current = "humidity";
                        break;
                    default:
                        var pos = new Regex(@"\d+").Matches(input[i]).Select(m => BigInteger.Parse(m.Value)).ToList();
                        allLists[current].Add(new SourceDest { Source = pos[1], Dest = pos[0], Range = pos[2] });
                        break;
                }
            }

            BigInteger min = int.MaxValue;
            foreach (var s in seeds)
            {
                BigInteger pos = s;
                BigInteger newPos = -1;
                foreach (var l in allLists)
                {
                    foreach (var sd in l.Value)
                    {
                        if (pos >= sd.Source && pos < sd.Source + sd.Range)
                        {
                            newPos = sd.Dest + (pos - sd.Source);
                            break;
                        }
                    }
                    if (newPos != -1)
                    {
                        pos = newPos;
                        newPos = -1;
                    }
                }

                if (pos < min)
                    min = pos;
            }

            return min.ToString();
        }

        public static string B()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var seed = new List<SourceDest>();
            var soil = new List<SourceDest>();
            var fertilizer = new List<SourceDest>();
            var water = new List<SourceDest>();
            var light = new List<SourceDest>();
            var temperature = new List<SourceDest>();
            var humidity = new List<SourceDest>();
            var allLists = new Dictionary<string, List<SourceDest>>()
            {
                { "seed", seed },
                { "soil", soil },
                { "fertilizer", fertilizer },
                { "water", water },
                { "light", light },
                { "temperature", temperature },
                { "humidity", humidity },
            };

            var current = "";
            for (int i = 1; i < input.Count; i++)
            {
                var line = input[i];
                switch (line)
                {
                    case "":
                        break;
                    case "seed-to-soil map:":
                        current = "seed";
                        break;
                    case "soil-to-fertilizer map:":
                        current = "soil";
                        break;
                    case "fertilizer-to-water map:":
                        current = "fertilizer";
                        break;
                    case "water-to-light map:":
                        current = "water";
                        break;
                    case "light-to-temperature map:":
                        current = "light";
                        break;
                    case "temperature-to-humidity map:":
                        current = "temperature";
                        break;
                    case "humidity-to-location map:":
                        current = "humidity";
                        break;
                    default:
                        var parts = new Regex(@"\d+").Matches(input[i]).Select(m => BigInteger.Parse(m.Value)).ToList();
                        allLists[current].Add(new SourceDest { Source = parts[1], Dest = parts[0], Range = parts[2] });
                        break;
                }
            }

            var seedPairs =
                new Regex(@"\d+").Matches(input[0])
                .Select(m => BigInteger.Parse(m.Value)).Chunk(2);

            BigInteger min = 0;
            BigInteger pos = 0;
            BigInteger newPos = -1;
            var found = false;
            for (int i = 79004093; i < int.MaxValue; i++)
            {
                pos = i;
                newPos = -1;
                foreach (var l in allLists.Reverse())
                {
                    foreach (var sd in l.Value)
                    {
                        if (pos >= sd.Dest && pos < sd.Dest + sd.Range)
                        {
                            newPos = sd.Source + (pos - sd.Dest);
                            break;
                        }
                    }
                    if (newPos != -1)
                    {
                        pos = newPos;
                        newPos = -1;
                    }
                }

                foreach (var sp in seedPairs)
                {
                    if (sp[0] <= pos && pos < sp[0] + sp[1])
                    {
                        min = i;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }

            return min.ToString();
        }
    }
}
