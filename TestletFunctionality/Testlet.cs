using System;
using System.Collections.Generic;
using System.Linq;

namespace TestletFunctionality
{
    public class Testlet
    {
        public string Id { get; }
        private List<Test> Items { get; }

        public Testlet(string testletId, List<Test> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "Collection of tests cannot be null.");
            }
            if (items.Count == 0)
            {
                throw new ArgumentException("Collection of tests cannot be empty.", nameof(items));
            }
            if (items.Count(t => t.Type == TestTypeEnum.Pretest) != 4)
            {
                throw new ArgumentException("Collection of tests should contain 4 pretests.", nameof(items));
            }
            Id = testletId;
            Items = new List<Test>(items);
        }

        public List<Test> Randomize()
        {
            var randomized = Items.FisherYatesShuffle();
            List<Test> pretests = randomized.Where(t => t.Type == TestTypeEnum.Pretest).Take(2).ToList();
            var other = randomized.Except(pretests);
            return pretests.Concat(other).ToList();
        }
    }
}
