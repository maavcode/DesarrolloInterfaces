using playground_.net_BasicThings.Backend.Modelos;

namespace playground_.net_BasicThings.Backend.Servicios
{
    /// <summary>
    /// Contrato de repositorio para la entidad <see cref="Articulo"/>.
    /// Extiende el repositorio genérico <see cref="IGenericRepository{T}"/>.
    /// </summary>
    public interface IArticuloRepository : IGenericRepository<Articulo>
    {
    }
}