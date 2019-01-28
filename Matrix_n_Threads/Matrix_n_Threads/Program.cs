using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_n_Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> firstMatrix = new List<List<int>> {
                new List<int> {1, 2, 3},
                new List<int> {4, 5, 6}};
            List<List<int>> secondMatrix = new List<List<int>> {
                new List<int> {1, 2, 3},
                new List<int> {4, 5, 6},
                new List<int> {7, 8, 9}};
            var sum = new MatrixMultiplierParallel().MatrixMultiply(firstMatrix, secondMatrix);
            foreach(var line in sum)
            {
                foreach(var el in line)
                {
                    Console.Write($" {el}");
                }
                Console.WriteLine();
            }
        }
    }
}
