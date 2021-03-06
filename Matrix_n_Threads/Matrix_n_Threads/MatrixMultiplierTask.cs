﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Matrix_n_Threads
{
    class MatrixMultiplierTask : AbstractMatrixMultiplier
    {
        private List<Task> _tasks;
        public List<List<int>> MatrixMultiply(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
        {
            var sizes = MatrixEqualsSize(firstMatrix, secondMatrix);
            if (sizes.Item1 == MatrixEqualSize.NoEquals)
            {
                throw new Exception("No Equal Sizes");
            }
            int height = sizes.Item1 == MatrixEqualSize.Height ? firstMatrix[0].Count : firstMatrix.Count;
            int width = sizes.Item2 == MatrixEqualSize.Height ? secondMatrix[0].Count : secondMatrix.Count;
            _tasks = new List<Task>();
            _innerMatrix = new List<List<int>>(height);
            for (int i = 0; i < height; i++)
            {
                _innerMatrix.Add(new List<int>(width));
                for (int j = 0; j < width; j++)
                {
                    _innerMatrix[i].Add(0);
                    var firstLine = TakeLine(firstMatrix, sizes.Item1, i).ToList();
                    var secondLine = TakeLine(secondMatrix, sizes.Item2, j).ToList();
                    var arc = new Tuple<List<int>, List<int>, int, int>(firstLine, secondLine, i, j);
                    Task task = new Task(() => MultiplyLines(arc));
                    task.Start();
                    //Task task = Task.Factory.StartNew(() => MultiplyLines(arc));
                    _tasks.Add(task);
                }
            }
            Task.WaitAll(_tasks.ToArray());
            return _innerMatrix;
        }

        private void MultiplyLines(object parameters)
        {
            var arc = (Tuple<List<int>, List<int>, int, int>)parameters;
            List<int> firstLine = arc.Item1;
            List<int> secondLine = arc.Item2;
            if (firstLine.Count != secondLine.Count)
            {
                throw new Exception("Wrong Lines");
            }
            int sum = 0;
            for (int i = 0; i < firstLine.Count; i++)
            {
                sum += firstLine[i] * secondLine[i];
            }

            _innerMatrix[arc.Item3][arc.Item4] = sum;
        }
    }
}
