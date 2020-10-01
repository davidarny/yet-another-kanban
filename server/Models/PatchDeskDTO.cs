using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class PatchDeskDTO : AbstractPatchDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
