using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACLC.Models;
using ACLC.Services;
using CommunityToolkit.Maui.Views;

namespace ACLC.Views;

public partial class ValidacionBoleta : ContentPage
{
    private bool valida = false;
    private int boleta;
    private string cadenaVigencia;
    private string escuela;
    private ModalCarga modal = new ModalCarga("espera un momento");

    public ValidacionBoleta(int boleta, string cadenaVigencia, string escuela, string mensajeBoton)
    {
        this.boleta = boleta;


        this.cadenaVigencia = cadenaVigencia;
        this.escuela = escuela;


        
        InitializeComponent();
        Accion.Text = mensajeBoton;


        if(this.boleta != 0)
            Boleta.Text = boleta.ToString();


    }


    private async Task ValidarDatos()
    {

        this.ShowPopup(modal);
        //Se espera al menos un segundo
        await  Task.Delay(1000);
        /*Se valida la boleta introducida*/
        if (Boleta.Text != null)
            this.boleta = int.Parse(Boleta.Text);
        else
            this.boleta = 0;


        var cadenaUpiit = "UPIIT";

        var enBD = await estaEnBase();
        



        if ((escuela == cadenaUpiit && isVigente(cadenaVigencia) || enBD))
        {

            valida = true;
        }


        //Se cierra el pop de carga
        CambiarInterfaz();
        
    }
    private async void Validar_onClick(object sender, EventArgs e)
    {
        


        await ValidarDatos();
        


    }

    public async Task<bool> estaEnBase()
    {
        IValidarBoleta validarBoleta = new ValidarBoletaServicio();

        

        var resultado = await validarBoleta.ValidarBoleta( this.boleta);
        




        //Se verifica que la boleta este en la base de datos
        return resultado;

    }

    private bool isVigente(string vigencia)
    {
        //Se obtiene el primer mes del semenstre
        //Ejemplo de cadenaVigencia: "Vigencia de enero a agosto de 2023"

        var fechaActual = DateTime.Now;
        var mesActual = fechaActual.Month;
        var anioActual = fechaActual.Year;

        //El mes de inicio de semenstre puede ser enero o agosto
        var mesInicioSemestre = 0;

        if (cadenaVigencia.Contains("enero"))
        {
            mesInicioSemestre = 1;
        }
        else if (cadenaVigencia.Contains("agosto"))
        {
            mesInicioSemestre = 8;
        }

        var mesFinSemestre = mesInicioSemestre + 5;

        //Se obtiene el año de la credencial
        string anioCredencial;
        anioCredencial = cadenaVigencia.Substring(cadenaVigencia.Length - 4);
        //Se convierte a entero
        int anioCredencialInt = Convert.ToInt32(anioCredencial);
        
        

        //Se verifica que el mes actual este en el perdiodo de vigencia

        //Se verifica que la cadena tenga la forma de cuando es vigente
        if (cadenaVigencia.Contains("Vigencia de"))
        {
            if (mesActual >= mesInicioSemestre && mesActual <= mesFinSemestre && anioCredencialInt == anioActual)
            {
                return true;
            }
        }

        return false;
    }

    private async void GuardarContrasenia_onClick(object sender, EventArgs e)
    {

        var popup = new ModalCarga("Guardando contraseña");

        try
        {


            this.ShowPopup(popup);
            //Se espera al menos un segundo
            await Task.Delay(10000);
            /*Se guarda la contraseña en la base de datos*/

            //Se obtiene la contraseña


            var contrasenia = Contrasenia.Text;

            if (contrasenia == null)
            {
                
                return;
            }

            //Se guarda la contraseña en la base de datos
            IRegistrarUsuario registrarUsuario = new RegistrarUsuarioServicio();
            var usuario = new Usuario()
            {
                boleta = this.boleta,
                password = contrasenia
            };

            var resultado = await registrarUsuario.RegistrarUsuario(usuario);

            //Se cierra el pop de carga
            await Task.Delay(1000);


            if (resultado)
            {
                //Se muestra un mensaje de que se guardo la contraseña
                await DisplayAlert("Contraseña guardada", "Se ha guardado tu contraseña", "Aceptar");
                //Se redirige a la pagina de inicio
                await Navigation.PushAsync(new MainPage());

                
            }


            

        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }

        popup.Close();

    }

    private void CambiarInterfaz()
    {
        if (valida)
        {
            Boleta.IsEnabled = false;
            Contrasenia.IsEnabled = true;
            BoletaValida.Text = "Boleta válida, puedes continuar";
            Accion.Text = "Guardar contraseña";
            Accion.Clicked -= Validar_onClick;
            Accion.Clicked += GuardarContrasenia_onClick;
        }
        else
        {
            Contrasenia.IsEnabled = false;
            Boleta.IsEnabled = true;
            BoletaValida.Text = "Ingresa una boleta válida";
        }

        modal.Close();
    }
}

