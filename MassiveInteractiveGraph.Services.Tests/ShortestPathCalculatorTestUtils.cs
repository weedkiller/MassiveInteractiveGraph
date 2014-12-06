using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MassiveInteractiveGraph.Services.Tests
{
    public class ShortestPathCalculatorTestUtils
    {
        public static List<TestLink> GetLinks()
        {
            var links = new List<TestLink>()
            {
                new TestLink(1, 3),
                new TestLink(2, 1),
                new TestLink(2, 10),
                new TestLink(2, 5),
                new TestLink(3, 5),
                new TestLink(5, 2),
                new TestLink(5, 8),
                new TestLink(5, 5),
                new TestLink(5, 7),
                new TestLink(6, 10),
                new TestLink(6, 7),
                new TestLink(7, 6),
                new TestLink(8, 9),
                new TestLink(9, 10),
                new TestLink(10, 9),
            };

            return links;
        }

        public static List<Tuple<TestLink, List<int[]>>> GetTestNodesAndResults()
        {
            var toReturn = new List<Tuple<TestLink, List<int[]>>>();

            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 2), new List<int[]>() { new[] { 1, 2 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 3), new List<int[]>() { new[] { 1, 3 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 4), null));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 5), new List<int[]>() { new[] { 1, 3, 5 }, new[] { 1, 2, 5 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 6), new List<int[]>() { new[] { 1, 2, 10, 6 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 7), new List<int[]>() { new[] { 1, 3, 5, 7 }, new[] { 1, 2, 5, 7 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 8), new List<int[]>() { new[] { 1, 3, 5, 8 }, new[] { 1, 2, 5, 8 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 9), new List<int[]>() { new[] { 1, 2, 10, 9 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(1, 10), new List<int[]>() { new[] { 1, 2, 10 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(3, 1), new List<int[]>() { new[] { 3, 1 } }));
            toReturn.Add(new Tuple<TestLink, List<int[]>>(new TestLink(10, 1), new List<int[]>() { new[] { 10, 2, 1 } }));

            return toReturn;
        } 
    }

    public class TestLink
    {
        public TestLink(int item1, int item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public int Item1 { get; set; }
        public int Item2 { get; set; }

    }
}
