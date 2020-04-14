﻿using ekklesia.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.EventModel
{
    public interface IEventRepository : IFilling
    {
        Task<IEnumerable<Occasion>> GetEvents();
        Occasion GetEventWithItsMembers(EventType eventType, int Id);
        SundaySchool GetSundaySchoolWithItsMembers(int Id);
        Reunion GetReunionWithItsMembers(int Id);
        Task<Occasion> GetEvent(int Id);
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

        public IFilling Next { get; set; }

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

        public async Task<ReportCreateViewModel> FillUpGroupReportModel(GroupBasedReportViewModel model)
        {
            var reunions = applicationContext.Occasions.OfType<Reunion>();
            var cults = applicationContext.Occasions.OfType<Cult>();
            var sundday_schools = applicationContext.Occasions.OfType<SundaySchool>();

            ////Fill up number of reunions
            model.Reunions = reunions.Count();

            ////Fill up number of bibles
            //model.Bibles = sundday_schools.Sum(ss => ss.NumberOfBibles);

            ////Fill up number of reunions with teachers
            //model.ReunionWithTeachers = reunions
            //    .Where(r => r.ReunionType.Equals(ReunionType.DOCÊNCIA))
            //    .Count();

            ////Fill up number of reunions with visitors
            ////TODO

            ////Fill up number of people present
            //model.PeoplePresent = reunions.Sum(r => r.PresentMembers.Count);

            return Next != null ? await Next.FillUpGroupReportModel(model) : model;
        }

        public async Task<ReportCreateViewModel> CompleteBaseReportFor(ReportCreateViewModel model)
        {
            var occasions = applicationContext.Occasions.OfType<Cult>()
                .Where(c => c.CultType.ToString() == model.Type.ToString());

            model.Reunions = occasions.Count();
            
            model.Convertions = occasions.Sum(c => c.Convertions);
            return Next != null ? await Next.CompleteBaseReportFor(model) : model;
        }
    }
}
