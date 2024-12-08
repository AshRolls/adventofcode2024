
namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        solve1();
        return new(_partOne);
    }

    private void solve1()
    {
        int h = _input.Length;
        int w = _input[0].Length;
        char[,] grid = new char[h + 6, w + 6];

        parseGrid(h, w, grid);

        int matches = 0;
        for (int y = 0; y < h + 6; y++)
        {
            //Console.Out.WriteLine();
            for (int x = 0; x < w + 6; x++)
            {
                //Console.Out.Write(grid[x, y]);
                if (grid[x, y] == 'X')
                {
                    matches += checkX(grid, x, y);
                }
            }
        }

        _partOne = matches.ToString();
    }

    private void parseGrid(int h, int w, char[,] grid)
    {
        for (int y = 0; y < h + 6; y++)
        {
            for (int x = 0; x < w + 6; x++)
            {
                grid[x, y] = '.';
            }
        }

        for (int y = 3; y < h + 3; y++)
        {
            for (int x = 3; x < w + 3; x++)
            {
                grid[x, y] = _input[y - 3][x - 3];
            }
        }
    }

    private int checkX(char[,] grid, int x, int y)
    {
        int matches = 0;
        foreach (Tuple<int,int> dir in AoCHelper.Dirs)
        {
            if (grid[x + dir.Item1, y + dir.Item2] == 'M' 
                && grid[x + dir.Item1 * 2, y + dir.Item2 * 2] == 'A' 
                && grid[x + dir.Item1 * 3, y + dir.Item2 * 3] == 'S') matches++;
        }
        return matches;
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        int h = _input.Length;
        int w = _input[0].Length;
        char[,] grid = new char[h + 6, w + 6];

        parseGrid(h, w, grid);

        int matches = 0;
        for (int y = 0; y < h + 6; y++)
        {
            //Console.Out.WriteLine();
            for (int x = 0; x < w + 6; x++)
            {
                //Console.Out.Write(grid[x, y]);
                if (grid[x, y] == 'A')
                {
                    if (checkA(grid, x, y)) matches++;
                }
            }
        }

        _partTwo = matches.ToString();
    }

    private bool checkA(char[,] grid, int x, int y)
    { 
        bool first = false;
        if ((grid[x + 1, y + 1] == 'M' && grid[x - 1, y - 1] == 'S') || (grid[x + 1, y + 1] == 'S' && grid[x - 1, y - 1] == 'M')) first = true;

        bool second = false;
        if ((grid[x - 1, y + 1] == 'M' && grid[x + 1, y - 1] == 'S') || (grid[x - 1, y + 1] == 'S' && grid[x + 1, y - 1] == 'M')) second = true;

        return first && second;
    }
}
