namespace di.examen._1EV._2025.Backend.Repositorios
{
        /// <summary>
        /// Interfaz genérica para operaciones CRUD (sin CancellationToken).
        /// </summary>
        /// <typeparam name="T">Tipo de entidad (entidad EF)</typeparam>
        public interface IGenericRepository<T>
            where T : class
        {
            /// <summary>
            /// Obtiene todas las filas o entidades de una tabla de la base de datos
            /// </summary>
            /// <returns>Una lista con las filas de la base de datos</returns>
            Task<IEnumerable<T>> GetAllAsync();
            /// <summary>
            /// Obtiene un objeto en función de su código
            /// </summary>
            /// <param name="id">Identificador del objeto</param>
            /// <returns>El objeto asociado a ese código</returns>
            Task<T?> GetByIdAsync(object id);
            /// <summary>
            /// Inserta la entidad. Devuelve true si se insertó correctamente.
            /// </summary>
            Task<bool> AddAsync(T entity);
            /// <summary>
            /// Actualiza una entidad
            /// </summary>
            /// <param name="entity">Entidad a actualizar</param>
            /// <returns>Boolean para saber el rresultado de la operación.</returns>
            Task<bool> UpdateAsync(T entity);
            Task<bool> DeleteAsync(object id);

            /// <summary>
            /// Ejecuta una consulta LINQ personalizada sobre el conjunto de la entidad.
            /// Se pasa una función que transforma un IQueryable{T} y se ejecuta devolviendo la lista resultante.
            /// Ejemplo de uso: repo.QueryAsync(q => q.Where(x => x.Prop == value).OrderBy(x => x.Id));
            /// </summary>
            Task<IEnumerable<T>> QueryAsync(Func<IQueryable<T>, IQueryable<T>> query);
    }
}
