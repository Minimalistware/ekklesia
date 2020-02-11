using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class MemberSreachViewModel
    {
        public MemberSreachViewModel()
        {
            PageTitle = "Buscar membro.";
        }

        [MaxLength(50, ErrorMessage = "Nome não pode exceder 50 caracteres.")]
        public string Name { get; set; }
        public Position? Position { get; set; }
        public string PageTitle { get; set; }        
    }
}
