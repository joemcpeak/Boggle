using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BogPoc.WordFinder
{
    public class Path
    {
        public List<Node> Nodes { get; private set; }

        public string Word
        {
            get
            {
                string w = "";
                foreach (Node n in Nodes)
                {
                    w += n.Letter;
                    if (n.Letter == "Q")
                        w += "U";
                }

                return w;
            }
        }

        public Path ()
        {
            Nodes = new List<Node>();
        }
        
        public Path (Path p, Node newNode)
        {
            Nodes = new List<Node>();
            foreach (Node n in p.Nodes)
                Nodes.Add(n);

            Nodes.Add(newNode);
        }

        public bool IsStartOfWord ()
        {
            return Dictionary.IsStartOfWord(Word);
        }

        public new string ToString ()
        {
            return Word;
        }
    }
}
