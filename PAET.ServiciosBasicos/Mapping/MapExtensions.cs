using AutoMapper;
using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.ServiciosBasicos.Mapping
{
    public static class MapExtensions
    {
        public static void ConfigureDefaultDtoMapping(this IMapperConfiguration mapConfig)
        {
            mapConfig.AddConditionalObjectMapper().Where((s, d) => s.Name == $"{d.Name}Dto");
            mapConfig.AddConditionalObjectMapper().Where((s, d) => $"{s.Name}Dto" == d.Name);
        }
    }
}
