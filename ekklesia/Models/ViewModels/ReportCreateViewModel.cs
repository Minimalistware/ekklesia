using ekklesia.Models.ReportModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public abstract class ReportCreateViewModel
    {
        public ReportCreateViewModel()
        {
            AllMembers = new HashSet<SelectListItem>();
            Date = DateTime.Now;
        }

        [Required]
        public ReportType Type { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int PreacherId { get; set; }
        public int CoordinatorId { get; set; }
        public HashSet<SelectListItem> AllMembers { get; set; }

        //ATIVIDADES COMUNS A TODOS OS EVENTOS
        [Required]
        public int Reunions { get; set; }
        [Required]
        public int Convertions { get; set; }
        [Required]

        //MOVIMENTO FINANCEIRO
        public decimal PreviousMonth { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Tenth { get; set; }
        public decimal Balance { get; set; }
    }
}
