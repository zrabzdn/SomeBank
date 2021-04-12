using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeBank.Core.Models
{
    public class Bank
    {
        [Key]
        public int BankID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }
    }
}
