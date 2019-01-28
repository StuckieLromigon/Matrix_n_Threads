using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Matrix_n_Threads
{
    class MatrixMultiplierParallel : AbstractMatrixMultiplier
    {
        private List<List<Tuple<List<int>, List<int>>>> _notCalculatedLines { get; set; }
        public List<List<int>> MatrixMultiply(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
        {
            var sizes = MatrixEqualsSize(firstMatrix, secondMatrix);
            if (sizes.Item1 == MatrixEqualSize.NoEquals)
            {
                throw new Exception("No Equal Sizes");
            }
            int height = sizes.Item1 == MatrixEqualSize.Height ? firstMatrix[0].Count : firstMatrix.Count;
            int width = sizes.Item2 == MatrixEqualSize.Height ? secondMatrix[0].Count : secondMatrix.Count;

            _innerMatrix = new List<List<int>>();
            _notCalculatedLines = new List<List<Tuple<List<int>, List<int>>>>();
            for (int i = 0; i < height; i++)
            {
                _innerMatrix.Add(new List<int>());
                _notCalculatedLines.Add(new List<Tuple<List<int>, List<int>>>());
                for (int j = 0; j < width; j++)
                {
                    _innerMatrix[i].Add(0);
                    var firstLine = TakeLine(firstMatrix, sizes.Item1, i).ToList();
                    var secondLine = TakeLine(secondMatrix, sizes.Item2, j).ToList();
                    _notCalculatedLines[i].Add(new Tuple<List<int>, List<int>>(firstLine, secondLine));
                }
                Parallel.For(0, width, MultiplyLines(,i);
            }
            return _innerMatrix;
        }

        private void MultiplyLines(int x, int y)
        {
            if (firstLine.Count != secondLine.Count)
            {
                throw new Exception("Wrong Lines");
            }
            int sum = 0;
            for (int i = 0; i < firstLine.Count; i++)
            {
                sum += firstLine[i] * secondLine[i];
            }

            return sum;
        }
    }
}
