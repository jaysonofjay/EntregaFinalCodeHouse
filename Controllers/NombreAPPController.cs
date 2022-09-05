using Microsoft.AspNetCore.Mvc;
using EntregaFinal.Model;
using EntregaFinal.Repository;
using EntregaFinal.Controllers.DTOS;


namespace EntregaFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class NombreAppController : ControllerBase
    {
        [HttpGet(Name = "GetNombre")]

        public string GetNombreApp()
        {
            return NombreHandler.display_nombreAPP();
        }
    }
}
