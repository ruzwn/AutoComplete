using System.Collections.Generic;

namespace AutoComplete;

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