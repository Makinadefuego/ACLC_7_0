using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Models
{
    public class RootReservaciones
    {
        public string mensaje { get; set; }
        public List<Reservacion> response { get; set; }
    }
}
