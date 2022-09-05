# TestletFunctionality

## Requirements
Candidates are given a set of items they need to answer. This set is called a Testlet.
There is a Testlet with a fixed set of 10 items. 6 of the items are operational and 4 of them are pretest items.
The requirement is that the _order_ of these items should be randomized such that -
- The first 2 items are always pretest items selected randomly from the 4 pretest items.
- The next 8 items are mix of pretest and operational items ordered randomly from the remaining 8
items.

## Comments
1. The class library was created using .Net Core 2.1 and MSTest 
2. For order randomization is used [Fisher-Yates shuffle](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle).
Validated in test ProperRandomization on 5000 testlets using a check that the distribution follows a uniform distribution +- 2.5%.
3. Library throws exceptions if the test collection is empty or null.
4. Algorithm is not thread safe (Random isn't thread safe).
