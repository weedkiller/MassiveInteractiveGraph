using System;
using System.Collections.Generic;
using System.Linq;
using MassiveInteractiveGraph.Services.ShortestPathCalculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MassiveInteractiveGraph.Services.Tests
{
    [TestClass]
    public class QuickGraphShortestPathCalculatorTest
    {
        QuickGraphShortestPathCalculator _spc;

        [TestInitialize]
        public void Init()
        {
            _spc = new QuickGraphShortestPathCalculator();
            var links = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 3),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(2, 10),
                new Tuple<int, int>(2, 5),
                new Tuple<int, int>(3, 5),
                new Tuple<int, int>(5, 2),
                new Tuple<int, int>(5, 8),
                new Tuple<int, int>(5, 5),
                new Tuple<int, int>(5, 7),
                new Tuple<int, int>(6, 10),
                new Tuple<int, int>(6, 7),
                new Tuple<int, int>(7, 6),
                new Tuple<int, int>(8, 9),
                new Tuple<int, int>(9, 10),
                new Tuple<int, int>(10, 9),
            };
            _spc.Init(links);
        }

        [TestMethod]
        public void CalculateShortestPathTest()
        {

            var result1 = _spc.CalculateShortestPath(1, 3);
            Assert.IsNotNull(result1);
            CollectionAssert.AreEqual(new[] { 1, 3 }, result1.ToArray());
            
            var result2 = _spc.CalculateShortestPath(1, 2);
            Assert.IsNotNull(result2);
            CollectionAssert.AreEqual(new[] { 1, 2 }, result2.ToArray());
            
            var result3 = _spc.CalculateShortestPath(1, 4);
            Assert.IsNull(result3);

            var result4 = _spc.CalculateShortestPath(1, 5).ToArray();
            Assert.IsNotNull(result4);
            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, result4.ToArray());
            
            var result5 = _spc.CalculateShortestPath(1, 6);
            Assert.IsNotNull(result5);
            CollectionAssert.AreEqual(new[] { 1, 3, 5, 7, 6 }, result5.ToArray());
            
            var result6 = _spc.CalculateShortestPath(1, 7);
            Assert.IsNotNull(result6);
            CollectionAssert.AreEqual(new[] { 1, 3, 5, 7 }, result6.ToArray());
            
            var result7 = _spc.CalculateShortestPath(1, 8);
            Assert.IsNotNull(result7);
            CollectionAssert.AreEqual(new[] { 1, 3, 5, 8 }, result7.ToArray());
            
            var result8 = _spc.CalculateShortestPath(1, 9);
            Assert.IsNotNull(result8);
            CollectionAssert.AreEqual(new[] { 1, 2, 10, 9 }, result8.ToArray());

            var result9 = _spc.CalculateShortestPath(1, 10);
            Assert.IsNotNull(result9);
            CollectionAssert.AreEqual(new[] { 1, 2, 10 }, result9.ToArray());
        }
    }
}
