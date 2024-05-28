using Bunit.Rendering;
using Microsoft.AspNetCore.Components;

namespace TestProject1;
public class TestNavigationManager : NavigationManager
{
    private readonly ITestRenderer _renderer;

    public TestNavigationManager(ITestRenderer renderer)
    {
        Initialize("https://localhost/", "https://localhost/");
        _renderer = renderer;
    }

    protected override void NavigateToCore(string uri, bool forceLoad)
    {
        Uri = ToAbsoluteUri(uri).ToString();

        _renderer.Dispatcher.InvokeAsync(() => NotifyLocationChanged(isInterceptedLink: false));
    }
}
