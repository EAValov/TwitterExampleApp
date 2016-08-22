using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using TwitterExampleApp.Model.DataModel;
using TwitterExampleApp.Model.Exceptions;

namespace TwitterExampleApp.Model.Repository
{
    /// <summary>
    /// Провайдер LinqToTwitter.
    /// </summary>
    public class LinqToTwitterRepository : ITwitterRepository
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        private const string EXCEPTION_MESSAGE = "При работе провайдера LinqToTwitter произошла ошибка!";

        /// <summary>
        /// Получение последних (по дате) твитов.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="auth_data">Данные для авторизации в твиттере.</param>
        /// <param name="count_twits">Количество записей.</param>
        /// <returns>Список последних твитов. </returns>
        public IEnumerable<Tweet> GetLastTweets(string username, TwitterAuthData auth_data, int count_twits)
        {
            try
            {
                using(var twitterCtx = new TwitterContext(GetAuthorizer(auth_data)))
                {
                    var list = from tweet in twitterCtx.Status
                               where tweet.Type == StatusType.User &&
                                      tweet.ScreenName == username
                               orderby tweet.CreatedAt descending
                               select tweet;
                    var recentTweets = list.ToArray().Take(count_twits).ToArray();
                    return recentTweets.Select(r => new Tweet() { Text = r.Text, DateTime = r.CreatedAt });
                }
            }
            catch (Exception ex)
            {
                throw new LinqToTwitterException(EXCEPTION_MESSAGE, ex);
            }
        }

        /// <summary>
        /// Метод, постящий твит.
        /// </summary>
        /// <param name="tweet">Строка твита.</param>
        /// <param name="auth_data">Данные для авторизации в твиттере.</param>
        public async void PostTweet(string tweet, TwitterAuthData auth_data)
        {
            try
            {
                if (tweet.Length > 140)
                    throw new TwitterValidationException("длинна твита не может быть больше 140 символов.");

                using (var twitterCtx = new TwitterContext(GetAuthorizer(auth_data)))
                {
                    await twitterCtx.TweetAsync(tweet);
                }
            }
            catch (Exception ex)
            {
                throw new LinqToTwitterException(EXCEPTION_MESSAGE, ex);
            }
        }

        /// <summary>
        /// Получение объекта - авторизатора для провайдера LinqToTwitter.
        /// </summary>
        /// <param name="auth_data">Данные для авторизации в твиттере.</param>
        /// <returns>SingleUserAuthorizer</returns>
        private SingleUserAuthorizer GetAuthorizer(TwitterAuthData auth_data)
        {
            return new SingleUserAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore()
                {
                    ConsumerKey = auth_data.ConsumerKey,
                    ConsumerSecret = auth_data.ConsumerSecret,
                    OAuthToken = auth_data.OAuthToken,
                    OAuthTokenSecret = auth_data.OAuthTokenSecret
                }
            };
        }
    }
}
