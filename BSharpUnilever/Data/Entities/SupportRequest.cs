﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSharpUnilever.Data.Entities
{
    public class SupportRequest
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SerialNumber { get; set; }

        public int State { get; set; } // Draft (0), Submitted (10), Approved (20), Posted (100), Rejected (-1), Canceled (-10)

        public string AccountExecutiveId { get; set; }
        public User AccountExecutive { get; set; }

        [Required]
        public string ManagerId { get; set; }
        public User Manager { get; set; }

        [Required]
        public string Reason { get; set; } // Display Contract (DC), Premium Support (PS), Price Reduction (PR)

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public ICollection<SupportRequestLineItem> LineItems { get; set; }

        public ICollection<StateChange> StateChanges { get; set; }

        public ICollection<GeneratedDocument> GeneratedDocuments { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTimeOffset Modified { get; set; }
    }

    // This is a weak entity, so we leave it in the same file as its parent
    public class SupportRequestLineItem
    {
        public int Id { get; set; }

        public int SupportRequestId { get; set; }
        public SupportRequest SupportRequest { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = SQL_DECIMAL)]
        public decimal Quantity { get; set; }

        [Column(TypeName = SQL_DECIMAL)]
        public decimal RequestedSupport { get; set; }

        [Column(TypeName = SQL_DECIMAL)]
        public decimal RequestedValue { get; set; }

        [Column(TypeName = SQL_DECIMAL)]
        public decimal ApprovedSupport { get; set; }

        [Column(TypeName = SQL_DECIMAL)]
        public decimal ApprovedValue { get; set; }

        [Column(TypeName = SQL_DECIMAL)]
        public decimal UsedSupport { get; set; }

        [Column(TypeName = SQL_DECIMAL)]
        public decimal UsedValue { get; set; }

        private const string SQL_DECIMAL = "decimal(18, 2)";
    }

    // Another weak entity, keep in the same file
    public class StateChange
    {
        public int Id { get; set; }

        public int SupportRequestId { get; set; }
        public SupportRequest SupportRequest { get; set; }

        public int FromState { get; set; } // Same states as SupportRequest

        public int ToState { get; set; }// Same states as SupportRequest

        public DateTimeOffset Time { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }

    // Another weak entity, keep in the same file
    public class GeneratedDocument
    {
        public int Id { get; set; }

        public int SupportRequestId { get; set; }
        public SupportRequest SupportRequest { get; set; }

        public int SerialNumber { get; set; }

        public int State { get; set; } // Valid (0), Void (-1)
    }
}