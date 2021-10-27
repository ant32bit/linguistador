using System;
using System.Linq;
using System.Collections.Generic;
using Linguistador.Models;

namespace Linguistador.Tests.Mocks
{
    public class ReplaySelector: ISelector
    {
        private IEnumerable<int> _values;
        private IEnumerator<int> _enumerator;

        public ReplaySelector(params int[] values)
        {
            _values = values;
            Reset();
        }

        public int Any()
        {
            if (_enumerator.MoveNext())
                return _enumerator.Current;
            
            throw new NoMoreValuesException();
        }

        public int Within(int minInclusive, int maxExclusive)
        {
            if (_enumerator.MoveNext())
            {
                var v = _enumerator.Current;
                if (v < minInclusive || v >= maxExclusive)
                    throw new ValueDoesNotSatisfyException();
                
                return v;
            }

            throw new NoMoreValuesException();
        }

        public void Reset()
        {
            _enumerator = _values.GetEnumerator();
        }

        public ISelector Remake() => new ReplaySelector(_values.ToArray());

        public class NoMoreValuesException : Exception
        {
            public NoMoreValuesException(string message = "No More Values") 
                : base(message) { }
        }

        public class ValueDoesNotSatisfyException : Exception
        {
            public ValueDoesNotSatisfyException(string message = "Value does not satisfy constraints")
                : base(message) { }
        } 
    }
}
