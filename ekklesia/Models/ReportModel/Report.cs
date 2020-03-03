using Caelum.Stella.CSharp.Vault;
using ekklesia.Models.MemberModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ReportModel
{
    public class Report : BaseModel
    {
        [Required]
        public DateTime Date { get; set; }
        public int PreacherId { get; set; }
        public Member Preacher { get; set; }
        public int CoordinatorId { get; set; }
        public Member Coordinator { get; set; }

        //ATIVIDADES DA ESCOLA BÍBLICA DE ALIANÇA
        [Required]
        public int Reunions { get; set; }
        [Required]
        public int Convertions { get; set; }        
        [Required]
        public int ReunionWithTeachers { get; set; }
        [Required]
        public int PeoplePresent { get; set; }
        [Required]
        public int PedagogicalBody { get; set; }

        //MOVIMENTO FINANCEIRO
        public Money PreviousMonth { get; set; }
        public Money Income { get; set; }
        public Money Expense { get; set; }
        public Money Tenth { get; set; }
        public Money Balance { get; set; }

    }
}
