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

        public string eventParticipants { get; set; }

        public int eventParticpantCount { get; set; }



        // public Event(Guid id, string eventtitle, DateTime eventdate, string eventdescription, int eventage, string[] eventparticipants)
        // {
        //     Id = id;
        //     eventTitle = eventtitle;
        //     eventDate = eventdate;
        //     eventDescription = eventdescription;
        //     eventAge = eventage;
        //     eventParticpants = eventparticipants;

        //     eventParticpantCount = eventParticpants.Length;
        // }
    }





    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid? Id { get; set; }

        public string userName { get; set; }

        public string userEmail { get; set; }

        public DateTime userBirthdate { get; set; }


        // public User(Guid id, string username, DateTime userbirthdate, string useremail)
        // {


        //     Id = id;
        //     userName = username;
        //     userBirthdate = userbirthdate;
        //     userEmail = useremail;

        // }

    }




}
