using ACLC.Models;
using ACLC.Services;
using CommunityToolkit.Maui.Views;

namespace ACLC.Views;

public partial class EleccionReserva : ContentPage
{
    //Se crea la lista que contiene el id de las computadoras que el usuario selecciono
    private readonly List<int> _computadorasSeleccionadas = new();
    private readonly DateTime _fecha;
    private readonly Laboratorio _laboratorio;
    private readonly int _modulo;

    private List<int> _reservadas;


    private readonly Usuario _usuario;

    //Se crean elementos de la grilla de 9*4
    private readonly Grid _computadoras = new()
    {
        //MinimumWidthRequest = 500,
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.Center,
        Margin = new Thickness(50, 0, 0, 0),
        BackgroundColor = Color.FromHex("#E9beda")
    };

    private readonly DatePicker _datePicker = new();

    private readonly HorizontalStackLayout _horizontalLayout = new();
    private readonly VerticalStackLayout _verticalLayout = new();

    private ModalCarga  modal ;

    public EleccionReserva(Usuario usuario, Laboratorio laboratorio, int modulo, DateTime fecha)
    {
        _usuario = usuario;
        _laboratorio = laboratorio;
        _modulo = modulo;
        _fecha = fecha;
        modal = new ModalCarga("Listando las computadoras disponibles");



        var cadenaFecha = fecha.ToString("dd/MM/yyyy");


        Console.WriteLine(cadenaFecha);


        _datePicker.Date = fecha;


        //HorizontalLayout.Add(datePicker);
        //HorizontalLayout.Add(mod);
        //HorizontalLayout.Add(ReservarTodo);
        _verticalLayout.Add(_horizontalLayout);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        _verticalLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos


        for (var i = 0; i < 9; i++)
            _computadoras.ColumnDefinitions.Add(new ColumnDefinition
                { Width = new GridLength(50, GridUnitType.Absolute) });
        for (var i = 0; i < 4; i++)
            _computadoras.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });

        

        CargarComputadoras();

        

        

        

        //VerticalLayout.Add(computadoras);
        //Se actualiza el contenido de la pagina	

        InitializeComponent();


        Grid.SetColumn(_computadoras, 1);
        Grid.SetRow(_computadoras, 1);


        contenedor.Children.Add(_computadoras);
        labelLab.Text = "Laboratorio: " + laboratorio.idLaboratorio;
        labelDate.Text = "Fecha: " + cadenaFecha;
        labelModulo.Text = "Modulo: " + modulo;

        labelTime.Text = "Horario: " + CalcularHorarioModulo(modulo);

        picker.Date = _fecha;
    }

    public string CalcularHorarioModulo(int modulo)
    {
        if (modulo == 1) return "7:00-7:30";
        if (modulo == 2) return "8:30-10:00";
        if (modulo == 3) return "10:00-11:30";
        if (modulo == 4) return "11:30-13:00";
        if (modulo == 5) return "13:00-14:30";
        if (modulo == 6) return "14:30-16:00";
        if (modulo == 7) return "16:00-17:30";
        if (modulo == 8) return "17:30-19:00";

        return null;
    }

    public void CrearComputadoras()
    {
        bool disponible;
        var sourceG = "pc_green.png";
        var sourceR = "pc_red.png";
#pragma warning disable CS0219 // La variable está asignada pero nunca se usa su valor
        var sourceY = "pc_yellow.png";
#pragma warning restore CS0219 // La variable está asignada pero nunca se usa su valor

        string origen;

        //Se pone en pantalla las resservasdas
        var label = new Label
        {
            Text = "Reservadas: "
        };


        for (var i = 0; i < _reservadas.Count; i++) label.Text += _reservadas[i] + " ";
        _verticalLayout.Children.Add(label);


        //Se crean los image button de cada elemento de la grilla
        var auxId = 1;
        for (var i = 0; i < 4; i++)
        for (var j = 0; j < 9; j++)
            if (!(j == 4 || (j == 2 && i == 0) || (j == 3 && i == 0)))
            {
                if (_reservadas.Contains(auxId))
                {
                    origen = sourceR;
                    disponible = false;
                }
                else
                {
                    origen = sourceG;
                    disponible = true;
                }

                var boton = new ImageButton
                {
                    ClassId = auxId.ToString(),
                    Source = origen,
                    Aspect = Aspect.AspectFit,
                    IsEnabled = disponible
                };
                boton.Clicked += Computadora_OnClick;
                Grid.SetRow(boton, i);
                Grid.SetColumn(boton, j);
                _computadoras.Children.Add(boton);
                auxId++;
            }


        modal.Close();
        //Content = VerticalLayout;
    }

    public async void ReservarModulo(object sender, EventArgs e)
    {
        IReservacionModulo reservarModulo = new ModuloReservacionServicio();

        bool reservacionExitosa = false;

        if (_usuario != null) 
            if (_usuario.boleta != 0 && _usuario.password != null) {
            
                reservacionExitosa = await reservarModulo.ReservarModulo(_usuario.boleta, _modulo, _fecha, _laboratorio.idLaboratorio);
            }

        var label = new Label
        {
            Text = ""
        };
        if (reservacionExitosa)
        {
            var informacion = "Reservacion hecha con exito: \n" +
                              "Laboratorio: " + _laboratorio.idLaboratorio + "\n" +
                              "Modulo: " + _modulo + "\n" +
                              "Fecha: " + _fecha + "\n" +
                              "Hora: " + CalcularHorarioModulo(_modulo) + "\n" +
                              "Usuario: " + _usuario.boleta + "\n";
            label.Text = "";
            label.Text += informacion;
            label.Margin = new Thickness(20);
        }

        _verticalLayout.Children.Add(label);

        Content = _verticalLayout;
    }

    public async void CargarComputadoras()
    {

        try
        {

            //Se crea la ventana modal de carga
            this.ShowPopup(modal);
            await Task.Delay(TimeSpan.FromSeconds(2));
            IObtenerModulo obtenerModulo = new ObtenerModuloServicio();

            var computadoras = await obtenerModulo.ObtenerModulo(_modulo, _laboratorio.idLaboratorio, _fecha);

            if (computadoras != null)
            {
                _reservadas = new List<int>();
                //Se crea una lista de IDs "ya reservados"

                foreach (var t in computadoras)
                {
                    var x = t.idComputadora;
                    _reservadas.Add(x);
                }

                labelModulo.Text += "    Hay " + _reservadas.Count + " computadoras reservadas";
            }


            if (_reservadas.Count == 0)
                ReservarTodo.Clicked += ReservarModulo;
            else
                ReservarTodo.Clicked += ReservarTodo_Clicked;

            if ( _reservadas.Count <= 30) btnReservarSeleccionadas.Clicked += ReservarSeleccionadas;


            CrearComputadoras();

            

        }
        catch (Exception e)
        {
            //Se regresa a la pantalla principal
            await DisplayAlert("Error de conexión", "Ha ocurrido un error al obtener la disponibilidad de las computadoras," +
                                                    " verifica la conexión a internet ", "Entendido!");
            modal.Close();
            await Navigation.PopAsync();
        }
        //Content = VerticalLayout;

        
    }


    public void Computadora_OnClick(object sender, EventArgs e)
    {
        var boton = (ImageButton)sender;

        var origen = boton.Source.ToString();

        if (origen != null && origen.Contains("pc_green.png"))
        {
            boton.Source = "pc_yellow.png";
            _computadorasSeleccionadas.Add(int.Parse(boton.ClassId));
        }
        else
        {
            boton.Source = "pc_green.png";
            _computadorasSeleccionadas.Remove(int.Parse(boton.ClassId));
        }
    }

    public async void ReservarSeleccionadas(object sender, EventArgs e)
    {
        if (_computadorasSeleccionadas.Count == 0)
        {
            await DisplayAlert("Error en la reservar",
                "Por favor asegurate de haber seleccionado al menos una computadora", "Entendido!");
            return;
        }

        IReservarComputadoras reservarComputadora = new ComputadorasReservacionServicio();


        bool reservacionExitosa = false;
        if (_usuario != null )
            if (_usuario.boleta != 0 && _usuario.password != null)
            {
                reservacionExitosa = await reservarComputadora.ReservarComputadora(_computadorasSeleccionadas, _fecha,
                    _usuario, _modulo, _laboratorio.idLaboratorio);
            }


        var label = new Label
        {
            Text = ""
        };
        if (reservacionExitosa)
        {
            var informacion = "Reservacion hecha con exito: \n" +
                              "Laboratorio: " + _laboratorio.idLaboratorio + "\n" +
                              "Modulo: " + _modulo + "\n" +
                              "Fecha: " + _fecha + "\n" +
                              "Hora: " + CalcularHorarioModulo(_modulo) + "\n" +
                              "Usuario: " + _usuario.boleta + "\n";
            label.Text = "";
            label.Text += informacion;
            label.Margin = new Thickness(20);
        }

        _verticalLayout.Children.Add(label);

        Content = _verticalLayout;
    }

    private async void ReservarTodo_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Reservación fallida",
            "El laboratorio no está completamente disponible por lo que no es posible reservarlo todo", "Entendido!");
    }

    
}