using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterExampleApp.Model.Repository;
using Moq;
using System.Collections.Generic;
using TwitterExampleApp.Model.DataModel;
using TwitterExampleApp.Model.Service;

namespace TwitterExampleApp.Tests
{
    [TestClass]
    public class TwitterServiceTests
    {
        /// <summary>
        /// Мок репозитория.
        /// </summary>
        private Mock<ITwitterRepository> _repoMock = new Mock<ITwitterRepository>();

        /// <summary>
        /// Тестовый список твитов.
        /// </summary>
        private static List<Tweet> _testTwits = new List<Tweet>()
        {
            new Tweet() { DateTime = DateTime.Now, Text = "Пока народ безграмотен, из вархамера? По ночам об окно царапается зубами землеройка, к кавказцам." },
            new Tweet() { DateTime = DateTime.Now, Text = "Быть трезвым в Газпроме мотайте на виртуальном хостинге? — НЕЛЬЗЯ. Он лучший Wednesday's gone 10 лет. Мы." },
            new Tweet() { DateTime = DateTime.Now, Text = "Iron Man 3 made me do it was done by comic creator Wouter Goedkoop. For more fan video ever made! -->." },
            new Tweet() { DateTime = DateTime.Now, Text = "Вернулся с нефтяной трубы не ожидали такого же мнения. Дружно переходим на гитаре, следя за кого? на!" },
            new Tweet() { DateTime = DateTime.Now, Text = "ЕСЛИ ПОСЛЕ ПРОСМОТРА ЗАКАЧИВАТЬ СКАЧАННЫЕ ФИЛЬМЫ ОБРАТНО В ГУГЛ ХРОМ ПЕРЕИМЕНУЮТ В торговом центре Пекина?" },
        };

        /// <summary>
        /// Тестовый юзернейм, может быть любым, только чтобы при валидации проходил.
        /// </summary>
        private string _testUsername = "@vasya";

        /// <summary>
        /// Кол-во твитов.
        /// </summary>
        private int _twitСount = _testTwits.Count;

        /// <summary>
        /// Тестовые параметры для аутентификации.
        /// </summary>
        private TwitterAuthData _authData = new TwitterAuthData()
        {
            ConsumerKey = "",
            ConsumerSecret = "",
            OAuthToken = "",
            OAuthTokenSecret = ""
        };

        [TestMethod]
        public void Process_Parameters_String()
        {
            _repoMock.Setup(h => h.GetLastTweets(_testUsername, _authData, _twitСount)).Returns(_testTwits);
            _repoMock.Setup(j => j.PostTweet(It.IsAny<string>(), _authData));

            var service = new TwitterService(_repoMock.Object);

            var result = service.Process(_testUsername, _authData, _twitСount);

            Assert.IsNotNull(result);
            Assert.AreEqual("@vasya чаще всего пользуется буквой \"а\": 26 раз за 5 твитов!.", result);
        }
    }
}
