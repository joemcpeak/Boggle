using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BogPoc.WordFinder
{
    public class Node
    {
        public String Letter { get; private set; }
        public List<Node> AdjacentNodes;   // don't deserialize this one into json!
        public int X { get; private set; }
        public int Y { get; private set; }

        public Node (int x, int y, string letter)
        {
            X = x;
            Y = y;
            Letter = letter.ToUpper();
            AdjacentNodes = new List<Node>();
        }

        public List<Path> GetPaths ()
        {
            return GetPathsRecursive(this, new Path());
        }

        public new string ToString ()
        {
            return Letter + " (" + X.ToString() + "," + Y.ToString() + ")";
        }

        public static List<Path> GetPathsRecursive (Node n, Path p)
        {
            List<Path> paths = new List<Path>();

            if (p.Nodes.Contains(n))
                return paths;

            Path potentialPath = new Path(p, n);
            
            if (potentialPath.IsStartOfWord() == false)
                return paths;
            else
            {
                paths.Add(potentialPath);

                foreach (Node adjacentNode in n.AdjacentNodes)
                    paths.AddRange(GetPathsRecursive(adjacentNode, potentialPath));

                return paths;
            }
        }
    }
}
