using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Meta.Core.Model
{
    [Table("meta_transaction")]
    public  class MetaTransaction
    {

        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Column("FromAccount")]
        public int FromAccount { get; set; }
        [Column("ToAccount")]
        public int ToAccount { get; set; }
        [Column("Amount")]
        public double Amount { get; set; }
        [Column("LastIP")]
        public string LastIP { get; set; }
        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }
        [Column("UserId")]
        public string UserId { get; set; } 
    }
}
