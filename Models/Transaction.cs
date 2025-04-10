using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCRMShield.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public string? UserEmail { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public bool IsFraudAlert { get; set; } = false;

        public bool IsBlocked { get; set; } = false;
    }
}
