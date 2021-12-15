using System;
using MyProject.API.Domain;

namespace MyProject.API.Controllers
{
    public class CreateEvent
    {
        public string eventTitle { get; set; }

        public DateTime eventDate { get; set; }

        public string eventDescription { get; set; }

        public int eventAge { get; set; }


        public int eventParticpantCount { get; set; }

        public Event ToEvent() => new Event { eventTitle = this.eventTitle, eventDate = this.eventDate, eventDescription = this.eventDescription, eventAge = this.eventAge, eventParticpantCount = 7 };
    }


    public class UpdateEvent
    {
        public Guid? Id { get; set; }

        public string eventTitle { get; set; }

        public DateTime eventDate { get; set; }

        public string eventDescription { get; set; }

        public int eventAge { get; set; }

        public int eventParticpantCount { get; set; }

        public Event ToEvent() => new Event { Id = this.Id, eventTitle = this.eventTitle, eventDate = this.eventDate, eventDescription = this.eventDescription, eventAge = this.eventAge };
    }


    public class ViewEvent
    {
        public string Id { get; set; }

        public string eventTitle { get; set; }

        public DateTime eventDate { get; set; }

        public string eventDescription { get; set; }

        public int eventAge { get; set; }

        public int eventParticpantCount { get; set; }

        public string eventParticpants { get; set; }

        public static ViewEvent FromModel(Event Event) => new ViewEvent
        {
            Id = Event.Id.ToString(),
            eventTitle = Event.eventTitle,
            eventDate = Event.eventDate,
            eventDescription = Event.eventDescription,
            eventParticpantCount = 100,
            eventAge = Event.eventAge
        };
    }


    public class enrollEvent
    {
        public Guid? Id { get; set; }

        public string eventTitle { get; set; }

        public string userName { get; set; }

        public Enrolled ToEnroll() => new Enrolled { eventTitle = this.eventTitle, userName = this.userName };
    }


    public class addEnroll
    {
        public Guid? Id { get; set; }
        public string eventEnrolled { get; set; }

        public Event ToAddEnroll() => new Event { Id = this.Id, eventEnrolled = this.eventEnrolled };
    }


    public class ViewEroll
    {
        public string Id { get; set; }

        public string eventTitle { get; set; }

        public string userName { get; set; }
        public static ViewEroll FromModel(Enrolled enroll) => new ViewEroll
        {
            Id = enroll.Id.ToString(),
            eventTitle = enroll.eventTitle,
            userName = enroll.userName,
        };
    }
}