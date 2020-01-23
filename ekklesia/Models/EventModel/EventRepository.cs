using System;
using System.Collections.Generic;

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
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationContext applicationContext;

        public EventRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Event Add(Event Event)
        {
            applicationContext.Add(Event);
            applicationContext.SaveChanges();
            return Event;
        }

        public Event Delete(int id)
        {
            Event occasion = applicationContext.Events.Find(id);
            if (occasion != null)
            {
                applicationContext.Events.Remove(occasion);
                applicationContext.SaveChanges();
            }
            return occasion;
        }

        public Event GetEvent(int Id)
        {
            return applicationContext.Events.Find(Id);
        }

        public IEnumerable<Event> GetEvents()
        {
            return applicationContext.Events;
        }

        public Event Update(Event alteredEvent)
        {
            var occasion = applicationContext.Events.Attach(alteredEvent);
            occasion.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredEvent;
        }
}
}
