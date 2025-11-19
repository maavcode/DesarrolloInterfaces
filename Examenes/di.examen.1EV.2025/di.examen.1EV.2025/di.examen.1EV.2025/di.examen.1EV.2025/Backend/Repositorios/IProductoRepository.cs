using di.examen._1EV._2025.Backend.Modelos;

namespace di.examen._1EV._2025.Backend.Repositorios
{
    /// <summary>
    /// Repositorio específico para la tabla/entidad Producto.
    /// Hereda las operaciones CRUD genéricas.
    /// </summary>
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        // Añade aquí métodos específicos de Producto si los necesitas.
        // Ejemplo: Task<IEnumerable<Producto>> GetByCategoriaAsync(string categoria);
    }
}
