using System;
using Labs_ZI.Lab1;
using Labs_ZI.Lab2;
using Labs_ZI.Lab4;
using Labs_ZI.Lab6;
using Labs_ZI.Utils;

namespace Labs_ZI
{
    class Program
    {
        delegate void Lab();

        static void Main(string[] args)
        {
            Lab[] labs = new Lab[] { Lab1, Lab2, Lab3, Lab4, Lab5, Lab6, Lab7, Lab8, Lab9, Lab10, Lab11, Lab12 };

            while (true)
            {
                Console.WriteLine("Лабораторные работы ЗИ\nВведите номер лабораторной работы");
                int n = Int32.Parse(Console.ReadLine());

                Lab currentLab = labs[n - 1];
                currentLab();

                Console.WriteLine("\n");
            }
        }

        static void Lab1()
        {
            string test1 = "абвгдеё жзийклм нопрсту фхцчшщъ ыьэюя";
            string test2 = "Очень, ну очень секретный текст";

            CaesarEncryptor encryptor = new CaesarEncryptor(-2);
            Console.WriteLine(test2);
            Console.WriteLine(encryptor.Encrypt(test2));
            Console.WriteLine(encryptor.Decrypt(encryptor.Encrypt(test2), -2));
            Console.WriteLine(encryptor.Decrypt(encryptor.Encrypt(test2), 2));
        }

        static void Lab2()
        {
            string plainText = "Attack at dawn";
            string key = "Queenly";

            AutokeyCipherer cipherer = new AutokeyCipherer();
            string cipherText = cipherer.Encrypt(key, plainText);
            Console.WriteLine("Result of encryption: " + cipherText + "\n");
            Console.WriteLine("Result of decryption: " + cipherer.Decrypt(key, cipherText));
        }

        static void Lab3()
        {

        }

        static void Lab4()
        {
            string data = "thistext";
            string key = "abcd";

            Console.WriteLine("Plain text = " + data + " , key = " + key);
            DESCryptoEncoder encoder = new DESCryptoEncoder();
            string encodingResult = encoder.Encode(data, key);

            Console.WriteLine("Encoding result = " + encodingResult + "\n");

            DESCryptoDecoder decoder = new DESCryptoDecoder();
            string decodingResult = decoder.Decode(encodingResult, key);

            Console.WriteLine("Decoding result = " + decodingResult);
        }

        static void Lab5()
        {

        }

        static void Lab6()
        {
            string plainText = "Hello World";
            Console.WriteLine("Plain " + plainText);
            RSAKeyGenerator keyGenerator = new RSAKeyGenerator();

            Tuple<Tuple<int, int>, Tuple<int, int>> keys = keyGenerator.GenerateKeys(3, 7);

            Tuple<int, int> openKey = keys.Item1;
            Tuple<int, int> secretKey = keys.Item2;

            RSAEncoder encoder = new RSAEncoder();
            string encodedText = encoder.Encode(openKey, plainText);
            Console.WriteLine("Encoded " + encodedText);

            RSADecoder decoder = new RSADecoder();
            string decodedText = decoder.Decode(secretKey, encodedText);
            Console.WriteLine("Decoded " + decodedText);
        }

        static void Lab7()
        {

        }

        static void Lab8()
        {

        }

        static void Lab9()
        {

        }

        static void Lab10()
        {

        }

        static void Lab11()
        {

        }

        static void Lab12()
        {

        }
    }

}
