using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Control;
using BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.RN
{
    public class ControlRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public ControlRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<ControlDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Controls.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<ControlDTO>>(entidades);

            return new ResponseListDTO<ControlDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<ControlDTO>> getById(int id)
        {

            var entity = await context.Controls
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var controlDTO = mapper.Map<ControlDTO>(entity);


            return new ResponseDTO<ControlDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = controlDTO,
            };


        }
        public async Task<ResponseDTO<ControlInsertDTO>> postInsert(ControlInsertDTO controlInsertDTO)
        {

            var entity = mapper.Map<Control>(controlInsertDTO);

            context.Controls.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ControlInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = controlInsertDTO,
            };

        }
        public async Task<ResponseDTO<ControlDTO>> putUpdate(int id, ControlUpdateDTO controlUpdateDTO)
        {

            var entity = await context.Controls.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(controlUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var controlUpdate = mapper.Map<ControlDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ControlDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = controlUpdate,
            };

        }
        public async Task<ResponseDTO<ControlDTO>> delete(int id)
        {

            var entity = await context.Controls.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Controls.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var control = mapper.Map<ControlDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ControlDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = control,
            };

        }
    }
}
