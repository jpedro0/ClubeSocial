using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository.Intefaces
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(dynamic id);
    }
}
