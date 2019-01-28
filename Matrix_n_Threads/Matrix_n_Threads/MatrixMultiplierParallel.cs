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
        private Tuple<MatrixEqualSize, MatrixEqualSize> sizes;
        private List<List<int>> _firstMatrix;
        private List<List<int>> _secondMatrix;
        private List<List<Tuple<int, int>>> _coords;
        public List<List<int>> MatrixMultiply(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
        {
            _firstMatrix = firstMatrix;
            _secondMatrix = secondMatrix;
            sizes = MatrixEqualsSize(firstMatrix, secondMatrix);
            if (sizes.Item1 == MatrixEqualSize.NoEquals)
            {
                throw new Exception("No Equal Sizes");
            }
            int height = sizes.Item1 == MatrixEqualSize.Height ? firstMatrix[0].Count : firstMatrix.Count;
            int width = sizes.Item2 == MatrixEqualSize.Height ? secondMatrix[0].Count : secondMatrix.Count;

            _innerMatrix = new List<List<int>>();
            _coords = new List<List<Tuple<int, int>>>();
            for (int i = 0; i < height; i++)
            {
                _innerMatrix.Add(new List<int>());
                _coords.Add(new List<Tuple<int, int>>());
                for (int j = 0; j < width; j++)
                {
                    _innerMatrix[i].Add(0);
                    var firstLine = TakeLine(firstMatrix, sizes.Item1, i).ToList();
                    var secondLine = TakeLine(secondMatrix, sizes.Item2, j).ToList();
                    _coords[i].Add(new Tuple<int, int>(i,j));
                }
                Parallel.ForEach<Tuple<int, int>>(_coords[i], MultiplyLines);
            }
            return _innerMatrix;
        }

        private void MultiplyLines(Tuple<int, int> arcCoords)
        {
            var firstLine = TakeLine(_firstMatrix, sizes.Item1, arcCoords.Item1).ToList();
            var secondLine = TakeLine(_secondMatrix, sizes.Item2, arcCoords.Item2).ToList();
            if (firstLine.Count != secondLine.Count)
            {
                throw new Exception("Wrong Lines");
            }
            int sum = 0;
            for (int i = 0; i < firstLine.Count; i++)
            {
                sum += firstLine[i] * secondLine[i];
            }

            _innerMatrix[arcCoords.Item1][arcCoords.Item2] = sum;
        }
    }
}
