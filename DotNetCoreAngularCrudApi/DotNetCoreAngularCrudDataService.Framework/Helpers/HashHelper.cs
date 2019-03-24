using System;
using System.Security.Cryptography;
using System.Text;
using NLog;

namespace DotNetCoreAngularCrudDataService.Framework.Helpers
{
    public class HashHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static string MD5Hash(string text)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(Encoding.ASCII.GetBytes(text));

                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }

                return strBuilder.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return string.Empty;
        }
    }
}
