using ClubeSocial.Context;
using ClubeSocial.Repository.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository
{
    public class RepositoryBase<TEntity>: IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ClubeDBContext Db;
        protected readonly DbSet<TEntity> DbSet;
        public RepositoryBase(ClubeDBContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
            Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
            Db.SaveChanges();
        }

        public void Remove(dynamic id)
        {
            DbSet.Remove(DbSet.Find(id));
            Db.SaveChanges();
        }
    }
}
