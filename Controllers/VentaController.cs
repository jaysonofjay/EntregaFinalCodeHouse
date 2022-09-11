using Microsoft.AspNetCore.Mvc;
using EntregaFinal.Controllers.DTOS;
using EntregaFinal.Model;
using EntregaFinal.Repository;

namespace EntregaFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VentaController : Controller
    {

        [HttpGet(Name = "GetVenta")]
        public List<Venta> GetVenta()
        {
            return VentaHandler.GetVentas();
        }


        // NOTA: ***Funcion CrearVenta modificada a base de comentario de Facu el 9/10/2022  ver la parte inferior para respuesta. ** 

        //[HttpPost]
        //public bool CrearVenta([FromBody] PostProductoVendido productovendido)
        //{
        //    /* Cargar Venta: Recibe una lista de productos y el número de IdUsuario de quien la efectuó, 
        //        problema : se me complico esta parte de ingresar varios productos en una lista para cargar la venta 
        //        resolucion:  el Post de venta solo carga un articulo, pero registra venta nueva en ventas y productos vendidos asi como descontar el stock en productos.*/
        //    int nuevoIDVenta = 0;
        //    bool productoAgregado = false;
        //    bool stockModificado = false;
        //    try
        //    {
        //        // primero cargar una nueva Venta en la base de datos, 
        //        nuevoIDVenta = VentaHandler.CrearVenta();
        //        //  cargar los productos recibidos en la base de ProductosVendidos
        //        productoAgregado = ProductosVendidosHandler.AgregarProductoVendido(new ProductoVendido
        //        {
        //            Stock = productovendido.Stock,
        //            IdProducto = productovendido.IdProducto,
        //            IdVenta = nuevoIDVenta
        //        });
        //        //  descontar el stock en la base de productos por el otro
        //        stockModificado = ProductoHandler.ModificarStockdeProducto(new Producto 
        //        {
        //            Stock = productovendido.Stock, 
        //            Id = productovendido.IdProducto 
        //        }  , nuevoIDVenta);
        //        if (productoAgregado && stockModificado) { return true; } else { return false; }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}


        // NOTA  >  CAMBIO REALIZADO 9/10/2022  RESPUESTA DE COMENTARIO DE FACU. 
        /* (Punto de desafio Cargar Venta): Recibe una lista de productos y el número de IdUsuario de quien la efectuó, 
            problema : se me complico esta parte de ingresar varios productos en una lista para cargar la venta 
            resolucion:  Aggregar List<Class>  para poder recibir una collection de productos vendidos e iterar por cada uno de ellos para poder subtraer la cantidad*/


        [HttpPost]
        public bool CrearVenta2([FromBody] List<PostProductoVendido> productovendidos)
        {
            int nuevoIDVenta = 0;
      
            //primero cargar una nueva Venta en la base de datos,
            nuevoIDVenta = VentaHandler.CrearVenta();

            try
            {
                int conteoAgregados = 0;
                foreach (PostProductoVendido productovendido in productovendidos)
                {
                   
                    bool productoAgregado = false;
                    bool stockModificado = false;

                    //  cargar los productos recibidos en la base de ProductosVendidos
                    productoAgregado = ProductosVendidosHandler.AgregarProductoVendido(new ProductoVendido
                    {
                        Stock = productovendido.Stock,
                        IdProducto = productovendido.IdProducto,
                        IdVenta = nuevoIDVenta

                    });

                    //  descontar el stock en la base de productos por el otro
                    stockModificado = ProductoHandler.ModificarStockdeProducto(new Producto
                    {
                        Stock = productovendido.Stock,
                        Id = productovendido.IdProducto
                    });

                    // contar cada producto que es agregado y descontado del stock
                    if (productoAgregado && stockModificado) { conteoAgregados++;} 
                }

                /* si no todos los productos agregados son igual a la cantidad de productions en la coleccion ingresada, entonces se genero un error dentro del foreach 
                        por ejemplo un id de producto que no existe */
                if (conteoAgregados == productovendidos.Count) { return true; } else { return false; }


            }
            catch (Exception ex)
            {
                    Console.WriteLine(ex.Message);
                    return false;
            
            }


        }



        // Eliminar venta
        [HttpDelete]
        public bool BorrarVenta([FromBody] int id)
        {

            bool borrar = VentaHandler.BorrarUnVenta(id);
            return borrar;

        }




    }
}
