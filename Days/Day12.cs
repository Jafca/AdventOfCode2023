using System.Data;

namespace AoC2023.Days
{
    internal class Day12
    {
        private const int _day = 12;

        internal class Row
        {
            public string Springs;
            public List<int> Sizes;
        }

        public static string A()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var rows = new List<Row>();
            foreach (var row in input)
            {
                rows.Add(new Row { 
                    Springs = row.Split(' ')[0],
                    Sizes = row.Split(' ')[1].Split(',').Select(int.Parse).ToList()
                });
            }

            return "";
        }

        public static string B()
        {
            return "";
        }
    }
}
