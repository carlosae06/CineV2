using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.DTOs.Cliente;
using BackEnd.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.RN
{
    public class ClienteRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public ClienteRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<ClienteDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Clientes.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<ClienteDTO>>(entidades);

            return new ResponseListDTO<ClienteDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<ClienteDTO>> getById(int id)
        {

            var entity = await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var clienteDTO = mapper.Map<ClienteDTO>(entity);


            return new ResponseDTO<ClienteDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = clienteDTO,
            };


        }
        public async Task<ResponseDTO<ClienteInsertDTO>> postInsert(ClienteInsertDTO clienteInsertDTO)
        {

            var entity = mapper.Map<Cliente>(clienteInsertDTO);

            context.Clientes.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ClienteInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = clienteInsertDTO,
            };

        }
        public async Task<ResponseDTO<ClienteDTO>> putUpdate(int id, ClienteUpdateDTO clienteUpdateDTO)
        {

            var entity = await context.Clientes.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(clienteUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var clienteUpdate = mapper.Map<ClienteDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ClienteDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = clienteUpdate,
            };

        }
        public async Task<ResponseDTO<ClienteDTO>> delete(int id)
        {

            var entity = await context.Clientes.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Clientes.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var cliente = mapper.Map<ClienteDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<ClienteDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = cliente,
            };

        }
    }
}
