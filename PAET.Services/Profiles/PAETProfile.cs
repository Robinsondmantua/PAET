using AutoMapper;
using PAET.DominioBase.Entidades_Dominio;
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
            CreateMap<Tecnologia, TecnologiaDto>().ReverseMap();
            CreateMap<PreguntasDto, Preguntas>()
            .ForMember(dest => dest.Respuestas, opt => opt.MapFrom(src => src.Respuestas)).ReverseMap();
            CreateMap<CandidatosDto, Candidatos>().ReverseMap();
            CreateMap<EntrevistasDto, Entrevistas>()
                .ForMember(dest => dest.EntrevistaCandidato, opt => opt.MapFrom(src => src.EntrevistaCandidato)).ReverseMap();
            CreateMap<EntrevistaCandidatoDto, EntrevistaCandidato>().ReverseMap();
            CreateMap<VwEntrevistaCandidatosDto, VwEntrevistaCandidatos>().ReverseMap();
        }
    }
}
