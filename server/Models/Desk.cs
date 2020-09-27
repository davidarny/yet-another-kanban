using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("desk")]
    public partial class Desk
    {
        public Desk()
        {
            UserHasDesk = new HashSet<UserHasDesk>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }

        [Column("description")]
        [StringLength(1024)]
        public string Description { get; set; }

        [InverseProperty("Desk")]
        public virtual ICollection<UserHasDesk> UserHasDesk { get; set; }

        [InverseProperty("Desk")]
        public virtual ICollection<DeskColumn> DeskColumns { get; set; }
    }
}
