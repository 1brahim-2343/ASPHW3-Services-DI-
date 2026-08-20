using ASPHW3_Services__DI_.Models;
using System.Collections;

namespace ASPHW3_Services__DI_.Services.Abstract
{
    public interface IBookService
    {
        IQueryable<Book> Get();
        Book? Get(int id);
        IEnumerable<Book> GetByTitle(string title);
        IEnumerable<Book> GetByAuthorName(string name);
        IEnumerable<Book> GetByCategory(string category);
        IQueryable<Book> GetAvailable();
        IQueryable<Book> GetByYear(int year);
        bool Delete(Book book);
        Book? Update(Book book);
        Book? Add(Book book);
        IQueryable<Book> SortByPrice(decimal? min, decimal? max);
        IEnumerable<Book> SortByYear(string direction);
    }
}
