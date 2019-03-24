using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreAngularCrudDataService.Framework.Models;
using DotNetCoreAngularCrudDataService.Framework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreAngularCrudDataService.Controllers
{
    [EnableCors("AllowCorsPolicy")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private readonly IPresentationService _presentationsService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public DataController(IPresentationService presentationsService, IHostingEnvironment hostingEnvironment)
        {
            _presentationsService = presentationsService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult<List<PresentationModel>> Get()
        {
            var result = _presentationsService.GetAllPresentations(_hostingEnvironment)?.OrderByDescending(m => m.createdAt)?.ToList();
            if (!result.Any())
            {
                return NotFound($"No Data");
            }

            return Ok(result);
        }

        [HttpGet("{title}")]
        public IActionResult Get(string title)
        {
            var result = _presentationsService.GetPresentationsByTitle(title, _hostingEnvironment);
            if (result == null)
            {
                return NotFound($"{title} is not found");
            }

            return Ok(result);

        }
    }
}
