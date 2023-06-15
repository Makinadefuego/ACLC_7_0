using ACLC.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACLC.Services
{
    public class ValidarBoletaServicio : IValidarBoleta
    {
        public async Task<bool> ValidarBoleta(int boleta)
        {
            bool resultado = false;

            string parametros = "?boleta=" + boleta;
            string url = Direccion.direccionLocal + "Boleta/ObtenerBoletaValida" + parametros;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    resultado = true;
                }
            }

            return resultado;
        }
    }
}