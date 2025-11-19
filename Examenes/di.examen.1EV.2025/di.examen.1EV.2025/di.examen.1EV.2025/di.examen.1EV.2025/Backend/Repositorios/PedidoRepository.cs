using Microsoft.EntityFrameworkCore;
using di.examen._1EV._2025.Backend.Modelos;

namespace di.examen._1EV._2025.Backend.Repositorios
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(DbContext context) : base(context)
        {
        }

        // Métodos específicos de Pedido aquí.
    }
}
