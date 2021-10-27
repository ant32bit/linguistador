using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Linguistador.Factories;

namespace Linguistador.Models 
{
    public class DataSet: IEnumerable<KeyValuePair<Guid, string>>
    {
        private IParagraphFactory _paragraphFactory;
        private ISelector _selector;
        private readonly uint _size;

        public DataSet(IParagraphFactory paragraphFactory, ISelector selector, uint size)
        {
            _paragraphFactory = paragraphFactory;
            _selector = selector;
            _size = size;
        }

        public IEnumerator<KeyValuePair<Guid, string>> GetEnumerator()
        {
            return new DataSetEnumerator(_paragraphFactory, _selector.Remake(), _size);
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) GetEnumerator();
    }

    public class DataSetEnumerator: IEnumerator<KeyValuePair<Guid, string>>
    {
        private readonly uint _size;
        private EnumeratorState _state;

        private uint _position;
        private KeyValuePair<Guid, string> _current;
        
        private IParagraphFactory _paragraphFactory;
        private ISelector _selector;

        public DataSetEnumerator(IParagraphFactory paragraphFactory, ISelector selector, uint size)
        {
            _paragraphFactory = paragraphFactory;
            _selector = selector;
            _size = size;
            Reset();
        }

        public KeyValuePair<Guid, string> Current => _current;
        object IEnumerator.Current => _current;

        public bool MoveNext()
        {
            if (_state == EnumeratorState.Waiting)
            {
                _state = EnumeratorState.Reading;
                GenerateCurrent();
                return true;
            }
            
            if (_state == EnumeratorState.Reading && _position < _size - 1)
            {
                _position++;
                GenerateCurrent();
                return true;
            }

            _state = EnumeratorState.Ended;
            return false;
        }

        public void Reset()
        {
            _selector.Reset();
            _position = 0;
            _state = EnumeratorState.Waiting;
            _current = default(KeyValuePair<Guid, string>);
        }

        public void Dispose() { }

        private void GenerateCurrent()
        {
            var length = (uint)_selector.Within(1, 10);
            var value = _paragraphFactory.CreateParagraph(_selector, length);
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var input = string.Join('|', _position, value);
                var bytes = System.Text.Encoding.ASCII.GetBytes(input);
                var hash = md5.ComputeHash(bytes);
                var guid = new Guid(hash);
                _current = new KeyValuePair<Guid, string>(guid, value);
            }
        }

        private enum EnumeratorState 
        {
            Waiting,
            Reading,
            Ended
        }
    }
}
