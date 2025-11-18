using Castle.Core.Logging;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playground_.net_BasicThings.Backend.Servicios
{
    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Departamento>> logger)
            : base(context, logger)
        {

        }
    }
}
