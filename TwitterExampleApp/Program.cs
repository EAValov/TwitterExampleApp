using System;
using System.Configuration;
using TwitterExampleApp.Model.DataModel;
using TwitterExampleApp.Model.Repository;
using TwitterExampleApp.Model.Service;

namespace TwitterExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TwitterService service = new TwitterService(new LinqToTwitterRepository());

                // Количество твитов для подсчета.
                const int TWIT_COUNT = 5;

                while (true)
                {
                    Console.WriteLine("Введите твиттер-логин. Допускаются варианты @username и username");
                    var username = Console.ReadLine();

                    // Если введена пустая строка, программа прекращает работу.
                    if (string.IsNullOrEmpty(username))
                        return;

                    var auth_data = new TwitterAuthData()
                    {
                        ConsumerKey = ConfigurationManager.AppSettings["consumerkey"],
                        ConsumerSecret = ConfigurationManager.AppSettings["consumersecret"],
                        OAuthToken = ConfigurationManager.AppSettings["accessToken"],
                        OAuthTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]
                    };

                    Console.WriteLine(service.Process(username, auth_data, TWIT_COUNT));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
