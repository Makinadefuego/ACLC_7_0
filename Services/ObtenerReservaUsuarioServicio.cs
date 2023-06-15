using ACLC.Models;
using Newtonsoft.Json;

namespace ACLC.Services;

public class ObtenerReservaUsuarioServicio : IObtenerReservasUsuario
{

    public async Task<List<Reservacion>> ObtenerReservasUsuario(int boleta)
    {
        try
        {
            var root = new RootReservaciones();
            var parametros = "?boleta=" + boleta;
            var url = Direccion.direccionLocal + "Reservacion/ObtenerReservacionesUsuario" + parametros;
            var httpClient = new HttpClient();

            //Se hace la petición
            var response = await httpClient.GetAsync(url);


            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                root = JsonConvert.DeserializeObject<RootReservaciones>(json);

                return root.response;
            }

            return null;


        }
        catch (Exception e)
        {
            return null;
        }


        
    }
    
}