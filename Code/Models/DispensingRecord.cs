using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetrolPumpApp.Models
{
    public class DispensingRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DispenserNo { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal QuantityFilled { get; set; }

        [Required]
        [StringLength(50)]
        public string VehicleNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMode { get; set; }

        [StringLength(500)]
        public string PaymentProofPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
