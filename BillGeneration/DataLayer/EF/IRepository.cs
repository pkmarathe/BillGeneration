 
using System.Linq; 

namespace BillGeneration.DataLayer.EF
{
    public interface IRepository
    {

        //Single Object Methods
        T Get<T>(object key) where T : class;

        IQueryable<T> GetAll<T>() where T : class;
        //CRUD Methods
        bool Add<T>(T entityToCreate) where T : class;
        bool Update<T>(T entityToEdit) where T : class;
        bool Remove<T>(T entityToDelete) where T : class;


    }
}
