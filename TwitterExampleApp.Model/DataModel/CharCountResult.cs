using System.Collections.Generic;

namespace TwitterExampleApp.Model.DataModel
{
    /// <summary>
    /// Результат подсчета количества символов.
    /// </summary>
    public class CharCountResult
    {
        /// <summary>
        /// Количество символов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Символы.
        /// </summary>
        public IEnumerable<char> Symbols { get; set; }
    }
}
