using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Butaca;
using BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.RN
{
    public class ButacaRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public ButacaRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<ButacaDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Butacas.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<ButacaDTO>>(entidades);

            return new ResponseListDTO<ButacaDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<ButacaDTO>> getById(int id)
        {

            var entity = await context.Butacas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var categoriaDTO = mapper.Map<ButacaDTO>(entity);


            return new ResponseDTO<ButacaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = categoriaDTO,
            };


        }
        public async Task<ResponseDTO<ButacaInsertDTO>> postInsert(ButacaInsertDTO butacaInsertDTO)
        {

            var entity = mapper.Map<Butaca>(butacaInsertDTO);

            context.Butacas.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ButacaInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = butacaInsertDTO,
            };

        }
        public async Task<ResponseDTO<ButacaDTO>> putUpdate(int id, ButacaUpdateDTO butacaUpdateDTO)
        {

            var entity = await context.Butacas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(butacaUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var butacaUpdate = mapper.Map<ButacaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ButacaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = butacaUpdate,
            };

        }
        public async Task<ResponseDTO<ButacaDTO>> delete(int id)
        {

            var entity = await context.Butacas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Butacas.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var bucaca = mapper.Map<ButacaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ButacaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = bucaca,
            };

        }
    }
}
