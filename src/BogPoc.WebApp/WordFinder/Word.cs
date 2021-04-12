using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BogPoc.WordFinder
{
    public class Word
    {
        public string Value { get; private set; }

        public int Points
        {
            get
            {
                if (Value.Length <= 4)
                    return 1;
                if (Value.Length <= 5)
                    return 2;
                if (Value.Length <= 6)
                    return 3;
                if (Value.Length <= 7)
                    return 5;
                return 11;
            }
        }

        public Path Path { get; private set; }

        public Word (Path path)
        {
            Path = path;
            Value = path.Word;
        }

        public override string ToString ()
        {
            return string.Format("{0} ({1} {2})", Value, Points, Points == 1 ? "point" : "points");
        }

        public static bool operator == (Word w1, Word w2)
        {
            return (w1.Value == w2.Value);
        }
        public static bool operator != (Word w1, Word w2)
        {
            return (w1.Value != w2.Value);
        }
    }

    public class WordComparerAlpha : IComparer<Word>
    {
        public int Compare (Word x, Word y)
        {
            return (x.Value.CompareTo(y.Value));
        }
    }

    public class WordComparerPoints : IComparer<Word>
    {
        public int Compare (Word x, Word y)
        {
            return (x.Points.CompareTo(y.Points));
        }
    }
}
