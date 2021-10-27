using System;
using Linguistador.Enums;
using Linguistador.Models;

namespace Linguistador.Extensions
{
    public static class NPTypeExtensions
    {
        public static NPType SelectNPType(this Noun n, ISelector selector)
        {
            return n.IsPronoun ? NPType.DefiniteSingle : (NPType)selector.Within(0, 4);
        }

        public static bool IsSingular(this NPType t)
        {
            return t == NPType.DefiniteSingle || t == NPType.IndefiniteSingle;
        }

        public static NPType MakeSingular(this NPType t)
        {
            if (t == NPType.IndefinitePlural)
                return NPType.IndefiniteSingle;
            if (t == NPType.DefinitePlural)
                return NPType.DefiniteSingle;
            return t;
        }

        public static NPType MakeDefinite(this NPType t)
        {
            if (t == NPType.IndefiniteSingle)
                return NPType.DefiniteSingle;
            if (t == NPType.IndefinitePlural)
                return NPType.DefinitePlural;
            return t;
        }
    }
}
