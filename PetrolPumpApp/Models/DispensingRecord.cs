using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetrolPumpApp.Models
{
    [Table("DispensingRecords")]
    public class DispensingRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DispenserNo { get; set; }

        [StringLength(50)]
        public string NozzleNo { get; set; }

        [StringLength(50)]
        public string FuelGrade { get; set; }

        public decimal Volume { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMode { get; set; }

        public DateTime TransactionDate { get; set; }

        [StringLength(100)]
        public string VehicleNumber { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}