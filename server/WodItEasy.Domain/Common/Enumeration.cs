namespace WodItEasy.Domain.Common
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Enumeration : IComparable
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<object>> EnumCache = new();

        protected Enumeration(int value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        public int Value { get; }

        public string Name { get; }

        public override string ToString() => this.Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var type = typeof(T);

            var values = EnumCache.GetOrAdd(type, _ =>
            {
                return type
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Select(f => f.GetValue(null)!);
            });

            return values.Cast<T>();
        }

        public static T FromValue<T>(int value) where T : Enumeration
            => Parse<T, int>(value, "value", item => item.Value == value);

        public static T FromName<T>(string name) where T : Enumeration
            => Parse<T, string>(name, "name", item => item.Name == name);

        public static string NameFromValue<T>(int value) where T : Enumeration
            => FromValue<T>(value).Name;

        public static bool HasValue<T>(int value) where T : Enumeration
        {
            try
            {
                FromValue<T>(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate) where T : Enumeration
            => GetAll<T>()
               .FirstOrDefault(predicate) 
               ?? throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = this.GetType() == obj.GetType();
            var valueMatches = this.Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => (this.GetType().ToString() + this.Value).GetHashCode();

        public int CompareTo(object? other) => this.Value.CompareTo(((Enumeration)other!).Value);
    }
}
