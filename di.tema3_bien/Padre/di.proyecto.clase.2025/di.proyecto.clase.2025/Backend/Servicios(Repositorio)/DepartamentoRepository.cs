using Castle.Core.Logging;
using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2025.Backend.Servicios_Repositorio_
{
    public class DepartamentoRepository: GenericRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(DiinventarioexamenContext context,ILogger<GenericRepository<Departamento>>logger)
            :base(context,logger)
        {

        }
    }
}
