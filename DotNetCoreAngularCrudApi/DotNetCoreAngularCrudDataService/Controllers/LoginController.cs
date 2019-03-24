using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using DotNetCoreAngularCrudDataService.Framework.Helpers;
using DotNetCoreAngularCrudDataService.Framework.Models;
using DotNetCoreAngularCrudDataService.Framework.Services;

namespace DotNetCoreAngularCrudDataService.Controllers
{
    [Route("/api/[controller]")]
    [EnableCors("AllowCorsPolicy")]
    public class LoginController : Controller
    {
        private readonly IOptions<JwtModel> _jwtModel;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IMemoryCache _memoryCache;
        public LoginController(IUserService userService, IOptions<JwtModel> jwtModel, IHostingEnvironment environment, IMemoryCache memoryCache)
        {
            _jwtModel = jwtModel;
            _hostingEnvironment = environment;
            _userService = userService;
            _memoryCache = memoryCache;
        }
        [HttpPost]
        public ActionResult<User> GetToken([FromBody]User user)
        {
            var username = user.Username;
            var password = user.Password;
            var result = new JwtHelper(_userService, _jwtModel, _hostingEnvironment).GenerateJwtToken(username, password);
            return result;
        }
    }
}
