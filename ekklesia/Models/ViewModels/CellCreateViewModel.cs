using System;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class CellCreateViewModel
    {        
        public DateTime Date { get; set; }
        [Required]
        public int Convertions { get; set; }
    }
}
