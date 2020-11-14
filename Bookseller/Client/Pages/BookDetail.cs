using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bookseller.Shared;
using Microsoft.AspNetCore.Components;

namespace Bookseller.Client.Pages
{
    public partial class BookDetail
    {
        [Parameter] public string BookId { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public Book Book { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Book = await HttpClient.GetFromJsonAsync<Book>($"api/books/{BookId}");
        }
    }
}
