using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProject.API.Domain
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid? Id { get; set; }

        public string eventTitle { get; set; }

        public DateTime eventDate { get; set; }

        public string eventDescription { get; set; }

        public int eventAge { get; set; }

        public string eventEnrolled { get; set; }

        public int eventParticpantCount { get; set; }

    }

    public class Enrolled
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid? Id { get; set; }

        public string eventTitle { get; set; }

        public string userName { get; set; }
    }
}