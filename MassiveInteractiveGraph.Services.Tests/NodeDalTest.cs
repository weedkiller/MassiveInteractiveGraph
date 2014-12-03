using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using log4net;
using MassiveInteractiveGraph.Dal;
using MassiveInteractiveGraph.Services.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MassiveInteractiveGraph.Services.Tests
{
    [TestClass]
    public class NodeDalTest
    {
        private IQueryable<Node> _data;

        [TestInitialize]
        public void TestInitialize()
        {
            _data = new List<Node> 
            { 
                new Node { Label = "label1", Id = 1 }, 
                new Node { Label = "label2", Id = 2 }, 
                new Node { Label = "label3", Id = 3 }, 
            }.AsQueryable();
        }

        [TestMethod]
        public void TestGetAll_Positive()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Node>>();
            mockSet.As<IQueryable<Node>>().Setup(m => m.Provider).Returns(_data.Provider);
            mockSet.As<IQueryable<Node>>().Setup(m => m.Expression).Returns(_data.Expression);
            mockSet.As<IQueryable<Node>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            mockSet.As<IQueryable<Node>>().Setup(m => m.GetEnumerator()).Returns(_data.GetEnumerator());

            var mockDb = new Mock<IMassiveInteractiveGraphDbEntities>();
            mockDb.Setup(c => c.Nodes).Returns(mockSet.Object);

            var mockDbLog = new Mock<ILog>();

            var nodesDal = new NodeDal(mockDb.Object, mockDbLog.Object);

            //Act
            var nodes = nodesDal.GetAll();

            //Assert
            Assert.AreEqual(3, nodes.Count());
        }

        [TestMethod]
        public void TestAdd_Positive()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Node>>();

            var mockDb = new Mock<IMassiveInteractiveGraphDbEntities>();
            mockDb.Setup(c => c.Nodes).Returns(mockSet.Object);

            var mockDbLog = new Mock<ILog>();

            var nodeDal = new NodeDal(mockDb.Object, mockDbLog.Object);

            //Act
            nodeDal.AddOrEdit(new Node() { Label = "label4", Id = 4 });
            nodeDal.Save();

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Node>()), Times.Once());
            mockDb.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void TestDelete_Positive()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Node>>();
            mockSet.As<IQueryable<Node>>().Setup(m => m.Provider).Returns(_data.Provider);
            mockSet.As<IQueryable<Node>>().Setup(m => m.Expression).Returns(_data.Expression);
            mockSet.As<IQueryable<Node>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            mockSet.As<IQueryable<Node>>().Setup(m => m.GetEnumerator()).Returns(_data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(_data.First());

            var mockDb = new Mock<IMassiveInteractiveGraphDbEntities>();
            mockDb.Setup(c => c.Nodes).Returns(mockSet.Object);

            var mockDbLog = new Mock<ILog>();

            var nodeDal = new NodeDal(mockDb.Object, mockDbLog.Object);

            //Act
            nodeDal.Delete(1);
            nodeDal.Save();

            //Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Node>()), Times.Once());
            mockDb.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDelete_Negative()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Node>>();
            mockSet.As<IQueryable<Node>>().Setup(m => m.Provider).Returns(_data.Provider);
            mockSet.As<IQueryable<Node>>().Setup(m => m.Expression).Returns(_data.Expression);
            mockSet.As<IQueryable<Node>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            mockSet.As<IQueryable<Node>>().Setup(m => m.GetEnumerator()).Returns(_data.GetEnumerator());

            //moq that I can't find id=0
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(_data.First());
            mockSet.Setup(m => m.Find(It.Is<int>(i => i == 0))).Returns(value: null);

            var mockDb = new Mock<IMassiveInteractiveGraphDbEntities>();
            mockDb.Setup(c => c.Nodes).Returns(mockSet.Object);

            var mockDbLog = new Mock<ILog>();

            var nodeDal = new NodeDal(mockDb.Object, mockDbLog.Object);

            //Act
            //NodeDal.Delete(1);
            nodeDal.Delete(0);

            //Assert
            //...expected Exception
        }

    }
}
