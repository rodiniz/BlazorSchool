#nullable enable
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.CourseCycle;

public partial class SaveCourseCycle
{
    [Inject] public IDialogService DialogService { get; set; }

    [Inject] public HttpClient HttpClient { get; set; }
    private CourseCycleDto CourseCycle { get; set; } = new();

    [Inject]
    protected NavigationManager? Manager { get; set; }

    [Parameter]
    public int? Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        if (Id.HasValue)
        {
            CourseCycle = (await HttpClient!.GetFromJsonAsync<CourseCycleDto>($"CourseCycle/{Id.Value}"))!;
        }
    }

    public void Delete(CourseTeacherDto courseTeacherDto)
    {
        CourseCycle.CourseTeachers.Remove(courseTeacherDto);
        StateHasChanged();
    }

    public async Task ShowDialog(CourseTeacherDto? courseTeacherDto)
    {
        var parameters = new DialogParameters<CourseTeacherDialog>();
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small };
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
            await HttpClient.PutAsJsonAsync($"CourseCycle/{CourseCycle.Id}", CourseCycle);
        }
        else
        {
            await HttpClient.PostAsJsonAsync("CourseCycle", CourseCycle);
        }
        Manager!.NavigateTo("/CourseCycle/List");

    }

}