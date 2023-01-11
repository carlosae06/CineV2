using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Venta;
using Microsoft.EntityFrameworkCore;
using BackEnd.Helpers;

namespace BackEnd.RN
{
    public class VentaRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public VentaRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<VentaDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Vents.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<VentaDTO>>(entidades);

            return new ResponseListDTO<VentaDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<VentaDTO>> getById(int id)
        {

            var entity = await context.Vents
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var ventaDTO = mapper.Map<VentaDTO>(entity);


            return new ResponseDTO<VentaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = ventaDTO,
            };


        }
        public async Task<ResponseDTO<VentaInsertDTO>> postInsert(VentaInsertDTO ventaInsertDTO)
        {

            var entity = mapper.Map<Vent>(ventaInsertDTO);

            context.Vents.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<VentaInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = ventaInsertDTO,
            };

        }
        public async Task<ResponseDTO<VentaDTO>> putUpdate(int id, VentaUpdateDTO ventaUpdateDTO)
        {

            var entity = await context.Vents.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(ventaUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var ventaUpdate = mapper.Map<VentaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<VentaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = ventaUpdate,
            };

        }
        public async Task<ResponseDTO<VentaDTO>> delete(int id)
        {

            var entity = await context.Vents.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Vents.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var venta = mapper.Map<VentaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<VentaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = venta,
            };

        }
    }
}
