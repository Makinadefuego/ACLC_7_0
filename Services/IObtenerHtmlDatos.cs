namespace ACLC.Services
{
    public interface IObtenerHtmlDatos
    {
        Task<string> ObtenerHtml(string url);

        List<string> ObtenerDato(string html, string clase);
    }
}