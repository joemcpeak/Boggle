using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BogPoc.WordFinder
{
    public class Board
    {
        public static Solution Solve (string letters)
        {
            // make sure the list of letters can be turned into a square
            var f = Math.Sqrt(letters.Length);
            if (int.TryParse(f.ToString(), out int size) == false)
                throw new Exception("The length of the letters string is not a perfect square!");

            // convert the letters into a list of strings
            var s = new List<string>();
            foreach (char c in letters)
                s.Add(c.ToString());

            // turn each letter into a node
            List<Node> nodes = GetNodesForLetters(s);

            // find all the possible paths starting at each nodes and traversing all adjacent nodes, recursively, stopping 
            // when the letters in the path could not possibly continue on to form any word
            List<Path> paths = new List<Path>();
            foreach (Node n in nodes)
                paths.AddRange(n.GetPaths());

            // check each path to see if it is a word
            List<Word> foundWords = new List<Word>();
            foreach (Path p in paths)
                if (Dictionary.IsWord(p.Word))
                    foundWords.Add(new Word(p));

            // return the final soution
            return new Solution(foundWords);
        }

        private static List<Node> GetNodesForLetters (List<string> boardLetters)
        {
            int level = (int) Math.Sqrt(boardLetters.Count());
            var boardNodes = new List<Node>();
            for (int y = 0; y < level; y++)
                for (int x = 0; x < level; x++)
                    boardNodes.Add(new Node(x, y, boardLetters[y * level + x]));  // check this!

            // for each node, find all the other nodes that are adjacent to it
            foreach (var currentNode in boardNodes)
            {
                currentNode.AdjacentNodes = new List<Node>();

                // top left
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X - 1, currentNode.Y - 1);
                // top middle
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X, currentNode.Y - 1);
                // top right
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X + 1, currentNode.Y - 1);
                // middle left
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X - 1, currentNode.Y);
                // middle right
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X + 1, currentNode.Y);
                // bottom left
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X - 1, currentNode.Y + 1);
                // bottom middle
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X, currentNode.Y + 1);
                // bottom right
                AddAdjacentNode(currentNode.AdjacentNodes, boardNodes, currentNode.X + 1, currentNode.Y + 1);
            }

            // return the array of nodes as a flat list
            return boardNodes;
        }

        private static void AddAdjacentNode (List<Node> adjacentNodes, List<Node> boardNodes, int targetX, int targetY)
        {
            // look for a node at the target X and Y and if it exists, add it to the adjacentNodes list
            var node = boardNodes.SingleOrDefault(n => n.X == targetX && n.Y == targetY);
            if (node != null)
                adjacentNodes.Add(node);
        }
    }
}
