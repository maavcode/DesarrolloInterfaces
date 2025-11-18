using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playground_.net_BasicThings.Backend.Servicios
{
    public class TipoArticuloRepository : GenericRepository<Tipoarticulo>, ITipoArticuloRepository
    {
        public TipoArticuloRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Tipoarticulo>> logger)
            : base(context, logger)
        {
        }
    }
}
