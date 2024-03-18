using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorSchool;

public class BaseSavePage<T>:ComponentBase where T:new()
{
        [Parameter]
        public int? Id { get; set; }

        [Inject] 
        private  HttpClient _client { get; set; }
        
        [Inject] 
        protected  NavigationManager _Manager { get; set; }

        private string _url{ get; set; }
        public BaseSavePage(string url)
        {
            _url=url;
        }

        protected T dto{ get; set; }= new ();
        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                dto = (await _client.GetFromJsonAsync<T>($"{_url}/{Id.Value}"))!;
            }
        }
        protected async Task<bool> Save()
        {
            HttpResponseMessage message;
            if (Id.HasValue)
            {
               message= await _client.PutAsJsonAsync($"{_url}/{Id.Value}", dto);
            }
            else
            {
               message=await _client.PostAsJsonAsync($"/{_url}", dto);
            }   
            return message.IsSuccessStatusCode;        
        }
}
