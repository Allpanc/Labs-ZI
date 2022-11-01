
namespace Labs_ZI.Utils
{
    class AlphabetHelper
    {
        private string _alphabet1 = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private string _alphabet2 = "abcdefghijklmnopqrstuvwxyz";

        public int[] convertTextToAlphabetIndexes(string text, bool isCyrillic = false)
        {
            text = text.ToLower();
            //string alphabet = isCyrillic ? _alphabet1 : _alphabet2;            

            string alphabet = _alphabet2;
            int[] letterIndexes = new int[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(text[i].ToString()))
                {
                    for (int j = 0; j < alphabet.Length; j++)
                        if (alphabet[j] == text[i])
                            letterIndexes[i] = j;
                }
                else
                    letterIndexes[i] = text[i];
            }

            return letterIndexes;
        }

        public string convertAlphabetIndexesToText(int[] indexes, bool isCyrillic = false)
        {
            string alphabet = isCyrillic ? _alphabet1 : _alphabet2;
            string text = "";

            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] < alphabet.Length)
                    text += alphabet[indexes[i]];
                else
                    text += (char)indexes[i];
            }
            return text;
        }
    }
}
