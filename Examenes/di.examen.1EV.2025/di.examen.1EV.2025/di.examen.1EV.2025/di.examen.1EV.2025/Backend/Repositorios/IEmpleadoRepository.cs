using System.Threading.Tasks;
using di.examen._1EV._2025.Backend.Modelos;

namespace di.examen._1EV._2025.Backend.Repositorios
{
    /// <summary>
    /// Repositorio específico para la tabla/entidad Empleado.
    /// Hereda las operaciones CRUD genéricas y añade métodos específicos.
    /// </summary>
    public interface IEmpleadoRepository : IGenericRepository<Empleado>
    {
        /// <summary>
        /// Devuelve el último (máximo) código de empleado o null si no hay empleados.
        /// </summary>
        Task<int> GetLastCodigoEmpleadoAsync();

        /// <summary>
        /// Indica si ya existe una extensión concreta (igualdad exacta).
        /// </summary>
        Task<bool> ExtensionExistsAsync(string extension);
    }
}
