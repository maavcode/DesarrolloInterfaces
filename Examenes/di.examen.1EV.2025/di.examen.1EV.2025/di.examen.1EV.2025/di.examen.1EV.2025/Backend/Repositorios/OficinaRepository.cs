using di.examen._1EV._2025.Backend.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.examen._1EV._2025.Backend.Repositorios
{
    public class OficinaRepository : GenericRepository<Oficina>, IOficinaRepository
    {
        public OficinaRepository(DbContext context) : base(context)
        {
        }
    }
}
