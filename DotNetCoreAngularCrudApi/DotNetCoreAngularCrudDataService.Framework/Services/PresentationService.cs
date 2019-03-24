using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using NLog;
using DotNetCoreAngularCrudDataService.Framework.Helpers.File;
using DotNetCoreAngularCrudDataService.Framework.Models;

namespace DotNetCoreAngularCrudDataService.Framework.Services
{
    public class PresentationService : IPresentationService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IFileReader<PresentationModel> _readPresentationsService;
        public PresentationService(IFileReader<PresentationModel> readPresentationsService)
        {
            _readPresentationsService = readPresentationsService;
        }
        public List<PresentationModel> GetAllPresentations(IHostingEnvironment hostingEnvironment)
        {
            List<PresentationModel> allItems = null;
            try
            {
                var path = Path.Combine(hostingEnvironment.ContentRootPath, "Data/Presentations.json");
                allItems = _readPresentationsService.ReadFile(path);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return allItems;
        }
        public List<PresentationModel> GetPresentationsByTitle(string title, IHostingEnvironment hostingEnvironment)
        {
            List<PresentationModel> selectedItem = null;
            try
            {
                var path = Path.Combine(hostingEnvironment.ContentRootPath, "Data/Presentations.json");
                var item = _readPresentationsService.ReadFile(path);
                var filteredItem = item.Where(m => m.title.Contains(title))?.ToList();
                if (filteredItem != null && filteredItem.Any())
                {
                    selectedItem = filteredItem;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return selectedItem;
        }
    }
}
