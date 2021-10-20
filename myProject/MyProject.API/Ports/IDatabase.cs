using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyProject.API.Domain;

namespace MyProject.API.Ports
{
    public interface IDatabase
    {
        Task<ReadOnlyCollection<Event>> GetAllEvents();
        Task<Event> GetEventByAge(int eventage);

        Task<Event> GetEventById(Guid id);

        Task<Event> PersistEvent(Event Event);

        Task<User> PersistUser(User User);

        // Task DeleteMovie(Guid parsedId);
    }
}