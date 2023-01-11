using Microsoft.AspNetCore.Mvc;
using ApiWithAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ApiWithAuthentication.Controllers
{
    
    [Authorize(Roles = "administrador")]
    [Route("[controller]")]
    public class userController : Controller
    {

        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public userController(UserManager<UserModel> userManager, 
            SignInManager<UserModel> signInManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CadastrarUsuário([FromQuery] UserRegister usuario)
        {
            if (User.Identity.IsAuthenticated)
            {

                var usuarioBD = new UserModel();
                usuarioBD.UserName = usuario.NomeUsuario;
                usuarioBD.NormalizedUserName = usuario.NomeUsuario.ToUpper().Trim();
                usuarioBD.DataNascimento = usuario.DataNascimento;
                usuarioBD.Email = usuario.Email;
                usuarioBD.Email = usuario.Email.ToUpper().Trim();
                usuarioBD.PhoneNumber = usuario.Telefone;

                if (usuario.Senha == usuario.ConfSenha)
                {
                    var resultado = await _userManager.CreateAsync(usuarioBD, usuario.Senha);

                    Console.WriteLine(resultado);
                    if (resultado.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(resultado);
                    }
                }
                else
                {
                    return Unauthorized("Senha e Confere senha incorretas");
                }
            }
            else
            {
                Console.WriteLine(User.Identity.IsAuthenticated);
                return StatusCode(403);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListaUsuarios()
        {
            return Ok(_userManager.Users.Select(x => $"{x.Id} | {x.UserName}").ToList());

        }

        [HttpPut("addadministrator")]
        public async Task<IActionResult> InserirAdministrador(int id)
        {
            Inicializador.InicializarPerfis(_roleManager);

            UserModel usuario = await _userManager.FindByIdAsync(id.ToString());
            if (usuario == null) {
                return NotFound("Usuario não encontrado");
            }

            var resultado = await _userManager.AddToRoleAsync(usuario, "administrador");
            if (resultado.Succeeded){
                return Ok($"{usuario.NomeCompleto} adicionado com sucesso aos administradores");
            }
            else
            {
                return NotFound(resultado);
            }
        }

        [HttpPut("removeadministrator")]
        public async Task<IActionResult> RemoverAdministrador(int id)
        {
            Inicializador.InicializarPerfis(_roleManager);

            UserModel usuario = await _userManager.FindByIdAsync(id.ToString());
            if (usuario == null)
            {
                return NotFound("Usuario não encontrado");
            }

            if (usuario.UserName != User.Identity.Name)
            {
                var resultado = await _userManager.RemoveFromRoleAsync(usuario, "administrador");
                if (resultado.Succeeded)
                {
                    return Ok($"{usuario.NomeCompleto} removido como sucesso aos administradores");
                }
                else
                {
                    return NotFound(resultado);
                }
            }
            else
            {
                return Unauthorized("Você não pode remover suas permissões como administrador!");
            }
        }
    }
}