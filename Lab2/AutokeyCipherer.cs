using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs_ZI.Lab2
{
    class AutokeyCipherer
    {
        private string _alphabet = "abcdefghijklmnopqrstuvwxyz";

        public string Encrypt(string keyword, string plainText)
        {
            plainText = plainText.Replace(" ", "").ToLower();
            keyword = keyword.ToLower();
            string runningKey = keyword + plainText;
            string cipherText = "";

            Console.WriteLine("Plain text: " + plainText);
            Console.WriteLine("Keyword: " + keyword);
            Console.WriteLine("Running key: " + runningKey);

            for (int i = 0; i < plainText.Length; i++)
            {
                if (_alphabet.Contains(plainText[i].ToString()))
                {
                    int cipherIndex = 0;
                    for (int j = 0; j < _alphabet.Length; j++)
                    {
                        if (plainText[i] == _alphabet[j])
                            cipherIndex += j;
                        if (runningKey[i] == _alphabet[j])
                            cipherIndex += j;
                    }
                    cipherText += _alphabet[cipherIndex % _alphabet.Length];

                    Console.WriteLine("Cipher text: " + cipherText);
                }
            }

            return cipherText;
        }

        public string Decrypt(string keyword, string cipherText)
        {
            cipherText = cipherText.Replace(" ", "").ToLower();
            keyword = keyword.ToLower();
            string runningKey = keyword;
            string plainText = "";

            Console.WriteLine("Cypher text: " + cipherText);
            Console.WriteLine("Keyword: " + keyword);
            Console.WriteLine("Running key: " + runningKey);

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (_alphabet.Contains(cipherText[i].ToString()))
                {
                    int plainIndex = 0;

                    for (int j = 0; j < _alphabet.Length; j++)
                    {
                        if (cipherText[i] == _alphabet[j])
                            plainIndex += j;
                        if (runningKey[i] == _alphabet[j])
                            plainIndex -= j;
                    }

                    if (plainIndex < 0)
                        plainIndex += _alphabet.Length;

                    plainText += _alphabet[plainIndex % _alphabet.Length];
                    runningKey += _alphabet[plainIndex % _alphabet.Length];

                    Console.WriteLine("Running key: " + runningKey);
                    Console.WriteLine("Plain text: " + plainText);
                }
            }
            return plainText;
        }
    }
}
