using System.Collections.Generic;
using Bookseller.DAL.Repositories;
using Bookseller.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookseller.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BooksController : ControllerBase
	{

		private readonly BookRepository _bookRepository;

		private readonly ILogger<BooksController> _logger;

		public BooksController(BookRepository bookRepository, ILogger<BooksController> logger)
		{
			_bookRepository = bookRepository;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(_bookRepository.GetAll());
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			return Ok(_bookRepository.GetById(id));
		}

		[HttpPost]
		public IActionResult Create([FromBody] Book book)
		{
			if (book == null)
			{
				return BadRequest();
			}

			Book addedBook = _bookRepository.Add(book);

			return Created("book", addedBook);
		}

		[HttpPut]
		public IActionResult Update([FromBody] Book book)
		{
			if (book == null)
			{
				return BadRequest();
			}

			Book bookToUpdate = _bookRepository.GetById(book.Id);

			if (bookToUpdate == null)
			{
				return BadRequest(ModelState);
			}

			_bookRepository.Update(book);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Book bookToDelete = _bookRepository.GetById(id);

			if (bookToDelete == null)
			{
				return NotFound();
			}

			_bookRepository.Delete(id);

			return NoContent();
		}
	}
}
