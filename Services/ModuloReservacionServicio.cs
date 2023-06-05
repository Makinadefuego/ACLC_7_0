using ACLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Services
{
    public class ModuloReservacionServicio : IReservacionModulo
    {
        public  async Task<bool> ReservarModulo(int usuario, int modulo, DateTime date, int lab)
        {
            bool resultado = false;
            var client = new HttpClient();

            string fechaFinal = date.ToString("yyyy/MM/dd");
            //string fechaFinal = "04/30/2023";

            string parametros = "?usuario="+usuario+ "&modulo="+modulo+"&date="+fechaFinal+"&lab="+lab;


            //string url = Direccion.direccionNancy + "Reservacion/ReservarModulo" + parametros;
            string url = Direccion.direccionLocal + "Reservacion/ReservarModulo" + parametros;
          
            client.BaseAddress = new Uri(url);

            HttpResponseMessage response = await client.PostAsync("", null);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            Console.WriteLine( response);

            return resultado;
        }
    }
}
