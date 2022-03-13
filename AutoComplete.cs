using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoComplete
{

    public struct FullName
    {
        public string Name;
        public string Surname;
        public string Patronymic;

        public override string ToString()
        {
            return string.Join(" ", Surname?.Trim(), Name?.Trim(), Patronymic?.Trim()).Trim();
        }
    }

    public class AutoCompleter
    {
        List<string> _fullNames;
        
        public void AddToSearch(List<FullName> fullNames)
        {
            //Добавьте новый элемент чтобы он начал участвовать в поиске по фио
            if (fullNames == null)
                throw new ArgumentNullException();
            
            _fullNames = new List<string>();
            
            foreach (var fullName in fullNames)
            {
                var fullNameStr = fullName.ToString();
                if (fullNameStr == string.Empty)
                    throw new ArgumentException();
                _fullNames.Add(fullNameStr);
            }
        }

        public List<string> Search(string prefix)
        {
            //Реализуйте алгоритм поиска по префиксу фио, как составить фио смотри условия задачи.
            if (string.IsNullOrEmpty(prefix) || string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException();
            
            if (prefix.Length > 100) // нужно ли считать пробелы ?
                throw new ArgumentOutOfRangeException();
            
            // нужно ли как-то преобразовывать prefix (убирать пробелы в начале, конце и лишние посередине)?

            return _fullNames.Where(fullName => fullName.StartsWith(prefix)).ToList();
        }
    }
}
