using System.Reflection;

namespace EntregaFinal.Repository
{
    public class NombreHandler
    {
        // metodo para regresar el nombre de la applicacion
        public static string display_nombreAPP()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            string nombre_app = domain.FriendlyName;
            return nombre_app;
                    
        }



    }
}
