using ekklesia.Models.ReportModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class GroupBasedReportViewModel : ReportCreateViewModel
    {
        public GroupBasedReportViewModel() { }

        public GroupBasedReportViewModel(ReportType Type)
        {
            this.ReportType = Type;
            this.Date = DateTime.Now;
            this.AllMembers = new HashSet<SelectListItem>();
        }

        public GroupBasedReportViewModel(GroupBasedReport report)
        {
            //ATIVIDADES BÁSICAS DE RELATÓRIO
            ReportType = report.ReportType;
            Date = report.Date;
            PreacherId = report.PreacherId;
            CoordinatorId = report.CoordinatorId;
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

        [Required]
        public int NumberOfVisits { get; set; }
        [Required]
        public int ExternalCults { get; set; }
        [Required]
        public int CellsNumber { get; set; }
        [Required]
        public int Baptized { get; set; }
        [Required]
        public int MeetingsWithTheCoordination { get; set; }
        [Required]
        public int MembersNumber { get; set; }
        [Required]
        public int BoardMembersNumber { get; set; }


    }
}
