using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.TransactionModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ReportModel
{
    public abstract class BaseReportBuilder
    {
        protected readonly MemberRepository memberRepository;
        protected readonly TransactionRepository transactionRepository;
        protected readonly EventRepository eventRepository;
        protected GroupBasedReportViewModel groupBasedReport;

        public BaseReportBuilder(MemberRepository memberRepository,
            TransactionRepository transactionRepository, EventRepository eventRepository)
        {
            this.memberRepository = memberRepository;
            this.transactionRepository = transactionRepository;
            this.eventRepository = eventRepository;
        }

        public virtual async void GenerateBaseReportFor(ReportType type)
        {
            //groupBasedReport = new GroupBasedReportViewModel(type, await GetAllMembers());
            /*Defaines a Chain of Reponsability.
             https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern
             */

            eventRepository.Next = memberRepository;
            memberRepository.Next = transactionRepository;

            //Executes the pattern.            
            //await eventRepository.FillUpGroupReportModel(groupBasedReport);

        }


        private async Task<HashSet<SelectListItem>> GetAllMembers()
        {
            var asyncmembers = await memberRepository
                            .GetMembers();

            var memberList = asyncmembers
                            .OrderBy(m => m.Name)
                            .ToList();


            HashSet<SelectListItem> members = new HashSet<SelectListItem>();
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
