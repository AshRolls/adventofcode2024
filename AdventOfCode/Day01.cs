using System.Linq;
using System.Security.Principal;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day01()
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
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        for (int i = 0; i < _input.Length; i++)
        {
            var nums = AoCHelper.GetNumsFromStr(_input[i]);
            left.Add(nums[0]);
            right.Add(nums[1]);
        }

        left.Sort();
        right.Sort();

        int total = 0;
        for(int i = 0; i < left.Count; i++)
        {
            total += Math.Abs(left[i] - right[i]);
        }

        _partOne = total.ToString();
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        for (int i = 0; i < _input.Length; i++)
        {
            var nums = AoCHelper.GetNumsFromStr(_input[i]);
            left.Add(nums[0]);
            right.Add(nums[1]);
        }

        left.Sort();
        right.Sort();

        double total = 0;
        for (int i = 0; i < left.Count; i++)
        {
            int occ = right.Where(x => x.Equals(left[i])).Count();
            total += left[i] * occ;
        }

        _partTwo = total.ToString();
    }
}
