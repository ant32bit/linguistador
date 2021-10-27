using System.Linq;
using NUnit.Framework;
using Linguistador.Enums;
using Linguistador.Models;

namespace Linguistador.Tests
{
    public class NounTests
    {
        [Test]
        public void Noun_StartsWithConsonant_IndefiniteArticle()
        {
            var nouns = "bcdfgjklmnpqrstvwxyz"
                .ToCharArray()
                .Select(x => x.ToString() + "-name")
                .Select(x => new Noun(x, x + "s"));

            foreach (var noun in nouns)
            {
                Assert.IsTrue(noun[NPType.IndefiniteSingle].StartsWith("a "));
            }
        }

        [Test]
        public void Noun_StartsWithVowel_IndefiniteArticle()
        {
            var nouns = "aehiou"
                .ToCharArray()
                .Select(x => x.ToString() + "-name")
                .Select(x => new Noun(x, x + "s"));

            foreach (var noun in nouns)
            {
                Assert.IsTrue(noun[NPType.IndefiniteSingle].StartsWith("an "));
            }
        }

        [Test]
        public void Noun_Noun_ValidNPTypes()
        {
            var noun = new Noun("dog", "dogs");
            Assert.AreEqual("the dog", noun[NPType.DefiniteSingle]);
            Assert.AreEqual("the dogs", noun[NPType.DefinitePlural]);
            Assert.AreEqual("a dog", noun[NPType.IndefiniteSingle]);
            Assert.AreEqual("dogs", noun[NPType.IndefinitePlural]);
            Assert.AreEqual(false, noun.IsPronoun);
        }


        [Test]
        public void Noun_Pronoun_ValidNPTypes()
        {
            var noun = new Noun("James");
            Assert.AreEqual("James", noun[NPType.DefiniteSingle]);
            Assert.IsNull(noun[NPType.DefinitePlural]);
            Assert.IsNull(noun[NPType.IndefiniteSingle]);
            Assert.IsNull(noun[NPType.IndefinitePlural]);
            Assert.AreEqual(true, noun.IsPronoun);
        }
    }
}
