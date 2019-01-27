using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Matrix_n_Threads
{
    class MatrixMultiplier
    {
        private List<List<int>> _innerMatrix;
        public List<List<int>> MatrixMultiply(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
        {
            var sizes = MatrixEqualsSize(firstMatrix, secondMatrix);
            if(sizes.Item1 == MatrixEqualSize.NoEquals)
            {
                throw new Exception("No Equal Sizes");
            }
            int height = sizes.Item1 == MatrixEqualSize.Height ? firstMatrix[0].Count : firstMatrix.Count;
            int width = sizes.Item2 == MatrixEqualSize.Height ? secondMatrix[0].Count : secondMatrix.Count;

            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            _innerMatrix = new List<List<int>>(height);
            for(int i = 0; i < height; i++)
            {
                _innerMatrix.Add(new List<int>(width));
                for(int j = 0; j < width; j++)
                {
                    _innerMatrix[i].Add(0);
                    var firstLine = TakeLine(firstMatrix, sizes.Item1, i).ToList();
                    var secondLine = TakeLine(secondMatrix, sizes.Item2, j).ToList();
                    var arc = new Tuple<List<int>, List<int>, int, int>(firstLine, secondLine, i, j);
                    ParameterizedThreadStart pts = new ParameterizedThreadStart(MultiplyLines);
                    Thread thread = new Thread(pts);
                    thread.Priority = ThreadPriority.Normal;
                    thread.Start(arc);
                }
            }
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            return _innerMatrix;
        }

        private static Tuple<MatrixEqualSize,MatrixEqualSize> MatrixEqualsSize(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
        {
            if (firstMatrix.Count == secondMatrix.Count)
                return new Tuple<MatrixEqualSize, MatrixEqualSize>(MatrixEqualSize.Height, MatrixEqualSize.Height);
            if (firstMatrix.Count == secondMatrix[0].Count)
                return new Tuple<MatrixEqualSize, MatrixEqualSize>(MatrixEqualSize.Height, MatrixEqualSize.Width);
            if (firstMatrix[0].Count == secondMatrix.Count)
                return new Tuple<MatrixEqualSize, MatrixEqualSize>(MatrixEqualSize.Width, MatrixEqualSize.Height);
            if (firstMatrix[0].Count == secondMatrix[0].Count)
                return new Tuple<MatrixEqualSize, MatrixEqualSize>(MatrixEqualSize.Width, MatrixEqualSize.Width);
            return new Tuple<MatrixEqualSize, MatrixEqualSize>(MatrixEqualSize.NoEquals, MatrixEqualSize.NoEquals);
        }

        private IEnumerable<int> TakeLine(List<List<int>> matrix, MatrixEqualSize side, int lineNumber)
        {
            int lineLength = side == MatrixEqualSize.Height ? matrix.Count : matrix[0].Count;
            for (int i = 0; i < lineLength; i++)
            {
                yield return side == MatrixEqualSize.Height ? matrix[i][lineNumber] : matrix[lineNumber][i];
            }
        }

        private void MultiplyLines(object parameters)
        {
            var arc = (Tuple<List<int>, List<int>, int, int>)parameters;
            List<int> firstLine = arc.Item1;
            List<int> secondLine = arc.Item2;
            if(firstLine.Count != secondLine.Count)
            {
                throw new Exception("Wrong Lines");
            }
            int sum = 0;
            for(int i = 0; i < firstLine.Count; i++)
            {
                sum += firstLine[i] * secondLine[i];
            }

            _innerMatrix[arc.Item3][arc.Item4] = sum;
        }
    }
}
