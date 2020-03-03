using ekklesia.Models.EventModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
        [DataMember]
        public string Name { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Número de telefone precisa conter 11 caracteres.")]
        [DataMember]
        public string Phone { get; set; }
        [Required]
        [DataMember]
        public Position Position { get; set; }
        [DataMember]
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
