using AutoFixture;
using MudBlazor.Services;

namespace BlazorSchoolTest;

public abstract class BaseMudBlazorTest: TestContext
{
    protected readonly Fixture Fixture = new ();
    protected BaseMudBlazorTest()
    {
        var authContext = this.AddTestAuthorization();
        authContext.SetAuthorizing();

        Services.AddMudServices();
        JSInterop.SetupVoid("mudPopover.initialize", "mudblazor-main-content", 0);
        JSInterop.SetupVoid("mudPopover.connect", _ => true);
        JSInterop.SetupVoid("mudKeyInterceptor.connect", _ => true);
    }
}