using Labs_ZI.Utils;
using System;

namespace Labs_ZI.Lab6
{
    class RSADecoder
    {
        public string Decode(Tuple<int, int> secretKey, string encodedText)
        {
            int d = secretKey.Item1;
            int n = secretKey.Item2;

            AlphabetHelper alphabetHelper = new AlphabetHelper();

            int[] letterIndexes = alphabetHelper.convertTextToAlphabetIndexes(encodedText);
            int[] decodedSymbols = new int[letterIndexes.Length];

            for (int i = 0; i < letterIndexes.Length; i++)
            {
                decodedSymbols[i] = MathHelper.ModulExp(letterIndexes[i], d, n);
            }

            string decodedText = alphabetHelper.convertAlphabetIndexesToText(decodedSymbols);

            return decodedText;
        }
    }
}
