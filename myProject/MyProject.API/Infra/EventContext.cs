using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyProject.API.Domain;

namespace MyProject.API.Infra
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> ctx) : base(ctx)
        {
        }
        public DbSet<Event> Event { get; set; }

        public DbSet<User> User { get; set; }

        //public string DbPath { get; private set; }





        /*public EventContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}events.db";
        }

        /// The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");*/

    }
}



