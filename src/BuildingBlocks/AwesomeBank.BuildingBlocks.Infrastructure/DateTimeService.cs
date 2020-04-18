namespace AwesomeBank.BuildingBlocks.Infrastructure
{
    using System;
    using AwesomeBank.BuildingBlocks.Application;

    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset GetDateTimeOffsetUtcNow()
            => DateTimeOffset.UtcNow;

        public DateTime GetDateTimeUtcNow()
            => DateTime.UtcNow;
    }
}