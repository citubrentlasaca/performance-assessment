using System.Globalization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PerformanceAssessmentApi.Utils
{
    public class StringUtil
    {
        /// <summary>
        /// Gets the current datetime
        /// </summary>
        /// <returns>The current datetime</returns>
        public static string GetCurrentDateTime()
        {
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            string currentDateTime = time.ToString(format);

            return currentDateTime;
        }
    }
}