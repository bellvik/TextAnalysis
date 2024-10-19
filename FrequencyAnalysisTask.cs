namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, Dictionary<string, int>> GetBigrams(List<List<string>> text)
    {
        Dictionary<string, Dictionary<string, int>> bigrams = new Dictionary<string, Dictionary<string, int>>();
        foreach (var sentence in text)
        {
            for (int i = 0; i < sentence.Count - 1; i++)
            { 
                if(!bigrams.ContainsKey(sentence[i])) bigrams.Add(sentence[i], new Dictionary<string, int>());
                if (!bigrams[sentence[i]].ContainsKey(sentence[i + 1])) bigrams[sentence[i]].Add(sentence[i + 1], 1);
                else bigrams[sentence[i]][sentence[i + 1]]++;
            }
        }
        return bigrams;
    }
    public static Dictionary<string, Dictionary<string, int>> GetTrigrams(List<List<string>> text)
    {
        Dictionary<string, Dictionary<string, int>> trigrams = new Dictionary<string, Dictionary<string, int>>();
        foreach (var sentence in text)
        {
            for (int i = 0; i < sentence.Count - 2; i++)
            {
                if (!trigrams.ContainsKey(sentence[i] + " " + sentence[i + 1])) trigrams.Add(sentence[i] + " " + sentence[i + 1], new Dictionary<string, int>());
                if (!trigrams[sentence[i] + " " + sentence[i + 1]].ContainsKey(sentence[i+2])) trigrams[sentence[i] + " " + sentence[i + 1]].Add(sentence[i+2], 1);
                else trigrams[sentence[i] + " " + sentence[i + 1]][sentence[i+2]]++;
            }
        }
        return trigrams;
    }
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, int>> bigrams = GetBigrams(text);
        Dictionary<string, Dictionary<string, int>> trigrams = GetBigrams(text);
        var bigram = GetDictionary(bigrams);
        var trigram = GetDictionary(trigrams);
        foreach (var e in bigram) result.Add(e.Key, e.Value);
        foreach (var e in trigram) result.Add(e.Key, e.Value);
        return result;
    }
    public static Dictionary<string, string> GetDictionary(Dictionary<string, Dictionary<string, int>> ngrams)
    {
        var result = new Dictionary<string, string>();
        foreach (var firstWord in ngrams)
        {
            var maximum = 0;
            string secondWord = null;
            foreach (var word in firstWord.Value)
            {
                if (word.Value == maximum)
                {
                    if (string.CompareOrdinal(secondWord, word.Key) > 0) secondWord = word.Key;
                }
                if (word.Value > maximum)
                {
                    secondWord = word.Key;
                    maximum = word.Value;
                }
            }
            result.Add(firstWord.Key, secondWord);
        }
        return result;
    }
}