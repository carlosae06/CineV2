using BackEnd.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {
            return queryable
                .Skip((paginacionDTO.Pagina - 1) * paginacionDTO.CantidadRegistrosPorPagina)
                .Take(paginacionDTO.CantidadRegistrosPorPagina);
        }

        public async static Task<Dictionary<string, string>> datosPaginacion<T>(
     this IQueryable<T> queryable, int cantidadRegistrosPorPagina)
        {
            double cantidad = await queryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadRegistrosPorPagina);

            return new Dictionary<string, string> {
                { "cantidadPaginas",  cantidadPaginas.ToString() },
                { "totalRegistros",  cantidad.ToString() },
            };


        }


    }
}
