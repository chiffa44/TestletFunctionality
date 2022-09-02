using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestletFunctionality.Tests
{
    [TestClass]
    public class RandomizeTests
    {
        private List<Test> tests = new List<Test>()
            {new Test("1", TestTypeEnum.Pretest),
             new Test("2", TestTypeEnum.Operational),
             new Test("3", TestTypeEnum.Operational),
             new Test("4", TestTypeEnum.Operational),
             new Test("5", TestTypeEnum.Pretest),
             new Test("6", TestTypeEnum.Operational),
             new Test("7", TestTypeEnum.Pretest),
             new Test("8", TestTypeEnum.Operational),
             new Test("9", TestTypeEnum.Pretest),
             new Test("10", TestTypeEnum.Operational),
           };

        [TestMethod]
        public void TwoFirstPretests()
        {
            //Arrange
            Testlet testlet = new Testlet("testId", tests);
            //Act
            var actualResult = testlet.Randomize();
            //Assert
            Assert.IsTrue(actualResult[0].Type == TestTypeEnum.Pretest,
                string.Format("First test should be pretest"));
            Assert.IsTrue(actualResult[0].Type == TestTypeEnum.Pretest && actualResult[1].Type == TestTypeEnum.Pretest,
                string.Format("Second test should be pretest"));
        }
    }
}
