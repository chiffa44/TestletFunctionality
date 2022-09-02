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
            Items = items;
        }
        public List<Test> Randomize()
        {
            Items.Shuffle();
            var pretests = Items.Where(t => t.Type == TestTypeEnum.Pretest).Take(2);
            var other = Items.Except(pretests);
            Items = pretests.Concat(other).ToList();

            return Items;
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
