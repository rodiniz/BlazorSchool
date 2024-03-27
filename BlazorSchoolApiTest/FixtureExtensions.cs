using AutoFixture;
using Moq;

namespace BlazorSchoolApiTest;

public static class FixtureExtensions
{
    public static Mock<T> FreezeMock<T>(this Fixture fixture) where T : class
    {
        var mock = new Mock<T>();
        fixture.Register(() => mock.Object);
        return mock;
    }
}