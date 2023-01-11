using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Rol;
using Microsoft.EntityFrameworkCore;
using BackEnd.Helpers;

namespace BackEnd.RN
{
    public class RolRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public RolRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<RolDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Rols.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<RolDTO>>(entidades);

            return new ResponseListDTO<RolDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<RolDTO>> getById(int id)
        {

            var entity = await context.Rols
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var rolDTO = mapper.Map<RolDTO>(entity);


            return new ResponseDTO<RolDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = rolDTO,
            };


        }
        public async Task<ResponseDTO<RolInsertDTO>> postInsert(RolInsertDTO rolInsertDTO)
        {

            var entity = mapper.Map<Rol>(rolInsertDTO);

            context.Rols.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<RolInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = rolInsertDTO,
            };

        }
        public async Task<ResponseDTO<RolDTO>> putUpdate(int id, RolUpdateDTO rolUpdateDTO)
        {

            var entity = await context.Rols.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(rolUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var rolUpdate = mapper.Map<RolDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<RolDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = rolUpdate,
            };

        }
        public async Task<ResponseDTO<RolDTO>> delete(int id)
        {

            var entity = await context.Rols.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Rols.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var rol = mapper.Map<RolDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<RolDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = rol,
            };

        }
    }
}
