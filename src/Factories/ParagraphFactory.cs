using System;
using System.Text;
using System.Collections.Generic;
using Linguistador.Enums;
using Linguistador.Models;
using Linguistador.Extensions;

namespace Linguistador.Factories
{
    public interface IParagraphFactory
    {
        string CreateParagraph(ISelector selector, uint nSentences);
    }

    public class ParagraphFactory: IParagraphFactory
    {
        private readonly IReadOnlyList<Verb> _verbs;
        private readonly IReadOnlyList<Noun> _nouns;

        public ParagraphFactory(IReadOnlyList<Noun> nouns, IReadOnlyList<Verb> verbs)
        {
            _verbs = verbs;
            _nouns = nouns;
        }

        public string CreateParagraph(ISelector selector, uint nSentences)
        {
            var sentences = new List<string>();
            var nounsPool = new ParagraphState(selector, _nouns.Count);
            var verbsPool = new ParagraphState(selector, _verbs.Count);

            var s = _nouns[nounsPool.SelectOne()];
            var sType = s.SelectNPType(selector);
            
            for (var i = 0; i < nSentences; i++) {
                var v = _verbs[verbsPool.SelectOne()];
                var o = v.IsTransitive ? _nouns[nounsPool.SelectOne()] : (Noun) null;
                var oType = o != null ? o.SelectNPType(selector) : sType;

                sentences.Add(CreateSentence(s, sType, v, o, oType));
                
                if (o != null) {
                    s = o;
                    sType = oType;
                }
                
                sType = sType.MakeDefinite();
            }
            
            return string.Join(" ", sentences);
        }

        public string CreateSentence(Noun s, NPType st, Verb v, Noun o = null, NPType ot = NPType.IndefiniteSingle)
        {
            var sCap = s[st];
                sCap = sCap.Substring(0,1).ToUpper() + sCap.Substring(1);
            
            var vText = st.IsSingular() ? v.SingleAgreement : v.PluralAgreement;

            if (v.IsTransitive && o == null)
                throw new ArgumentNullException("o", "Must supply an object for a transitive verb.");

            var sentence = v.IsTransitive ?
                string.Join(" ", sCap, vText, o[ot]) :
                string.Join(" ", sCap, vText);

            return sentence + '.';
        }
    }
}
