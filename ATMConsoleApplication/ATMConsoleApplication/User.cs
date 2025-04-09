using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMConsoleApplication
{
    [Table("TBL_User")]
    public class User
    {
        [Key]
        public Guid UserID { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("UserPassword")]
        public string Password { get; set; }

        [Column("Wallet")]
        public decimal Wallet { get; set; }

        // Custom constructor for your usage
        public User() { }

        public User(Guid userId, string password, decimal wallet, string userName)
        {
            UserID = userId;
            Password = password;
            Wallet = wallet;
            UserName = userName;
        }

    }
}
