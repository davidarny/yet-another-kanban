using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace server.Models
{
    [Table("user_x_desk")]
    public partial class UserHasDesk
    {
        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("id_desk")]
        public int IdDesk { get; set; }

        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(IdDesk))]
        public virtual Desk Desk { get; set; }
    }
}
