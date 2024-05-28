using RazorClassLibrary1;

namespace TestProject1;

[TestClass]
public class UnitTest1 : BunitTestWrapper
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public Task ErrorBody_UnexpectedExceptionWithCorrelationId_RendersCorrectlyWithDetails()
    {
        // Arrange
        Exception exception = new("9de73937-eb78-4d07-9c5a-1abb4d590f80");

        // Act
        IRenderedComponent<ErrorComponent> component = _testContext?.RenderComponent<ErrorComponent>(
            p => p.Add(x => x.UnexpectedError, exception));

        // Assert
        return Verify(component);
    }
}
