using ACLC.Services;
using Camera.MAUI.ZXingHelper;

namespace ACLC.Views;

public partial class RegistroQR : ContentPage
{
    private  IObtenerHtmlDatos _obtenerHtmlDatos;
    public RegistroQR()
    {
        InitializeComponent();
        CameraView.BarCodeOptions = new()
        {
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE }
        };
    }

    private void CameraView_OnCamerasLoaded(object sender, EventArgs e)
    {
        if (CameraView.Cameras.Count > 0)
        {
            CameraView.Camera = CameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await CameraView.StopCameraAsync();
                await CameraView.StartCameraAsync();
            });

        }
    }

    private void CameraView_OnBarcodeDetected(object sender, BarcodeEventArgs args)
    {
       MainThread.BeginInvokeOnMainThread(async () =>
       {
           await CameraView.StopCameraAsync();
           info.Text = $"{args.Result[0].BarcodeFormat} : {args.Result[0].Text}";

           var url = args.Result[0].Text;
           url= url.Replace("www", "servicios");
           _obtenerHtmlDatos = new ObtenerHtmlDatos();

           var html = await _obtenerHtmlDatos.ObtenerHtml(url);

           var datosAlumno = _obtenerHtmlDatos.ObtenerDato(html, "#wrapper");

           //await DisplayAlert("Datos", $"Nombre: {nombre}\nBoleta: {boleta}\nEscuela: {escuela}\nVigencia: {vigencia}", "Ok");
           await DisplayAlert("Datos", $"Nombre: {datosAlumno[3]}\nBoleta: {datosAlumno[4]}\nEscuela: {datosAlumno[6]}\nVigencia: {datosAlumno[7]}", "Ok");

           

       });
    }
}