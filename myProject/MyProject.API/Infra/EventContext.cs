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

        public DbSet<Post> Posts { get; set; }

    }
}