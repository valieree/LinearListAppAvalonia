using System;
using System.Collections.Generic;

namespace LinearListApp.Models
{
    public class LinearList<T>
    {
        private List<T> _items = new();
        private int _currentIndex = -1;

        public T? CurrentItem => _currentIndex >= 0 && _currentIndex < _items.Count ? _items[_currentIndex] : default;
        public int Count => _items.Count;
        public bool IsEmpty => _items.Count == 0;

        public void Add(T item)
        {
            _items.Add(item);
            if (_currentIndex == -1) _currentIndex = 0;
        }

        public bool Remove(T item)
        {
            int index = _items.IndexOf(item);
            if (index != -1)
            {
                _items.RemoveAt(index);
                if (_currentIndex >= _items.Count) _currentIndex = _items.Count - 1;
                return true;
            }
            return false;
        }

        public bool MoveNext()
        {
            if (_currentIndex < _items.Count - 1)
            {
                _currentIndex++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _currentIndex = _items.Count > 0 ? 0 : -1;
        }
    }
}
