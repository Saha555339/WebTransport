using WebTransport.DataBase;
using WebTransport.ProjectExceptions;
using System.Linq;

namespace WebTransport.DataLoading
{
    public class Delete<T> where T : BaseEntity
    {
        private TransportContext _dbContext;
        public Delete(TransportContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteEntity(int id)
        {
            var t = _dbContext.Set<T>().Where(s => s.Id == id).ToList();
            if (t.Count == 0)
                throw new TransportDataBaseException("Field with this id does not exist");
            else
                _dbContext.Set<T>().RemoveRange(t);
            _dbContext.SaveChanges();
        }
        
    }
}
