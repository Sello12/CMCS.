using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Claim
    {
        [Key]
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string SupportingDocumentPath { get; set; }
    }
}


