
using ACLC.Models;
using ACLC.Services;
using CommunityToolkit.Maui.Views;

namespace ACLC.Views;

public partial class InicioSesion : ContentPage
{

    private ModalCarga _modal = new ModalCarga("espera un momento");
    private Usuario _usuario = new Usuario();
    public InicioSesion()
    {
        InitializeComponent();
    }

    public async void OnButtonClicked(object sender, EventArgs e)
    {
        this.ShowPopup(_modal);
        //Se espera al menos un segundo
        await Task.Delay(1000);

        //Se obtiene el usuario
        IObtenerUsuario obtenerUsuario = new ObtenerUsuarioServicio();
        
        _usuario = await obtenerUsuario.ObtenerUsuario(int.Parse(Boleta.Text), Contra.Text);

        
        _modal.Close();


        //Si el usuario es diferente de null, se muestra la pantalla de inicio

        if (_usuario != null)
        {
            await Navigation.PushAsync(new MainPage(_usuario));
        }
        else
        {
            await DisplayAlert("Error", "Boleta o contraseña incorrecta", "Aceptar");
        }
    }


}

