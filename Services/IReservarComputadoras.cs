using ACLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Services
{
    interface IReservarComputadoras
    {
        Task<bool> ReservarComputadora(List<int> computadoras, DateTime fecha, Usuario usuario, int modulo, int lab);
    }
}
