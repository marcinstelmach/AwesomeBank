namespace AwesomeBank.Tests.Common
{
    public static class ObjectExtensions
    {
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            // ReSharper disable once PossibleNullReferenceException
            obj.GetType().GetProperty(propertyName).SetValue(obj, value);
        }
    }
}