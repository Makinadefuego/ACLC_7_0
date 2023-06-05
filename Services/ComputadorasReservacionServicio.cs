using ACLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Services
{
    public class ComputadorasReservacionServicio : IReservarComputadoras
    {
        public async Task<bool> ReservarComputadora(List<int> computadoras, DateTime fecha, Usuario usuario, int modulo, int lab)
        {
            

            //Se crea la cadena de enteros que conforman las computdoras seleccionadas
            StringBuilder cadena = new StringBuilder();
            cadena.Append("[");
            foreach (int i in computadoras)
            {
                cadena.Append(i);
                cadena.Append(",");
            }
            cadena.Remove(cadena.Length - 1, 1);
            cadena.Append("]");

            StringContent contenido = new StringContent(cadena.ToString(), Encoding.UTF8, "application/json");
            bool resultado = false;
            var client = new HttpClient();
            string fechaFinal = fecha.ToString("yyyy/MM/dd");
            //string fechaFinal = "04/30/2023";
            string parametros = "?boleta=" + usuario.boleta + "&fecha=" + fechaFinal + "&modulo=" + modulo + "&lab=" +lab;
            //string url = Direccion.direccionNancy + "Reservacion/ReservarLista" + parametros;
            string url = Direccion.direccionLocal + "Reservacion/ReservarLista" + parametros;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync(url, contenido);
            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }
            Console.WriteLine(response);
            return resultado;
        }
    }

}
