using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Funsion;
using BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.RN
{
    public class FuncionRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public FuncionRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<FunsionDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Funsions.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<FunsionDTO>>(entidades);

            return new ResponseListDTO<FunsionDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<FunsionDTO>> getById(int id)
        {

            var entity = await context.Funsions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var funsionDTO = mapper.Map<FunsionDTO>(entity);


            return new ResponseDTO<FunsionDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = funsionDTO,
            };


        }
        public async Task<ResponseDTO<FunsionInsertDTO>> postInsert(FunsionInsertDTO funsionInsertDTO)
        {

            var entity = mapper.Map<Funsion>(funsionInsertDTO);

            context.Funsions.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<FunsionInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = funsionInsertDTO,
            };

        }
        public async Task<ResponseDTO<FunsionDTO>> putUpdate(int id, FunsionUpdateDTO funsionUpdateDTO)
        {

            var entity = await context.Funsions.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(funsionUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var funsionUpdate = mapper.Map<FunsionDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<FunsionDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = funsionUpdate,
            };

        }
        public async Task<ResponseDTO<FunsionDTO>> delete(int id)
        {

            var entity = await context.Funsions.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Funsions.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var funsion = mapper.Map<FunsionDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<FunsionDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = funsion,
            };

        }
    }
}
