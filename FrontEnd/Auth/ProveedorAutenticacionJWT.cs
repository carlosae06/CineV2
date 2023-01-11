using FrontEnd.DTOs;
using FrontEnd.Helpers;
using FrontEnd.Repositorio;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace FrontEnd.Auth
{
    public class ProveedorAutenticacionJWT : AuthenticationStateProvider, ILoginService
    {
        public static readonly string TOKENKEY = "TOKENKEY";
        public static readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        private readonly IRepositorio repositorio;

        public ProveedorAutenticacionJWT(IJSRuntime js, HttpClient httpClient,
          IRepositorio repositorio)
        {
            this.js = js;
            this.httpClient = httpClient;
            this.repositorio = repositorio;
        }

        private AuthenticationState Anonimo =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        //convertir JWT en Claims con un listado de roles
        private async Task<ClaimsPrincipal> JWTtoClaimsPrincipal(string jwt)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwt);
                var tokenS = handler.ReadToken(jwt) as JwtSecurityToken;
                var claims = new List<Claim>();
                claims.AddRange(tokenS.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(x => new Claim(ClaimTypes.Role, x.Value)));
                //agregar los claims excepto los roles que ya se agregaron
                claims.AddRange(tokenS.Claims.Where(claim => claim.Type != ClaimTypes.Role).Select(x => new Claim(x.Type, x.Value)));
                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);
                return user;
            }
            catch (Exception)
            {

                await  Logout();
                return new ClaimsPrincipal(new ClaimsIdentity());   
            }
           
        }

       


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonimo;
            }

            var tiempoExpiracionString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
            DateTime tiempoExpiracion;

            if (DateTime.TryParse(tiempoExpiracionString, out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Limpiar();
                    return Anonimo;
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    token = await RenovarToken(token);

                    if (string.IsNullOrEmpty(token))
                    {
                        await Limpiar();
                        return Anonimo;
                    }
                }

            }

            return await ConstruirAuthenticationState(token);




        }

        public async Task<AuthenticationState> ConstruirAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(await JWTtoClaimsPrincipal(token));
        }



        private async Task Limpiar()
        {
            await js.RemoveItem(TOKENKEY);
            await js.RemoveItem(EXPIRATIONTOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private async Task<string> RenovarToken(string token)
        {
            Console.WriteLine("Renovando el token...");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var nuevoTokenResponse = await repositorio.Get<UserToken>("usuario/RenovarToken");

            if (!nuevoTokenResponse.Error)
            {
                var nuevoToken = nuevoTokenResponse.Response;
                await js.SetInLocalStorage(TOKENKEY, nuevoToken.Token);
                await js.SetInLocalStorage(EXPIRATIONTOKENKEY, nuevoToken.Expiracion.ToString());
                return nuevoToken.Token;
            }


            return "";


        }

        private bool DebeRenovarToken(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }
        private bool TokenExpirado(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion <= DateTime.UtcNow;
        }
    
        public async Task Login(UserToken userToken)
        {
            await js.SetInLocalStorage(TOKENKEY, userToken.Token);
            await js.SetInLocalStorage(EXPIRATIONTOKENKEY, userToken.Expiracion.ToString());
            var authState = await ConstruirAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Limpiar();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        public  async Task<bool> VerificarLogin()
        {
          var auth =   await GetAuthenticationStateAsync();
            if (auth.User.Identity!.IsAuthenticated)
            {
               return  true;
            }
            else
            {
                return false;

            }
        }
    }
}
