using di.tema3.proyecto.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.tema3.proyecto.Backend.Servicios
{

    public class ModeloArticuloRepository : GenericRepository<Modeloarticulo>, IModeloArticuloRepository
    {

        public ModeloArticuloRepository(DiinventarioexamenContext context, Microsoft.Extensions.Logging.ILogger<GenericRepository<Modeloarticulo>> logger)
            : base(context, logger)
        {
        }
    }
}
