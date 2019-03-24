using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using DotNetCoreAngularCrudDataService.Framework.Models;

namespace DotNetCoreAngularCrudDataService.Framework.Services
{
    public interface IPresentationService
    {
        List<PresentationModel> GetAllPresentations(IHostingEnvironment hostingEnvironment);
        List<PresentationModel> GetPresentationsByTitle(string title, IHostingEnvironment hostingEnvironment);
    }
}
