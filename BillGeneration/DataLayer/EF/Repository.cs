

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using LinqKit;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Validation;

namespace BillGeneration.DataLayer.EF
{
    public class Repository : AbstractRepository
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        public Repository()
        {
            objcontext = new BillGenerationEntities();
        }

        #endregion Constructor

        #region IRepository Members

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override T Get<T>(object key)
        {
            return objcontext.Set<T>().Find(key);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override IQueryable<T> GetAll<T>()
        {
            return objcontext.Set<T>();
        }

        /// <summary>
        /// Finds all by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Predicate value must be passed to FindAllBy<T>.</exception>
        public virtual IQueryable<T> FindAllBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                return objcontext.Set<T>().Where(predicate);
            }
            else
            {
                throw new ArgumentNullException("Predicate value must be passed to FindAllBy<T>.");
            }
        }


        /// <summary>
        /// Adds the specified entity to create.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToCreate">The entity to create.</param>
        /// <returns></returns>
        public override bool Add<T>(T entityToCreate)
        {
            try
            {
                objcontext.Set<T>().Add(entityToCreate);
                objcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the specified entity to edit.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToEdit">The entity to edit.</param>
        /// <returns></returns>
        public override bool Update<T>(T entityToEdit)
        {
            try
            {
                var updated = objcontext.Set<T>().Attach(entityToEdit);
                objcontext.Entry(entityToEdit).State = System.Data.Entity.EntityState.Modified;
                objcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Removes the specified entity to delete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        public override bool Remove<T>(T entityToDelete)
        {
            //first find and delate all relationships
            //need to further review code
            objcontext.Set<T>().Remove(entityToDelete);
            objcontext.SaveChanges();
            return true;
        }

        public virtual IEnumerable<T> FindAllBy1<T>(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = objcontext.Set<T>().AsExpandable(); //.Where(predicate);

            query = query.Where(predicate);

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        //public virtual IEnumerable<T> FindAllByProjection<T>(
        //    Expression<Func<T, T>> selector,
        //    Expression<Func<T, bool>> filter = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    params Expression<Func<T, object>>[] includeProperties) where T : class
        //{
        //    IQueryable<T> query = objcontext.Set<T>().AsExpandable();
        //    IQueryable<T> resultQuery = null;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        //    if (orderBy != null)
        //    {
        //        return orderBy(query).ToList();
        //    }

        //    resultQuery = query.Select(selector);

        //    return resultQuery.ToList();
        //}


        #endregion

        #region Stored Procedure Methods 

        //public List<GetGenieNearByUserLocationByMapRadius> GetSmartGenieUserCityWiseCount(string latitude, string longtitude, string radius, string condition, string filter)
        //{
        //    return objcontext.GetGenieNearByUserLocationByMapRadius(latitude, longtitude, radius, condition, filter).ToList();
        //} 

        #endregion
    }
}
