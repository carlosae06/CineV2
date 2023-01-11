using BackEnd.DTOs;
using System.Security.Claims;

namespace BackEnd.Helpers
{
    public class UserJWT
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public UserJWT(IHttpContextAccessor httpContextAccessor)
        {
            _httpAccessor = httpContextAccessor;
        }

        public UserAuth userAuth
        {
            get
            {
                if (_httpAccessor.HttpContext != null)
                {
                    if (_httpAccessor.HttpContext.User != null)
                    {
                        if (_httpAccessor.HttpContext.User.Identity != null)
                        {
                            if (_httpAccessor.HttpContext.User.Identity.IsAuthenticated)
                            {
                                var user = _httpAccessor.HttpContext.User;
                                var token = _httpAccessor.HttpContext.Request.Headers["Authorization"].ToString();



                                var xuserAuth = new UserAuth
                                {
                                    Id = int.Parse(user.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)!.Value),
                                    Email = user.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email)!.Value,
                                    UserName = user.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name)!.Value,
                                    Rol = user.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role)!.Value,
                                    Expiracion = long.Parse(user.Claims.FirstOrDefault(a => a.Type == "exp")!.Value),
                                    Token = token,

                                };

                                return xuserAuth;
                            }
                        }

                    }
                }


                return new UserAuth();
            }
        }
    }
}
