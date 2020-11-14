using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bookseller.Shared;

namespace Bookseller.Client.Services
{
	public class BookDataService : IBookDataService
	{
		private readonly HttpClient _httpClient;

		public BookDataService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<Book>> GetAll()
		{
			return await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(
				await _httpClient.GetStreamAsync("api/books"),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task<Book> GetById(int bookId)
		{
			return await JsonSerializer.DeserializeAsync<Book>(
				await _httpClient.GetStreamAsync($"api/books/{bookId}"),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task<Book> Add(Book book)
		{
			var bookJson = new StringContent(
				JsonSerializer.Serialize(book),
				Encoding.UTF8,
				"application/json");

			HttpResponseMessage response = await _httpClient.PatchAsync("api/books", bookJson);

			if (response.IsSuccessStatusCode)
			{
				return await JsonSerializer.DeserializeAsync<Book>(
					await response.Content.ReadAsStreamAsync());
			}

			return null;
		}

		public async Task Update(Book book)
		{
			var bookJson = new StringContent(
				JsonSerializer.Serialize(book),
				Encoding.UTF8,
				"application/json");

			await _httpClient.PutAsync("api/books", bookJson);
		}

		public async Task Delete(int bookId)
		{
			await _httpClient.DeleteAsync($"api/books/{bookId}");
		}
	}
}
