using System.Collections.Generic;
using TwitterExampleApp.Model.DataModel;

namespace TwitterExampleApp.Model.Repository
{
    /// <summary>
    /// Провайдер доступа к твиттеру.
    /// </summary>
    public interface ITwitterRepository
    {
        /// <summary>
        /// Получение последних (по дате) твитов.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="auth_data">Данные для авторизации в твиттере.</param>
        /// <param name="count_twits">Количество записей.</param>
        /// <returns>Список последних твитов. </returns>
        IEnumerable<Tweet> GetLastTweets(string username, TwitterAuthData auth_data, int count_twits = 5);

        /// <summary>
        /// Метод, постящий твит.
        /// </summary>
        /// <param name="tweet">Строка твита.</param>
        /// <param name="auth_data">Данные для авторизации в твиттере.</param>
        void PostTweet(string tweet, TwitterAuthData auth_data);
    }
}
