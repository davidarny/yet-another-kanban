using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("user_account")]
    public partial class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("login")]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [Column("email")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
