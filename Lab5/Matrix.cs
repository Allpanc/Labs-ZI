using Labs_ZI.Utils;
using System;
using System.Text;

namespace Labs_ZI.Lab5
{
    class Matrix
    {
        private string[,] matrix;
        private int rows = 0;
        private int columns = 0;

        public Matrix(int rows, int columns)
            : this("", rows, columns)
        {
        }

        public Matrix(string text)
            : this(text, 4, 4)
        {
        }

        public Matrix(string text, int rows, int columns)
        {
            if (text.Length != columns * rows * 8)
            {
                text = text.PadRight(columns * rows * 8 - text.Length, '0');
            }

            matrix = new string[rows, columns];
            int count = 0;
            this.rows = rows;
            this.columns = columns;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    matrix[j, i] = text.Substring(count * 8, 8);
                    count++;
                }
            }
        }

        public void setState(string text)
        {
            if (text.Length != columns * rows * 8)
            {
                throw new Exception("It's not equal size to state");
            }

            int count = 0;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    matrix[j, i] = text.Substring(count * 8, 8);
                    count++;
                }
            }
        }

        public int Rows
        {
            get
            {
                return rows;
            }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
        }

        public string this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                //If it gets hexa decimal transform to binary
                if (value.Length == 2)
                {
                    matrix[i, j] = SBitUtils.FromHexToBinary(value);
                }
                else if (value.Length == 8)
                {
                    matrix[i, j] = value;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder(128);
            if (matrix != null)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        text.Append(matrix[i, j]);
                    }
                }
            }

            return text.ToString();
        }

        public string[] getRow(int rowindex)
        {
            string[] row = new string[this.columns];

            if (rowindex > this.rows)
            {
                throw new IndexOutOfRangeException("out of row index error.");
            }

            for (int i = 0; i < this.columns; i++)
            {
                row[i] = matrix[rowindex, i];
            }

            return row;
        }

        public void setRow(string[] row, int rowindex)
        {
            if (rowindex > this.rows)
            {
                throw new IndexOutOfRangeException("out of row index error.");
            }

            for (int i = 0; i < this.columns; i++)
            {
                matrix[rowindex, i] = row[i];
            }
        }

        public string[] getWord(int wordindex)
        {
            string[] word = new string[this.rows];

            if (wordindex > this.rows)
            {
                throw new IndexOutOfRangeException("out of column index error.");
            }

            for (int i = 0; i < this.rows; i++)
            {
                word[i] = matrix[i, wordindex];
            }

            return word;
        }

        public void setWord(string[] word, int wordindex)
        {
            if (wordindex > this.rows)
            {
                throw new IndexOutOfRangeException("out of column index error.");
            }

            for (int i = 0; i < this.rows; i++)
            {
                matrix[i, wordindex] = word[i];
            }
        }

    }
}
