using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs_ZI.Lab5
{
    class AESCore
    {
        public Matrix SubBytes(Matrix state, bool IsReverse)
        {
            for (int i = 0; i < state.Rows; i++)
            {
                for (int j = 0; j < state.Columns; j++)
                {
                    int row = Convert.ToInt32(state[i, j].Substring(0, 4), 2);
                    int column = Convert.ToInt32(state[i, j].Substring(4, 4), 2);

                    if (IsReverse == false)
                    {
                        state[i, j] = TransformTables.sbox[row, column];
                    }
                    else
                    {
                        state[i, j] = TransformTables.inverse_sbox[row, column];
                    }
                }
            }

            return state;
        }

        public Matrix ShiftRows(Matrix state, bool IsReverse)
        {
            for (int i = 1; i < state.Rows; i++)
            {
                if (IsReverse == false)
                {
                    state.setRow(this.CircularLeftShift(state.getRow(i), i)
                        , i);
                }
                else
                {
                    state.setRow(this.CircularRightShift(state.getRow(i), i)
                        , i);
                }
            }

            return state;
        }

        private string[] CircularLeftShift(string[] row, int count)
        {
            for (int i = 0; i < count; i++)
            {
                string temp = row[0];
                row[0] = row[1];
                row[1] = row[2];
                row[2] = row[3];
                row[3] = temp;
            }

            return row;
        }

        private string[] CircularRightShift(string[] row, int count)
        {
            for (int i = 0; i < count; i++)
            {
                string temp = row[3];
                row[3] = row[2];
                row[2] = row[1];
                row[1] = row[0];
                row[0] = temp;
            }

            return row;
        }

        public Matrix AddRoundKey(Matrix state, Keys key, int Round)
        {
            if (Round > key.RoundKeys.Count - 1)
            {
                throw new IndexOutOfRangeException("The round key is must between 0 and 10 in 128bit AES.");
            }

            return MatrixMultiplication.XOR(state, key.RoundKeys[Round]);
        }

        public Matrix MixColumns(Matrix state, bool IsReverse)
        {
            if (IsReverse == false)
            {
                state = MatrixMultiplication.Multiply(TransformTables.MixColumnFactor, state, true)!;
            }
            else
            {
                state = MatrixMultiplication.Multiply(TransformTables.Inverse_MixColumnFactor, state, true)!;
            }

            return state;
        }

        public Keys KeyExpansion(Keys key, bool IsReverse)
        {
            for (int i = 4; i < key.RoundKeys.Count * 4; i++)
            {
                string[] Wi_1 = key.RoundKeys[(i - 1) / 4].getWord((i - 1) % 4);

                Matrix mat_Wi_1 = new Matrix(4, 1);
                mat_Wi_1.setWord(Wi_1, 0);

                if (i % 4 == 0)
                {
                    Wi_1 = this.RotWord(Wi_1);
                    mat_Wi_1.setWord(Wi_1, 0);
                    mat_Wi_1 = this.SubBytes(mat_Wi_1, false);
                    mat_Wi_1 = MatrixMultiplication.XOR(mat_Wi_1, TransformTables.Rcon[(i - 1) / 4]);
                }

                Matrix Wi_4 = new Matrix(4, 1);
                Wi_4.setWord(key.RoundKeys[(i - 4) / 4].getWord((i - 4) % 4), 0);

                Matrix temp = MatrixMultiplication.XOR(mat_Wi_1, Wi_4);

                string[] Wi = temp.getWord(0);

                key.RoundKeys[i / 4].setWord(Wi, i % 4);
            }

            return key;
        }

        private string[] RotWord(string[] state)
        {
            return this.CircularLeftShift(state, 1);
        }
    }
}
