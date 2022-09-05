using System.Collections.Generic;

namespace TestletFunctionality
{
    public interface IShuffler<T>
    {
        IList<T> Shuffle(IList<T> list);
    }
}