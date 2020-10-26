using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("desk")]
    public partial class Desk
    {
        public Desk()
        {
            DeskColumns = new HashSet<DeskColumn>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [Column("description")]
        [StringLength(1024)]
        public string Description { get; set; }

        [InverseProperty(nameof(DeskColumn.Desk))]
        public virtual ICollection<DeskColumn> DeskColumns { get; set; }
    }
}
