

using System.ComponentModel.Design;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day02()
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
        int safeCount = 0;
        foreach (var input in _input)
        {
            var nums = AoCHelper.GetNumsFromStr(input);
            bool negative = (nums[1] - nums[0] < 0);
            bool safe = true;
            for (int x = 0; x < nums.Length - 1; x++)
            {
                int diff = nums[x] - nums[x + 1];
                if (diff == 0 ) { safe = false; break; }
                if (negative)
                {
                    if (diff > 3 || diff <= 0)
                    {
                        safe = false; break;
                    }
                }
                else if (diff < -3 || diff >= 0)
                {
                    safe = false; break;
                }           
            }
            if (safe) { safeCount++; }
        }
        
        _partOne = safeCount.ToString();
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        int safeCount = 0;
        foreach (var input in _input)
        {
            var nums = AoCHelper.GetNumsFromStr(input);
            if (checkSequence(nums)) { safeCount++; continue; }

            for (int x = 0; x < nums.Length; x++)
            {
                var numsRemoved = nums.ToList();
                numsRemoved.RemoveAt(x);
                if (checkSequence(numsRemoved.ToArray())) { safeCount++; break; }
            }
        }

        _partTwo = safeCount.ToString();
    }

    private bool getNegative(int[] nums)
    {
        return (nums[1] - nums[0] < 0);
    }

    private bool checkSequence(int[] nums)
    {
        bool negative = getNegative(nums);
        for (int x = 0; x < nums.Length - 1; x++)
        {
            if (!checkValid(nums[x], nums[x + 1], negative)) return false;
        }
        return true;
    }

    private bool checkValid(int v1, int v2, bool negative)
    {
        int diff = v1 - v2;
        if (diff == 0)
        {
            return false;
        }
        if (negative)
        {
            if (diff > 3 || diff <= 0)
            {
                return false;
            }
        }
        else if (diff < -3 || diff >= 0)
        {
            return false;
        }
        return true;
    }
}
