using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace BogPoc.WordFinder
{
    public static class Dictionary
    {
        private static List<string> _words;

        static Dictionary ()
        {
            _words = new List<string>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resources = assembly.GetManifestResourceNames();

            // find the names of the reference bitmaps
            var wordsFileName = from r in resources
                                where r.ToLower().EndsWith("words.txt")
                                select r;

            string n = wordsFileName.FirstOrDefault();

            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(n)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    _words.Add(line.ToUpper());
            }

            _words.Sort();
        }

        public static bool IsWord (string word)
        {
            return (_words.BinarySearch(word) >= 0);
        }

        public static bool IsStartOfWord (string start)
        {
            int position = -1;
            int begin = 0;
            int end = _words.Count - 1;
            bool found = false;

            while ((begin <= end) && !found)
            {
                position = (begin + end) / 2;
                if (_words[position].StartsWith(start))
                    found = true; // just right 
                else if (_words[position].CompareTo(start) < 0)
                    begin = position + 1; // too small 
                else
                    end = position - 1; // too big 
            }

            return found;
        } 
    }
}
