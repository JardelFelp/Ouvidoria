using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ouvidoria.Repository
{
    public class RepositoryBase<TEntity> where TEntity : class, new()
    {
        public static void Add(TEntity obj)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Set<TEntity>().Add(obj);
                db.SaveChanges();
            }
        }
        public static TEntity GetById(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Set<TEntity>().Find(id);
            }
        }
    }
}