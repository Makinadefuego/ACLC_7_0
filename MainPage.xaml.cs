using ACLC.Models;
using ACLC.Views;

namespace ACLC;

public partial class MainPage : ContentPage
{
    private Usuario _usuario;
    public MainPage()
    {
        InitializeComponent();
    }

    public MainPage(Usuario usuario)
    {
        InitializeComponent();
        _usuario = usuario;


        Iniciar.Text = $"Hola {_usuario.boleta}";
    }

    private async void IrInicioSesion(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioSesion());
    }

    private async void IrLab1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VisualizacionReservas(new Laboratorio() { idLaboratorio = 1 }, _usuario,Navigation));
    }

    private async void IrLab2(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VisualizacionReservas(new Laboratorio() { idLaboratorio = 2 }, _usuario,Navigation));
    }

    private async void IrRegistroQR(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroQR());
    }

    private async void IrMisReservaciones(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MisReservas(_usuario));
    }
}
