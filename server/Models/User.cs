using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("note")]
    public partial class User
    {
        public User()
        {
            UserHasDesk = new HashSet<UserHasDesk>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("login")]
        [StringLength(255)]
        [Required]
        public string Login { get; set; }

        [Column("password")]
        [StringLength(255)]
        [Required]
        public string Password { get; set; }

        [Column("email")]
        [StringLength(255)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserHasDesk> UserHasDesk { get; set; }
    }
}
