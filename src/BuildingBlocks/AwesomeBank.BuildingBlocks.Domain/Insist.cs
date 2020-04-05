namespace AwesomeBank.BuildingBlocks.Domain
{
    using System;

    public static class Insist
    {
        public static void IsNotNull<T>(T item, string parameterName)
        {
            if (item is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void IsNotNullOrWhiteSpace(string item, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentException($"Parameter {parameterName} is null or whitespace.");
            }
        }
    }
}