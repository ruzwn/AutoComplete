using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace AutoComplete
{

    public struct FullName
    {
        public string Name;
        public string Surname;
        public string Patronymic;
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
                var surName = string.Empty;
                var name = string.Empty;
                var patronymic = string.Empty;

                if (fullName.Surname != null)
                    surName = fullName.Surname.Trim();
                if (fullName.Name != null)
                    name = fullName.Name.Trim();
                if (fullName.Patronymic != null)
                    patronymic = fullName.Patronymic.Trim();

                var fullNameStr = (surName + " " + name + " " + patronymic).Trim();
                if (fullNameStr == string.Empty)
                    throw new ArgumentException();
                _fullNames.Add(fullNameStr);
            }
        }

        public List<string> Search(string prefix)
        {
            //Реализуйте алгоритм поиска по префиксу фио, как составить фио смотри условия задачи.
            if (prefix == null)
                throw new ArgumentNullException();
            if (prefix.Length > 100) // нужно ли считать пробелы ?
                throw new ArgumentOutOfRangeException();
            
            var prefixTrimmed = prefix.Trim();
            if (prefixTrimmed == string.Empty)
                throw new ArgumentException();

            return _fullNames.Where(fullName => fullName.StartsWith(prefixTrimmed)).ToList();
        }
    }
}
