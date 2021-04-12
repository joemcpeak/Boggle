using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BogPoc.WordFinder
{
    public class Solution
    {
        public string Summary
        {
            get
            {
                return string.Format("Board contains {0} words, {1} total points.", Words.Count, Score);
            }
        }

        public int Score { get; private set; }

        public List<Word> Words { get; private set; }

        public Solution (List<Word> words)
        {
            Dictionary<string, Word> dict = new Dictionary<string, Word>();

            foreach (Word word in words)
            {
                if (dict.Keys.Contains(word.Value))
                    continue;

                if (word.Value.Length < 3)
                    continue;

                dict.Add(word.Value, word);
                Score += word.Points;
            }

            Words = new List<Word>();
            foreach (Word w in dict.Values)
                Words.Add(w);
        }

        public enum SortOrder
        {
            ByAlpha,
            ByPoints
        }

        public void Sort (SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.ByAlpha)
                Words.Sort(new WordComparerAlpha());
            else
                Words.Sort(new WordComparerPoints());
        }
    }
}
