using System;

namespace AutoComplete;

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