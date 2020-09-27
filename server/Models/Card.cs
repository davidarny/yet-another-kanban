using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("card")]
    public class Card
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_column")]
        [Required]
        public int IdColumn { get; set; }

        [ForeignKey(nameof(IdColumn))]
        [InverseProperty(nameof(Models.DeskColumn.Cards))]
        public virtual DeskColumn DeskColumn { get; set; }
    }
}
