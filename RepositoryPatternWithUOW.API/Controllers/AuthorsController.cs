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
        //private readonly IGenericRepository<Author> _authorsRepository;
        // injecting UnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            // _authorsRepository = authorsRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor(AuthorDTO authorDTO)
        {
            Author author = new Author() 
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName
            };
            var response = await _unitOfWork.Authors.Add(author);
            await _unitOfWork.Commit();
            return Ok(response);
        }

        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            //return Ok(_authorsRepository..GetById(id));
            return Ok(await _unitOfWork.Authors.GetById(id));
        }

        [HttpGet("GetAuthorByName")]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            //return Ok( await _authorsRepository.Find(a => a.FirstName.ToLower() == name.ToLower() || a.LastName.ToLower() == name.ToLower()));
            return Ok( await _unitOfWork.Authors.Find(a => a.FirstName.ToLower().Contains(name.ToLower()) || a.LastName.ToLower().Contains(name.ToLower())));
        }
    }
}
