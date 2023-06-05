using ACLC.Models;
using ACLC.Views;

namespace ACLC;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void IrInicioSesion(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioSesion());
    }

    private async void IrLab1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VisualizacionReservas(new Laboratorio() { idLaboratorio = 1 }, Navigation));
    }

    private async void IrLab2(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VisualizacionReservas(new Laboratorio() { idLaboratorio = 2 }, Navigation));
    }

    private async void IrRegistroQR(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroQR());
    }
}
