using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;

namespace Lab4Bonus
{


    class Program
    {
        static string Piggify(string word)
        {
            Regex rx = new Regex(@"^[aeiou]$");

            string wordLower = word.ToLower();

            string firstLetter = wordLower.Substring(0, 1);
            if (rx.IsMatch(firstLetter))
            {
                word = word + "way";
                return word;
            }
            // to determine all the consonants at the front, find index of first vowel
            else
            {
                int vowelIndex = 0;
                for (int i = 0; i < word.Length; i++)
                {
                    if (rx.IsMatch(Convert.ToString(wordLower[i])))
                    {
                        vowelIndex = i;
                        i = word.Length;
                    }
                }
                // vowelIndex is not equal to first vowel index
                string ending = word.Substring(0, vowelIndex) + "ay";
                string beginning = word.Substring(vowelIndex, word.Length - vowelIndex);
                word = beginning + ending;
                return word;
            }
        }

        static bool Continue()
        {
            bool done = false;
            while (!done)
            {

                Console.Write("Would you like to continue? (y/n) ");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y" || ans == "n")
                {
                    if (ans == "n")
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    Console.WriteLine("Sorry, that was not a valid input. Please try again");
                }
            }
            return false;
        }

        static string FixCasing(string word, string[] cases, int i, TextInfo myTI)
        {
            if (cases[i] == "title")
            {
                string toTitle = myTI.ToTitleCase(word);
                word = toTitle;
            }
            else if (cases[i] == "lower")
            {
                word = word.ToLower();
            }
            else if (cases[i] == "upper")
            {
                word = word.ToUpper();
            }

            return word;
        }

        static string[] LogCasing(string[] cases, string[] wordArray, TextInfo myTI)
        {
            for (int i = 0; i < wordArray.Length; i++)
            {
                string toLower = wordArray[i].ToLower();
                string toTitle = myTI.ToTitleCase(toLower);
                if (wordArray[i] == toTitle)
                {
                    cases[i] = "title";
                }
                else if (wordArray[i].ToLower() == wordArray[i])
                {
                    cases[i] = "lower";
                }
                else if (wordArray[i].ToUpper() == wordArray[i])
                {
                    cases[i] = "upper";
                }
            }
            return cases;
        }

        static void Main(string[] args)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            bool done = false;
            while (!done)
            {
                string word = "";
                bool valid = false;
                while (!valid)
                {
                    Console.Write("Please enter a word or phrase: ");
                    word = Console.ReadLine();
                    string wordLower = word.ToLower();
                    Regex abc = new Regex(@"^[a-z]+$");
                    if (abc.IsMatch(wordLower))
                    {
                        valid = true;
                        word = Piggify(wordLower);
                    }
                    else
                    {
                        Regex abcSpace = new Regex(@"^[a-z\s]+$");
                        if (abcSpace.IsMatch(wordLower))
                        {
                            valid = true;
                            string[] wordArray = word.Split();
                            string[] cases = new string[wordArray.Length];

                            // this part is to identify the casing for each word
                            cases = LogCasing(cases, wordArray, myTI);

                            word = "";
                            for (int i = 0; i < wordArray.Length; i++)
                            {
                                wordArray[i] = Piggify(wordArray[i]);
                                wordArray[i] = FixCasing(wordArray[i], cases, i, myTI);

                                if (i == wordArray.Length - 1)
                                {
                                    word += wordArray[i];
                                }
                                else
                                {
                                    word += wordArray[i] + " ";
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Sorry, that was not a valid input. Please try again");
                        }
                    }
                }
                // if start with vowel, just add "way" on end
                Console.WriteLine($"\n{word}\n");
                done = Continue();
            }
            Console.Clear();
            Console.WriteLine("Goodbye! Have a beautiful time!");
        }
    }
}
