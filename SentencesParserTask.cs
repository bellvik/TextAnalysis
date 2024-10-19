using System.Collections.Generic;
using System.Linq;
namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            text = text.ToLower();
            var sentencesList = new List<List<string>>();
            string[] sentences = text.Split(new char[] { '.', '!', '?', ':', ';', '(', ')' }, StringSplitOptions.RemoveEmptyEntries).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            foreach (string sentence in sentences)
            {
                List<string> wordList = new List<string>();
                int firstLetter = -1;
                for (int i = 0; i < sentence.Length; i++)
                {
                    if (char.IsLetter(sentence[i]) || sentence[i] == '\'')
                    {
                        if (firstLetter == -1) firstLetter = i;
                    }
                    else
                    {
                        if (firstLetter != -1) wordList.Add(sentence.Substring(firstLetter, i - firstLetter));
                        firstLetter = -1;
                    }
                    if (i == sentence.Length - 1 && firstLetter != -1) wordList.Add(sentence.Substring(firstLetter, sentence.Length - firstLetter));
                }
                if (wordList.Count > 0) sentencesList.Add(wordList);
            }
            return sentencesList;
        }
    }
}