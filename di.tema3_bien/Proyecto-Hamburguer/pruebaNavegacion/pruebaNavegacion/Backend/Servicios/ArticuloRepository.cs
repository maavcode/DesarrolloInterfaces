using Microsoft.Extensions.Logging;
using pruebaNavegacion.Backend.Modelo;

namespace pruebaNavegacion.Backend.Servicios
{
    /// <summary>
    /// Repositorio específico para <see cref="Articulo"/>, implementa <see cref="IArticuloRepository"/>.
    /// Sigue el mismo patrón que los repositorios existentes en la carpeta.
    /// </summary>
    public class ArticuloRepository : GenericRepository<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Articulo>> logger)
            : base(context, logger)
        {
        }
    }
}