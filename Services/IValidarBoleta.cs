namespace ACLC.Services;

public interface IValidarBoleta
{
    Task<bool> ValidarBoleta(int boleta);
}