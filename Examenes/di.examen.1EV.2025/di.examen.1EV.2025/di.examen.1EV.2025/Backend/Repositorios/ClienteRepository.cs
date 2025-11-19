using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using di.examen._1EV._2025.Backend.Modelos;

namespace di.examen._1EV._2025.Backend.Repositorios
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DbContext context) : base(context)
        {
        }

        // Métodos específicos de Cliente aquí. Usa QueryAsync para consultas personalizadas.
    }
}
