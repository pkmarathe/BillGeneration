
using System.Data.Entity;
using System.Linq;



namespace BillGeneration.DataLayer.EF
{
    public abstract class AbstractRepository : IRepository
    {
        protected BillGenerationEntities objcontext;

        public abstract T Get<T>(object key) where T : class;

        public abstract IQueryable<T> GetAll<T>() where T : class;

        public abstract bool Add<T>(T entityToCreate) where T : class;

        public abstract bool Update<T>(T entityToEdit) where T : class;

        public abstract bool Remove<T>(T entityToDelete) where T : class;
    }
}
