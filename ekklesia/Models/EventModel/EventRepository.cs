using System;
using System.Collections.Generic;

namespace ekklesia.Models.EventModel
{
    public interface IEventRepository
    {
        IEnumerable<Occasion> GetEvents();
        Occasion GetEvent(int Id);
        Occasion Add(Occasion Event);
        Occasion Update(Occasion Event);
        Occasion Delete(int id);

    }
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationContext applicationContext;

        public EventRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Occasion Add(Occasion Event)
        {
            applicationContext.Add(Event);
            applicationContext.SaveChanges();
            return Event;
        }

        public Occasion Delete(int id)
        {
            Occasion occasion = applicationContext.Occasions.Find(id);
            if (occasion != null)
            {
                applicationContext.Occasions.Remove(occasion);
                applicationContext.SaveChanges();
            }
            return occasion;
        }

        public Occasion GetEvent(int Id)
        {
            return applicationContext.Occasions.Find(Id);
        }

        public IEnumerable<Occasion> GetEvents()
        {
            return applicationContext.Occasions;
        }

        public Occasion Update(Occasion alteredEvent)
        {
            var occasion = applicationContext.Occasions.Attach(alteredEvent);
            occasion.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredEvent;
        }
    }
}
