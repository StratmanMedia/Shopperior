using System;
using System.Linq;
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

    protected int RandomInt(int min = int.MaxValue, int max = int.MaxValue)
    {
        return _random.Next(min, max);
    }

    protected long RandomLong(long min = long.MaxValue, long max = long.MaxValue)
    {
        return _random.NextInt64(min, max);
    }

    public string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    public void Dispose()
    {
        Mock?.Dispose();
    }
}