using System;

namespace TwitterExampleApp.Model.Exceptions
{
    /// <summary>
    /// Исключение, бросеамое при возникновении ошибки в провайдере LinqToTwitter .
    /// </summary>
    public class LinqToTwitterException : Exception
    {
        /// <summary>
        /// Конструктор исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="ex">Внутренее исключение.</param>
        public LinqToTwitterException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}

