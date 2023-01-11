using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Sala;
using Microsoft.EntityFrameworkCore;
using BackEnd.Helpers;

namespace BackEnd.RN
{
    public class SalaRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public SalaRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<SalaDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Salas.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<SalaDTO>>(entidades);

            return new ResponseListDTO<SalaDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<SalaDTO>> getById(int id)
        {

            var entity = await context.Salas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var salaDTO = mapper.Map<SalaDTO>(entity);


            return new ResponseDTO<SalaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = salaDTO,
            };


        }
        public async Task<ResponseDTO<SalaInsertDTO>> postInsert(SalaInsertDTO salaInsertDTO)
        {

            var entity = mapper.Map<Sala>(salaInsertDTO);

            context.Salas.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<SalaInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = salaInsertDTO,
            };

        }
        public async Task<ResponseDTO<SalaDTO>> putUpdate(int id, SalaUpdateDTO salaUpdateDTO)
        {

            var entity = await context.Salas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(salaUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var salaUpdate = mapper.Map<SalaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<SalaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = salaUpdate,
            };

        }
        public async Task<ResponseDTO<SalaDTO>> delete(int id)
        {

            var entity = await context.Salas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Salas.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var sala = mapper.Map<SalaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<SalaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = sala,
            };

        }
    }
}
