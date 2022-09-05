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
            if (items.Count(t => t.Type == TestTypeEnum.Pretest) != Configuration.NumberOfPretestsInTestlet)
            {
                throw new ArgumentException($"Collection of tests should contain {Configuration.NumberOfPretestsInTestlet} pretests.", nameof(items));
            }
            Id = testletId;
            Items = new List<Test>(items);
        }

        public List<Test> Randomize(IShuffler<Test> shuffler)
        {
            var randomized = shuffler.Shuffle(Items);
            List<Test> pretests = randomized.Where(t => t.Type == TestTypeEnum.Pretest).Take(Configuration.NumberOfPretestAtBeginning).ToList();
            var other = randomized.Except(pretests);
            return pretests.Concat(other).ToList();
        }
    }
}
