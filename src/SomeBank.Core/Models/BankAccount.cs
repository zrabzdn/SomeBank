using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeBank.Core.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string AccountNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string AccountHolder { get; set; }

        [Required]
        public int BankID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string BankCode { get; set; }
    }
}
