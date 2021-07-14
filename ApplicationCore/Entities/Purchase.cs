using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public Guid PurchaseNumber { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime PurchaseDateTime { get; set; }
        [Required]
        public int MovieId { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }

    }
}
