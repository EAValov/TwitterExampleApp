using System;
using System.Collections.Generic;
using System.Linq;
using TwitterExampleApp.Model.DataModel;

namespace TwitterExampleApp.Model.Service
{
    /// <summary>
    /// Сервис, считающий количество символов в списке слов.
    /// Обычно я выношу неспецифические сервисы, чтобы ими можно было пользоваться в других приложениях.
    /// </summary>
    public class CharCounterService
    {
        /// <summary>
        /// Возвращает самый часто используемый символ в списке слов.
        /// </summary>
        /// <param name="input_string"></param>
        /// <returns>Словарь самых частых букв.</returns>
        public CharCountResult CountMostOccuringCharInString(string input_string)
        {
            if (string.IsNullOrWhiteSpace(input_string))
                throw new Exception("Строка, поданная на вход была пуста или null или содержала только пробелы.");

            var count_dictionary = new Dictionary<char, int>();

            foreach(char c in input_string)
            {
                if (count_dictionary.ContainsKey(c))
                    count_dictionary[c]++;
                else
                    count_dictionary.Add(c, 1);
            }

            var max_count = count_dictionary.Values.Max();

            return new CharCountResult() { Count = max_count, Symbols = count_dictionary.Where(f => f.Value == max_count).Select(g => g.Key) };
        }      
    }
}
