using ekklesia.Models.TransactionModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class RevenueCreateViewModel : TransactionCreateViewModel
    {
        [Required]
        public RevenueType RevenueType { get; set; }

        public RevenueCreateViewModel() { }

        public RevenueCreateViewModel(HashSet<SelectListItem> events)
        {
            AllEvents = events;
        }

        public RevenueCreateViewModel(Revenue revenue)
        {
            Date = revenue.Date;
            Value = revenue.Value;
            RevenueType = revenue.RevenueType;
            OccasionId = revenue.OccasionId;
        }



    }
}
