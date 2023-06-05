using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace ACLC.Services
{
    public class ObtenerHtmlDatos : IObtenerHtmlDatos
    {
        public async Task<string> ObtenerHtml(string url)
        {
            HtmlWeb oWeb = new HtmlWeb();
            HtmlDocument doc = await oWeb.LoadFromWebAsync(url);

            HtmlNode html = doc.DocumentNode.CssSelect("html").First();
            
            string sHtml = html.InnerHtml;

            return sHtml;
        }

        public List<string> ObtenerDato(string html, string clase)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var contenerdor = doc.DocumentNode.CssSelect(clase).First();

            //Se obtienen los hijos del contenedor
            var hijos = contenerdor.ChildNodes;


            return hijos.Select(nodo => nodo.InnerHtml).ToList();

        }
    }
}
