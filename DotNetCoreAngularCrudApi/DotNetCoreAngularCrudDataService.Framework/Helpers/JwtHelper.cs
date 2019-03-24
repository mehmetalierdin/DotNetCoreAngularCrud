using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using DotNetCoreAngularCrudDataService.Framework.Models;
using DotNetCoreAngularCrudDataService.Framework.Services;

namespace DotNetCoreAngularCrudDataService.Framework.Helpers
{
    public class JwtHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly JwtModel _jwtModel;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public JwtHelper(IUserService userService, IOptions<JwtModel> jwtModel, IHostingEnvironment environment)
        {
            _jwtModel = jwtModel.Value;
            _userService = userService;
            _hostingEnvironment = environment;
        }

        public User GenerateJwtToken(string username, string password)
        {
            try
            {
                var allusers = _userService.GetAllUsers(_hostingEnvironment);
                var user = allusers.Find(m => m.Username == username && m.Password == password);

                if (user == null)
                {
                    return null;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtModel.JwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(_jwtModel.JwtExpireDays),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                user.Password = HashHelper.MD5Hash(password);

                return user;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }
    }
}
