using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0414 // Field is assigned but its value is never used
namespace utils
{
    /// <summary>
    ///     Generic Serializable Dictionary for Unity 2020.1 and above.
    ///     Simply declare your key/value types and you're good to go - zero boilerplate.
    /// </summary>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<KeyValuePair> _list = new();

        [SerializeField] [HideInInspector] private bool _keyCollision;

        private Dictionary<TKey, TValue> _dict = new();

        private Dictionary<TKey, int> _indexByKey = new();

        public TValue this[TKey key]
        {
            get => _dict[key];
            set
            {
                _dict[key] = value;
                if (_indexByKey.TryGetValue(key, out var index))
                {
                    _list[index] = new KeyValuePair(key, value);
                }
                else
                {
                    _list.Add(new KeyValuePair(key, value));
                    _indexByKey.Add(key, _list.Count - 1);
                }
            }
        }

        public ICollection<TKey> Keys => _dict.Keys;
        public ICollection<TValue> Values => _dict.Values;

        public int Count => _dict.Count;
        public bool IsReadOnly { get; set; }

        public void Add(TKey key, TValue value)
        {
            _dict.Add(key, value);
            _list.Add(new KeyValuePair(key, value));
            _indexByKey.Add(key, _list.Count - 1);
        }

        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (_dict.Remove(key))
            {
                var index = _indexByKey[key];
                _list.RemoveAt(index);
                UpdateIndexLookup(index);
                _indexByKey.Remove(key);
                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            Add(pair.Key, pair.Value);
        }

        public void Clear()
        {
            _dict.Clear();
            _list.Clear();
            _indexByKey.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            if (_dict.TryGetValue(pair.Key, out var value))
                return EqualityComparer<TValue>.Default.Equals(value, pair.Value);
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentException("The array cannot be null.");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (array.Length - arrayIndex < _dict.Count)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            foreach (var pair in _dict)
            {
                array[arrayIndex] = pair;
                arrayIndex++;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            TValue value;
            if (_dict.TryGetValue(pair.Key, out value))
            {
                var valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
                if (valueMatch) return Remove(pair.Key);
            }

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        public void OnBeforeSerialize()
        {
        }

        // Populate dictionary with pairs from list and flag key-collisions.
        public void OnAfterDeserialize()
        {
            _dict.Clear();
            _indexByKey.Clear();
            _keyCollision = false;
            for (var i = 0; i < _list.Count; i++)
            {
                var key = _list[i].Key;
                if (key != null && !ContainsKey(key))
                {
                    _dict.Add(key, _list[i].Value);
                    _indexByKey.Add(key, i);
                }
                else
                {
                    _keyCollision = true;
                }
            }
        }

        private void UpdateIndexLookup(int removedIndex)
        {
            for (var i = removedIndex; i < _list.Count; i++)
            {
                var key = _list[i].Key;
                _indexByKey[key]--;
            }
        }

        [Serializable]
        private struct KeyValuePair
        {
            public TKey Key;
            public TValue Value;

            public KeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}