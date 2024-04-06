using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.CourseCycle;

public partial class ListCourseCycle
{

    [Inject] public IDialogService DialogService { get; set; }

    [Inject] public HttpClient HttpClient { get; set; }
    public CourseCycleDto CourseCycle { get; set; } = new();

    public List<CourseDto>? Courses { get; set; }

    public async Task Delete(int id)
    {
        bool? result = await DialogService!.ShowMessageBox(
            "Warning",
            "Are you sure you want to delete?",
            yesText: "Yes", noText: "No");
        if (result.HasValue)
        {


        }
    }

    public async Task ShowDialog(int? id)
    {
        var parameters = new DialogParameters<CourseTeacherDialog>();
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
        if (id.HasValue)
        {
            parameters.Add(x => x.Id, id);
        }
        var dialog = await DialogService.ShowAsync<CourseTeacherDialog>("Course Cycle", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            CourseCycle.CourseTeachers.Add(result.Data as CourseTeacherDto);
            StateHasChanged();
        }

    }

    private async Task Save()
    {
        //TODO VALIDATE
        await HttpClient.PostAsJsonAsync("CourseCycle", CourseCycle);
    }
}