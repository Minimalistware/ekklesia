using Caelum.Stella.CSharp.Vault;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class ReportCreateViewModel
    {
        public ReportCreateViewModel()
        {
            AllMembers = new List<SelectListItem>();
            Date = DateTime.Now;
        }

        [Required]
        public DateTime Date { get; set; }
        public int PreacherId { get; set; }
        public int CoordinatorId { get; set; }
        public List<SelectListItem> AllMembers { get; set; }
        //ATIVIDADES DA ESCOLA BÍBLICA DE ALIANÇA
        [Required]
        public int Reunions { get; set; }
        [Required]
        public int Convertions { get; set; }
        [Required]
        public int Bibles { get; set; }
        [Required]
        public int ReunionWithTeachers { get; set; }
        [Required]
        public int Visitors { get; set; }
        [Required]
        public int PeoplePresent { get; set; }
        [Required]
        public int PedagogicalBody { get; set; }

        //MOVIMENTO FINANCEIRO
        public decimal PreviousMonth { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Tenth { get; set; }
        public decimal Balance { get; set; }
    }
}
