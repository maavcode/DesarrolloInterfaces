using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playground_.net_BasicThings.Backend.Servicios
{
    public class ModeloArticuloRepository : GenericRepository<Modeloarticulo>, IModeloArticuloRepository
    {
        public ModeloArticuloRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Modeloarticulo>> logger)
            : base(context, logger)
        {
        }
    }
}
