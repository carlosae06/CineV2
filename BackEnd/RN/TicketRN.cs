using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.DTOs.Ticket;
using BackEnd.Helpers;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.RN
{
    public class TicketRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public TicketRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<TicketDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Tickets.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<TicketDTO>>(entidades);

            return new ResponseListDTO<TicketDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<TicketDTO>> getById(int id)
        {

            var entity = await context.Tickets
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var ticketDTO = mapper.Map<TicketDTO>(entity);


            return new ResponseDTO<TicketDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = ticketDTO,
            };


        }
        public async Task<ResponseDTO<TicketInsertDTO>> postInsert(TicketInsertDTO ticketInsertDTO)
        {

            var entity = mapper.Map<Ticket>(ticketInsertDTO);

            context.Tickets.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<TicketInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = ticketInsertDTO,
            };

        }
        public async Task<ResponseDTO<TicketDTO>> putUpdate(int id, TicketUpdateDTO ticketUpdateDTO)
        {

            var entity = await context.Tickets.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(ticketUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var tiketUpdate = mapper.Map<TicketDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<TicketDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = tiketUpdate,
            };

        }
        public async Task<ResponseDTO<TicketDTO>> delete(int id)
        {

            var entity = await context.Tickets.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Tickets.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var ticket = mapper.Map<TicketDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<TicketDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = ticket,
            };

        }
    }
}
