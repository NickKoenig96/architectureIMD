using System;
using MyProject.API.Domain;

namespace MyProject.API
{
    public static class UserProvider
    {
        public static User[] StaticEventList = new[] {
        new User(Guid.Parse("6f905dea-e152-45e3-b001-d7d753e11fa4"), "jef",  DateTime.Now, "gbdgfdg"),
        new User(Guid.Parse("c018db1b-6b86-45a1-b39d-48bc5c6e52b5"), "jef",  DateTime.Now, "fdxgbdfbd")

        };
    }
}