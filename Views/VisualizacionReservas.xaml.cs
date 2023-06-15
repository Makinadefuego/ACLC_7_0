using System.Globalization;
using ACLC.Models;
using ACLC.Services;
using CommunityToolkit.Maui.Views;

namespace ACLC.Views;

public partial class VisualizacionReservas : ContentPage
{
    private readonly INavigation _navigation;
    private readonly Laboratorio _labseleccionado;
    private readonly List<DatosBoton> _listaBotones = new();
    private List<Reservacion> _reservacionesSemana = new();
    private Usuario _usuario;


    public VisualizacionReservas(Laboratorio laboratorio, Usuario usuario, INavigation navigation)
    {
        _usuario = usuario;
        _navigation = navigation;
        const int desfase = 0;
        _labseleccionado = laboratorio;
        var fechaActual = DateTime.Now.AddDays(desfase);

        const string bgColor = "#82014A";
        const string rosaClaro = "#E9beda";
        const string rosaOscuro = "#cf0276";
        const string guindaClaro = "#82014A";
        const string guidaOscuro = "#661943";


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var scrollView = new ScrollView
        {
            BackgroundColor = Color.FromHex(bgColor)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

        var verticalPrincipal = new VerticalStackLayout();

        var horizontalPrincipal = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center
        };


        var trianguloI = new ImageButton
        {
            Source = "triangulo.png",
            MaximumHeightRequest = 50,
            MaximumWidthRequest = 50,
            BackgroundColor = Colors.Transparent,
            Rotation = -90
        };

        var trianguloD = new ImageButton
        {
            Source = "triangulo.png",
            MaximumHeightRequest = 50,
            MaximumWidthRequest = 50,
            BackgroundColor = Colors.Transparent,
            Rotation = 90
        };


        //Se crea el primer grid 3 columnas y 1 fila
        var etiquetas = new Grid
        {
            BackgroundColor = Colors.White,
            MaximumHeightRequest = 50
        };

        //Se crean las defeniciones del columnas y filas para ese grid
        for (var i = 0; i < 2; i++)
            etiquetas.ColumnDefinitions.Add(new ColumnDefinition
                { Width = new GridLength(100, GridUnitType.Absolute) });
        etiquetas.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Absolute) });
        etiquetas.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });

        //Se crean los elementos que van dentreo del gri etiquetas
        var etiqueta1 = new Label
        {
            Text = "Reservaciones",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        Grid.SetColumn(etiqueta1, 0);
        Grid.SetRow(etiqueta1, 0);

        var etiqueta2 = new Label
        {
            Text = "LAB " + laboratorio.idLaboratorio,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        Grid.SetColumn(etiqueta2, 1);
        Grid.SetRow(etiqueta2, 0);


        var cultura = new CultureInfo("es-ES");
        var listaFechas = CalcularInicioSemana(fechaActual);
        var fechaInicio = listaFechas[0];
        var fechaFin = listaFechas[1];
        var fechaEtiqueta3  = "Semana de " + (fechaInicio.Day + 1) + " de " + cultura.DateTimeFormat.GetMonthName(fechaInicio.Month) + " a " +
            (fechaFin.Day + 1) + " de " + cultura.DateTimeFormat.GetMonthName(fechaFin.Month);
        var etiqueta3 = new Label
        {
            Text = fechaEtiqueta3,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        Grid.SetColumn(etiqueta3, 2);
        Grid.SetRow(etiqueta3, 0);


        etiquetas.Children.Add(etiqueta1);
        etiquetas.Children.Add(etiqueta2);
        etiquetas.Children.Add(etiqueta3);

        //Se hace el grid que contiene los botones
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var horario = new Grid
        {
            MinimumWidthRequest = 750,
            Margin = new Thickness(0, 20, 0, 0),
            BackgroundColor = Color.FromHex("#C40093"),
            HorizontalOptions = LayoutOptions.Center
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

        //Se crea las definiciones de fila y columna de de la grilla
        for (var i = 0; i < 6; i++)
            horario.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(125, GridUnitType.Absolute) });
        for (var i = 0; i < 9; i++)
            horario.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });

        //Primero se crean los elementos que no son dinámicos, los horarios de la parte izquierda y los días de la semana que van en la parte de arriba
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var labelModulos = new Label
        {
            Text = "Módulos",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guindaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(labelModulos, 0);
        Grid.SetColumn(labelModulos, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var lunes = new Label
        {
            Text = "Lunes",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(lunes, 0);
        Grid.SetColumn(lunes, 1);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var martes = new Label
        {
            Text = "Martes",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(martes, 0);
        Grid.SetColumn(martes, 2);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var miercoles = new Label
        {
            Text = "Miercoles",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(miercoles, 0);
        Grid.SetColumn(miercoles, 3);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var jueves = new Label
        {
            Text = "Jueves",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(jueves, 0);
        Grid.SetColumn(jueves, 4);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var viernes = new Label
        {
            Text = "Viernes",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(guidaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(viernes, 0);
        Grid.SetColumn(viernes, 5);


        //Se crean las etiquetas de los módulos
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo1 = new Label
        {
            Text = "7:00-8:30",
            TextColor = Colors.Black,
            Padding = new Thickness(30, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo1, 1);
        Grid.SetColumn(modulo1, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo2 = new Label
        {
            Text = "8:30-10:00",
            TextColor = Colors.White,
            Padding = new Thickness(25, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo2, 2);
        Grid.SetColumn(modulo2, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo3 = new Label
        {
            Text = "10:00-11:30",
            TextColor = Colors.Black,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo3, 3);
        Grid.SetColumn(modulo3, 0);


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo4 = new Label
        {
            Text = "11:30-13:00",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo4, 4);
        Grid.SetColumn(modulo4, 0);


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo5 = new Label
        {
            Text = "13:00-14:30",
            TextColor = Colors.Black,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo5, 5);
        Grid.SetColumn(modulo5, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo6 = new Label
        {
            Text = "14:30-16:00",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo6, 6);
        Grid.SetColumn(modulo6, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo7 = new Label
        {
            Text = "16:00-17:30",
            TextColor = Colors.Black,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaClaro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo7, 7);
        Grid.SetColumn(modulo7, 0);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        var modulo8 = new Label
        {
            Text = "17:30-19:00",
            TextColor = Colors.White,
            Padding = new Thickness(20, 10, 0, 0),
            BackgroundColor = Color.FromHex(rosaOscuro)
        };
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Grid.SetRow(modulo8, 8);
        Grid.SetColumn(modulo8, 0);


        //Se insertan todas las etiquetas estáticas en el grid llamado horario
        horario.Children.Add(modulo1);
        horario.Children.Add(modulo2);
        horario.Children.Add(modulo3);
        horario.Children.Add(modulo4);
        horario.Children.Add(modulo5);
        horario.Children.Add(modulo6);
        horario.Children.Add(modulo7);
        horario.Children.Add(modulo8);
        horario.Children.Add(labelModulos);
        horario.Children.Add(lunes);
        horario.Children.Add(martes);
        horario.Children.Add(miercoles);
        horario.Children.Add(jueves);
        horario.Children.Add(viernes);

        //Se crean los botones que llevan a la pagina de EleccionReserva
        //Primero se crea una lista de datosboton

        //


        //Para obtener la fecha en la que inicia la semana hay que verificar el caso especial en el que
        //la semana inicia en un mes y termina en otro, debido a que el la variable fechaFinal guarda 


        var fechaFinal = fechaInicio;

        for (var i = 0; i < 5; i++)
        for (var j = 0; j < 8; j++)
        {
            var nuevoBoton = new DatosBoton(j + 1, fechaFinal.AddDays(i));
            Grid.SetRow(nuevoBoton.boton, j + 1);
            Grid.SetColumn(nuevoBoton.boton, i + 1);

            nuevoBoton.boton.Clicked += Button_Clicked;

            horario.Children.Add(nuevoBoton.boton);
            _listaBotones.Add(nuevoBoton);
        }

        scrollView.Content = verticalPrincipal;
        horizontalPrincipal.Children.Add(trianguloI);
        horizontalPrincipal.Children.Add(etiquetas);
        horizontalPrincipal.Children.Add(trianguloD);

        verticalPrincipal.Children.Add(horizontalPrincipal);
        verticalPrincipal.Children.Add(horario);


        
        //Se llama a la funcion que cambia el color de los botones
        ObtenerSemana(laboratorio.idLaboratorio, fechaFinal);
        


        InitializeComponent();


        Content = scrollView;
    }

    public List<DateTime> CalcularInicioSemana(DateTime fechaActual)
    {
        var cultura = new CultureInfo("es-ES");
        var diaDeLaSemana = (int)fechaActual.DayOfWeek;

        var diaInicio = fechaActual.Day;
        int diaFin;

        if (diaDeLaSemana != 0) diaInicio -= diaDeLaSemana;
        diaFin = diaInicio + 6;

        var mesInicio = fechaActual.Month;
        var mesFin = fechaActual.Month;

        if (diaInicio < 1)
        {
            if (mesInicio == 1)
                mesInicio = 12; // Mes anterior a diciembre
            else
                mesInicio--;

            var fechaMesAnterior = new DateTime(fechaActual.Year, mesInicio, 1);
            var ultimoDiaMesAnterior = DateTime.DaysInMonth(fechaMesAnterior.Year, fechaMesAnterior.Month);
            diaInicio = ultimoDiaMesAnterior + diaInicio;
        }

        if (diaFin > DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month))
        {
            if (mesFin == 12)
                mesFin = 1; // Mes siguiente a enero
            else
                mesFin++;

            diaFin = diaFin - DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
        }

        List<DateTime> InicioyFin = new List<DateTime>();
        InicioyFin.Add(new DateTime(fechaActual.Year, mesInicio, diaInicio +1));
        InicioyFin.Add(new DateTime(fechaActual.Year, mesFin, diaFin + 1));

        return InicioyFin;
    }


    public int InicioSemana(DateTime fechaActual)
    {
        var diaDeLaSemana = (int)fechaActual.DayOfWeek;
        var diaInicio = fechaActual.Day;
        diaInicio -= diaDeLaSemana;

        if (diaInicio < 0)
        {
            //Saber si el mes anterior tiene 30 o 31 dias
            var mesInicio = fechaActual.Month;
            if (mesInicio == 1)
                mesInicio = 12; // Mes anterior a diciembre
            else
                mesInicio--;

            //Saber si el mes anterior tiene 30 o 31 dias
            var fechaMesAnterior = new DateTime(fechaActual.Year, mesInicio, 1);
            var ultimoDiaMesAnterior = DateTime.DaysInMonth(fechaMesAnterior.Year, fechaMesAnterior.Month);
            diaInicio = ultimoDiaMesAnterior + diaInicio;
        }

        Console.WriteLine("Dia de la semana: " + diaDeLaSemana);

        return diaInicio + 1;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var modulo = 0;
        var fechaDelBoton = new DateTime();


        // Encontrar el DatosBoton dentro de la lista
        DatosBoton botonEncontrado = _listaBotones.FirstOrDefault(boton => sender.Equals(boton.boton));

        if (botonEncontrado != null)
        {
            modulo = botonEncontrado.modulo;
            fechaDelBoton = botonEncontrado.fecha;
        }


        await Navigation.PushAsync(new EleccionReserva( _usuario,
            _labseleccionado, modulo, fechaDelBoton));
    }

    public async void ObtenerSemana(int laboratorio, DateTime fecha)
    {
        IObtenerReservacionesSemana obtenerReservacionesSemana = new ObtenerReservacionesSemanaServicio();

        var loadingPage = new ModalCarga("Cargando disponibilidad de los módulos" +
                                         "\n\t      Un momento...");

        

        this.ShowPopup(loadingPage);

        await Task.Delay(1000);

        _reservacionesSemana = await obtenerReservacionesSemana.ObtenerReservacionesSemana(laboratorio, fecha);

        

        CambiarColorBoton(loadingPage);
    }

    public async void CambiarColorBoton(ModalCarga modal)
    {
        //
        List<DatosBoton> checkedButons= new List<DatosBoton>();
        try
        {
            foreach (var reservacion in _reservacionesSemana)
            {
                
                    foreach (var boton in _listaBotones)

                        if (reservacion.modulo == boton.modulo && reservacion.fecha == boton.fecha && !checkedButons.Contains(boton))
                        {
                            checkedButons.Add(boton);
                            //Se verifica cuantas computadoras tiene resercvadas el laboratorio en la fecha actual
                            IObtenerModulo obtenerModulo = new ObtenerModuloServicio();
                            var lista = await obtenerModulo.ObtenerModulo(boton.modulo, _labseleccionado.idLaboratorio,
                                reservacion.fecha);

                            //Se le suma al atributo computadorasReservadas de la clase DatosBoton el numero de computadoras reservadas en la fecha actual
                            boton.cantidadReservas += lista.Count;

                            //Si la cantidad es 30, se cambia el color del boton a rojo; si la cantidad es 0, se cambia el color del boton a verde, si la cantidad esta entre 1 y 29, se cambia el color del boton a amarillo
                            if (boton.cantidadReservas >= 30)
                                boton.boton.BackgroundColor = Colors.Red;
                            else if (boton.cantidadReservas > 0 && boton.cantidadReservas < 30)
                                boton.boton.BackgroundColor = Colors.Yellow;
                        }
                    

            }
           
        }
        catch(Exception e)
        {
            //Se muestra en pantalla un alerta de que ha habido un error de conexion y se regresa a la pagina anterior
            await DisplayAlert("Error", "Ha habido un error de conexion", "OK");
            modal.Close();
            await Navigation.PopAsync();

        }
        //Se cierra el popup de carga
        modal.Close();

    }
}