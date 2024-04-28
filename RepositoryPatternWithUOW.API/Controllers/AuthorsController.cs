using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.DTOs;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IGenericRepository<Author> _authorsRepository;
        public AuthorsController(IGenericRepository<Author> authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        [HttpPost("AddAuthor")]
        public IActionResult AddAuthor(AuthorDTO authorDTO)
        {
            Author author = new Author() 
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName
            };
            var response = _authorsRepository.Add(author);
            return Ok(response);
        }

        [HttpGet("GetAuthorById")]
        public IActionResult GetAuthorById(int id)
        {
            return Ok(_authorsRepository.GetById(id));
        }

        [HttpGet("GetAuthorByName")]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            return Ok( await _authorsRepository.Find(a => a.FirstName.ToLower() == name.ToLower() || a.LastName.ToLower() == name.ToLower()));
        }
    }
}
