using System;
using System.Collections.Generic;
using Linguistador.Enums;

namespace Linguistador.Models
{
    public class Noun
    {
        public bool IsPronoun { get; }
        
        private IReadOnlyDictionary<NPType, string> _variations;
        public string this[NPType index] => _variations[index];

        public Noun(string single, string plural)
        {
            var first = single[0];
            _variations = new Dictionary<NPType, string>
            {
                {NPType.DefiniteSingle, string.Join(" ", "the", single)},
                {NPType.DefinitePlural, string.Join(" ", "the", plural)},
                {NPType.IndefiniteSingle,
                    ((first == 'a' || first == 'e' || first == 'h' ||
                    first == 'i' || first == 'o' || first == 'u') ?
                    "an " : "a ") + single},
                {NPType.IndefinitePlural, plural}
            };
            
            IsPronoun = false;
        }

        public Noun(string name)
        {
            _variations = new Dictionary<NPType, string>
            {
                {NPType.DefiniteSingle, name},
                {NPType.DefinitePlural, null},
                {NPType.IndefiniteSingle, null},
                {NPType.IndefinitePlural, null}
            };

            IsPronoun = true;
        }
    }
}
