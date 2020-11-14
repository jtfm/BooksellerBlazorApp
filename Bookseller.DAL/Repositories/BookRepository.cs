using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookseller.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bookseller.DAL.Repositories
{
	public class BookRepository
	{
		private readonly BooksellerContext _booksellerContext;

		public BookRepository(BooksellerContext booksellerContext)
		{
			_booksellerContext = booksellerContext;
		}

		public IEnumerable<Book> GetAll()
		{
			return _booksellerContext.Books.ToList();
		}

		public Book GetById(int id)
		{
			return _booksellerContext.Find<Book>(id);
		}

		public Book Add(Book book)
		{
			EntityEntry<Book> added = _booksellerContext.Books.Add(book);
			_booksellerContext.SaveChanges();
			return added.Entity;
		}

		public Book Update(Book book)
		{
			Book bookInDb = _booksellerContext.Books.FirstOrDefault(b => b.Id == book.Id);

			if (bookInDb == null)
			{
				return null;
			}

			bookInDb.Author = book.Author;
			bookInDb.Title = book.Title;

			_booksellerContext.SaveChanges();
			return bookInDb;
		}

		public void Delete(int id)
		{
			Book bookToDelete = _booksellerContext.Books.FirstOrDefault(b => b.Id == id);
			if (bookToDelete == null) return;
			_booksellerContext.Books.Remove(bookToDelete);
			_booksellerContext.SaveChanges();
		}
	}
}
