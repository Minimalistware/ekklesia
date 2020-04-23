using ekklesia.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ReportModel
{
    public class GroupBasedReport : Report
    {
        public GroupBasedReport()
        {
        }

        public GroupBasedReport(GroupBasedReportViewModel model)
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
            ExternalCults = model.ExternalCults;
            CellsNumber = model.CellsNumber;
            MeetingsWithTheCoordination = model.MeetingsWithTheCoordination;
            Baptized = model.Baptized;

            //MOVIMENTO FINANCEIRO
            PreviousMonth = model.PreviousMonth;
            Income = model.Income;
            Expense = model.Expense;
            Tenth = model.Tenth;
            Balance = model.Balance;
            
           
        }

        [Required]
        public int ExternalCults { get; set; }
        [Required]
        public int CellsNumber { get; set; }
        [Required]
        public int Baptized { get; set; }
        [Required]
        public int MeetingsWithTheCoordination { get; set; }

    }
}
