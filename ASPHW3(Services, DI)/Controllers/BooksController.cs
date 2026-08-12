using ASPHW3_Services__DI_.Models;
using ASPHW3_Services__DI_.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPHW3_Services__DI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        private static readonly List<Book> _books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "The Wealth of Nations",
                    Author = "Adam Smith",
                    Category = "Political Economy",
                    Price = 14.99m,
                    Stock = 45,
                    PageCount = 1080,
                    PublishedYear = 1776,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    Title = "Capital in the Twenty-First Century",
                    Author = "Thomas Piketty",
                    Category = "Political Economy",
                    Price = 22.50m,
                    Stock = 18,
                    PageCount = 696,
                    PublishedYear = 2013,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 3,
                    Title = "Why Nations Fail",
                    Author = "Daron Acemoglu, James A. Robinson",
                    Category = "Development Economics",
                    Price = 18.99m,
                    Stock = 30,
                    PageCount = 544,
                    PublishedYear = 2012,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 4,
                    Title = "The Road to Serfdom",
                    Author = "Friedrich Hayek",
                    Category = "Political Philosophy",
                    Price = 15.00m,
                    Stock = 22,
                    PageCount = 274,
                    PublishedYear = 1944,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 5,
                    Title = "The Great Transformation",
                    Author = "Karl Polanyi",
                    Category = "Economic History",
                    Price = 19.95m,
                    Stock = 12,
                    PageCount = 360,
                    PublishedYear = 1944,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 6,
                    Title = "Capitalism and Freedom",
                    Author = "Milton Friedman",
                    Category = "Economics",
                    Price = 16.50m,
                    Stock = 0,
                    PageCount = 240,
                    PublishedYear = 1962,
                    IsAvailable = false
                }
            };



        //GET /api/books 
        [HttpGet]
        public ActionResult<Book> GetAll()
        {
            var books = _books;

            return Ok(books);
        }

        //GET /api/books 
        [HttpGet("{id:int}")]
        public ActionResult<Book> GetById(int id)
        {
            var resultBook = _books.FirstOrDefault(b => b.Id == id);

            if (id <= 0) return BadRequest($"Invalid id {id}");

            if (resultBook == null) return NotFound($"Book with id {id} was not found");

            return Ok(resultBook);
        }

        //POST /api/books 
        [HttpPost]
        public ActionResult<Book> Add(
            [FromBody] Book newBook)
        {
            if (String.IsNullOrWhiteSpace(newBook.Title) ||
                String.IsNullOrWhiteSpace(newBook.Author) ||
                String.IsNullOrWhiteSpace(newBook.Category))
            {
                return BadRequest("Book must have title, author, and category");
            }
            if (newBook.Price <= 0)
            {
                return BadRequest("Book price must be greater than 0");
            }
            if (newBook.Stock <= 0)
            {
                return BadRequest("Book stock count must be greater than 0");
            }
            if (newBook.PageCount <= 0)
            {
                return BadRequest("Book page count must be greater than 0");
            }
            if (newBook.PublishedYear <= 1445 || newBook.PublishedYear >= DateTime.Now.Year)
            {
                return BadRequest($"Book publication year must be within 1445 and {DateTime.Now.Year}");
            }

            var lastId = _books.OrderByDescending(b => b.Id).First().Id;

            var createdBook = newBook;

            createdBook.Id = lastId + 1;

            _books.Add(createdBook);

            return CreatedAtAction(
             nameof(GetById),
                 new
                 {
                     id = createdBook.Id
                 },
                 createdBook
             );
        }


        //PUT /api/books/{id}
        [HttpPut("{id:int}")]

        public ActionResult<Book> Update(int id, [FromBody] Book updatedBook)
        {
            if (id <= 0) return BadRequest($"Invalid id {id}");

            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound($"Book with id {id} was not found");

            if (String.IsNullOrWhiteSpace(updatedBook.Title) ||
                String.IsNullOrWhiteSpace(updatedBook.Author) ||
                String.IsNullOrWhiteSpace(updatedBook.Category))
            {
                return BadRequest("Book must have title, author, and category");
            }
            if (updatedBook.Price <= 0)
            {
                return BadRequest("Book price must be greater than 0");
            }
            if (updatedBook.Stock <= 0)
            {
                return BadRequest("Book stock count must be greater than 0");
            }
            if (updatedBook.PageCount <= 0)
            {
                return BadRequest("Book page count must be greater than 0");
            }
            if (updatedBook.PublishedYear <= 1445 || updatedBook.PublishedYear >= DateTime.Now.Year)
            {
                return BadRequest($"Book publication year must be within 1445 and {DateTime.Now.Year}");
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Category = updatedBook.Category;
            book.Price = updatedBook.Price;
            book.Stock = updatedBook.Stock;
            book.PageCount = updatedBook.PageCount;
            book.PublishedYear = updatedBook.PublishedYear;
            book.IsAvailable = updatedBook.IsAvailable;

            return Ok(book);
        }


        //PATCH /api/books/{id}/stock?stock=10 

        [HttpPatch("{id:int}/stock")]

        public ActionResult<Book> UpdateStock(int id, int stock)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return BadRequest($"Book with id {id} was not found");
            }

            book.Stock = stock;

            return Ok(book);
        }

        //DELETE /api/books/{id}
        [HttpDelete("{id:int}")]

        public ActionResult<Book> Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return BadRequest($"Book with id {id} was not found");
            }

            _books.Remove(book);

            return NoContent();
        }

        //GET /api/books/search?title=code 
        [HttpGet("search")]

        public ActionResult<Book> GetByTtile([FromQuery] string title)
        {
            var result = _books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (result.Count() == 0) return BadRequest($"Book with title {title} was not found");

            return Ok(result);
        }

        //GET /api/books/author?name=Martin

        [HttpGet("author")]

        public ActionResult<Book> GetByAuthorName([FromQuery] string name)
        {
            var result = _books.Where(b => b.Author.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (result.Count() == 0) return BadRequest($"Book with title {name} was not found");

            return Ok(result);
        }

        //GET /api/books/category/{category} 

        [HttpGet("category/{category}")]

        public ActionResult<Book> GetByCategory(string category)
        {
            var result = _books.Where(b => b.Category.Contains(category, StringComparison.OrdinalIgnoreCase));

            if (result.Count() == 0) return BadRequest($"Book with title {category} was not found");

            return Ok(result);
        }

        //GET /api/books/filter?minPrice=20&maxPrice=100 

        [HttpGet("filter")]

        public ActionResult<Book> SortByPrice([FromQuery] decimal? min, [FromQuery] decimal? max)
        {
            if (min.HasValue && min < 0)
            {
                return BadRequest(new
                {
                    message = "Minimum price can not be negative"
                });
            }

            if (max.HasValue && max < 0)
            {
                return BadRequest(new
                {
                    message = "Maximum price can not be negative"
                });
            }

            if (min.HasValue &&
                max.HasValue &&
                min > max)
            {
                return BadRequest(new
                {
                    message = "Minimum value can not be greater than maximum value"
                });
            }

            IEnumerable<Book> result = _books;

            if (min.HasValue)
            {
                result = result.Where(b => b.Price >= min.Value);
            }

            if (max.HasValue)
            {
                result = result.Where(b => b.Price <= max.Value);
            }


            return Ok(result);
        }


        //GET /api/books/available 
        [HttpGet("available")]

        public ActionResult<Book> GetAvailable()
        {
            var result = _books.Where(b => b.IsAvailable == true);

            return Ok(result);
        }

        //GET /api/books/year/{year} 
        [HttpGet("year/{year:int}")]

        public ActionResult<Book> GetByYear(int year)
        {
            var result = _books.Where(b => b.PublishedYear == year);

            return Ok(result);
        }

        //GET /api/books/sorted?direction=asc 

        [HttpGet("sorted")]

        public ActionResult<Book> GetByYear([FromQuery] string direction)
        {
            if (!(direction.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                direction.Equals("desc", StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Invalid direction");
            }


            IEnumerable<Book> result = _books;
            if (direction.Equals("asc", StringComparison.OrdinalIgnoreCase))
            {
                result = result.OrderBy(b => b.Price);
            }
            else if (direction.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                result = result.OrderByDescending(b => b.Price);
            }

            return Ok(result);
        }


        //GET /api/books/statistics 

        [HttpGet("statistics")]

        public ActionResult<Book> GetStats()
        {
            Dictionary<string, float> result = new Dictionary<string, float>();
            var totalCount = _books.Count;
            var totalStock = _books.Sum(b => b.Stock);
            var avgPrice = (float)_books.Average(b => b.Price);
            var minPrice = (float)_books.Min(b => b.Price);
            var maxPrice = (float)_books.Max(b => b.Price);

            result.Add("Total books", totalCount);
            result.Add("Total stock", totalStock);
            result.Add("Average price", avgPrice);
            result.Add("Minimum price", minPrice);
            result.Add("Maximum price", maxPrice);

            return Ok(result);
        }
    }
}
