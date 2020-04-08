﻿using ekklesia.Models.TransactionModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ekklesia.Models.ViewModels
{
    public class ExpenditureCreateViewModel : TransactionCreateViewModel

    {
        public ExpenditureCreateViewModel()
        {
        }

        public ExpenditureCreateViewModel(HashSet<SelectListItem> events)
        {
            AllEvents = events;
        }

        [Required]
        public string Description { get; set; }
        public IFormFile Invoice { get; set; }

    }
}
