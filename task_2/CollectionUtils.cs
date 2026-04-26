using System;
using System.Collections.Generic;

public static class CollectionUtils
{
    // 1. Вернуть новый список без дубликатов, сохраняя порядок первых вхождений.
    public static List<T> Distinct<T>(List<T> source)
    {
        List<T> result = new List<T>();
        HashSet<T> seen = new HashSet<T>();

        foreach (T item in source)
        {
            if (seen.Add(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    // 2. Сгруппировать элементы по ключу, возвращаемому селектором.
    public static Dictionary<TKey, List<TValue>> GroupBy<TValue, TKey>(
        List<TValue> source,
        Func<TValue, TKey> keySelector) where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> result = new Dictionary<TKey, List<TValue>>();

        foreach (TValue item in source)
        {
            TKey key = keySelector(item);

            if (!result.ContainsKey(key))
            {
                result[key] = new List<TValue>();
            }

            result[key].Add(item);
        }

        return result;
    }

    // 3. Объединить два словаря. При конфликте ключей применить resolver.
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        Dictionary<TKey, TValue> first,
        Dictionary<TKey, TValue> second,
        Func<TValue, TValue, TValue> conflictResolver) where TKey : notnull
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(first);

        foreach (var kvp in second)
        {
            if (result.ContainsKey(kvp.Key))
            {
                result[kvp.Key] = conflictResolver(result[kvp.Key], kvp.Value);
            }
            else
            {
                result[kvp.Key] = kvp.Value;
            }
        }

        return result;
    }

    // 4. Найти элемент с максимальным значением селектора.
    // Если коллекция пуста — выбросить InvalidOperationException.
    public static T MaxBy<T, TKey>(List<T> source, Func<T, TKey> selector)
        where TKey : IComparable<TKey>
    {
        if (source == null || source.Count == 0)
        {
            throw new InvalidOperationException("Source collection is empty.");
        }

        T maxItem = source[0];
        TKey maxValue = selector(maxItem);

        foreach (T item in source)
        {
            TKey value = selector(item);
            if (value.CompareTo(maxValue) > 0)
            {
                maxItem = item;
                maxValue = value;
            }
        }

        return maxItem;
    }
}
