using System.Text;

namespace AoC2023.Days
{
    internal class Day11
    {
        private const int _day = 11;

        internal class Galaxy
        {
            public int X;
            public int Y;
        }

        static int Calc(int expand = 2)
        {
            var sum = 0;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var addedRows = new List<string>();
            var addedCols = new List<string>();

            foreach (var line in input)
            {
                addedRows.Add(line);
                if (!line.Contains('#'))
                    for (int i = 1; i < expand; i++)
                        addedRows.Add(new string('.', line.Length));
            }

            for (int x = 0; x < addedRows[0].Length; x++)
            {
                var col = new StringBuilder();
                for (int y = 0; y < addedRows.Count; y++)
                    col.Append(addedRows[y][x]);

                addedCols.Add(col.ToString());
                if (!col.ToString().Contains('#'))
                    for (int i = 1; i < expand; i++)
                        addedCols.Add(new string('.', col.Length));
            }

            var galaxies = new List<Galaxy>();

            for (int x = 0; x < addedCols[0].Length; x++)
                for (int y = 0; y < addedCols.Count; y++)
                    if (addedCols[y][x] == '#')
                        galaxies.Add(new Galaxy { X = x, Y = y });

            foreach (var g in galaxies)
                foreach (var g2 in galaxies)
                    if (g != g2)
                        sum += Math.Abs(g.X - g2.X) + Math.Abs(g.Y - g2.Y);

            return sum / 2;
        }

        public static string A()
        {
            return Calc(2).ToString();
        }

        public static string B()
        {
            double expand = 1000000;
            double diff = Calc(2) - Calc(1); // 593813
            double start = Calc(1) - diff; // 8230983
            double result = start + diff * expand;
            return result.ToString(); 
        }
    }
}
