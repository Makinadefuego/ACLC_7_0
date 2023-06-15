using ACLC.Models;

namespace ACLC.Services;

public interface IObtenerUsuario
{
    public Task<Usuario> ObtenerUsuario(int boleta, string contrasenia);
}