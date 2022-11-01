
using System;

namespace Labs_ZI.Utils
{
    class MathHelper
    {
        public static int ModulExp(int numberBase, int index_n, int modulus)
        {
            int result = 1;

            for (int i = 0; i < index_n; i++)
                result = (result * numberBase) % modulus;
            
            //Console.WriteLine(result);

            return result;
        }
    }
}
