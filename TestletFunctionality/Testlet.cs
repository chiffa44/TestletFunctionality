using System;
using System.Collections.Generic;
using System.Linq;

namespace TestletFunctionality
{
    public class Testlet
    {
        public string Id;
        private List<Test> Items;
        public Testlet(string testletId, List<Test> items)
        {
            Id = testletId;
            Items = new List<Test>(items);
        }
        public List<Test> Randomize()
        {
            var randomized = Items.Shuffle();
            var pretests = randomized.Where(t => t.Type == TestTypeEnum.Pretest).Take(2);
            var other = randomized.Except(pretests);
            return pretests.Concat(other).ToList();

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
