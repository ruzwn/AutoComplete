using System;
using System.Collections.Generic;

namespace AutoComplete;

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