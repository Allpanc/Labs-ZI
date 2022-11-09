using Labs_ZI.Utils;
using System.Text;

namespace Labs_ZI.Lab5
{
    class AES_Encoder : AESCore
    {
        public string Encode(string plainText, string cipherKey)
        {
            plainText = SBitUtils.FromTextToBinary(plainText);

            StringBuilder binaryText = new(SBitUtils.setTextMutipleOf128Bits(plainText));

            StringBuilder EncryptedTextBuilder = new(binaryText.Length);

            Matrix Matrix_CipherKey = new(SBitUtils.FromHexToBinary(cipherKey));
            Keys key = new Keys();
            key.setCipherKey(Matrix_CipherKey);
            key = KeyExpansion(key, false);

            for (int j = 0; j < (binaryText.Length / 128); j++)
            {

                Matrix state = new Matrix(binaryText.ToString().Substring(j * 128, 128));

                state = AddRoundKey(state, key, 0);

                for (int i = 1; i < 11; i++)
                {
                    if (i == 10)
                    {
                        state = SubBytes(state, false);
                        state = ShiftRows(state, false);
                        state = AddRoundKey(state, key, i);
                    }
                    else
                    {
                        state = SubBytes(state, false);
                        state = ShiftRows(state, false);
                        state = MixColumns(state, false);
                        state = AddRoundKey(state, key, i);
                    }
                }

                EncryptedTextBuilder.Append(state.ToString());

            }
            return SBitUtils.FromBinaryToText(EncryptedTextBuilder.ToString());
        }
    }
}
