using ACLC.Models;
using Newtonsoft.Json;

namespace ACLC.Services;

public class ObtenerUsuarioServicio : IObtenerUsuario
{

    public async Task<Usuario> ObtenerUsuario(int boleta, string contrasenia)
    {
        var client = new HttpClient();

        

        var contraEncriptada = RegistrarUsuarioServicio.Encriptar(contrasenia, "MexEdkirNancyLiz");
        var parametros = "?boleta=" + boleta + "&contrasenia=" + contraEncriptada;

        var direccion = Direccion.direccionLocal + "Usuario/Validar"+ parametros;

        var response = await client.GetAsync(direccion);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            RootUsuario raizUsuario = JsonConvert.DeserializeObject<RootUsuario>(json);

            Usuario usuario = raizUsuario.response;

            return usuario;
        }
        else
        {
            return null;
        }

    }
}