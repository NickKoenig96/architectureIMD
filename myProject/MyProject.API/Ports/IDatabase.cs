using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyProject.API.Domain;

namespace MyProject.API.Ports
{
    public interface IDatabase
    {
        //events
        Task<ReadOnlyCollection<Event>> GetAllEvents();
        Task<Event[]> GetByAge(int eventage);
        Task<Event> GetEventById(Guid id);
        Task<Event> GetEvent(Guid id);
        Task<Event> PersistEvent(Event Event);
        Task<Event> UpdateEvent(Event Event);

        //user
        Task<ReadOnlyCollection<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> PersistUser(User User);
        Task DeleteUser(Guid parsedId);

        //enroll
        Task<Enrolled> EnrollEvent(Enrolled Enrolled);
        Task UnenrollEvent(Guid parsedId);
        Task<ReadOnlyCollection<Enrolled>> GetAllEnrolls();
        Task<Enrolled> GetEnrollById(Guid id);
        Task<Event> EnrollEvent2(Event addEnroll);
    }
}