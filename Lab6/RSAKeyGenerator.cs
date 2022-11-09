using System;
using System.Collections.Generic;

namespace Labs_ZI.Lab6
{
    class RSAKeyGenerator
    {
        public Tuple<Tuple<int, int>, Tuple<int, int>> GenerateKeys(int p, int q)
        {
            if (!IsPrime(p) || !IsPrime(q))
                throw new Exception("Числа p и q должны быть простыми");

            int n = p * q;
            int eulerFunc = (p - 1) * (q - 1);

            List<int> ePossibleValues = new List<int>();

            for (int i = 0; i < eulerFunc; i++)
            {
                if (IsPrime(i) && IsCoprime(i, eulerFunc))
                    ePossibleValues.Add(i);
            }

            int e = ePossibleValues[new Random().Next(0, ePossibleValues.Count)];

            Tuple<int, int> openKey = new Tuple<int, int>(e, n);

            int d = FindRandomInvMod(e, eulerFunc, 1);
            Tuple<int, int> secretKey = new Tuple<int, int>(d, n);

            //Console.WriteLine("Euler func = " + eulerFunc);
            Console.WriteLine("Open key: {" + openKey.Item1 + " " + openKey.Item2 + "}");
            Console.WriteLine("Secret key: {" + secretKey.Item1 + " " + secretKey.Item2 + "}");


            return new Tuple<Tuple<int, int>, Tuple<int, int>>(openKey, secretKey);
        }

        private bool IsPrime(int n)
        {
            if (n == 2 || n == 3)
                return true;

            if (n <= 1 || n % 2 == 0 || n % 3 == 0)
                return false;

            for (int i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
            }

            return true;
        }

        private bool IsCoprime(int num1, int num2)
        {
            if (num1 == num2)
            {
                return num1 == 1;
            }
            else
            {
                if (num1 > num2)
                {
                    return IsCoprime(num1 - num2, num2);
                }
                else
                {
                    return IsCoprime(num2 - num1, num1);
                }
            }
        }

        private int FindRandomInvMod(int num, int module, int optionsCount)
        {
            int d = 2;

            List<int> dPossibleValues = new List<int>();

            while (dPossibleValues.Count < optionsCount)
            {
                if ((d * num) % module == 1 && d != num)
                    dPossibleValues.Add(d);
                d++;
            }

            d = dPossibleValues[new Random().Next(0, dPossibleValues.Count)];
            //Console.WriteLine("d * n % euler = " + d * num % module);

            return d;
        }
    }
}
