using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using server.DTO;

namespace server
{
    /// <summary>
    /// Class that plugs in to Newtonsoft deserialization pipeline for classes descending from <see cref="PatchDtoBase"/>.
    /// For all properties, that are present in JSON it calls <see cref="PatchDtoBase.SetHasProperty"/>.`
    /// </summary>
    public class PatchRequestContractResolver : CamelCasePropertyNamesContractResolver
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
