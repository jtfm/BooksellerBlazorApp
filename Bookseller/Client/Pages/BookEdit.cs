using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bookseller.Client.Services;
using Bookseller.Shared;
using Microsoft.AspNetCore.Components;

namespace Bookseller.Client.Pages
{
    public partial class BookEdit : ComponentBase
    {
        [Parameter]
        public string BookId { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IBookDataService BookDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Book Book { get; set; }

        public bool Saved { get; set; }

        public string StatusClass { get; set; }

        public string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(BookId, out int bookId);

            if (bookId == 0)
            {
                Book = new Book { Title = "Default Title", Author = "Default Author" };
            }
            else
            {
                Book = await BookDataService.GetById(bookId);
            }

        }

        protected async Task HandleValidSubmit()
        {
            if (Book.Id == 0)
            {
                Book book = await BookDataService.Add(Book);

                if (book != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                //HttpResponseMessage response = await HttpClient.PutAsJsonAsync($"api/books/{BookId}", Book);

                await BookDataService.Update(Book);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async void DeleteBook()
        {
            await BookDataService.Delete(Book.Id);
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/books");
        }
    }
}
