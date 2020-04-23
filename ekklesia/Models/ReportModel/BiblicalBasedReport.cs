using ekklesia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ReportModel
{
    public class BiblicalBasedReport : Report
    {
        public BiblicalBasedReport()
        {
            ReportType = ReportType.BIBLÍCO;
        }

        public BiblicalBasedReport(BiblicalBasedReportViewModel model)
        {
            //ATIVIDADES BÁSICAS DE RELATÓRIO
            ReportType = model.ReportType;
            Date = model.Date;
            PreacherId = model.PreacherId;
            CoordinatorId = model.CoordinatorId;
            Reunions = model.Reunions;
            Convertions = model.Convertions;

            //ATIVIDADES BÁSICAS PARA EVENTOS
            Reunions = model.Reunions;
            Convertions = model.Convertions;
            Bibles = model.Bibles;
            ReunionWithTeachers = model.ReunionWithTeachers;
            Visitants = model.Visitants;
            PeoplePresent = model.PeoplePresent;
            PedagogicalBody = model.PedagogicalBody;

            //MOVIMENTO FINANCEIRO
            PreviousMonth = model.PreviousMonth;
            Income = model.Income;
            Expense = model.Expense;
            Tenth = model.Tenth;
            Balance = model.Balance;
            
        }

        [Required]
        public int Bibles { get; set; }
        [Required]
        public int ReunionWithTeachers { get; set; }
        [Required]
        public int Visitants { get; set; }
        [Required]
        public int PeoplePresent { get; set; }
        [Required]
        public int PedagogicalBody { get; set; }

    }
}
