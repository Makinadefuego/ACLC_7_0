using ACLC.Models;

namespace ACLC.Services;

public interface IRegistrarUsuario
{
    public Task<bool> RegistrarUsuario(Usuario usuario);
}