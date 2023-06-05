using ACLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Services
{
    public interface IObtenerReservacionesSemana
    {
        Task<List<Reservacion>> ObtenerReservacionesSemana(int lab, DateTime fecha);
    }
}
