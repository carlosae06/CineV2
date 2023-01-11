using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.DTOs.Pelicula;
using BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.RN
{
    public class PeliculaRN
    {

        private readonly CineContext context;
        private readonly IMapper mapper;

        public PeliculaRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<PeliculaDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Peliculas.Include(x => x.IdCategoriaNavigation).AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<PeliculaDTO>>(entidades);

            return new ResponseListDTO<PeliculaDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }
        public async Task<ResponseDTO<PeliculaDTO>> getById(int id)
        {

            var entity = await context.Peliculas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var peliculaDTO = mapper.Map<PeliculaDTO>(entity);


            return new ResponseDTO<PeliculaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = peliculaDTO,
            };


        }
        public async Task<ResponseDTO<PeliculaInsertDTO>> postInsert(PeliculaInsertDTO peliculaInsertDTO)
        {

            var entity = mapper.Map<Pelicula>(peliculaInsertDTO);

            context.Peliculas.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<PeliculaInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = peliculaInsertDTO,
            };

        }
        public async Task<ResponseDTO<PeliculaDTO>> putUpdate(int id, PeliculaUpdateDTO peliculaUpdateDTO)
        {

            var entity = await context.Peliculas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(peliculaUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var peliculaUpdate = mapper.Map<PeliculaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<PeliculaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = peliculaUpdate,
            };

        }
        public async Task<ResponseDTO<PeliculaDTO>> delete(int id)
        {

            var entity = await context.Peliculas.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Peliculas.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var pelicula = mapper.Map<PeliculaDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<PeliculaDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = pelicula,
            };

        }
    }
}
