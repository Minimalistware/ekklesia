using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetEvents();
        Event GetEvent(int Id);
        Event Add(Event Event);
        Event Update(Event Event);
        Event Delete(int id);

    }
    public class EventRepository:IEventRepository
    {
        private readonly ApplicationContext applicationContext;

        public EventRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Event Add(Event Event)
        {
            throw new NotImplementedException();
        }

        public Event Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Event GetEvent(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEvents()
        {
            throw new NotImplementedException();
        }

        public Event Update(Event Event)
        {
            throw new NotImplementedException();
        }
    }
}
