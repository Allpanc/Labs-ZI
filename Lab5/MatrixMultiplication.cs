using System.Text;

namespace Labs_ZI.Lab5
{
    class MatrixMultiplication
    {
        public static Matrix Multiply(Matrix a, Matrix b, bool IsMixColumns)
        {
            if (IsMixColumns == true)
            {
                return MixColumnsMultiply(a, b);
            }
            else
            {
                return null;
            }
        }

        public static Matrix XOR(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.Rows, a.Columns);

            for (int i = 0; i < c.Rows; i++)
            {
                for (int j = 0; j < c.Columns; j++)
                {
                    c[i, j] = MultiplicativeInverse.XOR(a[i, j], b[i, j]);
                }
            }

            return c;
        }

        private static Matrix MixColumnsMultiply(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.Rows, b.Columns);
            for (int j = 0; j < c.Columns; j++)
            {

                for (int i = 0; i < c.Rows; i++)
                {
                    StringBuilder temp = new StringBuilder(32);

                    temp.Append(MultiplicativeInverse.GetInverse(a[i, 0], b[0, j], "00011011", 8));
                    temp.Append(MultiplicativeInverse.GetInverse(a[i, 1], b[1, j], "00011011", 8));
                    temp.Append(MultiplicativeInverse.GetInverse(a[i, 2], b[2, j], "00011011", 8));
                    temp.Append(MultiplicativeInverse.GetInverse(a[i, 3], b[3, j], "00011011", 8));

                    string temp2 = "";

                    temp2 = MultiplicativeInverse.XOR(temp.ToString().Substring(0, 8), temp.ToString().Substring(8, 8));

                    temp2 = MultiplicativeInverse.XOR(temp2, temp.ToString().Substring(16, 8));
                    temp2 = MultiplicativeInverse.XOR(temp2, temp.ToString().Substring(24, 8));

                    c[i, j] = temp2;
                }
            }

            return c;
        }
    }
}
