
using System.Buffers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day03()
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
        int total = 0;
        foreach (var input in _input)
        {
            foreach (string pots in getBetween(input, "mul(", ")"))
            {
                //var nums = AoCHelper.GetNumsFromStr(pots);
                var nums = pots.Split(',');

                if (nums.Length == 2 && int.TryParse(nums[0], out int x) && int.TryParse(nums[1], out int y))
                {
                    total += x * y;
                }
            }
        }

        _partOne = total.ToString();
    }

    public static List<string> getBetween(string strSource, string strStart, string strEnd)
    {
        var spanText = strSource.AsSpan();
        var offset = 0;
        int index = spanText.IndexOf(strStart);
        List<int> potIdx = new List<int>();
        List<string> potStrs = new List<string>();

        while (index != -1)
        {
            potIdx.Add(index + strStart.Length + offset);
            offset += index + strStart.Length;
            spanText = spanText[(index + strStart.Length)..];
            index = spanText.IndexOf(strStart);
        }

        foreach(int idx in potIdx)
        {
            var start = strSource.IndexOf(strStart, idx);
            var end = strSource.IndexOf(strEnd, idx);
            if (start == -1 || end < start)
            {
                potStrs.Add(strSource[idx .. end].ToString());
            }   
        }

        return potStrs;
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        _partTwo = "Not Solved";
    }
}
