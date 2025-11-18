using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;

namespace playground_.net_BasicThings.Backend.Servicios
{
    /// <summary>
    /// Repositorio específico para la entidad <see cref="Usuario"/>.
    /// Hereda la funcionalidad CRUD básica de <see cref="GenericRepository{T}"/>
    /// y expone operaciones específicas de usuario (login, cambio de contraseña).
    /// </summary>
    public class UsuarioRepository : GenericRepository<Usuario>
    {
        private readonly ILogger<GenericRepository<Usuario>> _logger;
        /// <summary>
        /// Crea una nueva instancia de <see cref="UsuarioRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        /// <param name="logger">Logger para el repositorio.</param>
        public UsuarioRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Usuario>> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Intenta autenticar un usuario por nombre y contraseña.
        /// Devuelve la entidad <see cref="Usuario"/> si las credenciales son correctas, o null en caso contrario.
        /// Nota: el método compara la cadena de contraseña tal cual. Si usas hashing (recomendado), aplica el
        /// verificador de hash aquí antes de comparar.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña en texto plano (o ya hasheada si ese es tu flujo).</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Usuario autenticado o null si las credenciales no son válidas.</returns>
        public async Task<bool> LoginAsync(string username, string password)
        {
            bool isAuthenticated = false;
            try
            {
                // Obtengo el usuario por username
                var usuario = await Query(asNoTracking: true)
                    .FirstOrDefaultAsync(u => u.Username == username)
                    .ConfigureAwait(false);
                // Compruebo si el usuario existe y la contraseña coincide
                if (usuario != null && usuario.Password == password)
                {
                    isAuthenticated = true;
                }
                return isAuthenticated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al autenticar usuario {Username}.", username);
                throw;
            }
        }

        /// <summary>
        /// Cambia la contraseña de un usuario verificando la contraseña actual.
        /// Devuelve true si la contraseña se actualizó correctamente, false si no se encontró el usuario o la contraseña actual no coincide.
        /// </summary>
        /// <param name="userId">Id del usuario.</param>
        /// <param name="currentPassword">Contraseña actual en texto plano (o hasheada si ese es tu flujo).</param>
        /// <param name="newPassword">Nueva contraseña (texto plano o hasheada según tu política).</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>True si el cambio tuvo éxito; false en caso contrario.</returns>
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("La nueva contraseña no puede estar vacía.", nameof(newPassword));

            try
            {
                var usuario = await GetByIdAsync(userId).ConfigureAwait(false);
                if (usuario == null)
                {
                    _logger.LogWarning("Cambio de contraseña: usuario con id {Id} no encontrado.", userId);
                    return false;
                }

                // Verificar contraseña actual (simple). Si usas hashing, verifica el hash en lugar de comparar strings.
                if (usuario.Password != currentPassword)
                {
                    _logger.LogWarning("Cambio de contraseña fallido: contraseña actual incorrecta para usuario id {Id}.", userId);
                    return false;
                }

                usuario.Password = newPassword;

                // UpdateAsync en la implementación persiste los cambios (SaveChangesAsync).
                await UpdateAsync(usuario).ConfigureAwait(false);

                _logger.LogInformation("Contraseña actualizada correctamente para usuario id {Id}.", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar la contraseña del usuario id {Id}.", userId);
                throw;
            }
        }

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario (sin tracking).
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Usuario o null si no existe.</returns>
        public async Task<Usuario?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await Query(asNoTracking: true)
                         .FirstOrDefaultAsync(u => u.Username == username, cancellationToken)
                         .ConfigureAwait(false);
        }

        /// <summary>
        /// Comprueba si existe un usuario con el nombre proporcionado.
        /// </summary>
        /// <param name="username">Nombre de usuario a comprobar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>True si existe, false en caso contrario.</returns>
        public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await Query(asNoTracking: true)
                         .AnyAsync(u => u.Username == username, cancellationToken)
                         .ConfigureAwait(false);
        }

        /// <summary>
        /// Obtiene un usuario junto con sus colecciones de artículos (ejemplo de include).
        /// Devuelve entidades trackeadas porque puede usarse para edición.
        /// </summary>
        /// <param name="id">Id del usuario.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Usuario con navegación incluida o null.</returns>
        public async Task<Usuario?> GetWithArticulosAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Query(asNoTracking: false,
                               u => u.ArticuloUsuarioaltaNavigations,
                               u => u.ArticuloUsuariobajaNavigations)
                         .FirstOrDefaultAsync(u => u.Idusuario == id, cancellationToken)
                         .ConfigureAwait(false);
        }
    }
}