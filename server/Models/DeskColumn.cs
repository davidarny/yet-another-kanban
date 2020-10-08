using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("desk_column")]
    public partial class DeskColumn
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_desk")]
        public int IdDesk { get; set; }

        [Required]
        [Column("label")]
        [StringLength(255)]
        public string Label { get; set; }

        [Required]
        [Column("order")]
        [MinLength(0)]
        public int Order { get; set; }

        [ForeignKey(nameof(IdDesk))]
        [InverseProperty(nameof(Models.Desk.DeskColumns))]
        public virtual Desk Desk { get; set; }

        [InverseProperty(nameof(Card.DeskColumn))]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
