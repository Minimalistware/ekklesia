using ekklesia.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public interface IEventRepository
    {
        Task<IEnumerable<Occasion>> GetEvents();
        Occasion GetEventWithItsMembers(EventType eventType, int Id);
        SundaySchool GetSundaySchoolWithItsMembers(int Id);
        Reunion GetReunionWithItsMembers(int Id);
        Task<Occasion> GetEvent(int Id);
        Occasion Add(Occasion Event);
        Occasion Update(Occasion Event);
        Occasion Delete(int id);
        IEnumerable<Occasion> Search(EventSearchViewModel model);

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

        public async Task<Occasion> GetEvent(int Id)
        {
            return await applicationContext.Occasions.FindAsync(Id);
        }

        public async Task<IEnumerable<Occasion>> GetEvents()
        {
            return await applicationContext.Occasions.ToListAsync();
        }

        public Occasion Update(Occasion alteredEvent)
        {
            var occasion = applicationContext.Occasions.Attach(alteredEvent);
            occasion.State = EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredEvent;
        }

        public Occasion GetEventWithItsMembers(EventType eventType, int Id)
        {
            if (eventType == EventType.ESCOLA_DOMINICAL)
            {
                return applicationContext.Occasions
                .OfType<SundaySchool>()
                .Include(oc => oc.Members)
                .ThenInclude(om => om.Member)
                .Where(oc => oc.Id == Id)
                .FirstOrDefault();
            }
            else
            {
                return applicationContext.Occasions
                .OfType<Reunion>()
                .Include(oc => oc.PresentMembers)
                .ThenInclude(om => om.Member)
                .Where(oc => oc.Id == Id)
                .FirstOrDefault();
            }

        }
        public SundaySchool GetSundaySchoolWithItsMembers(int Id)
        {
            return applicationContext.Occasions
            .OfType<SundaySchool>()
            .Include(oc => oc.Members)
            .ThenInclude(om => om.Member)
            .Where(oc => oc.Id == Id)
            .FirstOrDefault();

        }

        public Reunion GetReunionWithItsMembers(int Id)
        {
            return applicationContext.Occasions
            .OfType<Reunion>()
            .Include(oc => oc.PresentMembers)
            .ThenInclude(om => om.Member)
            .Where(oc => oc.Id == Id)
            .FirstOrDefault();

        }

        public IEnumerable<Occasion> Search(EventSearchViewModel model)
        {

            var query = "SELECT * FROM dbo.Occasions WHERE ";
            if (model.EventType != null)
            {
                query += "EventType = @p0 AND ";
            }            
            if (model.Days != null)
            {
                query += "DAY([Date]) < @p1 AND ";
            }
            query += "1 = 1";
            return applicationContext.Occasions.FromSql(query, model.EventType,model.Days);
        }
    }
}
