using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.examen._1EV._2025.Backend.Repositorios
{
        /// <summary>
        /// Implementación genérica de IRepository usando Entity Framework Core (sin CancellationToken).
        /// </summary>
        /// <typeparam name="T">Entidad EF (clase)</typeparam>
        public class GenericRepository<T> : IGenericRepository<T>
            where T : class
        {
            private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
            private readonly DbContext _context;
            private DbSet<T> _dbSet;

            public GenericRepository(DbContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _dbSet = _context.Set<T>();
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                try
                {
                    return await _context.Set<T>().AsNoTracking().ToListAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "GetAllAsync fallo.");
                    throw;
                }
            }

            public async Task<T?> GetByIdAsync(object id)
            {
                try
                {
                    var found = await _dbSet.FindAsync(new object[] { id }).ConfigureAwait(false);
                    return found;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "GetByIdAsync fallo.");
                    throw;
                }
            }

            public async Task<bool> AddAsync(T entity)
            {
                Logger.Info("AddAsync: entidad={0}", typeof(T).FullName);
                try
                {
                    _context.Entry(entity).State = EntityState.Unchanged;
                    await _dbSet.AddAsync(entity).ConfigureAwait(false);
                    var affected = await _context.SaveChangesAsync().ConfigureAwait(false);
                    Logger.Debug("AddAsync: SaveChangesAsync afectó {0} filas.", affected);
                    return affected > 0;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "AddAsync fallo.");
                    throw;
                }
            }

            public async Task<bool> UpdateAsync(T entity)
            {
                Logger.Info("UpdateAsync: entidad={0}", typeof(T).FullName);
                try
                {
                    _dbSet.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    var changed = await _context.SaveChangesAsync().ConfigureAwait(false);
                    return changed > 0;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "UpdateAsync fallo.");
                    throw;
                }
            }

            public async Task<bool> DeleteAsync(object id)
            {
                Logger.Info("DeleteAsync: entidad={0} id={1}", typeof(T).FullName, id);
                try
                {
                    var entity = await _dbSet.FindAsync(new object[] { id }).ConfigureAwait(false);
                    if (entity == null)
                    {
                        Logger.Warn("DeleteAsync: entidad no encontrada para id={0}", id);
                        return false;
                    }

                    _context.Set<T>().Remove(entity);
                    var changed = await _context.SaveChangesAsync().ConfigureAwait(false);
                    return changed > 0;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "DeleteAsync fallo.");
                    throw;
                }
            }

            public async Task<IEnumerable<T>> QueryAsync(Func<IQueryable<T>, IQueryable<T>> query)
            {
                Logger.Info("QueryAsync: entidad={0}", typeof(T).FullName);
                try
                {
                    if (query == null) throw new ArgumentNullException(nameof(query));
                    var q = query(_dbSet.AsQueryable());
                    return await q.AsNoTracking().ToListAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "QueryAsync fallo.");
                    throw;
                }
            }

            private static bool IsIntegerType(Type? t)
            {
                if (t == null) return false;
                var type = Nullable.GetUnderlyingType(t) ?? t;
                return type == typeof(byte)
                    || type == typeof(sbyte)
                    || type == typeof(short)
                    || type == typeof(ushort)
                    || type == typeof(int)
                    || type == typeof(uint)
                    || type == typeof(long)
                    || type == typeof(ulong);
            }
        }
}
