﻿using System;
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
            var links = ShortestPathCalculatorTestUtils.GetLinks();
            _spc.Init(links.Select(l => new Tuple<int, int>(l.Item1, l.Item2)));
        }

        [TestMethod]
        public void CalculateShortestPathTest()
        {
            foreach (var testNodesAndResult in ShortestPathCalculatorTestUtils.GetTestNodesAndResults())
            {
                var sourceAndTarget = testNodesAndResult.Item1;
                var possibleResults = testNodesAndResult.Item2;

                var result = _spc.CalculateShortestPath(sourceAndTarget.Item1, sourceAndTarget.Item2);
                if (possibleResults == null)
                {
                    Assert.IsNull(result);
                }
                else
                {
                    Assert.IsNotNull(result);
                    var oneOfPossileResultsIsOK = false;
                    foreach (var possibleResult in possibleResults)
                    {
                        if (possibleResult.SequenceEqual(result))
                        {
                            oneOfPossileResultsIsOK = true;
                            break;
                        }
                    }

                    Assert.IsTrue(oneOfPossileResultsIsOK);
                }
            }
        }
    }
}
