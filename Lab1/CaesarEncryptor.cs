using Labs_ZI.Utils;

namespace Labs_ZI.Lab1
{
    class CaesarEncryptor
    {
        private int _key;
        private string _alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public int Key { get => _key; set => _key = value; }

        public CaesarEncryptor(int key)
        {
            _key = key;
        }

        public string Encrypt(string message)
        {
            return Caesar(message, true);
        }

        public string Decrypt(string message, int key)
        {
            if (key != _key)
                return "Жулик не воруй!!!";
            return Caesar(message, false);
        }

        private string Caesar(string message, bool isDoingDirectOperation)
        {
            string result = "";
            message = message.ToLower();
            int coef = isDoingDirectOperation ? 1 : -1;

            for (int i = 0; i < message.Length; i++)
            {
                if (_alphabet.Contains(message[i].ToString()))
                {
                    for (int j = 0; j < _alphabet.Length; j++)
                        if (message[i] == _alphabet[j])
                        {
                            if (j - _key >= 0)
                                result += _alphabet[(j + coef * _key) % _alphabet.Length];
                            else
                                result += _alphabet[(_alphabet.Length + j + coef * _key) % _alphabet.Length];
                        }
                }
                else
                    result += message[i];
            }
            return result;
        }
    }
}
