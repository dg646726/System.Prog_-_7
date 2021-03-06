using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Prog_Дз_7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            using (FileStream fs = new FileStream("numbers.txt", FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    numbers.Add(int.Parse(sr.ReadLine()));
                }
            }
            List<int> indices = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                indices.Add(i);
            }
            foreach (var item in numbers)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            Console.WriteLine();
            //1
            var uniqueValues = numbers.AsParallel()
                                      .AsOrdered()
                                      .Where(n => Task1(n, numbers) == false)
                                      .Select(n => n);

            foreach (var item in uniqueValues)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            //2
            var counts = indices.AsParallel()
                              .AsOrdered()
                              .Select(n => Task2(n, numbers));

            Console.WriteLine($"Count {counts.Max()}");
            //3
            var countsPositives = indices.AsParallel()
                                         .AsOrdered()
                                         .Select(n => Task3(n, numbers));

            Console.WriteLine($"Count {countsPositives.Max()}");

        }
        static bool Task1(int x, List<int> numbers)
        {
            int count = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == x)
                    count++;
                if (count > 1)
                    return true;
            }
            return false;
        }
        static int Task2(int x, List<int> numbers)
        {
            int count = 1;
            for (int i = x; i < numbers.Count - 1; i++)
            {
                if (numbers[i] < numbers[i + 1])
                    count++;
                else
                    return count;
            }
            return count;
        }
        static int Task3(int x, List<int> numbers)
        {
            int count = 0;
            for (int i = x; i < numbers.Count; i++)
            {
                if (numbers[i] >= 0)
                    count++;
                else
                    return count;
            }
            return count;
        }
    }

}
