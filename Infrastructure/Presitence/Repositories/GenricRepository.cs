using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presitence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenricRepository<TEntity, TKey> : IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenricRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return trackChanges ?
                    await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync() as IEnumerable<TEntity>
                   : await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).AsNoTracking().ToListAsync() as IEnumerable<TEntity>; 

            }


            return trackChanges ?
                 await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

            //if (trackChanges) return await _context.Set<TEntity>().ToListAsync();
            //return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Where(P => P.Id == id as int?).Include(P => P.ProductBrand).Include(P => P.ProductType).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            }

            return await _context.Set<TEntity>().FindAsync(id);
            //return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
            //return await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id));
            //return await _context.Set<TEntity>().Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
             await _context.AddAsync(entity);
          

        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool trackChanges = false)
        {
           return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> spec)
        {
          return await ApplySpecification(spec).FirstOrDefaultAsync();
        } 
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity,TKey> spec )
        {
            return SpescificationEvaluator.GetQuery(_context.Set<TEntity>(), spec); 
        }
    }
}
