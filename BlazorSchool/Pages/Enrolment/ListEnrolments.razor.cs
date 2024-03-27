using BlazorSchoolShared.Dto;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.Enrolment;

public partial class ListEnrolments
{

    public List<EnrolmentDto>? EnrolmentDtos { get; set; } = new List<EnrolmentDto>();
    protected override async Task OnInitializedAsync()
    {
        EnrolmentDtos = await HttpClient!.GetFromJsonAsync<List<EnrolmentDto>>("Enrolment");
    }
    public void NavidateToSave(int? id)
    {
        Manager!.NavigateTo($"/Enrolment/Save/{id}");
    }

    public async Task Delete(int id)
    {
        bool? result = await DialogService!.ShowMessageBox(
            "Warning",
            "Are you sure you want to delete?",
            yesText: "Yes", noText: "No");
        if (result.HasValue)
        {
            await HttpClient?.DeleteAsync($"Enrolment/{id}")!;
            EnrolmentDtos = (await HttpClient.GetFromJsonAsync<List<EnrolmentDto>>("Enrolment"))!;
        }
    }
}