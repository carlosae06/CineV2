using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.RN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.DTOs.Seccion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SeccionesController : ControllerBase
    {

        private readonly SeccionRN seccionRN;

        public SeccionesController(SeccionRN seccionRN)
        {
            this.seccionRN = seccionRN;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseListDTO<SeccionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get([FromQuery] PaginacionDTO paginacion)
        {
            try
            {
                var datos = await seccionRN.getAll(paginacion);
                return Ok(datos);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<SeccionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var entity = await seccionRN.getById(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseListDTO<SeccionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> postInsert([FromBody] SeccionInsertDTO seccionInsertDTO)
        {
            try
            {
                var entity = await seccionRN.postInsert(seccionInsertDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<SeccionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> putUpdate(int id, [FromBody] SeccionUpdateDTO seccionUpdateDTO)
        {
            try
            {
                var entity = await seccionRN.putUpdate(id, seccionUpdateDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<SeccionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                var entity = await seccionRN.delete(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
    }
}
