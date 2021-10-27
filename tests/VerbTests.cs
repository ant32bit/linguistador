using System.Linq;
using NUnit.Framework;
using Linguistador.Enums;
using Linguistador.Models;

namespace Linguistador.Tests
{
    public class VerbTests
    {
        [Test]
        public void Verb_Constructors()
        {
            var verb1 = new Verb("verb-s", "verb-p", true);
            Assert.AreEqual("verb-s", verb1.SingleAgreement);
            Assert.AreEqual("verb-p", verb1.PluralAgreement);
            Assert.AreEqual(true, verb1.IsTransitive);

            var verb2 = new Verb("verb-s", "verb-p", false);
            Assert.AreEqual("verb-s", verb2.SingleAgreement);
            Assert.AreEqual("verb-p", verb2.PluralAgreement);
            Assert.AreEqual(false, verb2.IsTransitive);

            var verb3 = new Verb("verb-a", true);
            Assert.AreEqual("verb-a", verb3.SingleAgreement);
            Assert.AreEqual("verb-a", verb3.PluralAgreement);
            Assert.AreEqual(true, verb3.IsTransitive);

            var verb4 = new Verb("verb-a", false);
            Assert.AreEqual("verb-a", verb4.SingleAgreement);
            Assert.AreEqual("verb-a", verb4.PluralAgreement);
            Assert.AreEqual(false, verb4.IsTransitive);
        }
    }
}
