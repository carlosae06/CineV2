using AutoMapper;
using BackEnd.DTOs.Butaca;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs.Cliente;
using BackEnd.DTOs.Control;
using BackEnd.DTOs.Funsion;
using BackEnd.DTOs.Pelicula;
using BackEnd.DTOs.Persona;
using BackEnd.DTOs.Rol;
using BackEnd.DTOs.Sala;
using BackEnd.DTOs.Seccion;
using BackEnd.DTOs.Ticket;
using BackEnd.DTOs.Usuario;
using BackEnd.DTOs.Venta;
using BackEnd.Models;
using BackEnd.RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            //CreateMap<MateriaContenido, MateriaContenidoDTO>()
            //    .ForMember(x => x.EstadoNombre, x => x.MapFrom(y => y.IdEstadoNavigation != null ? y.IdEstadoNavigation.Nombre : ""))
            //    .ForMember(x => x.ModeloEstudioNombre, x => x.MapFrom(y => y.IdModeloEstudioNavigation != null ? y.IdModeloEstudioNavigation.Nombre : ""))
            //      .ForMember(x => x.GrupoAcademicoNombre, x => x.MapFrom(y => y.GrupoAcademico != null ? y.GrupoAcademico.Nombre : ""))
            //    .ReverseMap();

            //pelicula
            CreateMap<Pelicula, PeliculaDTO>().ForMember(x => x.Categoria, x => x.MapFrom(y => y.IdCategoriaNavigation != null ? y.IdCategoriaNavigation.Nombre : "")).ReverseMap();

            CreateMap<PeliculaInsertDTO, Pelicula>().ReverseMap();

            CreateMap<PeliculaUpdateDTO, Pelicula>().ReverseMap();

            //categoria
            CreateMap<CategoriaUpdateDTO, Categori>().ReverseMap();

            CreateMap<CategoriaDTO, Categori>().ReverseMap();

            CreateMap<CategoriaInsertDTO, Categori>().ReverseMap();

            //persona
            CreateMap<PersonaDTO, Persona>().ReverseMap();

            CreateMap<PersonaInsertDTO, Persona>().ReverseMap();

            CreateMap<PersonaUpdateDTO, Persona>().ReverseMap();

            //cliente
            CreateMap<ClienteDTO, Cliente>().ReverseMap();

            CreateMap<ClienteInsertDTO, Cliente>().ReverseMap();

            CreateMap<ClienteUpdateDTO, Cliente>().ReverseMap();

            //usuario
            // CreateMap<UsuarioDTO, Usuario>().ReverseMap();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(x => x.PersonaNombre, x => x.MapFrom(y => y.IdpersonaNavigation != null ? y.IdpersonaNavigation.Nombre + " " + y.IdpersonaNavigation.ApellidoPaterno + " " + y.IdpersonaNavigation.ApellidoMaterno : ""))
                 .ForMember(x => x.RolNombre, x => x.MapFrom(y => y.IdRolNavigation != null ? y.IdRolNavigation.Nombre : "")).ReverseMap();

            CreateMap<UsuarioInsertDTO, Usuario>().ReverseMap();

            CreateMap<UsuarioUpdateDTO, Usuario>().ReverseMap();

            //butaca
            CreateMap<ButacaDTO, Butaca>().ReverseMap();

            CreateMap<ButacaInsertDTO, Butaca>().ReverseMap();

            CreateMap<ButacaUpdateDTO, Butaca>().ReverseMap();

            //ticket
            CreateMap<TicketDTO, Ticket>().ReverseMap();

            CreateMap<TicketInsertDTO, Ticket>().ReverseMap();

            CreateMap<TicketUpdateDTO, Ticket>().ReverseMap();

            //control
            CreateMap<ControlDTO, Control>().ReverseMap();

            CreateMap<ControlInsertDTO, Control>().ReverseMap();

            CreateMap<ControlUpdateDTO, Control>().ReverseMap(); 

            //venta
            CreateMap<VentaDTO, Vent>().ReverseMap();

            CreateMap<VentaInsertDTO, Vent>().ReverseMap();

            CreateMap<VentaUpdateDTO, Vent>().ReverseMap();

            //sala
            CreateMap<SalaDTO, Sala>().ReverseMap();

            CreateMap<SalaInsertDTO, Sala>().ReverseMap();

            CreateMap<SalaUpdateDTO, Sala>().ReverseMap();

            //seccion
            CreateMap<SeccionDTO, Seccion>().ReverseMap();

            CreateMap<SeccionInsertDTO, Seccion>().ReverseMap();

            CreateMap<SeccionUpdateDTO, Seccion>().ReverseMap();

            //roll
            CreateMap<RolDTO, Rol>().ReverseMap();

            CreateMap<RolInsertDTO, Rol>().ReverseMap();

            CreateMap<RolUpdateDTO, Rol>().ReverseMap();

            //funcion 
            CreateMap<FunsionDTO, Funsion>().ReverseMap();

            CreateMap<FunsionInsertDTO, Funsion>().ReverseMap();

            CreateMap<FunsionUpdateDTO, Funsion>().ReverseMap();

           
        }
    
    }
}
