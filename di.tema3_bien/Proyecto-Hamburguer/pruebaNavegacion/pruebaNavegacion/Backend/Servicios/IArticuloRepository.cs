using pruebaNavegacion.Backend.Modelo;

namespace pruebaNavegacion.Backend.Servicios
{
    /// <summary>
    /// Contrato de repositorio para la entidad <see cref="Articulo"/>.
    /// Extiende el repositorio genérico <see cref="IGenericRepository{T}"/>.
    /// </summary>
    public interface IArticuloRepository : IGenericRepository<Articulo>
    {
    }
}