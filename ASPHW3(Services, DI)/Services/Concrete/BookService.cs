using ASPHW3_Services__DI_.Models;
using ASPHW3_Services__DI_.Repository.Abstract;
using ASPHW3_Services__DI_.Services.Abstract;

namespace ASPHW3_Services__DI_.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;


        public BookService(IBookRepository bookRepository)
        {
            _bookRepo = bookRepository;
        }
        public Book Add(Book book)
        {
            var result = _bookRepo.Add(book);
            _bookRepo.SaveChanges();
            return result;
        }

        public bool Delete(Book book)
        {
            _bookRepo.Delete(book);
            var result = _bookRepo.SaveChanges();
            return result;
        }

        public IQueryable<Book> Get()
        {
            return _bookRepo.Get();
        }

        public Book? Get(int id)
        {
            return _bookRepo.Get(id);
        }

        public Book Update(Book book)
        {
            var result = _bookRepo.Update(book);
            _bookRepo.SaveChanges();
            return result;
        }
    }
}
