using System.Collections;
using System.Collections.Generic;
using Linguistador.Models;

namespace Linguistador.Repositories
{
    public class NounBank: IReadOnlyList<Noun>
    {
        private static readonly Noun[] _nouns = GenerateNouns();
        
        public Noun this[int index] => _nouns[index];
        public int Count => _nouns.Length;
        public IEnumerator<Noun> GetEnumerator() => (IEnumerator<Noun>) _nouns.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) GetEnumerator();

        private static Noun[] GenerateNouns()
        {
            return new Noun[]
            {
                new Noun("aardvark", "aardvarks"),
                new Noun("aeroplane", "aeroplanes"),
                new Noun("ant", "ants"),
                new Noun("ball", "balls"),
                new Noun("Boris"),
                new Noun("cat", "cats"),
                new Noun("computer", "computers"),
                new Noun("cow", "cows"),
                new Noun("dog", "dogs"),
                new Noun("fish", "fish"),
                new Noun("horse", "horses"),
                new Noun("James"),
                new Noun("Jessica"),
                new Noun("lollipop", "lollipops"),
                new Noun("Oregon"),
                new Noun("person", "people"),
                new Noun("pie", "pies"),
                new Noun("politician", "politicians"),
                new Noun("slipper", "slippers"),
                new Noun("the Sydney Opera House"),
                new Noun("toy", "toys"),
                new Noun("typewriter", "typewriters"),
                new Noun("William"),
            };
        }
    }
}
