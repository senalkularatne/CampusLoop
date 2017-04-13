using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using UCEvents_WebAPI.DataAccess;
using UCEvents_WebAPI.Models;

namespace UCEvents_WebAPI
{
    public partial class Startup
    {
        /// <summary>
        /// Configures seeding data into the database and creating default and check constraints in the database.
        /// Also handles initial creation of the database if it doesn't exist, and updating an older version of the database schema.
        /// </summary>
        /// <param name="app">The app.</param>
        public static void ConfigureDatabase(IAppBuilder app)
        {
            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UCEventsContext, UCEventsDatabaseMigrationConfiguration>());

            var configuration = new UCEventsDatabaseMigrationConfiguration();
            var migrator = new DbMigrator(configuration);
            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
        }
    }
}