using Microsoft.AspNetCore.Mvc;
using EntregaFinal.Controllers.DTOS;
using EntregaFinal.Model;
using EntregaFinal.Repository;

namespace EntregaFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "ConseguirUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
        }

        [HttpDelete]
        public bool EliminarUsuario([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            try
            { 
                return UsuarioHandler.ModificarNombreDeUsuario(new Usuario
                {
                    Id = usuario.Id,
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario
                });


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }  
        }

        [HttpPost]
        public bool CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CrearUsuario(new Usuario
                {
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
