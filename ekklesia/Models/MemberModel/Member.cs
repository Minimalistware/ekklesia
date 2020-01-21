using ekklesia.Models.EventModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.MemberModel
{
    public class Member : BaseModel
    {
        public Member()
        {
            this.Meetings = new List<EventMember>();
        }

        [Required]
        [MaxLength(50, ErrorMessage = "Nome não pode exceder mais de 50 caracteres.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Número de telefone precisa conter 11 caracteres.")]
        public string Phone { get; set; }
        [Required]
        public Position Position { get; set; }
        public string PhotoPath { get; set; }
        public virtual ICollection<EventMember> Meetings { get; set; }

    }
}
