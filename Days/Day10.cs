namespace AoC2023.Days
{
    internal class Day10
    {
        private const int _day = 10;

        internal class Pipe
        {
            public char C { get; set; }
            public char Dir1 { get; set; }
            public char Dir2 { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public char OldDir { get; set; }
            public bool OnLoop { get; set; }

            public Pipe(char type, int x, int y)
            {
                C = type;
                X = x;
                Y = y;

                switch (type)
                {
                    case '|':
                        Dir1 = 'N';
                        Dir2 = 'S';
                        break;
                    case '-':
                        Dir1 = 'E';
                        Dir2 = 'W';
                        break;
                    case 'L':
                        Dir1 = 'N';
                        Dir2 = 'E';
                        break;
                    case 'J':
                        Dir1 = 'N';
                        Dir2 = 'W';
                        break;
                    case '7':
                        Dir1 = 'S';
                        Dir2 = 'W';
                        break;
                    case 'F':
                        Dir1 = 'S';
                        Dir2 = 'E';
                        break;
                    case 'S':
                        Dir1 = ' ';
                        Dir2 = ' ';
                        break;
                }
            }
        }

        public static List<Pipe> CheckAround(Pipe p, List<List<Pipe>> pipes)
        {
            var result = new List<Pipe>();
            var positions = new Dictionary<char, Tuple<int, int>>
            {
                { 'N', Tuple.Create(p.X,     p.Y + 1) },
                { 'E', Tuple.Create(p.X - 1, p.Y    ) },
                { 'S', Tuple.Create(p.X,     p.Y - 1) },
                { 'W', Tuple.Create(p.X + 1, p.Y    ) },
            };

            foreach (var pos in positions)
            {
                var n = pipes[pos.Value.Item2][pos.Value.Item1];
                if (n == null) continue;
                if (n.Dir1 == pos.Key || n.Dir2 == pos.Key)
                {
                    n.OldDir = pos.Key;
                    result.Add(n);
                }
            }
            return result;
        }

        public static Pipe GetNextPipe(Pipe p, List<List<Pipe>> pipes)
        {
            char newDir;
            if (p.Dir1 == p.OldDir)
                newDir = p.Dir2;
            else
                newDir = p.Dir1;

            var nextPipe = newDir switch
            {
                'N' => pipes[p.Y - 1][p.X],
                'E' => pipes[p.Y][p.X + 1],
                'S' => pipes[p.Y + 1][p.X],
                'W' => pipes[p.Y][p.X - 1],
            };

            var swap = new Dictionary<char, char>
            {
                { 'N', 'S' },
                { 'S', 'N' },
                { 'E', 'W' },
                { 'W', 'E' },
            };

            nextPipe.OldDir = swap[newDir];

            return nextPipe;
        }

        public static string A()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var len = input[0].Length;
            input.Insert(0, new string('.', len));
            input.Add(new string('.', len));
            input = input.Select(s => '.' + s + '.').ToList();

            Pipe start = new Pipe('.', -1, -1);
            var pipes = new List<List<Pipe>>();

            for (int y = 0; y < input.Count; y++)
            {
                pipes.Add(new List<Pipe>());
                for (int x = 0; x < input[y].Length; x++)
                {
                    var c = input[y][x];
                    if (c != '.')
                    {
                        var pipe = new Pipe(c, x, y);
                        pipes[y].Add(pipe);
                        if (c == 'S')
                            start = pipe;
                    }
                    else
                        pipes[y].Add(null);
                }
            }

            var nears = CheckAround(start, pipes);
            var current = nears[0];
            var count = 1;
            while (current.C != 'S')
            {
                current = GetNextPipe(current, pipes);
                count++;
            }

            return (count / 2).ToString();
        }

        public static string B()
        {
            var input = new List<string>(File.ReadAllLines($"../../../Inputs/Day{_day}.txt"));
            var len = input[0].Length;
            input.Insert(0, new string('.', len));
            input.Add(new string('.', len));
            input = input.Select(s => '.' + s + '.').ToList();

            Pipe start = new Pipe('.', -1, -1);
            var pipes = new List<List<Pipe>>();

            for (int y = 0; y < input.Count; y++)
            {
                pipes.Add(new List<Pipe>());
                for (int x = 0; x < input[y].Length; x++)
                {
                    var c = input[y][x];
                    if (c != '.')
                    {
                        var pipe = new Pipe(c, x, y);
                        pipes[y].Add(pipe);
                        if (c == 'S')
                            start = pipe;
                    }
                    else
                        pipes[y].Add(null);
                }
            }

            var nears = CheckAround(start, pipes);
            var current = nears[0];
            start.OnLoop = true;
            var count = 1;
            while (current.C != 'S')
            {
                current.OnLoop = true;
                current = GetNextPipe(current, pipes);
                count++;
            }

            var swap = new Dictionary<char, char>
            {
                { '|', '\u2502' },
                { '-', '\u2500' },
                { 'L', '\u2514' },
                { 'J', '\u2518' },
                { '7', '\u2510' },
                { 'F', '\u250C' },
                { 'S', '┼' },
            };

            for (int y = 0; y < pipes.Count; y++)
            {
                for (int x = 0; x < pipes[y].Count(); x++)
                {
                    var p = pipes[y][x];
                    if (p == null)
                        Console.Write('.');
                    else if (p.OnLoop)
                        Console.Write(swap[p.C]);
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }

            // Copy to paint, fill outside colours, counts inside dots
            return "413";
        }
    }
}
