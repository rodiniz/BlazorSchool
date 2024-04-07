using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BlazorSchool.Pages.CourseCycle;

public partial class SaveCourseCycle
{
    [Inject] public IDialogService DialogService { get; set; }

    [Inject] public HttpClient HttpClient { get; set; }
    private CourseCycleDto CourseCycle { get; set; } = new();

    
    protected override void OnInitialized()
    {
        CourseCycle ??= new();
    }

    public void Delete(CourseTeacherDto courseTeacherDto)
    {
        CourseCycle.CourseTeachers.Remove(courseTeacherDto);
        StateHasChanged();
    }

    public async Task ShowDialog(CourseTeacherDto? courseTeacherDto)
    {
        var parameters = new DialogParameters<CourseTeacherDialog>();
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
        if (courseTeacherDto is not null)
        {
            parameters.Add(x => x.CourseTeacher, courseTeacherDto);
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
        if (CourseCycle.Id != 0)
        {
            await HttpClient.PutAsJsonAsync("CourseCycle", CourseCycle);
        }
        else
        {
            await HttpClient.PostAsJsonAsync("CourseCycle", CourseCycle);
        }

    }

    private async void LoadData(int? i)
    {
        CourseCycle = await HttpClient.GetFromJsonAsync<CourseCycleDto>("CourseCycle");
    }
}