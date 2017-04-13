using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UCEvents_WebAPI.Infrastructure
{
    public class ApplicationUserContext : IdentityDbContext<ApplicationUser>
    {
        private static string _connectionName = "CampusLoopDb";

        public ApplicationUserContext()
            : base(_connectionName)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationUserContext Create()
        {
            return new ApplicationUserContext();
        }
    }
}