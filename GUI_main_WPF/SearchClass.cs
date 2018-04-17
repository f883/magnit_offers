using System;
using System.Collections.Generic;
using System.Linq;

namespace GUI_main_WPF
{
    class Search
    {
        private static string[] GetSearchDictionary(string searchWord)
        {
            List<string> resultDict = new List<string>() { };
            List<char> charList = searchWord.ToCharArray().ToList();
            char removedChar = '#';

            { // добавление буквы в конце
                int index = charList.Count;
                for (int charNumber = 0; charNumber < 32; charNumber++)
                {
                    charList.Insert(index, GetChar(charNumber));
                    resultDict.Add(GetStringFromList(charList));
                    //Console.WriteLine(GetStringFromList(charList));
                    charList.RemoveAt(index);
                }
            }

            for (int index = 0; index < charList.Count; index++) // замена буквы в слове
            {
                removedChar = charList[index];
                charList.RemoveAt(index);
                for (int charNumber = 0; charNumber < 32; charNumber++)
                {
                    charList.Insert(index, GetChar(charNumber));
                    resultDict.Add(GetStringFromList(charList));
                    //Console.WriteLine(GetStringFromList(charList));
                    charList.RemoveAt(index);
                }
                charList.Insert(index, removedChar);
            }

            for (int index = 0; index < charList.Count; index++) // удаление буквы из слова
            {
                removedChar = charList[index];
                charList.RemoveAt(index);
                resultDict.Add(GetStringFromList(charList));
                //Console.WriteLine(GetStringFromList(charList));
                charList.Insert(index, removedChar);
            }

            for (int index = 0; index < charList.Count - 1; index++) // обмен двух рядом стоящих букв
            {
                char temp = charList[index];
                charList.RemoveAt(index);
                charList.Insert(index + 1, temp);
                resultDict.Add(GetStringFromList(charList));
                //Console.WriteLine(GetStringFromList(charList));
                temp = charList[index];
                charList.RemoveAt(index);
                charList.Insert(index + 1, temp);
            }
            return resultDict.ToArray();
        }

        private static char GetChar(int number)
            // 0 - русская 'а'
            // 31 - русская 'я'
        {
            return (char)(number + 1072);
        }

        private static string GetStringFromList(List<char> chars)
        {
            string str = "";
            foreach (char ch in chars)
            {
                str = str + ch;
            }
            return str;
        }

        public static Offer[] FuzzySearch(Offer[] allOffers, string stringForSearch)
        {
            Offer[] resultArray = new Offer[0];
            stringForSearch = stringForSearch.ToLower();
            string[] searchWordArray = stringForSearch.Split(' ');
            
            foreach (Offer offer in allOffers)
            {
                string allActionText = offer.header + " " + offer.body + " " + offer.largeBody;
                string loweredActionText = "";

                allActionText = allActionText.ToLower();

                foreach (char c in allActionText) 
                    // перевод всего текста в нижний регистр, очистка лишних знаков
                {
                    if (Char.IsLetter(c) || c == ' ')
                        loweredActionText = loweredActionText + c;
                }

                string[] allWords = loweredActionText.Split(' ');
                bool isFound = false;

                foreach (string searchWord in searchWordArray)
                {
                    foreach (string word in allWords)
                    {
                        foreach (string dictionaryWord in GetSearchDictionary(searchWord))
                        {
                            if (word == dictionaryWord)
                            {
                                Array.Resize(ref resultArray, resultArray.Length + 1);
                                resultArray[resultArray.Length - 1] = offer;
                                isFound = true;
                                break;
                            }
                        }
                        if (isFound)
                            break;
                    }
                }
            }
            return resultArray;
        }
    }
}
