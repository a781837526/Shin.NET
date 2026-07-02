// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Common;

/// <summary>
/// 集合扩展
/// </summary>
public static class CollectionHelper
{
    /// <summary>
    /// 集合转为数组，加锁确保安全
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="collection">集合</param>
    /// <returns>数组副本，永不返回 null</returns>
    public static T[] ToArray<T>(this ICollection<T> collection)
    {
        lock (collection)
        {
            int count = collection.Count;
            if (count == 0)
            {
                return Array.Empty<T>();
            }

            T[] array = new T[count];
            collection.CopyTo(array, 0);
            return array;
        }
    }

    /// <summary>
    /// 将对象转成字典
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Dictionary<string, object> ToIDictionary(this object input)
    {
        if (input == null) return default;

        if (input is IDictionary<string, object> dictionary)
            return dictionary.ToDictionary();
        if (input is Dictionary<string, object> dic2)
            return dic2;

        if (input is Clay clay && clay.IsObject)
        {
            var dic = new Dictionary<string, object>();
            foreach (KeyValuePair<string, dynamic> item in (dynamic)clay)
            {
                dic.Add(item.Key, item.Value is Clay v ? v.ToDictionary() : item.Value);
            }
            return dic;
        }

        if (input is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
        {
            return jsonElement.ToObject() as Dictionary<string, object>;
        }

        var properties = input.GetType().GetProperties();
        var fields = input.GetType().GetFields();
        var members = properties.Concat(fields.Cast<MemberInfo>());

        return members.ToDictionary(m => m.Name, m => GetValue(input, m));
    }

    /// <summary>
    /// 将对象转字典类型，其中值返回原始类型 Type 类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IDictionary<string, Tuple<Type, object>> ToDictionaryWithType(this object input)
    {
        if (input == null) return default;

        if (input is IDictionary<string, object> dictionary)
            return dictionary.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value == null ?
                    new Tuple<Type, object>(typeof(object), kvp.Value) :
                    new Tuple<Type, object>(kvp.Value.GetType(), kvp.Value)
            );

        var dict = new Dictionary<string, Tuple<Type, object>>();

        // 获取所有属性列表
        foreach (var property in input.GetType().GetProperties())
        {
            dict.Add(property.Name, new Tuple<Type, object>(property.PropertyType, property.GetValue(input, null)));
        }

        // 获取所有成员列表
        foreach (var field in input.GetType().GetFields())
        {
            dict.Add(field.Name, new Tuple<Type, object>(field.FieldType, field.GetValue(input)));
        }

        return dict;
    }

    /// <summary>
    /// 获取成员值
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    private static object GetValue(object obj, MemberInfo member)
    {
        if (member is PropertyInfo info)
            return info.GetValue(obj, null);

        if (member is FieldInfo info1)
            return info1.GetValue(obj);

        throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
    }
}