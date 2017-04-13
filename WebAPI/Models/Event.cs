using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCEvents_WebAPI.Models
{
    public class Event : EntityData
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string PhotoURL { get; set; }
        public string Description { get; set; }
    }
}