using System;

namespace TwitterExampleApp.Model.Exceptions
{
    /// <summary>
    /// Исключение, бросеамое при попытке зайти под неправильным логином.
    /// </summary>
    public class TwitterValidationException : Exception
    {
        /// <summary>
        /// Конструктор исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public TwitterValidationException(string message) : base(message)
        {

        }
    }
}
