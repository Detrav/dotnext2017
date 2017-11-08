package detrav;

public class Matrix {
    private double[] data;
    private int width;
    private int height;

        public double get(int column, int row)
        {
            int pos = column + getWidth() * row;
            if (pos < getWidth() * getHeight())
                return data[column + getWidth() * row];
            return 0;
        }

        public  void set(int column, int row, double value) {
            int pos = column + getWidth() * row;
            if (pos < getWidth() * getHeight())
                data[column + getWidth() * row] = value;
        }

    public Matrix(int width, int height)
    {
        this.width = width;
        this.height = height;
        data = new double[width * height];
    }

    public double Determinant()
    {
        if (getWidth() != getHeight())
            return 0;
        if (getWidth() == 1)
            return get(0,0);
        double determinant = 0;
        int sign = 1;
        for (int i = 0; i < getWidth(); i++)
        {
            determinant += sign * get(i, 0) * GetMatrixWithoutRowAndColumn(i, 0).Determinant();
            sign *= -1;
        }
        return determinant;
    }

    private Matrix GetMatrixWithoutRowAndColumn(int column, int row)
    {
        Matrix m = new Matrix(getWidth() - 1, getHeight() - 1);
        for(int i =0; i< m.getWidth(); i++)
            for(int j =0; j< m.getHeight(); j++)
            {
                m.set(i,j, get(i >= column ? i + 1 : i, j >= row ? j + 1 : j));
            }
        return m;
    }

    public int getWidth() {
        return width;
    }


    public int getHeight() {
        return height;
    }

}