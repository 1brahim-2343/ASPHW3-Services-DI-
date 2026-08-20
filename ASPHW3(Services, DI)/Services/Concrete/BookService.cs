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

        public Book? Add(Book book)
        {
            bool validBook;
            try
            {
                ValidateBook(book);
                validBook = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                validBook = false;
            }

            if (validBook)
            {
                var result = _bookRepo.Add(book);
                _bookRepo.SaveChanges();
                return result;
            }
            else
            {
                return null;
            }
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
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }
            return _bookRepo.Get(id);
        }

        public Book? Update(Book book)
        {
            bool validBook;
            try
            {
                ValidateBook(book);
                validBook = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                validBook = false;
            }

            if (validBook)
            {
                var result = _bookRepo.Update(book);
                _bookRepo.SaveChanges();
                return result;
            }
            else
            {
                return null;
            }
            
        }

        private bool ValidateBook(Book book)
        {
            if (String.IsNullOrEmpty(book.Title) || book.Title.Length <= 5)
            {
                throw new ArgumentException("Book must have title");
            }

            if (String.IsNullOrEmpty(book.Author) || book.Author.Length <= 3)
            {
                throw new ArgumentException("Book must have valid author name");
            }

            if (String.IsNullOrEmpty(book.Category) || book.Category.Length <= 3)
            {
                throw new ArgumentException("Book must have valid category");
            }

            if (book.PageCount <= 0)
            {
                throw new ArgumentException("Page count must be greater than 0");
            }

            if (book.Price <= 0)
            {
                throw new ArgumentException("Book price must be greater than 0");
            }

            if (book.PublishedYear <= 1700 || book.PublishedYear > DateTime.Now.Year)
            {
                throw new ArgumentException(
                    $"Book publication year must be at least 1700 and less than {DateTime.Now.Year}");
            }

            if (book.Stock < 0)
            {
                throw new ArgumentException("Book stock count can not be negative");
            }

            if (book.Stock == 0 && book.IsAvailable != false)
            {
                throw new ArgumentException("Stock count and availability status inconsistency");
            }

            return true;
        }
    }
}