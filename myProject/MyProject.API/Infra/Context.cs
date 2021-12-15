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

        public DbSet<Enrolled> Enrolled { get; set; }

        public DbSet<User> User { get; set; }

    }
}