using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //class : referans tip olabilir
    //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //new() : nem'lenebilir olmalı -- yani soyut nesne olmasın

    //filtreleyerek listelemek için Expression yazarız
    //Örnegin GetAll() ile Category ye göre getir diyebileceğiz
    //Örnegin Get() ile Tek bir ürünü getirme//Tek  bir listenin detayına bakma

    public interface IEntityRepository<T> where T : class, IEntity, new()
    { 
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
