using Newtonsoft.Json;
using ACLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACLC.Services
{
    public class ObtenerModuloServicio : IObtenerModulo
    {
        public async Task<List<Computadora>> ObtenerModulo(int modulo, int lab, DateTime fechaInicio)
        {
           List<Computadora> computadoras = new List<Computadora>();
           var client = new HttpClient();

            string fechaFinal = fechaInicio.ToString("yyyy-MM-dd");
            //string fechaFinal = "04/30/2023";

            string parametros = "?dateTime="+ fechaFinal + "&modulo=" + modulo + "&lab=" + lab;

        //string url = Direccion.direccionNancy + "Reservacion/ReservarModulo" + parametros;
        
            string url = Direccion.direccionLocal + "Reservacion/ObtenerComputadorasReservadasModulo" + parametros;

            client.BaseAddress = new Uri(url);

            HttpResponseMessage response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                //se crea un arreglo de tipo computadora
               
                var result = await response.Content.ReadAsStringAsync();

                RootComputadoras root = JsonConvert.DeserializeObject<RootComputadoras>(result);

                computadoras = root.response;

            

                Console.WriteLine("Computadoras: " + computadoras.Count);

                
            }
            else
            {
                computadoras = null;
            }

            
            return computadoras;
          

        }
    }
}
