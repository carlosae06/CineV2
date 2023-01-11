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
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRN usuarioRN;

        public UsuariosController(UsuarioRN usuarioRN)
        {
            this.usuarioRN = usuarioRN;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseListDTO<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get([FromQuery] PaginacionDTO paginacion)
        {
            try
            {
                var datos = await usuarioRN.getAll(paginacion);
                return Ok(datos);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var entity = await usuarioRN.getById(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseListDTO<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> postInsert([FromBody] UsuarioInsertDTO usuarioInsertDTO)
        {
            try
            {
                var entity = await usuarioRN.postInsert(usuarioInsertDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> putUpdate(int id, [FromBody] UsuarioUpdateDTO usuarioUpdateDTO)
        {
            try
            {
                var entity = await usuarioRN.putUpdate(id, usuarioUpdateDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                var entity = await usuarioRN.delete(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDTO) 
        {
            try
            {
                var usuario = await usuarioRN.Login(usuarioLoginDTO);
                if (usuario == null)
                {
                    return new ResponseError(StatusCodes.Status400BadRequest, "Usuario o contraseña incorrectos").GetObjectResult();

                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        
        }
    }
}
