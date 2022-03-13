using System;
using System.Collections.Generic;

namespace AutoComplete
{
    public struct FullName
    {
        public string Name;
        public string Surname;
        public string Patronymic;

        public override string ToString() =>
            string.Join(" ", Surname?.Trim(), Name?.Trim(), Patronymic?.Trim()).Trim();
        
        public override bool Equals(object? obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
            
            var fullName = (FullName) obj;

            return
                Name == fullName.Name &&
                Surname == fullName.Surname &&
                Patronymic == fullName.Patronymic;
        }

        public override int GetHashCode()
        {
            var hash = 19;

            if (Surname != null)
                hash = 23 * hash + Surname.GetHashCode();
            if (Name != null)
                hash = 23 * hash + Name.GetHashCode();
            if (Patronymic != null)
                hash = 23 * hash + Patronymic.GetHashCode();

            return hash;
        }
    }
    
    public class TrieNode
    {
        FullName? _data;
        readonly Dictionary<char, TrieNode> _children = new();

        public void Add(string path, FullName data)
        {
            Add(0, path, data);
        }

        void Add(int index, string path, FullName data)
        {
            if (index == path.Length)
            {
                _data = data;
                return;
            }

            var key = path[index];
            if (!_children.ContainsKey(key))
                _children.Add(key, new TrieNode());
            
            _children[key].Add(index + 1, path, data);
        }

        public void GetByPrefix(string prefix, List<string> result) =>
            GetByPrefix(0, prefix, result);

        void GetByPrefix(int index, string prefix, List<string> result)
        {
            if (_data.HasValue)
                result.Add(_data.Value.ToString());
            
            if (index == prefix.Length)
            {
                foreach (var childValue in _children.Values)
                    childValue.GetByPrefix(index, prefix, result);
            }
            else
            {
                var key = prefix[index];
                if (_children.ContainsKey(key))
                    _children[key].GetByPrefix(index + 1, prefix, result);
            }
        }
    }

    public class AutoCompleter
    {
        readonly TrieNode _trie = new();

        public void AddToSearch(List<FullName> fullNames)
        {
            if (fullNames == null)
                throw new ArgumentNullException();

            foreach (var fullName in fullNames)
            {
                var fullNameStr = fullName.ToString();
                if (fullNameStr == string.Empty)
                    throw new ArgumentException();
                _trie.Add(fullNameStr, fullName);
            }
        }

        public List<string> Search(string prefix)
        {
            if (string.IsNullOrEmpty(prefix) || string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException();

            if (prefix.Length > 100)
                throw new ArgumentOutOfRangeException();
            
            var result = new List<string>();
            _trie.GetByPrefix(prefix, result);

            return result;
        }
    }
}
