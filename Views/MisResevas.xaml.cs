using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ACLC.Models;
using ACLC.Services;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;


namespace ACLC.Views
{
    public partial class MisReservas : ContentPage
    {
        private List<Reservacion> _reservaciones = new List<Reservacion>();
        private ModalCarga _modal = new ModalCarga("Cargando tus reservaciones");
        public ObservableCollection<Reservacion> Reservaciones { get; set; }
        public Command<Reservacion> EliminarReservacionCommand { get; }

        public MisReservas(Usuario usuario)
        {
            InitializeComponent();
            BindingContext = this;

            EliminarReservacionCommand = new Command<Reservacion>(EliminarReservacion);

            if (usuario != null)
            {
                CargarReservaciones(usuario.boleta);
            }
            
        }

        public async Task CargarReservaciones(int boleta)
        {
            this.ShowPopup(_modal);
            await Task.Delay(1000);
            IObtenerReservasUsuario servicio = new ObtenerReservaUsuarioServicio();
            _reservaciones = await servicio.ObtenerReservasUsuario(boleta);

            await Task.Delay(1000);
            _modal.Close();

            Reservaciones = new ObservableCollection<Reservacion>(_reservaciones);
            OnPropertyChanged(nameof(Reservaciones));
        }

        private async void EliminarReservacion(Reservacion reservacion)
        {
            // Aquí debes implementar la lógica para eliminar la reservación
            // Puedes usar reservacion.id para identificar la reservación específica a eliminar
            // Por ejemplo:
            // await servicio.EliminarReservacion(reservacion.id);

            ICancelarReserva servicio = new CancelarReservaServicio();

            var resultado = await servicio.CancelarReserva(reservacion.id);

            if (!resultado)
            {
                await DisplayAlert("Error", "No se pudo eliminar la reservación", "Aceptar");
                return;
            }

            // Actualizar la lista de reservaciones
            _reservaciones.Remove(reservacion);
            Reservaciones = new ObservableCollection<Reservacion>(_reservaciones);
            OnPropertyChanged(nameof(Reservaciones));

            await DisplayAlert("Reservación eliminada", "La reservación ha sido eliminada correctamente.", "Aceptar");
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            EliminarReservacion((Reservacion)((Button)sender).BindingContext);
        }
    }
}
