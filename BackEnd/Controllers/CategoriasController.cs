using BackEnd.DTOs;
using BackEnd.DTOs.Categoria;
using BackEnd.RN;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaRN categoriaRN;

        public CategoriasController(CategoriaRN categoriaRN)
        {
            this.categoriaRN = categoriaRN;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseListDTO<CategoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get([FromQuery] PaginacionDTO paginacion)
        {
            try
            {
                var datos = await categoriaRN.getAll(paginacion);
                return Ok(datos);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<CategoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var entity = await categoriaRN.getById(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseListDTO<CategoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> postInsert([FromBody] CategoriaInsertDTO categoriaInsertDTO)
        {
            try
            {
                var entity = await categoriaRN.postInsert(categoriaInsertDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<CategoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> putUpdate(int id, [FromBody] CategoriaUpdateDTO categoriaUpdateDTO)
        {
            try
            {
                var entity = await categoriaRN.putUpdate(id, categoriaUpdateDTO);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ResponseListDTO<CategoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                var entity = await categoriaRN.delete(id);
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return new ResponseError(StatusCodes.Status400BadRequest, ex.Message).GetObjectResult();
            }
        }
    }
}
