using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.DTOs.Categoria;
using BackEnd.Helpers;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BackEnd.RN
{
    public class CategoriaRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public CategoriaRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<CategoriaDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Categoris.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<CategoriaDTO>>(entidades);

            return new ResponseListDTO<CategoriaDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<CategoriaDTO>> getById(int id)
        {
           
                var entity = await context.Categoris
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                    throw new Exception("No existe el recurso");

                var categoriaDTO = mapper.Map<CategoriaDTO>(entity);


                return new ResponseDTO<CategoriaDTO> { 
                    Success = true,
                    StatusCode = 200,
                    Message = "OK",
                    value = categoriaDTO,
                };


        }
        public async Task<ResponseDTO<CategoriaInsertDTO>> postInsert(CategoriaInsertDTO categoriaInsertDTO)
        {

            var entity = mapper.Map<Categori>(categoriaInsertDTO);

            context.Categoris.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<CategoriaInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = categoriaInsertDTO,
            };

        }
        public async Task<ResponseDTO<CategoriaDTO>> putUpdate(int id, CategoriaUpdateDTO categoriaUpdateDTO)
        {

            var entity = await context.Categoris.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(categoriaUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var categoriaUpdate = mapper.Map<CategoriaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<CategoriaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = categoriaUpdate,
            };

        }
        public async Task<ResponseDTO<CategoriaDTO>> delete(int id)
        {

            var entity = await context.Categoris.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Categoris.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var categoria = mapper.Map<CategoriaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<CategoriaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = categoria,
            };

        }
    }
}

