using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagementConsoleApplication
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Column("Stock")]
        public int Stock { get; set; }
        [Column("Price")]
        public decimal Price { get; set; }
        [Column("Cost")]
        public decimal Cost { get; set; }
        [Column("Profit")]
        public decimal Profit => Price - Cost;
    }  
}
