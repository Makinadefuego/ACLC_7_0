using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLC.Services
{
    public interface IReservacionModulo
    {
         Task<bool> ReservarModulo(int usuario, int modulo, DateTime date, int lab);
    }
}
