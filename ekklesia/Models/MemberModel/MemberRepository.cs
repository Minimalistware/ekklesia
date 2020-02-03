using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.MemberModel
{
    public interface IMemberRepository
    {
        Member GetMember(int id);
        IEnumerable<Member> GetMembers();
        IEnumerable<int> GetMembersInEvent(int id);
        Member Add(Member member);
        Member Update(Member member);
        Member Delete(int id);

    }
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationContext applicationContext;

        public MemberRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Member Add(Member member)
        {
            applicationContext.Members.Add(member);
            applicationContext.SaveChanges();
            return member;
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

        public Member GetMember(int id)
        {
            return applicationContext.Members.Find(id);
        }


        public IEnumerable<Member> GetMembers()
        {
            return applicationContext.Members;
        }

        public IEnumerable<int> GetMembersInEvent(int id)
        {
            return applicationContext.Members
                .Include(m => m.Meetings)
                .ThenInclude(oc => oc.Occasion)
                .Where(m => m.Meetings.Any(oc => oc.OccasionId == id))
                .Select(m => m.Id);
        }

        public Member Update(Member alteredMember)
        {
            var member = applicationContext.Members.Attach(alteredMember);
            member.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredMember;

        }
    }
}
