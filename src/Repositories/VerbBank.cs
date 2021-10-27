using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Linguistador.Models;

namespace Linguistador.Repositories
{
    public class VerbBank: IReadOnlyList<Verb>
    {
        private static readonly Verb[] _verbs = GenerateVerbs();
        
        public Verb this[int index] => _verbs[index];
        public int Count => _verbs.Length;
        public IEnumerator<Verb> GetEnumerator() => (IEnumerator<Verb>) _verbs.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) GetEnumerator();

        private static IEnumerable<Verb> MakeTransitiveSet(string root, string singular, string present, string past, Irregulars irregulars = null)
        {
            return new List<Verb> {
                new Verb(singular, root),
                new Verb($"will {root}"),
                new Verb($"can {root}"),
                new Verb($"should {root}"),
                new Verb(irregulars?.Past ?? past),
                new Verb($"will be {past} by"),
                new Verb($"should be {past} by"),
                new Verb($"has {past}", $"have {past}"),
                new Verb($"is {present}", $"are {present}"),
                new Verb($"was {present}", $"were {present}"),
                new Verb($"has been {present}", $"have been {present}"),
                new Verb($"is {past} by", $"are {past} by"),
                new Verb($"has been {past} by", $"have been {past} by")
            };
        }

        private static IEnumerable<Verb> MakeIntransitiveSet(string root, string singular, string present, string past, Irregulars irregulars = null)
        {
            return new List<Verb> {
                new Verb(singular, root, false),
                new Verb($"will {root}", false),
                new Verb($"can {root}", false),
                new Verb($"should {root}", false),
                new Verb(irregulars?.Past ?? past, false),
                new Verb($"has {past}", $"have {past}", false),
                new Verb($"is {present}", $"are {present}", false),
                new Verb($"was {present}", $"were {present}", false),
                new Verb($"has been {present}", $"have been {present}", false),
                new Verb($"{singular} with", $"{root} with", true),
                new Verb($"will {root} with", true),
                new Verb($"has {past} with", $"have {past} with", true),
                new Verb($"is {present} with", $"are {present} with", true),
                new Verb($"was {present} with", $"were {present} with", true),
                new Verb($"has been {present} with", $"have been {present} with", true),
            };
        }

        private class Irregulars
        {
            public string Past { get; set; }
        }

        public static Verb[] GenerateVerbs()
        {
            return new List<IEnumerable<Verb>>
            {
                MakeTransitiveSet("buy", "buys", "buying", "bought"),
                MakeTransitiveSet("eat", "eats", "eating", "eaten", new Irregulars { Past = "ate" }),
                MakeTransitiveSet("follow", "follows", "following", "followed"),
                MakeTransitiveSet("love", "loves", "loving", "loved"),
                MakeTransitiveSet("make", "makes", "making", "made"),
                MakeTransitiveSet("meet", "meets", "meeting", "met"),
                MakeTransitiveSet("need", "needs", "needing", "needed"),
                MakeTransitiveSet("own", "owns", "owning", "owned"),
                MakeIntransitiveSet("play", "plays", "playing", "played"),
                MakeTransitiveSet("please", "pleases", "pleasing", "pleased"),
                MakeTransitiveSet("praise", "praises", "praising", "praised"),
                MakeTransitiveSet("see", "sees", "seeing", "seen", new Irregulars { Past = "saw" }),
                MakeIntransitiveSet("sleep", "sleeps", "sleeping", "slept"),
                MakeIntransitiveSet("speak", "speaks", "speaking", "spoken", new Irregulars { Past = "spoke" }),
                MakeIntransitiveSet("think", "thinks", "thinking", "thought"),
                MakeTransitiveSet("visit", "visits", "visiting", "visited"),
                MakeIntransitiveSet("wait", "waits", "waiting", "waited"),
                MakeTransitiveSet("want", "wants", "wanting", "wanted"),
                MakeTransitiveSet("wash", "washes", "washing", "washed"),
                MakeTransitiveSet("watch", "watches", "watching", "watched"),
            }
                .SelectMany(x => x)
                .ToArray();
        }
    }
}
