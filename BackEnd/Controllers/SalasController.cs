using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.RN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.DTOs.Sala;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalasController : ControllerBase
    {

        private readonly SalaRN salaRN;

        public SalasController(SalaRN salaRN)
        {
            this.salaRN = salaRN;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseListDTO<SalaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get([FromQuery] PaginacionDTO paginacion)
        {
            try
            {
                var datos = await salaRN.getAll(paginacion);
                return Ok(datos);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<SalaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var entity = await salaRN.getById(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseListDTO<SalaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> postInsert([FromBody] SalaInsertDTO salaInsertDTO)
        {
            try
            {
                var entity = await salaRN.postInsert(salaInsertDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<SalaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> putUpdate(int id, [FromBody] SalaUpdateDTO salaUpdateDTO)
        {
            try
            {
                var entity = await salaRN.putUpdate(id, salaUpdateDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<SalaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                var entity = await salaRN.delete(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
    }
}
