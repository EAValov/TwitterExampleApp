using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterExampleApp.Model.Repository;
using Moq;
using System.Collections.Generic;
using TwitterExampleApp.Model.DataModel;
using TwitterExampleApp.Model.Service;

namespace TwitterExampleApp.Tests
{
    /// <summary>
    /// Тесты сервиса TwitterService.
    /// </summary>
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
            new Tweet() { DateTime = DateTime.Now, Text = "ффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффф" },
            new Tweet() { DateTime = DateTime.Now, Text = "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee" },
            new Tweet() { DateTime = DateTime.Now, Text = "sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" },
            new Tweet() { DateTime = DateTime.Now, Text = "hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh" },
            new Tweet() { DateTime = DateTime.Now, Text = "qqqqqqqqqqqqqqqqqqqqqqq" },
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
            Assert.AreEqual("@vasya чаще всего пользуется буквами \"ф\", \"e\" и \"s\": 87 раз за 5 твитов!.", result);
        }
    }
}
