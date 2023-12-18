namespace AoC2023.Days
{
    internal class Day13
    {
        private const int _day = 13;

        static int CheckRows(List<string> rows)
        {
            var possible = new List<int>();

            for (int i = rows.Count / 2 - 1; i < rows.Count - 1; i++)
            {
                if (rows[i] == rows[i + 1])
                    possible.Add(i);
            }

            foreach (var pos in possible)
            {
                if (pos < (rows.Count / 2 - 1) && rows[0] == rows[rows.Count - 2])
                    return pos + 1;
                else if (pos > (rows.Count / 2 - 1) && rows[1] == rows[rows.Count - 1])
                    return pos + 1;
            }

            return -1;
        }

        static int CheckColumns(List<string> rows)
        {
            var cols = new List<string>(new string[rows[0].Length]);
            for (int y = 0; y < rows.Count; y++)
            {
                for (int x = 0; x < rows[0].Length; x++)
                {
                    cols[x] += rows[y][x];
                }
            }

            var possible = new List<int>();

            for (int i = cols.Count / 2 - 1; i < cols.Count - 1; i++)
            {
                if (cols[i] == cols[i + 1])
                    possible.Add(i);
            }

            foreach (var pos in possible)
            {
                if (pos < (cols.Count / 2 - 1) && cols[0] == cols[cols.Count - 2])
                    return pos + 1;
                else if (pos > (cols.Count / 2 - 1) && cols[1] == cols[cols.Count - 1])
                    return pos + 1;
            }

            return -1;
        }

        public static string A()
        {
            var sum = 0;
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var allRows = new List<List<string>> { new List<string>() };
            var i = 0;
            foreach (var row in input)
            {
                if (string.IsNullOrWhiteSpace(row))
                {
                    i++;
                    allRows.Add(new List<string>());
                }
                else
                    allRows[i].Add(row);
            }

            foreach (var rows in allRows)
            {
                var x = CheckRows(rows);
                if (x != -1)
                    sum += x * 100;
                else
                    sum += CheckColumns(rows);
            }

            return sum.ToString();
        }

        public static string B()
        {
            return "";
        }
    }
}
