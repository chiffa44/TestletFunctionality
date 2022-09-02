using System;
using System.Collections.Generic;

namespace TestletFunctionality
{
    public class Testlet
    {
        public string TestletId;
        private List<Test> Items;
        public Testlet(string testletId, List<Test> items)
        {
            TestletId = testletId;
            Items = items;
        }
        public List<Test> Randomize()
        {
            //Items private collection has 6 Operational and 4 Pretest Items.
            //Randomize the order of these items as per the requirement(with TDD)
            //The assignment will be reviewed on the basis of – Tests written first, Correct
            //logic, Well structured &clean readable code.
            throw new NotImplementedException();
        }
    }
    public class Test
    {
        public string Id;
        public TestTypeEnum Type;
        public Test(string id, TestTypeEnum type)
        {
            Id = id;
            Type = type;
        }
    }
    public enum TestTypeEnum
    {
        Pretest = 0,
        Operational = 1
    }
}
