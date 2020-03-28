namespace AwesomeBank.BuildingBlocks.Infrastructure.Tests
{
    using System;
    using AwesomeBank.BuildingBlocks.Application;

    public class FakeCommand : ICommand
    {
        public string FirstProperty { get; set; }

        public int SecondProperty { get; set; }

        public DateTime ThirdProperty { get; set; }
    }
}