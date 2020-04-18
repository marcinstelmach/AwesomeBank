namespace AwesomeBank.BuildingBlocks.Application
{
    using System;

    public interface IDateTimeService
    {
        DateTimeOffset GetDateTimeOffsetUtcNow();

        DateTime GetDateTimeUtcNow();
    }
}