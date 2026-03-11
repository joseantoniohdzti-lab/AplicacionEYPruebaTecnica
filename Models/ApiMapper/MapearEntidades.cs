using AutoMapper;
using Models.Entidades;
using Models.EntidadesDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ApiMapper
{
    public class MapearEntidades : Profile
    {
        public MapearEntidades()
        {
            CreateMap<Usuarios,UsuarioDto>().ReverseMap();
            CreateMap<AddUsuariosDto,UsuarioDto>().ReverseMap();
            CreateMap<AddUsuariosDto,Usuarios>().ReverseMap();
            CreateMap<PasswordDtos, Usuarios>().ReverseMap();
        }
    }
}
