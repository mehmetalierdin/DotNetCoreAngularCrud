using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using NLog;

namespace DotNetCoreAngularCrudDataService.Framework.Helpers.File
{
    public class FileReader<T> : IFileReader<T> where T : class
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public List<T> ReadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    var userJson = sr.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<List<T>>(userJson);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}