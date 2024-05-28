using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace TestProject1;

public class BunitTestWrapper : VerifyBase
{
    public Bunit.TestContext _testContext;

    [TestInitialize]
    public void Setup()
    {
        _testContext = new Bunit.TestContext();

        Dictionary<string, string> myConfiguration = new()
        {
            {"ClientVersion", "1.1.1.1"}
        };

        IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(myConfiguration).Build();
        _testContext.Services.AddSingleton<IConfiguration>(configuration);
        _testContext.Services.AddSingleton<NavigationManager, TestNavigationManager>();

    }

    [TestCleanup]
    public void TearDown() => _testContext.Dispose();
}
