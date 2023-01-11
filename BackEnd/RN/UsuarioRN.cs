using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Helpers;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using BackEnd.DTOs.Usuario;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.RN
{
    public class UsuarioRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UsuarioRN(CineContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<ResponseListDTO<UsuarioDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Usuarios.Include(x => x.IdpersonaNavigation).Include(x => x.IdRolNavigation).AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<UsuarioDTO>>(entidades);

            return new ResponseListDTO<UsuarioDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<UsuarioDTO>> getById(int id)
        {

            var entity = await context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var usuarioDTO = mapper.Map<UsuarioDTO>(entity);


            return new ResponseDTO<UsuarioDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuarioDTO,
            };


        }
        public async Task<ResponseDTO<UsuarioInsertDTO>> postInsert(UsuarioInsertDTO usuarioInsertDTO)
        {

            var entity = mapper.Map<Usuario>(usuarioInsertDTO);
            entity.Password = Encrypt.GetSHA256(usuarioInsertDTO.Password);

            context.Usuarios.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            var usuario = mapper.Map<UsuarioInsertDTO>(entity);
            return new ResponseDTO<UsuarioInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuario,
            };

        }
        public async Task<ResponseDTO<UsuarioDTO>> putUpdate(int id, UsuarioUpdateDTO usuarioUpdateDTO)
        {

            var entity = await context.Usuarios.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(usuarioUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var usuarioUpdate = mapper.Map<UsuarioDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<UsuarioDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuarioUpdate,
            };

        }
        public async Task<ResponseDTO<UsuarioDTO>> delete(int id)
        {

            var entity = await context.Usuarios.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Usuarios.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var usuario = mapper.Map<UsuarioDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<UsuarioDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuario,
            };

        }

        public async Task<UserToken> Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            var passEncryp = Encrypt.GetSHA256(loginDTO.Password);
            var usuario = await context.Usuarios
                            .Include(a => a.IdRolNavigation)
                            .FirstOrDefaultAsync(x => x.UserName == loginDTO.UserName && x.Password == passEncryp && x.Estado);
            if (usuario == null)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }
            //var passEncryp = Encrypt.GetSHA256(usuarioLogin.Password);
            if (usuario.Password != passEncryp)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }
            var usuarioDTO = mapper.Map<UsuarioDTO>(usuario);
            //construir token
            var userToken = ConstruirToken(usuarioDTO);
            return userToken;

            //try
            //{
            //    var pass = Encrypt.GetSHA256(loginDTO.Password);

            //    var usuario = await context.Usuarios
            //        .Include(x => x.IdRolNavigation)
            //        .FirstOrDefaultAsync(x => x.UserName == loginDTO.UserName && x.Password == pass && x.Estado);
            //    if (usuario == null)
                    
            //        return new ResponseDTO<UserToken>
            //        {
            //            Success = false,
            //            StatusCode = 500,
            //            Message = "",
            //            value = null,

            //        };

            //    var userdto = mapper.Map<UsuarioDTO>(usuario);

            //    var token = ConstruirToken(userdto);


            //    return new ResponseDTO<UserToken>
            //    {
            //        Success = true,
            //        StatusCode = 200,
            //        Message = "OK",
            //        value = token,

            //    };

            //}
            //catch (Exception ex)
            //{

            //    return new ResponseDTO<UserToken>
            //    {
            //        Success = false,
            //        StatusCode = 500,
            //        Message = ex.Message,
            //        value = null,

            //    };
            //}

        }



        private UserToken ConstruirToken(UsuarioDTO usuario)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,usuario.UserName),
                new Claim(ClaimTypes.Role,usuario.RolCod),

            };

            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(2);

            JwtSecurityToken token = new JwtSecurityToken(
                   issuer: null,
                   audience: null,
                   claims: claims,
                   expires: expiracion,
                    signingCredentials: creds);


            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion,
            };
        }
    }
}
