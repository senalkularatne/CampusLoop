using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using UCEvents_WebAPI.Models;

namespace UCEvents_WebAPI.DataAccess
{
    public class UCEventsDatabaseMigrationConfiguration : DbMigrationsConfiguration<UCEventsContext>
    {
        public UCEventsDatabaseMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UCEventsContext context)
        {
            //This method will be called after migrating to the latest ver

            // TODO: seed initial data(lookup tables, admin user, etc)

            //You can use the DbSet<T>.AddOrUpdate() helper extension method
            //to avoid creating duplicate seed data.E.g.

            //  context.People.AddOrUpdate(
            //    p => p.FullName,
            //    new Person { FullName = "Andrew Peters" },
            //    new Person { FullName = "Brice Lambson" },
            //    new Person { FullName = "Rowan Miller" }
            //  );
        }
    }
}