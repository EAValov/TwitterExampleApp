using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TwitterExampleApp.Model.DataModel;
using TwitterExampleApp.Model.Exceptions;
using TwitterExampleApp.Model.Repository;

namespace TwitterExampleApp.Model.Service
{
    /// <summary>
    /// Сервис.
    /// </summary>
    public class TwitterService
    {
        /// <summary>
        /// Регексп для проверки введенного твиттер логина. Должно состоять из латинских букв или цифр, максимум 15 символов. Еще можно _ 
        /// https://support.twitter.com/articles/101299
        /// </summary>
        private readonly Regex login_regex = new Regex("^@?([a-zA-Z0-9_]){1,15}$", RegexOptions.Compiled);

        /// <summary>
        /// Репозиторий твиттер - провайдера.
        /// </summary>
        private readonly ITwitterRepository _twitterRepository;

        /// <summary>
        /// Сервис, считающий количество символов в списке строк.
        /// </summary>
        private CharCounterService letter_counter = new CharCounterService();

        /// <summary>
        /// Конструктор с инекцией зависимости.
        /// </summary>
        /// <param name="twitter_repository">Репозиторий твиттер - провайдера.</param>
        public TwitterService(ITwitterRepository twitter_repository)
        {
            this._twitterRepository = twitter_repository;
        }

        /// <summary>
        /// Метод, принимающий имя пользователя и постящий твит с количеством наиболее встречающихся букв.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="auth_data">Данные для авторизации в твиттере.</param>
        /// <param name="count_twits">Количество записей.</param>
        public string Process(string username, TwitterAuthData auth_data, int count_twits)
        {
            if (!login_regex.IsMatch(username))
                throw new TwitterValidationException("Некорректное имя пользователя!");

            var tweets = _twitterRepository.GetLastTweets(username, auth_data, count_twits);

            // склеим из твитов пользователя строку.
            var string_tweets = ConcatenateStrings(tweets.Select(f => f.Text));

            var count_result = letter_counter.CountMostOccuringCharInString(string_tweets);

            var return_msg = GetReturnMessage(count_result, username, count_twits);

            // На всякий случай проверим, что не вышли за пределы 140 символов.
            if(return_msg.Length <= 140)
                _twitterRepository.PostTweet(return_msg, auth_data);

            return return_msg;
        }

        /// <summary>
        /// Склеиваем из твитов одну строку. Заодно удалим пробелы, т.к. они часто будут самыми частыми символами. 
        /// </summary>
        /// <param name="strings">Список строк для склеивания.</param>
        /// <returns>Склееная строка.</returns>
        public string ConcatenateStrings(IEnumerable<string> strings)
        {
            var sb = new StringBuilder();

            foreach (string s in strings)
                sb.Append(Regex.Replace(s, @"\s+", "")); // удаление пробелов не баг, а фича!

            return sb.ToString();
        }

        /// <summary>
        /// Метод, возвращаюший строку с количеством наиболее часто используемых символов.
        /// </summary>
        /// <param name="count_result">Результат подсчета строк.</param>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="count_twits">Количество твитов.</param>
        /// <returns>Строка по шаблону.</returns>
        public string GetReturnMessage(CharCountResult count_result, string username, int count_twits)
        {
            if (!count_result.Symbols.Any())
                throw new ArgumentException("Список символов должен содержать хотя бы одну запись.");

            string letters_string;
            string letters_count_ending;

            if (count_result.Symbols.Count() > 1)
            {
                letters_string = string.Join(", ", count_result.Symbols.Take(count_result.Symbols.Count() - 1)) + (count_result.Symbols.Count() <= 1 ? "" : " и ") + count_result.Symbols.LastOrDefault();
                letters_count_ending = "буквами";
            }
            else
            {
                letters_string = count_result.Symbols.First().ToString();
                letters_count_ending = "буквой";
            }
                
            return string.Format("{0} чаще всего пользуется {1} \"{2}\": {3} раз за {4} твитов!.", username, letters_count_ending, letters_string, count_result.Count, count_twits);
        }
    }
}
