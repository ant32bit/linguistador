namespace Linguistador.Models
{
    public class Verb
    {
        public string SingleAgreement { get; }
        public string PluralAgreement { get; }
        public bool IsTransitive { get; }

        public Verb(string single, string plural, bool transitive = true)
        {
            SingleAgreement = single;
            PluralAgreement = plural;
            IsTransitive = transitive;
        }

        public Verb(string phrase, bool transitive = true)
        {
            SingleAgreement = phrase;
            PluralAgreement = phrase;
            IsTransitive = transitive;
        }
    }
}
