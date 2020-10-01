using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class PatchUserDTO : AbstractPatchDTO
    {
        public string? Email { get; set; }
    }
}
