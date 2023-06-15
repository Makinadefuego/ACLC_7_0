namespace ACLC.Services;

public interface ICancelarReserva
{
    public Task<bool> CancelarReserva(int idReserva);
}