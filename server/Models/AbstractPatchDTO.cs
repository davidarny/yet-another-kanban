using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class AbstractPatchDTO
    {
        private HashSet<string> PropertiesInHttpRequest { get; set; } = new HashSet<string>();

        /// <summary>
        /// Returns true if property was present in http request; false otherwise
        /// </summary>
        public bool IsFieldPresent(string prop)
        {
            return PropertiesInHttpRequest.Contains(prop.ToLowerInvariant());
        }

        public void SetHasProperty(string prop)
        {
            PropertiesInHttpRequest.Add(prop.ToLowerInvariant());
        }
    }
}
