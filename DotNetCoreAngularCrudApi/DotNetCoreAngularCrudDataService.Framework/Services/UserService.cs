using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using DotNetCoreAngularCrudDataService.Framework.Helpers.File;
using DotNetCoreAngularCrudDataService.Framework.Models;

namespace DotNetCoreAngularCrudDataService.Framework.Services
{
    public class UserService : IUserService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IFileReader<User> _readUsersService;
        private IMemoryCache _cache;

        public UserService(IFileReader<User> readUsersService, IMemoryCache memoryCache)
        {
            _readUsersService = readUsersService;
            _cache = memoryCache;
        }
        public List<User> GetAllUsers(IHostingEnvironment hostingEnvironment)
        {
            try
            {
                var path = Path.Combine(hostingEnvironment.ContentRootPath, "Data/Users.json");
                var data = _cache.Set("AllUsers", _readUsersService.ReadFile(path), TimeSpan.FromSeconds(30));
                return data;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
