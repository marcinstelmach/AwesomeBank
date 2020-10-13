namespace AwesomeBank.Tests.Common
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ObjectExtensions
    {
        public static void SetPropertyValue<T, TProperty>(this T obj, Expression<Func<T, TProperty>> propertyPicker, object value)
        {
            var memberExpression = (MemberExpression)propertyPicker.Body;
            var property = (PropertyInfo)memberExpression.Member;

            // ReSharper disable once PossibleNullReferenceException
            obj.GetType().GetProperty(property.Name).SetValue(obj, value);
        }

        public static void SetPrivateFieldValue<T>(this T source, string propertyName, object value)
        {
            // ReSharper disable once PossibleNullReferenceException
            typeof(T).GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(source, value);
        }

        public static TResult GetPrivateFieldValue<T, TResult>(this T source, string propertyName)
        {
            // ReSharper disable once PossibleNullReferenceException
            return (TResult)typeof(T).GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(source);
        }
    }
}