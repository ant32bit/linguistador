using System;
using System.Collections.Generic;

namespace Linguistador.Models
{
    public class ParagraphState
    {
        private ISelector _selector { get; }
        private int _librarySize { get; }
        private List<int> _used { get; }

        public ParagraphState(ISelector selector, int librarySize)
        {
            _selector = selector;
            _librarySize = librarySize;
            _used = new List<int>();
        }

        public int SelectOne()
        {
            if (_librarySize - _used.Count <= 0)
                throw new IndexOutOfRangeException();
            
            var idx = _selector.Within(0, _librarySize - _used.Count);
            var inserted = false;
            for (var i = 0; i < _used.Count; i++)
            {
                if (_used[i] <= idx) 
                {
                    idx++;
                }
                else {
                    _used.Insert(i, idx);
                    inserted = true;
                    break;
                }
            }

            if (!inserted)
                _used.Add(idx);

            return idx;
        }
    }
}
