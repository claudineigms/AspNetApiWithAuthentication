using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWithAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class applicationController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public applicationController(UserManager<UserModel> userManager, 
            RoleManager<IdentityRole<int>> roleManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;;
        }

        [HttpGet("update")]
        public async void UpdateServer(){
            Inicializador.InicializarIdentity(_roleManager, _userManager);
        }
    }
}