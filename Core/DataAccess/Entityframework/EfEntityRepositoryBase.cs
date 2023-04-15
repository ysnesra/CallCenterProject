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
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //using içine yazdığımız nesneler using bitince GarbageCollector tarafından bellekten atılır
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);  //eklenen 'entity'yi git veritabanıyla ilişkilendir. referansı yakala
                addedEntity.State = EntityState.Added;    //o aslında eklenecek bir nesne
                context.SaveChanges();                    //ve ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)  //tek data getirecek metotumuz
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // context.Set<Product>() ifadesi Dbset deki Productıma yerleş demek
                return filter == null ? context.Set<TEntity>().ToList()
                                     : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
