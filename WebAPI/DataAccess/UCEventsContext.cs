using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using UCEvents_WebAPI.Models;
using UCEvents_WebAPI.Maps;
using UCEvents_WebAPI.DataAccess;

namespace UCEvents_WebAPI.Models
{
    public class UCEventsContext : DbContext
    {
        private const string connectionStringName = "Name=CampusLoopDb";

        static UCEventsContext()
        {
            Database.SetInitializer<UCEventsContext>(new MigrateDatabaseToLatestVersion<UCEventsContext, UCEventsDatabaseMigrationConfiguration>());
        }

        public UCEventsContext() : base(connectionStringName)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
            modelBuilder.Configurations.Add(new TodoItemConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
        }
    }
}