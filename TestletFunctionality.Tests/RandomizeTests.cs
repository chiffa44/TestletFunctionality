using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestletFunctionality.Tests
{
    [TestClass]
    public class RandomizeTests
    {
        private List<Test> generalTests = new List<Test>()
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

        private List<Test> testsWithDuplicatedId = new List<Test>()
        {new Test("1", TestTypeEnum.Pretest),
            new Test("2", TestTypeEnum.Pretest),
            new Test("3", TestTypeEnum.Operational),
            new Test("4", TestTypeEnum.Operational),
            new Test("5", TestTypeEnum.Pretest),
            new Test("6", TestTypeEnum.Operational),
            new Test("7", TestTypeEnum.Operational),
            new Test("2", TestTypeEnum.Operational),
            new Test("9", TestTypeEnum.Pretest),
            new Test("10", TestTypeEnum.Operational),
        };

        [TestMethod]
        public void TwoFirstPretests()
        {
            //Arrange
            Testlet testlet = new Testlet("testId", generalTests);
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
            Testlet testlet = new Testlet("testId", generalTests);
            //Act
            var actualResult = testlet.Randomize();
            //Assert
            CollectionAssert.AreNotEqual(generalTests, actualResult, "Initial order of generalTests should be not the same with randomized");
        }

        [TestMethod]
        public void DifferentOrderEachTime()
        {
            //Arrange
            Testlet testlet = new Testlet("testId", generalTests);
            //Act
            var actualResult1 = testlet.Randomize();
            var actualResult2 = testlet.Randomize();
            //Assert
            CollectionAssert.AreNotEqual(actualResult1, actualResult2, "There should be a different order in the testlet for each randomization");
        }

        [TestMethod]
        public void NoDuplicatesInTestlet()
        {
            //Arrange
            Testlet testlet = new Testlet("testId", generalTests);
            int initialSum = generalTests.Select(test => Int32.Parse(test.Id)).Sum();
            
            for (int i = 0; i < 50; i++)
            {
                //Act 
                var actualResult =testlet.Randomize();
                //Assert
                //sum of all ids should be the same with initial sum of all ids in testlet, if this is wrong there are duplicated generalTests
                int currentSum = actualResult.Select(test => Int32.Parse(test.Id)).Sum();
                Assert.AreEqual(initialSum, currentSum, string.Format("Expected sum of test ids: {0}, Actual sum of test ids: {1} ", initialSum, currentSum));
            }

            
        }

        [TestMethod]
        public void SameIdTestsNoDuplicates()
        {
            //Arrange - 2 tests with id = 2, one operational, one pretest
            Testlet testlet = new Testlet("testId", testsWithDuplicatedId);
            //Act
            var actualResult = testlet.Randomize();
            //Assert check that 2 tests with id=2, one operational, one pretest
            var testsWithId2 = actualResult.Where(t => t.Id == "2").ToList();
            Assert.AreEqual(2, testsWithId2.Count());
            Assert.IsTrue(testsWithId2[0].Type != testsWithId2[1].Type, "Tests with id = 2 should have different types");

        }

        [TestMethod]
        //Check that randomization gives a uniform distribution (approximately)
        //TODO: Better as integration test with large amount of data
        public void ProperRandomizing()
        {
            //Arrange
            int numberOfTestlets = 5000;
            int numberOfTests = generalTests.Count;
            int initialSum = generalTests.Select(test => Int32.Parse(test.Id)).Sum();
            //mathematical expectation (average) for uniform distribution (ideally) = sum(value*probability) 
            float probabilityIdeal = 1 / (float)numberOfTests;
            float avgIdeal = initialSum * probabilityIdeal;
            float[] avgReal = new float[numberOfTests];
            for (int i = 0; i < numberOfTestlets; i++)
            {
                //Act
                var actualResult = generalTests.Shuffle();
                
                //real mathematical expectation (average) for 1st, 2nd ..., 10 place in Testlet
                for (int j = 0; j < numberOfTests; j++)
                {
                    avgReal[j] += Int32.Parse(actualResult[j].Id);
                }

            }
            //Assert
            //should be uniform distribution +- 2,5%
            float minThreshold = avgIdeal - (float)0.025 * avgIdeal;
            float maxThreshold = avgIdeal + (float)0.025 * avgIdeal;
            for (int i = 0; i < numberOfTests; i++)
            {
                avgReal[i] /= numberOfTestlets;
                Assert.IsTrue(avgReal[i] > minThreshold && avgReal[i] < maxThreshold, string.Format("Average id for {0} place in Testlet should be between {1} and {2}, actual: {3}", i, minThreshold, maxThreshold, avgReal[i]));
            }

        }

        [TestMethod]
        public void ConstructorThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(()=>new Testlet("testId", null));
        }

        [TestMethod]
        public void ConstructorThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Testlet("testId", new List<Test>()));
        }
    }
}
