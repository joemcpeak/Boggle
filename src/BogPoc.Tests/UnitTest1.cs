using BogPoc.WordFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BogPoc.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1 ()
        {
            var letters = "ABCDEFGHI";
            var s = Board.Solve(letters);

            Assert.AreEqual("ABE", s.Words[0].Value);
            Assert.AreEqual(0, s.Words[0].Path.Nodes[0].X);
            Assert.AreEqual(0, s.Words[0].Path.Nodes[0].Y);
            Assert.AreEqual(1, s.Words[0].Path.Nodes[1].X);
            Assert.AreEqual(0, s.Words[0].Path.Nodes[1].Y);
            Assert.AreEqual(1, s.Words[0].Path.Nodes[2].X);
            Assert.AreEqual(1, s.Words[0].Path.Nodes[2].Y);

        }

        [TestMethod]
        public void TestMethod2 ()
        {
            // recreates a specific case where it didn't find "ghost" in a board that contained it
            var letters = "";
            letters += "SGETWX";
            letters += "HOITKA";
            letters += "LOSNVB";
            letters += "HMINDT";
            letters += "WAAYSH";
            letters += "MIDTLF";
            var s = Board.Solve(letters);

            Assert.AreEqual(148, s.Words.Count());
            Assert.AreEqual(206, s.Score);
            s.Words.Single(w => w.Value == "GHOST");
        }
    }
}
