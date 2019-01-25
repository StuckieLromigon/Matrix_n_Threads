using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_n_Threads
{
    class MatrixMultiplier
    {
        public static List<List<int>> MatrixMultiply(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
        {
            MatrixEqualSize equalSize = MatrixEqualsSize(firstMatrix, secondMatrix);
            if(equalSize == MatrixEqualSize.NoEquals)
            {
                throw new Exception("No Equal Sizes");
            }
        }

        private static MatrixEqualSize MatrixEqualsSize(List<List<int>> firstMatrix, List<List<int>> secondMatrix)
        {
            if (firstMatrix.Count == secondMatrix.Count)
                return MatrixEqualSize.HeightHeight;
            if (firstMatrix.Count == secondMatrix[0].Count)
                return MatrixEqualSize.HeightWidth;
            if (firstMatrix[0].Count == secondMatrix.Count)
                return MatrixEqualSize.WidthHeight;
            if (firstMatrix[0].Count == secondMatrix[0].Count)
                return MatrixEqualSize.WidthWidth;
            return MatrixEqualSize.NoEquals;
        }
    }
}
