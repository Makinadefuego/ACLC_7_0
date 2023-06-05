using ACLC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Services
{
    public class ObtenerReservacionesSemanaServicio : IObtenerReservacionesSemana
    {
        public async Task<List<Reservacion>> ObtenerReservacionesSemana(int lab, DateTime fecha)
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            var client = new HttpClient();

            string fechaFinal = fecha.ToString("yyyy-MM-dd");
            //string fechaFinal = "04/30/2023";

            string parametros = "?dateTime=" + fechaFinal + "&labo=" + lab;

            //string url = Direccion.direccionNancy + "Reservacion/ReservacionesSemana" + parametros;
            string url = Direccion.direccionLocal + "Reservacion/ReservacionesSemana" + parametros;

            client.BaseAddress = new Uri(url);

            HttpResponseMessage response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                //se crea un arreglo de tipo computadora

                var result = await response.Content.ReadAsStringAsync();

                RootReservaciones root = JsonConvert.DeserializeObject<RootReservaciones>(result);

                reservaciones = root.response;



                Console.WriteLine("Reservaciones: " + reservaciones.Count);


            }
            else
            {
                reservaciones = null;
            }


            return reservaciones;
        }
    }
}
