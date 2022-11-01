using System;
using Labs_ZI.Utils;

namespace Labs_ZI.Lab6
{
    class RSAEncoder
    {
        public string Encode(Tuple<int, int> openKey, string plainText)
        {
            int e = openKey.Item1;
            int n = openKey.Item2;

            AlphabetHelper alphabetHelper = new AlphabetHelper();
            int[] letterIndexes = alphabetHelper.convertTextToAlphabetIndexes(plainText);
            int[] encodedSymbols = new int[letterIndexes.Length];
            string encodedStr = "";

            for (int i = 0; i < letterIndexes.Length; i++)
            {
                encodedSymbols[i] = MathHelper.ModulExp(letterIndexes[i], e, n);
                Console.WriteLine(letterIndexes[i] + " -> " + encodedSymbols[i]);
            }

            encodedStr = alphabetHelper.convertAlphabetIndexesToText(encodedSymbols);

            return encodedStr;
        }
    }
}
