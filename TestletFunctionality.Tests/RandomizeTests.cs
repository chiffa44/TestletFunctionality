using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestletFunctionality.Tests
{
    [TestClass]
    public class RandomizeTests
    {
        private List<Test> tests = new List<Test>()
            {new Test("1", TestTypeEnum.Pretest),
             new Test("2", TestTypeEnum.Pretest),
             new Test("3", TestTypeEnum.Operational),
             new Test("4", TestTypeEnum.Operational),
             new Test("5", TestTypeEnum.Pretest),
             new Test("6", TestTypeEnum.Operational),
             new Test("7", TestTypeEnum.Operational),
             new Test("8", TestTypeEnum.Operational),
             new Test("9", TestTypeEnum.Pretest),
             new Test("10", TestTypeEnum.Operational),
           };

        [TestMethod]
        public void TwoFirstPretests()
        {
            //Arrange
            Testlet testlet = new Testlet("testId", tests.ToList());
            //Act
            var actualResult = testlet.Randomize();
            //Assert
            Assert.IsTrue(actualResult[0].Type == TestTypeEnum.Pretest,
                string.Format("First test should be pretest"));
            Assert.IsTrue(actualResult[0].Type == TestTypeEnum.Pretest && actualResult[1].Type == TestTypeEnum.Pretest,
                string.Format("Second test should be pretest"));
        }

        [TestMethod]
        public void RandomizedTestletIsNotEqualInitial()
        {
            //Arrange
            Testlet testlet = new Testlet("testId", tests.ToList());
            //Act
            var actualResult = testlet.Randomize();
            //Assert
            CollectionAssert.AreNotEqual(tests, actualResult, "Initial order of tests should be not the same with randomized");
        }

        [TestMethod]
        public void DifferentOrderEachTime()
        {
            //Arrange
            Testlet testlet = new Testlet("testId", tests.ToList());
            //Act
            var actualResult1 = testlet.Randomize();
            var actualResult2 = testlet.Randomize();
            //Assert
            CollectionAssert.AreNotEqual(actualResult1, actualResult2, "There should be a different order in the testlet for each randomization");
        }
    }
}
