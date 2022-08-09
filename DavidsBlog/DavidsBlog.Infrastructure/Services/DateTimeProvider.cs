using DavidsBlog.Application.Common.Interfaces.Services;

namespace DavidsBlog.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
