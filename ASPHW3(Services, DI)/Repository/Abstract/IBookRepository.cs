using ASPHW3_Services__DI_.Models;

namespace ASPHW3_Services__DI_.Repository.Abstract
{
    public interface IBookRepository
    {
        IQueryable<Book> Get();
        Book? Get(int id);
        void Delete(Book book);
        Book Update(Book book);
        Book Add(Book book);
        bool SaveChanges();
    }
}
