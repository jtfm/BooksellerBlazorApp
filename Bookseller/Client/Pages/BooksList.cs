using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bookseller.Shared;
using Microsoft.AspNetCore.Components;

namespace Bookseller.Client.Pages
{
    public partial class BooksList : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        public IEnumerable<Book> Books { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Books = await HttpClient.GetFromJsonAsync<IEnumerable<Book>>("api/books");
        }
    }
}
