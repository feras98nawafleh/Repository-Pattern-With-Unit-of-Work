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
        //private readonly IGenericRepository<Book> _booksRepository;
        // injecting UnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            //_booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddBook")]
        public IActionResult AddAuthor(BookDTO bookDTO)
        {
            Book book = new Book()
            {
                Title = bookDTO.Title,
                AuthorId = bookDTO.AuthorId
            };
            //var response = _booksRepository.Add(book);
            var response = _unitOfWork.Books.Add(book);
            _unitOfWork.Commit();
            return Ok(response);
        }

        [HttpGet("GetBookById")]
        public IActionResult GetAuthorById(int id)
        {
            //return Ok(_booksRepository.GetById(id));
            return Ok(_unitOfWork.Books.GetById(id));
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            //return Ok(await _booksRepository.GetAll());
            return Ok(await _unitOfWork.Books.GetAll());
        }

        [HttpPost("GetBookByName")]
        public async Task<IActionResult> GetBookByName(string name, string[] includes)
        {
            //return Ok(await _booksRepository.Find(b => b.Title.ToLower() == name.ToLower() || b.Author.FirstName.ToLower() == name.ToLower() || b.Author.LastName.ToLower() == name.ToLower(), includes));
            return Ok(await _unitOfWork.Books.Find(b => b.Title.ToLower() == name.ToLower() || b.Author.FirstName.ToLower() == name.ToLower() || b.Author.LastName.ToLower() == name.ToLower(), includes));
        }
    }
}
