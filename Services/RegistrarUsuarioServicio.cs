using System.Security.Cryptography;
using System.Text;
using ACLC.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ACLC.Services;

public class RegistrarUsuarioServicio : IRegistrarUsuario
{
    public async Task<bool> RegistrarUsuario(Usuario usuario)
    {

        try
        {
            var client = new HttpClient();
            string url = Direccion.direccionLocal + "Usuario/Registrar";
            client.BaseAddress = new Uri(url);

            //Se codifica la contraseña del usuario antes de meterla en la base de datos 
            var contrasena = usuario.password;

            //Se encripta la contraseña

            contrasena = Encriptar(contrasena, "MexEdkirNancyLiz");


            //Se crea el objeto que se va a enviar al servidor
            Usuario usuarioNuevo = new Usuario
            {
                boleta = usuario.boleta,
                password = contrasena
            };

            //Se convierte el objeto a formato JSON
            string json = JsonConvert.SerializeObject(usuarioNuevo);

            //Se crea el contenido que se va a enviar al servidor
            StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

            //Se envia la peticion al servidor

            HttpResponseMessage response = await client.PostAsync(url, contenido);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        




    }

    public static string Encriptar(string contrasena, string llaveArbitraria)
    {
        byte[] llave = Encoding.UTF8.GetBytes(llaveArbitraria.Substring(0, 16));
        byte[] contrasenaBytes = Encoding.UTF8.GetBytes(contrasena);

        using (Aes aes = Aes.Create())
        {
            aes.Key = llave;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.IV = new byte[16]; // IV en blanco para que sea determinístico

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(contrasenaBytes, 0, contrasenaBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] contrasenaEncriptadaBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(contrasenaEncriptadaBytes);
                }
            }
        }
    }


}