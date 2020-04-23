using Caelum.Stella.CSharp.Vault;
using ekklesia.Models.MemberModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ReportModel
{
    public abstract class Report : BaseModel
    {
        //ATIVIDADES BÁSICAS DE RELATÓRIO
        [Required]
        public ReportType ReportType { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int PreacherId { get; set; }
        public Member Preacher { get; set; }
        public int CoordinatorId { get; set; }
        public Member Coordinator { get; set; }

        //ATIVIDADES BÁSICAS PARA EVENTOS
        [Required]
        public int Reunions { get; set; }
        [Required]
        public int Convertions { get; set; }

       
        //MOVIMENTO FINANCEIRO
        public Money PreviousMonth { get; set; }
        public Money Income { get; set; }
        public Money Expense { get; set; }
        public Money Tenth { get; set; }
        public Money Balance { get; set; }

    }
}
