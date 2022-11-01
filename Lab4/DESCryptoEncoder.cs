using System;
using System.Text;
using System.Threading.Tasks;

namespace Labs_ZI.Lab4
{
    class DESCryptoEncoder : DESCrypto
    {
        public String Encode(String data, String key, bool isParallel = false)
        {
            return Encoding.Unicode.GetString(Encode(Encoding.Unicode.GetBytes(data), key, isParallel));
        }

        byte[] Encode(byte[] data, String key, bool isParallel = false)
        {

            string plainBytesStr = "";
            foreach (byte b in data)
                plainBytesStr += b.ToString() + ", ";

            Console.WriteLine("Plain bytes: " + plainBytesStr.Trim(new char[] { ',', ' ' }) + "\n"); // здесь один символ занимает 2 байта

            return Encode(data, Encoding.Unicode.GetBytes(key), isParallel);
        }

        byte[] Encode(byte[] data, byte[] key, bool isParallel = false)
        {
            var subkeys = GenerateSubkeys(key);
            var result = new byte[data.Length];
            var block = new byte[8];

            if (isParallel)
            {
                Parallel.For(0, data.Length / 8, i =>
                {
                    Array.Copy(data, 8 * i, block, 0, 8);
                    Array.Copy(EncodeBlock(block, subkeys), 0, result, 8 * i, 8);
                });
            }
            else
            {
                for (int i = 0; i < data.Length / 8; i++) // N blocks 64bits length.
                {
                    Array.Copy(data, 8 * i, block, 0, 8);
                    Array.Copy(EncodeBlock(block, subkeys), 0, result, 8 * i, 8);
                }
            }

            string encodedBytesStr = "";
            foreach (byte b in result)
                encodedBytesStr += b.ToString() + ", ";

            Console.WriteLine("Encoded bytes: " + encodedBytesStr.Trim(new char[] { ',', ' ' })); // здесь один символ занимает 2 байта

            return result;
        }

        private byte[] EncodeBlock(byte[] block, byte[][] subkeys)
        {
            var message = SelectBits(block, InputPermutation);

            int blockSize = InputPermutation.Length;

            var left = SelectBits(message, 0, blockSize / 2);
            var right = SelectBits(message, blockSize / 2, blockSize / 2);


            for (int i = 0; i < subkeys.Length; i++)
            {
                byte[] rightTemp = right;

                right = SelectBits(right, ExpansionPermutation);

                right = XORBytes(right, subkeys[i]);

                right = Substitution6x4(right);

                right = SelectBits(right, Permutation);

                right = XORBytes(left, right);

                left = rightTemp;
            }

            var result = ConcatenateBits(right, blockSize / 2, left, blockSize / 2);
            return SelectBits(result, FinalPermutation);
        }
    }
}
