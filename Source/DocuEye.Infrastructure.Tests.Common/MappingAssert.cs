using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DocuEye.Infrastructure.Tests.Common
{
    public static class MappingAssert
    {
        public static void AssertMapped<TSrc, TDest>(
            TSrc src,
            TDest dest,
            IEnumerable<string>? ignoreDestProps = null,
            IDictionary<string, Func<TSrc, object?>>? customSourceResolvers = null,
            IEnumerable<string>? unorderedProps = null)
        {
            var ignore = new HashSet<string>(ignoreDestProps ?? Enumerable.Empty<string>(), StringComparer.OrdinalIgnoreCase);
            var unordered = new HashSet<string>(unorderedProps ?? Enumerable.Empty<string>(), StringComparer.OrdinalIgnoreCase);

            var destProps = typeof(TDest).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                         .Where(p => p.CanRead);

            foreach (var dp in destProps)
            {
                if (ignore.Contains(dp.Name)) continue;

                var destValue = dp.GetValue(dest);
                var defaultValue = dp.PropertyType.IsValueType ? Activator.CreateInstance(dp.PropertyType) : null;

                // If destination holds the default value, it’s likely unmapped
                if (Equals(destValue, defaultValue))
                    throw new Exception($"Destination property '{dp.Name}' appears unmapped (default value).");

                // Resolve expected source value (custom resolver first)
                object? expected;
                bool hasExpected = false;

                if (customSourceResolvers != null && customSourceResolvers.TryGetValue(dp.Name, out var resolver))
                {
                    expected = resolver(src);
                    hasExpected = true;
                }
                else
                {
                    var sp = typeof(TSrc).GetProperty(dp.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (sp != null && sp.CanRead)
                    {
                        expected = sp.GetValue(src);
                        hasExpected = true;
                    }
                    else
                    {
                        throw new Exception($"Destination property '{dp.Name}' has no matching source or resolver. Add to ignore list or provide resolver.");
                    }
                }

                var orderInsensitive = unordered.Contains(dp.Name);
                if (!AreEqual(expected, destValue, orderInsensitive))
                    throw new Exception(
                        $"Property '{dp.Name}' mismatch. Expected '{FormatValue(expected)}', got '{FormatValue(destValue)}'.");
            }
        }

        private static bool AreEqual(object? a, object? b, bool ignoreOrderForSequences)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;

            // Strings: scalar comparison
            if (a is string sa && b is string sb) return sa == sb;

            // Dictionaries
            if (a is IDictionary da && b is IDictionary db)
                return DictionariesEqual(da, db, ignoreOrderForSequences);

            // Enumerables (arrays, lists)
            if (a is IEnumerable ea && b is IEnumerable eb && !(a is string) && !(b is string))
                return SequencesEqual(ea, eb, ignoreOrderForSequences);

            // Value types / other reference types
            return Equals(a, b);
        }

        private static bool SequencesEqual(IEnumerable a, IEnumerable b, bool ignoreOrder)
        {
            var la = a.Cast<object?>().ToList();
            var lb = b.Cast<object?>().ToList();
            if (la.Count != lb.Count) return false;

            if (!ignoreOrder)
            {
                for (int i = 0; i < la.Count; i++)
                    if (!AreEqual(la[i], lb[i], ignoreOrder)) return false;
                return true;
            }

            // Order-insensitive: multiset compare
            var used = new bool[lb.Count];
            foreach (var ai in la)
            {
                var idx = -1;
                for (int j = 0; j < lb.Count; j++)
                {
                    if (!used[j] && AreEqual(ai, lb[j], ignoreOrder))
                    {
                        idx = j; break;
                    }
                }
                if (idx < 0) return false;
                used[idx] = true;
            }
            return true;
        }

        private static bool DictionariesEqual(IDictionary a, IDictionary b, bool ignoreOrder)
        {
            if (a.Count != b.Count) return false;
            foreach (DictionaryEntry entry in a)
            {
                var key = entry.Key;
                if (!b.Contains(key)) return false;
                if (!AreEqual(entry.Value, b[key], ignoreOrder)) return false;
            }
            return true;
        }

        private static string FormatValue(object? v)
        {
            if (v is null) return "null";
            if (v is string s) return $"\"{s}\"";
            if (v is IEnumerable e && v is not string)
            {
                var items = e.Cast<object?>().Select(FormatValue);
                return "[" + string.Join(", ", items) + "]";
            }
            return v.ToString() ?? "<unprintable>";
        }
    }
}
