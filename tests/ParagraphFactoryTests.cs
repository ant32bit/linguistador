using System;
using System.Linq;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Linguistador.Enums;
using Linguistador.Models;
using Linguistador.Factories;
using Linguistador.Tests.Mocks;

namespace Linguistador.Tests
{
    public class ParagraphFactoryTests
    {
        [Test]
        public void CreateSentence_SubjectPlural_VerbTransitive()
        {
            var s = new Noun("s-s", "s-p");
            var st = NPType.DefinitePlural;
            var v = new Verb("v-s", "v-p", transitive: true);
            var o = new Noun("o-s", "o-p");
            var ot = NPType.DefiniteSingle;

            var factory = new ParagraphFactory(new List<Noun>(), new List<Verb>());

            var sentence = factory.CreateSentence(s, st, v, o, ot);

            Assert.AreEqual("The s-p v-p the o-s.", sentence);
        }

        [Test]
        public void CreateSentence_SubjectSingle_VerbTransitive()
        {
            var s = new Noun("s-s", "s-p");
            var st = NPType.DefiniteSingle;
            var v = new Verb("v-s", "v-p", transitive: true);
            var o = new Noun("o-s", "o-p");
            var ot = NPType.DefiniteSingle;

            var factory = new ParagraphFactory(new List<Noun>(), new List<Verb>());

            var sentence = factory.CreateSentence(s, st, v, o, ot);

            Assert.AreEqual("The s-s v-s the o-s.", sentence);
        }
        
        [Test]
        public void CreateSentence_SubjectPlural_VerbIntransitive()
        {
            var s = new Noun("s-s", "s-p");
            var st = NPType.DefinitePlural;
            var v = new Verb("v-s", "v-p", transitive: false);

            var factory = new ParagraphFactory(new List<Noun>(), new List<Verb>());

            var sentence = factory.CreateSentence(s, st, v);

            Assert.AreEqual("The s-p v-p.", sentence);
        }

        [Test]
        public void CreateSentence_SubjectSingle_VerbIntransitive()
        {
            var s = new Noun("s-s", "s-p");
            var st = NPType.DefiniteSingle;
            var v = new Verb("v-s", "v-p", transitive: false);

            var factory = new ParagraphFactory(new List<Noun>(), new List<Verb>());

            var sentence = factory.CreateSentence(s, st, v);

            Assert.AreEqual("The s-s v-s.", sentence);
        }

        [Test]
        public void CreateSentence_SubjectSingle_VerbTransitive_ObjectNull()
        {
            var s = new Noun("s-s", "s-p");
            var st = NPType.DefiniteSingle;
            var v = new Verb("v-s", "v-p", transitive: true);

            var factory = new ParagraphFactory(new List<Noun>(), new List<Verb>());

            var exception = Assert.Throws<ArgumentNullException>( () => { factory.CreateSentence(s, st, v); } );
            Assert.AreEqual("o", exception.ParamName);
            Assert.AreEqual("Must supply an object for a transitive verb. (Parameter 'o')", exception.Message);
        }

        [Test]
        public void CreateParagraph_3Sentences()
        {
            var nounBank = new List<Noun> { 
                new Noun("a-s", "a-p"),
                new Noun("b-s", "b-p"),
                new Noun("c-s", "c-p")
            };

            var verbBank = new List<Verb> { 
                new Verb("d-s", "d-p"),
                new Verb("e-s", "e-p"),
                new Verb("f-s", "f-p", transitive: false)
            };

            var factory = new ParagraphFactory(nounBank, verbBank);
            var selector = new ReplaySelector(
                0, (int)NPType.DefiniteSingle, // first subject
                1, // first verb
                1, (int)NPType.IndefinitePlural, // first object (becomes second subject)
                0, // second verb
                0, (int)NPType.IndefiniteSingle, // second object
                0); // third verb

            var paragraph = factory.CreateParagraph(selector, 3);

            Assert.AreEqual("The a-s e-s c-p. The c-p d-p a b-s. The b-s f-s.", paragraph);
        }
    }
}
