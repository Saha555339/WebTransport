using LibraryDataBase.Entities;

namespace LibraryDataBase.DataLoading
{
    public class Create<T> where T:BaseEntity
    {
        private TransportContext _dbContext { get; set; }
        public Create(TransportContext dbContex)
        {
            _dbContext = dbContex;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
    }
}
