using System;
namespace DotNetCoreAngularCrudDataService.Framework.Models
{
    public class JwtModel
    {
        public string JwtIssuer { get; set; }
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
    }
}
