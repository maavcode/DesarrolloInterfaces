using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;

namespace playground_.net_BasicThings.Backend.Servicios
{
    /// <summary>
    /// Repositorio específico para <see cref="Articulo"/>, implementa <see cref="IArticuloRepository"/>.
    /// Sigue el mismo patrón que los repositorios existentes en la carpeta.
    /// </summary>
    public class ArticuloRepository : GenericRepository<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Articulo>> logger)
        : base(context, logger)
        { }

        public async Task<int> GetUltimoIdAsync()
        {
            var max = await _dbSet
                .Select(a => (int?)a.Idarticulo)
                .MaxAsync();

            return max ?? 0;
        }
    }
}