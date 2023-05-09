using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Entityframework
{
    //Hangi TEntity'yi (yani tabloyu) verirsek onun  IEntityRepository'sini implemente edecek

    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {

        private readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            //using içine yazdığımız nesneler using bitince GarbageCollector tarafından bellekten atılır
            var addedEntity = _context.Entry(entity);  //eklenen 'entity'yi git veritabanıyla ilişkilendir. referansı yakala
            addedEntity.State = EntityState.Added;    //o aslında eklenecek bir nesne
        }

        public void Delete(TEntity entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)  //tek data getirecek metotumuz
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)  
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {

            // context.Set<Product>() ifadesi Dbset deki Productıma yerleş demek
            return filter == null ? _context.Set<TEntity>().ToList()
                                 : _context.Set<TEntity>().Where(filter).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }
    }
}
