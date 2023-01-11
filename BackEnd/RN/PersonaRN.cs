using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.DTOs.Persona;
using BackEnd.Helpers;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.RN
{
    public class PersonaRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public PersonaRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<PersonaDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Personas.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<PersonaDTO>>(entidades);

            return new ResponseListDTO<PersonaDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<PersonaDTO>> getById(int id)
        {

            var entity = await context.Personas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var personaDTO = mapper.Map<PersonaDTO>(entity);


            return new ResponseDTO<PersonaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = personaDTO,
            };


        }
        public async Task<ResponseDTO<PersonaInsertDTO>> postInsert(PersonaInsertDTO personaInsertDTO)
        {

            var entity = mapper.Map<Persona>(personaInsertDTO);

            context.Personas.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<PersonaInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = personaInsertDTO,
            };

        }
        public async Task<ResponseDTO<PersonaDTO>> putUpdate(int id, PersonaUpdateDTO personaUpdateDTO)
        {

            var entity = await context.Personas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(personaUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var personaUpdate = mapper.Map<PersonaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<PersonaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = personaUpdate,
            };

        }
        public async Task<ResponseDTO<PersonaDTO>> delete(int id)
        {

            var entity = await context.Personas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para eliminar no existe");

            context.Personas.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var persona = mapper.Map<PersonaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<PersonaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = persona,
            };

        }
    }
}
