using System;
namespace DotNetCoreAngularCrudDataService.Framework.Models
{
    public class PresentationModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public Creator creator { get; set; }
        public DateTime createdAt { get; set; }
    }
    public class Creator
    {
        public string name { get; set; }
        public string profileUrl { get; set; }
    }
}