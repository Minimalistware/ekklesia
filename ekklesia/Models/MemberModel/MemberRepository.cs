﻿using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.MemberModel
{
    public interface IMemberRepository : IFilling
    {
        Task<Member> GetMember(int id);
        Task<IEnumerable<Member>> GetMembers();
        IEnumerable<Member> GetMembersInEvent(int id);
        Task Add(Member member);
        Task Update(Member member);
        Member Delete(int id);
        IEnumerable<Member> Search(MemberSreachViewModel model);
    }
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationContext applicationContext;
        public IFilling Next { get; set; }

        public MemberRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
               
        public async Task Add(Member member)
        {
            await applicationContext.Members.AddAsync(member);
            await applicationContext.SaveChangesAsync();           
        }

        public Member Delete(int id)
        {
            Member member = applicationContext.Members.Find(id);
            if (member != null)
            {
                applicationContext.Members.Remove(member);
                applicationContext.SaveChanges();
            }
            return member;

        }

        public async Task<ReportCreateViewModel> FillUpModel(ReportCreateViewModel model)
        {
            var members = applicationContext.Members;
            //Fill up number of people in pedagogical body
            model.PedagogicalBody = members.Count(m => m.Position == Position.Membro);

            model.AllMembers = await GetAllMembersAsSelectList();

            if (Next != null)
            {

            }

            return Next != null ? await Next.FillUpModel(model) : model;
        }

        public async Task<Member> GetMember(int id)
        {
            return await applicationContext.Members.FindAsync(id);
        }


        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await applicationContext.Members.ToListAsync();
        }

        public IEnumerable<Member> GetMembersInEvent(int id)
        {
            return applicationContext.Members
              .Include(m => m.Meetings)
              .ThenInclude(oc => oc.Occasion)
              .Where(m => m.Meetings.Any(oc => oc.OccasionId == id));
        }

        public IEnumerable<Member> Search(MemberSreachViewModel model)
        {
            var query = "SELECT * FROM dbo.Members WHERE ";
            if (model.Name != null)
            {
                query += "Name LIKE '%'+ @p0 +'%' AND ";
            }
            if (model.Position != null)
            {
                query += "Position = @p1 AND ";
            }

            query += "1 = 1";
            return applicationContext.Members.FromSql(query, model.Name, model.Position);
        }

        public async Task Update(Member alteredMember)
        {
            var member = applicationContext.Members.Attach(alteredMember);
            member.State = EntityState.Modified;
            await applicationContext.SaveChangesAsync();       
        }

        private async Task<List<SelectListItem>> GetAllMembersAsSelectList()
        {
            var memberList = await applicationContext
                            .Members
                            .OrderBy(m => m.Name)
                            .ToListAsync();


            List<SelectListItem> members = new List<SelectListItem>();
            foreach (var item in memberList)
            {
                members.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name

                });
            }

            return members;
        }

    }
}
