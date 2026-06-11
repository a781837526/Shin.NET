// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Common.Internal;

/// <summary>
/// 对象拓展类
/// </summary>
[SuppressSniffer]
public static class ObjectExtensions
{
    /// <summary>
    /// JsonElement 转 Object
    /// </summary>
    /// <param name="jsonElement"></param>
    /// <returns></returns>
    internal static object ToObject(this JsonElement jsonElement)
    {
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.String:
                return jsonElement.GetString();

            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                return default;

            case JsonValueKind.Number:
                return jsonElement.GetDecimal();

            case JsonValueKind.True:
            case JsonValueKind.False:
                return jsonElement.GetBoolean();

            case JsonValueKind.Object:
                var enumerateObject = jsonElement.EnumerateObject();
                var dic = new Dictionary<string, object>();
                foreach (var item in enumerateObject)
                {
                    dic.Add(item.Name, item.Value.ToObject());
                }
                return dic;

            case JsonValueKind.Array:
                var enumerateArray = jsonElement.EnumerateArray();
                var list = new List<object>();
                foreach (var item in enumerateArray)
                {
                    list.Add(item.ToObject());
                }
                return list;

            default:
                return default;
        }
    }
}