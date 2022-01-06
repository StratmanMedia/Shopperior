using System.Text;

namespace StratmanMedia.Exceptions.Extensions;

public static class ExceptionExtensions
{
    public static string JoinAllMessages(this Exception ex)
    {
        var sb = new StringBuilder();
        var exception = ex;
        while (exception != null)
        {
            sb.AppendJoin('|', exception.Message);
            exception = ex.InnerException;
        }

        return sb.ToString();
    }
}