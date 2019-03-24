using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using DotNetCoreAngularCrudDataService.Framework.Models;

namespace DotNetCoreAngularCrudDataService.Framework.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers(IHostingEnvironment hostingEnvironment);
    }
}
