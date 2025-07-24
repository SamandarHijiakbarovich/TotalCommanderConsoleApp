using System;

namespace TotalCommanderApp.Models
{
    /// <summary>
    /// Bajarilgan komanda natijasini saqlovchi model
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Operatsiya muvaffaqiyatli bo'lganligi
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Foydalanuvchiga ko'rsatiladigan xabar
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Xatolik yuz berganda Exception obyekti
        /// </summary>
        public Exception Error { get; }

        /// <summary>
        /// Komanda natijasi konstruktori
        /// </summary>
        /// <param name="success">Operatsiya holati</param>
        /// <param name="message">Xabar matni</param>
        /// <param name="error">Xatolik (agar bo'lsa)</param>
        public CommandResult(bool success, string message, Exception error = null)
        {
            Success = success;
            Message = message ?? string.Empty;
            Error = error;
        }

        /// <summary>
        /// Muvaffaqiyatli natija uchun fabrika metodi
        /// </summary>
        /// <param name="message">Xabar matni</param>
        public static CommandResult SuccessResult(string message)
        {
            return new CommandResult(true, message);
        }

        /// <summary>
        /// Xatolik natijasi uchun fabrika metodi
        /// </summary>
        /// <param name="message">Xabar matni</param>
        /// <param name="error">Xatolik obyekti</param>
        public static CommandResult ErrorResult(string message, Exception error = null)
        {
            return new CommandResult(false, message, error);
        }

        /// <summary>
        /// Xatolik natijasi uchun fabrika metodi (exception asosida)
        /// </summary>
        /// <param name="ex">Xatolik obyekti</param>
        public static CommandResult FromException(Exception ex)
        {
            return new CommandResult(false, ex.Message, ex);
        }
    }
}