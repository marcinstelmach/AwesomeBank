namespace AwesomeBank.BuildingBlocks.Infrastructure.Tests
{
    using System;
    using AwesomeBank.BuildingBlocks.Application;

    public class FakeQuery : IQuery<string>
    {
        public string FirstProperty { get; set; }

        public int SecondProperty { get; set; }

        public DateTime ThirdProperty { get; set; }
    }
}