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

        [Required]
        [Column("id_column")]
        public int IdColumn { get; set; }

        [Required]
        [Column("content")]
        [StringLength(1024)]
        public string Context { get; set; }

        [Required]
        [Column("order")]
        [MinLength(0)]
        public int Order { get; set; }

        [ForeignKey(nameof(IdColumn))]
        [InverseProperty(nameof(Models.DeskColumn.Cards))]

        public virtual DeskColumn DeskColumn { get; set; }
    }
}
