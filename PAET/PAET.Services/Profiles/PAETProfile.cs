using AutoMapper;
using EMVS.Comun.DominioBase.PAET;
using PAET.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Services.Profiles
{
    public class PAETProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PreguntasDto, Preguntas>()
            .ForMember(dest => dest.Respuestas, opt => opt.MapFrom(src => src.Respuestas));
        }
    }
}
