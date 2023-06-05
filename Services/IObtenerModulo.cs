using ACLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Services
{
    public interface IObtenerModulo
    {
        Task<List<Computadora>> ObtenerModulo(int modulo, int lab, DateTime fecha);
    }
}
