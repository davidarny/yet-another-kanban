using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace server.Models
{
    /// <summary>
    /// Class that plugs in to Newtonsoft deserialization pipeline for classes descending from <see cref="PatchDtoBase"/>.
    /// For all properties, that are present in JSON it calls <see cref="PatchDtoBase.SetHasProperty"/>.`
    /// </summary>
    public class PatchRequestContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            var prop = base.CreateProperty(member, serialization);

            prop.SetIsSpecified += (o, o1) =>
            {
                if (o is AbstractPatchDTO dto)
                {
                    dto.SetHasProperty(prop.PropertyName);
                }
            };

            return prop;
        }
    }
}
