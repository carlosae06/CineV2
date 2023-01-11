using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Seccion;
using Microsoft.EntityFrameworkCore;
using BackEnd.Helpers;

namespace BackEnd.RN
{
    public class SeccionRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public SeccionRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<SeccionDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Seccions.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<SeccionDTO>>(entidades);

            return new ResponseListDTO<SeccionDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<SeccionDTO>> getById(int id)
        {

            var entity = await context.Seccions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var seccionDTO = mapper.Map<SeccionDTO>(entity);


            return new ResponseDTO<SeccionDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = seccionDTO,
            };


        }
        public async Task<ResponseDTO<SeccionInsertDTO>> postInsert(SeccionInsertDTO seccionInsertDTO)
        {

            var entity = mapper.Map<Seccion>(seccionInsertDTO);

            context.Seccions.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<SeccionInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = seccionInsertDTO,
            };

        }
        public async Task<ResponseDTO<SeccionDTO>> putUpdate(int id, SeccionUpdateDTO seccionUpdateDTO)
        {

            var entity = await context.Seccions.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(seccionUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var seccionUpdate = mapper.Map<SeccionDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<SeccionDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = seccionUpdate,
            };

        }
        public async Task<ResponseDTO<SeccionDTO>> delete(int id)
        {

            var entity = await context.Seccions.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Seccions.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var seccion = mapper.Map<SeccionDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<SeccionDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = seccion,
            };

        }
    }
}
