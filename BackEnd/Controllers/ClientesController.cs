using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs.Cliente;
using BackEnd.DTOs.Persona;
using BackEnd.DTOs.Usuario;
using BackEnd.Helpers;
using BackEnd.Models;
using BackEnd.RN;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRN clienteRN;

        public ClientesController(ClienteRN clienteRN)
        {
            this.clienteRN = clienteRN;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseListDTO<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get([FromQuery] PaginacionDTO paginacion)
        {
            try
            {
                var datos = await clienteRN.getAll(paginacion);
                return Ok(datos);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var entity = await clienteRN.getById(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseListDTO<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> postInsert([FromBody] ClienteInsertDTO clienteInsertDTO)
        {
            try
            {
                var entity = await clienteRN.postInsert(clienteInsertDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> putUpdate(int id, [FromBody] ClienteUpdateDTO clienteUpdateDTO)
        {
            try
            {
                var entity = await clienteRN.putUpdate(id, clienteUpdateDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                var entity = await clienteRN.delete(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
    }
}
