//https://contest.yandex.ru/contest/25597/run-report/69009142/


/*
-- ПРИНЦИП РАБОТЫ --
Для решения я воспользовался динамическим программированием.
Представил, что мне нужно решить классическую задачу о рюкзаке для sum/2, где sum - 
это сумма очков за все выигранные партии.

Я вдохновился идеей решения из обсуждения https://leetcode.com/problems/partition-equal-subset-sum/discuss/

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Если удается найти сумму очков за партии, равную полусумме всех очков, значит, можем разделить
данные партии на 2 части. В первой части будут те, которые составляют данную полусумму, во второй - 
оставшиеся.

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
O(mn), где m - полусумма всех очков, n - количество партий.

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
O(m), так как для хранения использую 2 строчки длины sum + 1. (sum == m, полусумма всех очков).

*/




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace B_EqualSums
{
    internal class Program
    {
        private static TextReader _reader;
        private static TextWriter _writer;
        static void Main(string[] args)
        {
            InitialiseStreams();

            var n = ReadInt();
            var nums = ReadList();

            int sum = nums.Sum();

            if (sum % 2 != 0)
            {
                _writer.WriteLine(false);
                CloseStreams();
                return;
            }

            sum /= 2;
            bool[,] dp = new bool[2, sum + 1];

            dp[0, 0] = true;
            dp[1, 0] = true;


            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j <= sum; j++)
                {
                    if (j < nums[i])
                    {
                        dp[i & 1, j] = dp[(i + 1) & 1, j];
                    }
                    else
                    {
                        dp[i & 1, j] = dp[(i + 1) & 1, j - nums[i]] || dp[(i + 1) & 1, j];
                    }
                }
            }

            _writer.WriteLine(dp[0, sum]);

            CloseStreams();
        }

        private static void CloseStreams()
        {
            _reader.Close();
            _writer.Close();
        }

        private static void InitialiseStreams()
        {
            _reader = new StreamReader(Console.OpenStandardInput());
            _writer = new StreamWriter(Console.OpenStandardOutput());
        }


        private static int ReadInt()
        {
            return int.Parse(_reader.ReadLine());
        }

        private static List<int> ReadList()
        {
            return _reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }
}
