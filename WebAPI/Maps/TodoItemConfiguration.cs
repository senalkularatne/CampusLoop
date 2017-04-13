using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using UCEvents_WebAPI.Models;

namespace UCEvents_WebAPI.Maps
{
    public class TodoItemConfiguration : EntityTypeConfiguration<TodoItem>
    {
        public TodoItemConfiguration()
            : this("dbo")
        {
        }

        public TodoItemConfiguration(string schema)
        {
            ToTable("TodoItem", schema);
            HasKey(x => x.Id);

            Property(x => x.Text).HasColumnName(@"Text").HasColumnType("varchar(max)");
            Property(x => x.Complete).HasColumnName(@"Complete").HasColumnType("bit");
        }
    }
}