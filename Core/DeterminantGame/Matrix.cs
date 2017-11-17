using System;
using System.Collections.Generic;
using System.Text;

namespace DeterminantGame
{
    public class Matrix
    {
        private double[] data;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public double this[int column, int row]
        {
            get
            {
                int pos = column + Width * row;
                if (pos < Width * Height)
                    return data[column + Width * row];
                return 0;
            }

            set
            {
                int pos = column + Width * row;
                if (pos < Width * Height)
                    data[column + Width * row] = value;
            }
        }
        public Matrix(int width, int height)
        {
            Width = width;
            Height = height;
            data = new double[width * height];
        }

        public double Determinant()
        {
            if (Width != Height)
                return 0;
            if (Width == 1)
                return this[0, 0];
            double determinant = 0;
            int sign = 1;
            for (int i = 0; i < Width; i++)
            {
                determinant += sign * this[i, 0] * GetMatrixWithoutRowAndColumn(i, 0).Determinant();
                sign *= -1;
            }
            return determinant;
        }

        private Matrix GetMatrixWithoutRowAndColumn(int column, int row)
        {
            Matrix m = new Matrix(Width - 1, Height - 1);
            for(int i =0; i< m.Width; i++)
                for(int j =0; j< m.Height; j++)
                {
                    m[i, j] = this[i >= column ? i + 1 : i, j >= row ? j + 1 : j];
                }
            return m;
        }
    }
}
