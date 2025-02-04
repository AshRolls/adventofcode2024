﻿using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace AdventOfCode
{
    internal static class AoCHelper
    {
        public static int[] GetNumsFromStr(string s) => Array.ConvertAll(Regex.Matches(s, @"-?\d+").OfType<Match>().Select(m => m.Groups[0].Value).ToArray(), x => int.Parse(x));
        public static long[] GetLongNumsFromStr(string s) => Array.ConvertAll(Regex.Matches(s, @"-?\d+").OfType<Match>().Select(m => m.Groups[0].Value).ToArray(), x => long.Parse(x));
        public static int[] GetSingleNumsFromStr(string s) => Array.ConvertAll(Regex.Matches(s, @"-?\d{1}").OfType<Match>().Select(m => m.Value).ToArray(), x => int.Parse(x));

        public static string ToDebugString<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            //Console.Out.WriteLine(ToDebugString<string,int>(clonedVisited) + " " + maxSmallCaves);
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static int GetCommonDivisor(int a, int b)
        {
            if (a == 0)
                return b;
            return GetCommonDivisor(b % a, a);
        }

        public static long GetCommonDivisor(long a, long b)
        {
            if (a == 0)
                return b;
            return GetCommonDivisor(b % a, a);
        }

        //recursive implementation
        public static int LeastCommonMultipleOfArray(int[] arr, int idx)
        {
            // lcm(a,b) = (a*b/gcd(a,b))
            if (idx == arr.Length - 1)
            {
                return arr[idx];
            }
            int a = arr[idx];
            int b = LeastCommonMultipleOfArray(arr, idx + 1);
            return (a * b / GetCommonDivisor(a, b));
        }

        public static long LeastCommonMultipleOfArray(long[] arr, int idx)
        {
            // lcm(a,b) = (a*b/gcd(a,b))
            if (idx == arr.Length - 1)
            {
                return arr[idx];
            }
            long a = arr[idx];
            long b = LeastCommonMultipleOfArray(arr, idx + 1);
            return (a * b / GetCommonDivisor(a, b));
        }

        public static IEnumerable<long> GetAllDivisors(long[] arr, long N)
        {
            // Variable to find the gcd
            // of N numbers
            long g = arr[0];

            // Set to store all the
            // common divisors
            HashSet<long> divisors = new HashSet<long>();

            // Finding GCD of the given
            // N numbers
            for (int i = 1; i < N; i++)
            {
                g = GetCommonDivisor(arr[i], g);
            }

            // Finding divisors of the
            // HCF of n numbers
            for (int i = 1; i * i <= g; i++)
            {
                if (g % i == 0)
                {
                    divisors.Add(i);
                    if (g / i != i)
                        divisors.Add(g / i);
                }
            }

            return divisors;
        }

        public static int GetManhattanDist(int fromX, int fromY, int toX, int toY)
        {
            return Math.Abs(fromX - toX) + Math.Abs(fromY - toY);
        }

        // Chebyshev distance (diagonal move costs the same as lateral)
        public static int GetDiagonalDist(int fromX, int fromY, int toX, int toY)
        {
            int dx = Math.Abs(fromX - toX);
            int dy = Math.Abs(fromY - toY);
            return (dx + dy) - Math.Min(dx, dy);
        }

        public const char SolidBlockChar = '\u2588';

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static int GetSumRange(int a, int b)
        {
            return (b * (b + 1) - (a - 1) * a) / 2;
        }

        /// <summary>
        /// An optimized method using an array as buffer instead of 
        /// string concatenation. This is faster for return values having 
        /// a length > 1.
        /// </summary>
        public static string IntToBaseString(int value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int i = 32;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }

        public static string StripWhiteSpace(string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        public static Tuple<int, int>[] Dirs = {
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(1, 1),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(1, -1),
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(-1, -1),
            new Tuple<int, int>(-1, 0),
            new Tuple<int, int>(-1, 1)
        };
    }

    public static class ListExtensions
    {
        public static int FindIndexOfFirstValueLessThan<T>(this List<T> sortedList, T value, IComparer<T> comparer = null)
        {
            var index = sortedList.BinarySearch(value, comparer);

            // The value was found in the list. Just return its index.
            if (index >= 0)
                return index;

            // The value was not found and "~index" is the index of the next value greater than the search value.
            index = ~index;

            // There are values in the list less than the search value. Return the index of the closest one.
            if (index > 0)
                return index - 1;

            // All values in the list are greater than the search value.
            return -1;
        }
    }
}
