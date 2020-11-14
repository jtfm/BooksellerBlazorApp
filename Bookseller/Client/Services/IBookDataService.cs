using System.Collections.Generic;
using System.Threading.Tasks;
using Bookseller.Shared;

namespace Bookseller.Client.Services
{
    public interface IBookDataService
    {
        public Task<IEnumerable<Book>> GetAll();

        public Task<Book> GetById(int bookId);

        public Task<Book> Add(Book book);

        public Task Update(Book book);

        public Task Delete(int bookId);
    }
}
