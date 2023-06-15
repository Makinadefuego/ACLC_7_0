using ACLC.Models;
using Newtonsoft.Json;

namespace ACLC.Services;

public class CancelarReservaServicio : ICancelarReserva
{
    public async Task<bool> CancelarReserva(int idReserva)
    {
        var client = new HttpClient();

        var parametros = "?id=" + idReserva;

        var direccion = Direccion.direccionLocal + "Reservacion/CancelarReservacion" + parametros;

        var response = await client.DeleteAsync(direccion);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}