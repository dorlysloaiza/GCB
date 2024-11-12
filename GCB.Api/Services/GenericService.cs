using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GCB.Api.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly GCBContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericService(GCBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params string[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, params string[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                await LogAuditAsync(entity, "Added");
                await transaction.CommitAsync();
                return entity;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<T> UpdateAsync(Guid id, T entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingEntity = await _dbSet.FindAsync(id);
                if (existingEntity == null)
                {
                    return null;
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                await LogAuditAsync(entity, "Modified");
                await transaction.CommitAsync();
                return existingEntity;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<T> PatchAsync(Guid id, T entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingEntity = await _dbSet.FindAsync(id);
                if (existingEntity == null)
                {
                    return null;
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                await LogAuditAsync(entity, "Modified");
                await transaction.CommitAsync();
                return existingEntity;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    return false;
                }

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                await LogAuditAsync(entity, "Deleted");
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task LogAuditAsync(T entity, string action)
        {
            var entityName = entity.GetType().Name;
            var entityId = (Guid)entity.GetType().GetProperty("Id").GetValue(entity);
            var userName = (string)entity.GetType().GetProperty("NombreUsuario").GetValue(entity);
            var changes = JsonConvert.SerializeObject(entity);
            var log = new RegistroAuditoria
            {
                Id = Guid.NewGuid(),
                NombreEntidad = entityName,
                IdEntidad = entityId,
                Accion = action,
                Fecha = DateTime.UtcNow,
                NombreUsuario = userName,
                ObjetoActual = changes
            };

            _context.Set<RegistroAuditoria>().Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
