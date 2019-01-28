using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Matrix_n_Threads
{
    abstract class AbstractMatrixMultiplier
    {
        protected List<List<int>> _innerMatrix;

        protected Tuple<MatrixEqualSize, MatrixEqualSize> MatrixEqualsSize(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
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

        protected IEnumerable<int> TakeLine(List<List<int>> matrix, MatrixEqualSize side, int lineNumber)
        {
            int lineLength = side == MatrixEqualSize.Height ? matrix.Count : matrix[0].Count;
            for (int i = 0; i < lineLength; i++)
            {
                yield return side == MatrixEqualSize.Height ? matrix[i][lineNumber] : matrix[lineNumber][i];
            }
        }
    }
}
