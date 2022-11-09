using Labs_ZI.Utils;
using System.Text;

namespace Labs_ZI.Lab5
{
    class AES_Decoder : AESCore
    {
        public string Decode(string PlainText, string CipherKey)
        {
            string binaryText = SBitUtils.FromTextToBinary(PlainText);

            StringBuilder DecryptedTextBuilder = new StringBuilder(binaryText.Length);
            Matrix Matrix_CipherKey = new Matrix(SBitUtils.FromHexToBinary(CipherKey));
            Keys key = new Keys();
            key.setCipherKey(Matrix_CipherKey);
            key = this.KeyExpansion(key, false);

            for (int j = 0; j < (binaryText.Length / 128); j++)
            {
                Matrix state = new Matrix(binaryText.Substring(j * 128, 128));

                state = this.AddRoundKey(state, key, 10);

                for (int i = 9; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        state = this.ShiftRows(state, true);
                        state = this.SubBytes(state, true);
                        state = this.AddRoundKey(state, key, i);
                    }
                    else
                    {
                        state = this.ShiftRows(state, true);
                        state = this.SubBytes(state, true);
                        state = this.AddRoundKey(state, key, i);
                        state = this.MixColumns(state, true);
                    }
                }

                if ((j * 128 + 128) == binaryText.Length)
                {
                    StringBuilder last_text = new StringBuilder(state.ToString().TrimEnd('0'));

                    int count = state.ToString().Length - last_text.Length;

                    if ((count % 8) != 0)
                    {
                        count = 8 - (count % 8);
                    }

                    string append_text = "";

                    for (int k = 0; k < count; k++)
                    {
                        append_text += "0";
                    }
                    DecryptedTextBuilder.Append(last_text.ToString() + append_text);
                }
                else
                {
                    DecryptedTextBuilder.Append(state.ToString());
                }
            }

            return SBitUtils.FromBinaryToText(DecryptedTextBuilder.ToString());
        }
    }
}
