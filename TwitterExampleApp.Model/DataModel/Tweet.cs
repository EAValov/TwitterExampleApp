using System;

namespace TwitterExampleApp.Model.DataModel
{
    /// <summary>
    /// Твит пользователя.
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// Текст твита.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата и время.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
