using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ViewModels
{
    public class MemberDetailsViewModel
    {
        public string Name { get; internal set; }
        public string Phone { get; internal set; }
        public Position Position { get; internal set; }
        public string PageTitle { get; internal set; }
    }
}
