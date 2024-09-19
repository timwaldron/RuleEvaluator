using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace RuleEvaluator.Models
{
    // This class was mostly just auto-generated, essentially just a wrapper class around IDictionary<string, int>
    public class Transaction : IDictionary<string, int>
    {
        private readonly IDictionary<string, int> _dictionary;

        public Transaction()
        {
            _dictionary = new Dictionary<string, int>();
        }

        public int this[string key] { get => _dictionary[key]; set => _dictionary[key] = value; }

        public ICollection<string> Keys => _dictionary.Keys;

        public ICollection<int> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => _dictionary.IsReadOnly;

        public void Add(string key, int value)
        {
            _dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<string, int> item)
        {
            _dictionary.Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, int> item)
        {
            return _dictionary.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, int>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<string, int> item)
        {
            return _dictionary.Remove(item);
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out int value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
