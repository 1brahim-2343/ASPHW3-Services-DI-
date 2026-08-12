using ASPHW3_Services__DI_.Models;

namespace ASPHW3_Services__DI_.Services.Abstract
{
    public interface IBookService
    {
        IQueryable<Book> Get();
        Book? Get(int id);
        bool Delete(Book book);
        Book Update(Book book);
        Book Add(Book book);
    }
}
