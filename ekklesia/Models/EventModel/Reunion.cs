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
        public DateTime EndTime { get; set; }
        public Member Speaker { get; set; }
        public int SpeakerId { get; set; }
        public ICollection<OccasionMember> PresentMembers { get; set; }

        public Reunion()
        {
            this.PresentMembers = new List<OccasionMember>();
        }

        public Reunion(CreateReunionViewModel model)
        {
            Date = model.Date;
            EventType = EventType.Reunião;
            Topic = model.Topic;
            EndTime = model.EndTime;
            this.PresentMembers = new List<OccasionMember>();

        }

        public void AddMember(Member member)
        {
            this.PresentMembers.Add(new OccasionMember() { Member = member, Occasion = this });
        }
    }
}
