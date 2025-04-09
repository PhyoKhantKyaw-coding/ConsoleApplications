using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagementConsoleApplication
{
    [Table("Sale")]
    public class Sale
    {
        [Key]
        public Guid Id { get; set; }
        [Column("ProductId")]
        public int ProductId { get; set; }
        [Column("ProductName")]
        public string ProductName { get; set; }
        [Column("Quantity")]
        public int Quantity { get; set; }
        [Column("Price")]
        public decimal Price { get; set; }
        [Column("Profit")]
        public decimal Profit { get; set; }
    }
}
