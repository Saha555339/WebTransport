using LibraryDataBase.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDataBase.DataLoading
{
    public class Edit<T> where T:BaseEntity
    {
        private TransportContext _dbContext { get; set; }
        public Edit(TransportContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<T> Edits()
        {
            var _edits = _dbContext.Set<T>().ToList();
            return _edits;
        }
    }
}
