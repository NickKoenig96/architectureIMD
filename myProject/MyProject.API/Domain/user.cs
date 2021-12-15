using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.API.Domain
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid? Id { get; set; }

        public string userName { get; set; }

        public string userEmail { get; set; }

        public DateTime userBirthdate { get; set; }
    }
}