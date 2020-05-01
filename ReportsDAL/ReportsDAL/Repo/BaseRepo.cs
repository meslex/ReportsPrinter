using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ReportsDAL.EF;
using ReportsDAL.Models.Base;

namespace ReportsDAL.Repo
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly ReportEntities _db;
        public BaseRepo()
        {
            _db = new ReportEntities();
            _table = _db.Set<T>();
        }
        protected ReportEntities Context => _db;

        public void Dispose()
        {
            _db?.Dispose();
        }

        public T GetOne(int? id) => _table.Find(id);

        public virtual List<T> GetAll() => _table.ToList();

        public List<T> ExecuteQuery(string sql) => _table.SqlQuery(sql).ToList();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects)
         => _table.SqlQuery(sql, sqlParametersObjects).ToList();

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Save(T entity)
        {
            //entity._IsChanged = false;
            _db.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public int Delete(int id, byte[] timeStamp)
        {
            _db.Entry(new T() { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

            //Thrown when there is a concurrency error
            //for now, just rethrow the exception
                throw;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional
                //details and affected objects
                //for now, just rethrow the exception
                throw;
            }
            catch (CommitFailedException ex)
            {
                //handle transaction failures here
                //for now, just rethrow the exception
                throw;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw;
            }
        }


    }
}
