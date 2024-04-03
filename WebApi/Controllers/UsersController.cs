using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApi.Models;
using WebApi.Token;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> applicationUser, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = applicationUser;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email)|| string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }
            
            var result = await _signInManager.PasswordSignInAsync(login.email, login.senha, false, false);
            
            if (result.Succeeded) 
            {
                var userCurrent = await _userManager.FindByEmailAsync(login.email);
                var IdUser = userCurrent.Id;

                var token = new TokenJwtBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678!@@@-453DEFSE77-8FGGHD"))
                    .AddSubject("Empresa")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("IdUser", IdUser)
                    .AddExpiry(10)
                    .Builder();

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
          
        }

        [AllowAnonymous]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return NotFound("Falta alguns dados");

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                CPF = login.cpf,
                Tipo = Entities.Enums.TipoUsuario.Comum,
            };
            var result = await _userManager.CreateAsync(user, login.senha);
            if (result.Errors.Any())
            {
                return Ok(result.Errors);   
            }

            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar usuários");
        }
    }
}
