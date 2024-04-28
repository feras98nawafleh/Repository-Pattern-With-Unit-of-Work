using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.DTOs;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IGenericRepository<Book> _booksRepository;
        public BooksController(IGenericRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpPost("AddBook")]
        public IActionResult AddAuthor(BookDTO bookDTO)
        {
            Book book = new Book()
            {
                Title = bookDTO.Title,
                AuthorId = bookDTO.AuthorId
            };
            var response = _booksRepository.Add(book);
            return Ok(response);
        }

        [HttpGet("GetBookById")]
        public IActionResult GetAuthorById(int id)
        {
            return Ok(_booksRepository.GetById(id));
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _booksRepository.GetAll());
        }

        [HttpPost("GetBookByName")]
        public async Task<IActionResult> GetBookByName(string name, string[] includes)
        {
            return Ok(await _booksRepository.Find(b => b.Title.ToLower() == name.ToLower() || b.Author.FirstName.ToLower() == name.ToLower() || b.Author.LastName.ToLower() == name.ToLower(), includes));
        }
    }
}
