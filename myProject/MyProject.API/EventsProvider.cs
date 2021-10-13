using System;
using MyProject.API.Domain;

namespace MyProject.API
{
    public static class EventProvider
    {
        public static Event[] StaticEventList = new[] {
           new Event(),
           new Event()
           };
    }
}
//Guid.Parse("6f905dea-e152-45e3-b001-d7d753e11fa4"), "event1", DateTime.Now  , "this is event 1", 16, new[] {"jeff", "tom"}
//Guid.Parse("c018db1b-6b86-45a1-b39d-48bc5c6e52b5"), "event2", DateTime.Now , "this is event 2", 10, new[] {"jeff", "tom"}