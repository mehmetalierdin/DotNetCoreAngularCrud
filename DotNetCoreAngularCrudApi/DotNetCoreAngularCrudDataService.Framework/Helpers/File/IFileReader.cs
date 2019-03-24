using System;
using System.Collections.Generic;

namespace DotNetCoreAngularCrudDataService.Framework.Helpers.File
{
    public interface IFileReader<T> where T : class
    {
        List<T> ReadFile(string path);
    }
}
