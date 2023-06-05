using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Models
{
    public class DatosBoton
    {
        public int modulo{ get; set; }
        public DateTime fecha { get; set; }

        public Button boton { get; set; }
        public int cantidadReservas { get; set; } = 0;
        public bool disponible{ get; set; }

        public DatosBoton(int modulo, DateTime fecha)
        {
            this.modulo = modulo;
            this.fecha = fecha;
            this.boton = new Button()
            {
                BorderColor = Colors.White,
                BorderWidth = 1,
                CornerRadius = 0,
                BackgroundColor = Colors.Green
            };
            this.disponible = true;
        }
    }
}
