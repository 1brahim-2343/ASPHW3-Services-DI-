using ASPHW3_Services__DI_.Data;
using ASPHW3_Services__DI_.Models;
using ASPHW3_Services__DI_.Repository.Abstract;

namespace ASPHW3_Services__DI_.Repository.Concrete
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }
        public Book Add(Book book)
        {
            var createdBook = _context.Books.Add(book).Entity;
            return createdBook;
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);
        }

        public IQueryable<Book> Get()
        {
            return _context.Books;
        }

        public Book? Get(int id)
        {
            return _context.Books.SingleOrDefault(b => b.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public Book Update(Book book)
        {
            var updatedBook = _context.Books.Update(book).Entity;
            return updatedBook;
        }
    }
}
