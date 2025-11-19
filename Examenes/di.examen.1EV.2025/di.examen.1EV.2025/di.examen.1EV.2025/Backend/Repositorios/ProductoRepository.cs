using di.examen._1EV._2025.Backend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace di.examen._1EV._2025.Backend.Repositorios
{
    /// <summary>
    /// Implementación del repositorio para Producto, reutiliza GenericRepository.
    /// </summary>
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(DbContext context) : base(context)
        {
        }

        // Implementa aquí métodos específicos usando QueryAsync si es necesario.
        // Ejemplo:
        // public Task<IEnumerable<Producto>> GetByCategoriaAsync(string categoria)
        //     => QueryAsync(q => q.Where(p => p.Categoria == categoria));
    }
}
