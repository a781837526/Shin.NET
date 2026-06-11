// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 泛型集合扩展
/// </summary>
public static class ListExtensions
{
    public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
    {
        foreach (var value in list)
        {
            await func(value);
        }
    }

    public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
    {
        foreach (var value in source)
        {
            await action(value);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> consumer)
    {
        foreach (T item in enumerable)
        {
            consumer(item);
        }
    }

    public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
    {
        if (list is List<T> list2)
        {
            list2.AddRange(items);
            return;
        }

        foreach (T item in items)
        {
            list.Add(item);
        }
    }
}