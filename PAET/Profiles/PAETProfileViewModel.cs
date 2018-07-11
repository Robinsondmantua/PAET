using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PAET.DominioBase.Entidades_Dominio;
using PAET.Models;

namespace PAET.Profiles
{
    public class PAETProfileViewModel : Profile
    {
        protected override void Configure()
        {
            CreateMap<PreguntasDto, PreguntasViewModel>().ReverseMap();
            CreateMap<GeneralDto, TecnologiaDto>()
            .ForMember(dest => dest.IdTecnologia, opt => opt.MapFrom(src => src.Identificador))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Descripcion))
            .ReverseMap();
            CreateMap<GeneralDto, DificultadDto>()
                .ForMember(dest => dest.IdDificultad, opt => opt.MapFrom(src => src.Identificador))
                .ReverseMap();
            CreateMap<GeneralDto, TipoPreguntaDto>()
                .ForMember(dest => dest.IdTipoPregunta, opt => opt.MapFrom(src => src.Identificador))
                .ReverseMap();
            CreateMap<GeneralDto, ValoracionDto>()
                .ForMember(dest => dest.IdValoracion, opt => opt.MapFrom(src => src.Identificador))
                .ForMember(dest => dest.Puntuacion, opt => opt.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }
    }

}
