using ekklesia.Models.MemberModel;
using ekklesia.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class GroupBasedReportDetailsViewModel
    {

        public ReportType ReportType { get; set; }
        public DateTime Date { get; set; }
        public Member Preacher { get; set; }
        public Member Coordinator { get; set; }

        //ATIVIDADES COMUNS A TODOS OS EVENTOS
        public int Reunions { get; set; }
        public int Convertions { get; set; }

        //MOVIMENTO FINANCEIRO
        public decimal PreviousMonth { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Tenth { get; set; }
        public decimal Balance { get; set; }

        //ATIVIDADES DE GRUPO

        public int NumberOfVisits { get; set; }
        public int ExternalCults { get; set; }
        public int CellsNumber { get; set; }
        public int Baptized { get; set; }
        public int MeetingsWithTheCoordination { get; set; }
        public int MembersNumber { get; set; }
        public int BoardMembersNumber { get; set; }


        public GroupBasedReportDetailsViewModel(GroupBasedReport report)
        {
            //ATIVIDADES BÁSICAS DE RELATÓRIO
            ReportType = report.ReportType;
            Date = report.Date;
            Preacher = report.Preacher;
            Coordinator = report.Coordinator;
            Reunions = report.Reunions;
            Convertions = report.Convertions;

            //ATIVIDADES BÁSICAS PARA EVENTOS
            Reunions = report.Reunions;
            Convertions = report.Convertions;
            ExternalCults = report.ExternalCults;
            CellsNumber = report.CellsNumber;
            MeetingsWithTheCoordination = report.MeetingsWithTheCoordination;
            Baptized = report.Baptized;

            //MOVIMENTO FINANCEIRO
            PreviousMonth = report.PreviousMonth;
            Income = report.Income;
            Expense = report.Expense;
            Tenth = report.Tenth;
            Balance = report.Balance;
        }
    }
}
