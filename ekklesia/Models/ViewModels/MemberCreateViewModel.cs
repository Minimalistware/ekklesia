using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class MemberCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Nome não pode exceder 50 caracteres.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Número de telefone não pode exceder 11 caracteres.")]
        public string Phone { get; set; }
        [Required]
        public Position Position { get; set; }
        public string PageTitle { get; set; }
        public IFormFile Photo { get; set; }

    }
}
