using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorSchool.Pages;

public class BaseSavePage<T>:ComponentBase where T:new()
{
        [Parameter]
        public int? Id { get; set; }

        [Inject] 
        protected  HttpClient? Client { get; set; }
        
        [Inject] 
        protected  NavigationManager? Manager { get; set; }

        protected string Url{ get; set; }
        public BaseSavePage(string url)
        {
            Url=url;
        }

        protected T Dto{ get; set; }= new ();
        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                Dto = (await Client!.GetFromJsonAsync<T>($"{Url}/{Id.Value}"))!;
            }
        }
        protected async Task<bool> Save()
        {
            HttpResponseMessage message;
            if (Id.HasValue)
            {
               message= await Client!.PutAsJsonAsync($"{Url}/{Id.Value}", Dto);
            }
            else
            {
               message=await Client!.PostAsJsonAsync($"/{Url}", Dto);
            }   
            return message.IsSuccessStatusCode;        
        }
}
