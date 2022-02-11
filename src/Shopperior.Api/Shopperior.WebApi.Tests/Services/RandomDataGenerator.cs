namespace Shopperior.WebApi.Tests.Services;

public class RandomDataGenerator
{
    private readonly Random _random = new Random(DateTime.UtcNow.Millisecond);

    public int RandomInt(int min = int.MinValue, int max = int.MaxValue)
    {
        return _random.Next(min, max);
    }

    public long RandomLong(long min = long.MinValue, long max = long.MaxValue)
    {
        return _random.NextInt64(min, max);
    }

    public double RandomDouble(double min = double.MinValue, double max = double.MaxValue)
    {
        var rndDouble = _random.NextDouble();
        var range = max - min;
        var diff = range * rndDouble;
        var point = min + diff;

        return point;
    }

    public decimal RandomDecimal(int decimals, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        var rndFloat = _random.NextSingle();
        var range = max - min;
        var diff = range * (decimal)rndFloat;
        var point = min + diff;

        return decimal.Round(point, decimals);
    }

    public string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    public bool RandomBoolean()
    {
        var rndFloat = _random.NextSingle();
        return rndFloat < 0.5;
    }
}