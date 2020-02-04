using ekklesia.Models.EventModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.MemberModel
{
    public class Member : BaseModel
    {
        public Member()
        {
            this.Meetings = new List<OccasionMember>();
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
        public virtual ICollection<OccasionMember> Meetings { get; set; }


        public void AddMeeting(Occasion occasion)
        {
            Meetings.Add(new OccasionMember() { Occasion = occasion });
        }

        public override bool Equals(object obj)
        {
            Member member = obj as Member;

            if (member == null)
            {
                return false;
            }
            return this.Id.Equals(member.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
