using System;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shopperior.Tests;

[TestClass]
public class BaseTest<T> : IDisposable
{
    private readonly Random _random = new Random(DateTime.UtcNow.Millisecond);
    protected AutoMock Mock { get; } = AutoMock.GetLoose();

    protected T GetSystemUnderTest()
    {
        return Mock.Create<T>();
    }

    protected long RandomLong(long min = long.MaxValue, long max = long.MaxValue)
    {
        return _random.NextInt64(min, max);
    }

    public void Dispose()
    {
        Mock?.Dispose();
    }
}