using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    /*
     * Double for now; implementing a generic Matrix is not that easy
     */

    internal class Matrix
    {
        public double[,] Value { get; private set; }

        public Matrix(int dimension0, int dimension1)
        {
            Value = new double[dimension0,dimension1];
        }

        public Matrix(double[,] matrix)
        {
            Value = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1) + 1];

            // double is a value type so this should make an actual copy
            Array.Copy(matrix, Value, matrix.Length);
        }

        public Matrix(int dimension0, bool isI = false)
            : this(dimension0, dimension0)
        {
            if (isI)
            {
                for (int index = 0; index < dimension0; index++)
                {
                    Value[index, index] = 1.0;
                }
            }
        }

        public static Matrix operator +(Matrix argLeft, Matrix argRight)
        {
            int argLeftDim0 = argLeft.Value.GetUpperBound(0);
            int argLeftDim1 = argLeft.Value.GetUpperBound(1);
            int argRightDim0 = argRight.Value.GetUpperBound(0);
            int argRightDim1 = argRight.Value.GetUpperBound(1);

            if ((argLeftDim0 != argRightDim0) || (argLeftDim1 != argRightDim1)) throw new ArgumentException("Dimensions of two matrices to not match");

            double[,] resultValue = new double[argLeftDim0 + 1, argLeftDim1 + 1];
            for (int index0 = 0; index0 <= argLeftDim0; index0++)
                for (int index1 = 0; index1 <= argLeftDim1; index1++)
                {
                    resultValue[index0, index1] = argLeft.Value[index0, index1] + argRight.Value[index0, index1];
                }
            return new Matrix(resultValue);
        }

        public static Matrix operator -(Matrix argLeft, Matrix argRight)
        {
            int argLeftDim0 = argLeft.Value.GetUpperBound(0);
            int argLeftDim1 = argLeft.Value.GetUpperBound(1);
            int argRightDim0 = argRight.Value.GetUpperBound(0);
            int argRightDim1 = argRight.Value.GetUpperBound(1);

            if ((argLeftDim0 != argRightDim0) || (argLeftDim1 != argRightDim1)) throw new ArgumentException("Dimensions of two matrices to not match");

            double[,] resultValue = new double[argLeftDim0 + 1, argLeftDim1 + 1];
            for (int index0 = 0; index0 <= argLeftDim0; index0++)
                for (int index1 = 0; index1 <= argLeftDim1; index1++)
                {
                    resultValue[index0, index1] = argLeft.Value[index0, index1] - argRight.Value[index0, index1];
                }
            return new Matrix(resultValue);
        }

    }
}
