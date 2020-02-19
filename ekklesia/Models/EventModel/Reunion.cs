using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekklesia.Models.EventModel
{
    [Table("Reunions")]
    public class Reunion : Occasion
    {

        [Required]
        public string Topic { get; set; }
        [Required]
        public ReunionType ReunionType { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public Member Speaker { get; set; }
        public int SpeakerId { get; set; }
        public ICollection<OccasionMember> PresentMembers { get; set; }

        public Reunion()
        {
            this.PresentMembers = new List<OccasionMember>();
            EventType = EventType.REUNIÃO;
        }

        public Reunion(CreateReunionViewModel model)
        {
            Date = model.Date;
            EndTime = model.EndTime;
            EventType = EventType.REUNIÃO;
            Topic = model.Topic;
            this.PresentMembers = new List<OccasionMember>();

        }

        public void AddMember(Member member)
        {
            this.PresentMembers.Add(new OccasionMember() { Member = member, Occasion = this });
        }

        public bool Contains(Member member)
        {
            var om = new OccasionMember() { Member = member, Occasion = this };
            return this.PresentMembers.Contains(om);
        }

        public bool Remove(Member member)
        {
            var om = new OccasionMember() { Member = member, Occasion = this };
            return this.PresentMembers.Remove(om);
        }
    }
}
