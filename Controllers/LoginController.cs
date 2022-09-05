using Microsoft.AspNetCore.Mvc;
using EntregaFinal.Model;
using EntregaFinal.Repository;
using EntregaFinal.Controllers.DTOS;


namespace EntregaFinal.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class LoginController : ControllerBase
    {
        [HttpGet(Name = "GetLogin")]

        public string Login([FromBody] GetLogin usuario)
        {

            // call UserLogin metod from  UsuariosHandler
            bool passwordValidation = UsuarioHandler.LoginUser(usuario.NombreUsuario, usuario.Contraseña);
            if (passwordValidation)
            {
                return "Login Succesful";
            }
            else { return "Login Fail"; }

        }

    }
}
