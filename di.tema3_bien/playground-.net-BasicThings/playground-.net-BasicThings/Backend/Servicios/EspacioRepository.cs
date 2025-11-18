using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playground_.net_BasicThings.Backend.Servicios
{
    public class EspacioRepository : GenericRepository<Espacio>, IEspacioRepository
    {
        public EspacioRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Espacio>> logger)
            : base(context, logger)
        {

        }
    }
}
