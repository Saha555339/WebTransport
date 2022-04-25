using LibraryDataBase.Entities;

namespace LibraryDataBase.DataLoading
{
    public class CreateAction<T> where T:BaseEntity
    {
        private TransportContext _dbContext { get; set; }
        public CreateAction(TransportContext dbContex)
        {
            _dbContext = dbContex;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
    }
}
