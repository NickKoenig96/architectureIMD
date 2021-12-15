using System;
using MyProject.API.Domain;

namespace MyProject.API.Controllers
{
    public class CreateUser
    {
        public string userName { get; set; }

        public string userEmail { get; set; }

        public DateTime userBirthdate { get; set; }

        public User ToUser() => new User { userName = this.userName, userBirthdate = this.userBirthdate, userEmail = this.userEmail };
    }


    public class ViewUser
    {
        public string Id { get; set; }

        public string userName { get; set; }

        public string userEmail { get; set; }

        public DateTime userBirthdate { get; set; }

        public static ViewUser FromModel(User User) => new ViewUser
        {
            Id = User.Id.ToString(),
            userName = User.userName,
            userEmail = User.userEmail,
            userBirthdate = User.userBirthdate
        };
    }
}