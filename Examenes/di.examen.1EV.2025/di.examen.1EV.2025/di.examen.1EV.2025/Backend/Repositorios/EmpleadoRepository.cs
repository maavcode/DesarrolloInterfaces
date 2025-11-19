using System;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using di.examen._1EV._2025.Backend.Modelos;

namespace di.examen._1EV._2025.Backend.Repositorios
{
    /// <summary>
    /// Implementación del repositorio para Empleado.
    /// Reutiliza la lógica del GenericRepository y añade métodos específicos.
    /// </summary>
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleadoRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public EmpleadoRepository(JardineriaContext context) : base(context)
        {
        }
        /// <summary>
        /// Obtiene el mayor CódigoEmpleado actualmente en la tabla Empleados.
        /// </summary>
        /// <returns>El código si la tabla no está vacía, cero en caso contrario</returns>
        public async Task<int> GetLastCodigoEmpleadoAsync()
        {
            Logger.Info("GetLastCodigoEmpleadoAsync: buscando máximo CodigoEmpleado");
            try
            {
                int codigo;
                var list = await QueryAsync(q => q.OrderByDescending(e => e.CodigoEmpleado).Take(1)).ConfigureAwait(false);
                var first = list.FirstOrDefault();
                if( first == null)
                {
                    codigo = 0;
                    Logger.Info("GetLastCodigoEmpleadoAsync: no hay empleados, se asigna CodigoEmpleado={0}", codigo);
                } else { codigo = first.CodigoEmpleado; }
                return codigo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetLastCodigoEmpleadoAsync fallo.");
                throw;
            }
        }

        public async Task<bool> ExtensionExistsAsync(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension)) return false;

            Logger.Info("ExtensionExistsAsync: comprobando existencia de extensión='{0}'", extension);
            try
            {
                var list = await QueryAsync(q => q.Where(e => e.Extension == extension).Take(1)).ConfigureAwait(false);
                return list.Any();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "ExtensionExistsAsync fallo.");
                throw;
            }
        }
    }
}
