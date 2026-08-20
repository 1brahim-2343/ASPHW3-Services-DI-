using ASPHW3_Services__DI_.DTOs;
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




        //GET /api/books 
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = _bookService.Get();
            return Ok(books);
        }

        //GET /api/books 
        [HttpGet("{id:int}")]
        public ActionResult<Book> Get(int id)
        {
            var resultBook = _bookService.Get(id);

            if (id <= 0) return BadRequest($"Invalid id {id}");

            if (resultBook == null) return NotFound($"Book with id {id} was not found");

            return Ok(resultBook);
        }

        //POST /api/books 
        [HttpPost]
        public ActionResult<Book> Add([FromBody] BookAddDto dto)
        {
            var book = new Book
            {
                Author = dto.Author,
                Title = dto.Title,
                Category = dto.Category,
                Price = dto.Price,
                Stock = dto.Stock,
                PageCount = dto.PageCount,
                PublishedYear = dto.PublishedYear,
                IsAvailable = dto.IsAvailable
            };

            var createdBook = _bookService.Add(book);
            return CreatedAtAction(nameof(Get), new
            {
                id = createdBook?.Id,
            }, createdBook);
        }


        //PUT /api/books/{id}
        [HttpPut("{id:int}")]

        public ActionResult<Book> Update(int id, [FromBody] BookUpdateDto dto)
        {
            try
            {
                var book = _bookService.Get(id);
                if (book == null) return NotFound();

                book.Price = dto.Price;
                book.Stock = dto.Stock;
                book.PageCount = dto.PageCount;
                book.IsAvailable = dto.IsAvailable;

                var updatedBook = _bookService.Update(book);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //PATCH /api/books/{id}/stock?stock=10 

        [HttpPatch("{id:int}/stock")]

        public ActionResult<Book> UpdateStock(int id, int stock)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return BadRequest($"Book with id {id} was not found");
            }

            book.Stock = stock;
            _bookService.Update(book);

            return Ok(book);
        }

        //DELETE /api/books/{id}
        [HttpDelete("{id:int}")]

        public ActionResult<Book> Delete(int id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return BadRequest($"Book with id {id} was not found");
            }

            _bookService.Delete(book);

            return NoContent();
        }

        //GET /api/books/search?title=code 
        [HttpGet("search")]

        public ActionResult<IEnumerable<Book>> GetByTtile([FromQuery] string title)
        {
            try
            {
                var result = _bookService.GetByTitle(title);

                if (!result.Any()) return BadRequest($"Book with title {title} was not found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET /api/books/author?name=Martin

        [HttpGet("author")]
        public ActionResult<IEnumerable<Book>> GetByAuthorName([FromQuery] string name)
        {
            try
            {
                var result = _bookService.GetByAuthorName(name);

                if (!result.Any()) return BadRequest($"Book with author name {name} was not found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET /api/books/category/{category} 

        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<Book>> GetByCategory(string category)
        {
            try
            {
                var result = _bookService.GetByCategory(category);

                if (!result.Any()) return BadRequest($"Book with category {category} was not found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET /api/books/filter?minPrice=20&maxPrice=100 

        [HttpGet("filter")]

        public ActionResult<IEnumerable<Book>> SortByPrice([FromQuery] decimal? min, [FromQuery] decimal? max)
        {
            var result = _bookService.SortByPrice(min, max);
            return Ok(result);
        }


        //GET /api/books/available 
        [HttpGet("available")]

        public ActionResult<IEnumerable<Book>> GetAvailable()
        {
            var result = _bookService.GetAvailable();

            return Ok(result);
        }

        //GET /api/books/year/{year} 
        [HttpGet("year/{year:int}")]

        public ActionResult<IEnumerable<Book>> GetByYear(int year)
        {
            var result = _bookService.GetByYear(year);

            return Ok(result);
        }

        //GET /api/books/sorted?direction=asc 

        [HttpGet("sorted")]

        public ActionResult<IEnumerable<Book>> GetByYear([FromQuery] string direction)
        {
            var result = _bookService.SortByYear(direction);
            return Ok(result);
        }


        //GET /api/books/statistics 

        [HttpGet("statistics")]

        public ActionResult<Dictionary<string, float>> GetStats()
        {
            Dictionary<string, float> result = new Dictionary<string, float>();
            var allBooks = _bookService.Get().ToList();

            var totalCount = allBooks.Count;
            var totalStock = allBooks.Sum(b => b.Stock);
            var avgPrice = (float)allBooks.Average(b => b.Price);
            var minPrice = (float)allBooks.Min(b => b.Price);
            var maxPrice = (float)allBooks.Max(b => b.Price);

            result.Add("Total books", totalCount);
            result.Add("Total stock", totalStock);
            result.Add("Average price", avgPrice);
            result.Add("Minimum price", minPrice);
            result.Add("Maximum price", maxPrice);

            return Ok(result);
        }
    }
}
