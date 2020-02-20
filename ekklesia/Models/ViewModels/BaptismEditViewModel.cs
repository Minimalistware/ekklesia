using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using System.Collections.Generic;

namespace ekklesia.Models.ViewModels
{
    public class BaptismEditViewModel : BaptismCreateViewModel
    {
        public BaptismEditViewModel()
        {
            BaptizedMembers = new HashSet<Member>();            
        }

        public BaptismEditViewModel(Baptism baptism)
        {
            Id = baptism.Id;
            Date = baptism.Date;
            Place = baptism.Place;
            BaptizerId = baptism.BaptizerId;
            BaptizedMembers = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string PageTitle { get; set; }

        public IEnumerable<Member> BaptizedMembers { get; set; }
    }
}
