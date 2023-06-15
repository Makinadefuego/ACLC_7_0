using ACLC.Models;

namespace ACLC.Services;

public interface IObtenerReservasUsuario
{
    public  Task<List<Reservacion>> ObtenerReservasUsuario(int boleta);
}