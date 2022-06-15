// https://contest.yandex.ru/contest/25597/run-report/68967337/
using System;

namespace A_LevenshteinDistance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s1 = Console.ReadLine();
            string s2 = Console.ReadLine();

            var result = GetLevenshteinDistance(s1, s2);

            Console.WriteLine(result);
        }

        public static int GetLevenshteinDistance(string s1, string s2)
        {
            int n = s1.Length;
            int m = s2.Length;
            int[,] dp = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else if (j == 0)
                    {
                        dp[i, j] = i;
                    }
                    else if (i == 0)
                    {
                        dp[i, j] = j;
                    }
                    else
                    {
                        int temp = Math.Min(dp[i, j - 1], dp[i - 1, j]);
                        dp[i, j] = Math.Min(temp + 1, dp[i - 1, j - 1] + (s1[i - 1] == s2[j - 1] ? 0 : 1));
                    }
                }
            }

            return dp[n, m];
        }
    }
}
