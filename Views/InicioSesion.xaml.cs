
namespace ACLC.Views;

public partial class InicioSesion : ContentPage
{


    public InicioSesion()
    {
        InitializeComponent();
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }


}

