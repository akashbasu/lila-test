using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.IoC
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class DependencyAttribute : Attribute
    {
        public static List<FieldInfo> GetUnresolvedDependencies(Type type, object instance)
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(field =>
                IsDefined(field, typeof(DependencyAttribute)) && field.GetValue(instance) == null).ToList();
        }
    }
}