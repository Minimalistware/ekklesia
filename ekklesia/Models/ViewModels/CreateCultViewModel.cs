using ekklesia.Models.EventModel;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class CreateCultViewModel: CreateEventViewModel
    {
        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        public string MainVerse { get; set; }
        [Required]
        public CultType Type { get; set; }
        [Required]
        public int Convertions { get; set; }
    }
}
