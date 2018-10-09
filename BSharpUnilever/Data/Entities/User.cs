﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BSharpUnilever.Data.Entities
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Required]
        public string FullName { get; set; }

        public ICollection<Store> AssignedStores { get; set; }

        public ICollection<SupportRequest> Requests { get; set; }

        public ICollection<SupportRequest> ManagedRequests { get; set; }
    }
}