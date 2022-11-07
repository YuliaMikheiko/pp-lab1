﻿using System;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int elementCount;
            int threadCount;
            Console.WriteLine("Введите кол-во потоков");
            while (!int.TryParse(Console.ReadLine(), out threadCount)
                || (threadCount < 4))
            {
                Console.WriteLine("Число введено неверно. Попробуйте ещё раз");
            }
            Console.WriteLine("Введите кол-во элементов");
            while (!int.TryParse(Console.ReadLine(), out elementCount)
                || (elementCount < 1))
            {
                Console.WriteLine("Число введено неверно. Попробуйте ещё раз");
            }
            int[] array = new int[elementCount];
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 16);
            }
            int[] returns = new int[threadCount];
            Thread[] threads = new Thread[threadCount];
            for (int i = 0; i < threads.Length; i++)
            {

                threads[i] = new Thread(() => { returns[i] = (ThreadArray(array, i, threadCount)); });
                threads[i].Start();

            }
            for (int i = 0; i < threads.Length; i++)
            {

                threads[i].Join();

            }
            int multOdd = 1;
            for (int i = 0; i < returns.Length; i++)
            {
                multOdd *= returns[i];
            }
            Console.WriteLine($"Произведение нечетных элементов: {multOdd}");
            Console.ReadKey();
        }

        static int ThreadArray(object? paramArray, object? totalI, object? threadCount)
        {
            int beg, h, end;
            int multOdd = 1;
            int[]? array = paramArray as int[];
            int nt = Convert.ToInt32(totalI);
            int count = Convert.ToInt32(threadCount);
            h = array.Length / count;
            beg = h * nt; end = beg + h;
            if (nt == count - 1)
            {
                end = array.Length;
            }
            for (int i = beg; i < end; i++)
            {
                if (array[i] % 2 != 0)
                {
                    multOdd *= array[i];
                }
            }
            return multOdd;
        }
    }
}
