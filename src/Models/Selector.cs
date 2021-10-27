using System;

namespace Linguistador.Models
{
    public interface ISelector
    {
        int Any();
        int Within(int minInclusive, int maxExclusive);
        void Reset();
        ISelector Remake();
    }

    public class RandomSelector: ISelector
    {
        private Random _rng;
        private int _seed;

        public RandomSelector(int? seed = null)
        {
            _seed = seed ?? new Random().Next();
            Reset();
        }

        public int Any() => _rng.Next();
        public int Within(int minInclusive, int maxExclusive) => _rng.Next(minInclusive, maxExclusive);
        public void Reset()
        {
            _rng = new Random(_seed);
        }

        public ISelector Remake()
        {
            return new RandomSelector(_seed);
        }
    }
}
