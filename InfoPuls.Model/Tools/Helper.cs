using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace InfoPuls.Model.Tools
{
    public static class Helper
    {
        public static bool isEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime GetDateInUsFormat(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
                throw new ArgumentException("dateTime can't be empty!");

            try
            {
                DateTime result = DateTime.Parse(dateTime, new CultureInfo("en-US", false));
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(String.Format("Error parsing US dateTime value : {0}", dateTime), "dateTime", ex);
            }
        }

        public static string[] SplitAndTrim(string text, char separator)
        {
            if (string.IsNullOrWhiteSpace(text)) 
                return new string[0];
                
            return text.Split(separator).Select(x => x.Trim()).ToArray();
        }

        public static string GetFullPathToFileInCurrentFolder(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName can't be empty!");

            string currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string path = Path.Combine(currentDirectory, fileName);

            return path;
        }


        public static void ArgumentNullReferenceException(object obj, string argName, string methodName)
        {
            string format = "Argument '{0}' of method '{1}' can't be null";
            string errorMessage = String.Format(format, argName, methodName);

            Helper.ArgumentNullReferenceException(obj, errorMessage);
        }

        public static void ArgumentNullReferenceException(object obj, string message = "")
        {
            
            if (obj == null)
            {
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentNullException();
                
                throw new ArgumentNullException(message);
            }
        }
    }
}