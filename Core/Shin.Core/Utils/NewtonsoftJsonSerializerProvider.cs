// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Shin.Core;

/// <summary>
/// 自定义序列化提供器 Newtonsoft.Json 实现
/// </summary>
public class NewtonsoftJsonSerializerProvider : IJsonSerializerProvider, ISingleton
{
    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="value"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public string Serialize(object value, object jsonSerializerOptions = null)
    {
        return JsonConvert.SerializeObject(value, (jsonSerializerOptions ?? GetSerializerOptions()) as JsonSerializerSettings);
    }

    /// <summary>
    /// 反序列化字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public T Deserialize<T>(string json, object jsonSerializerOptions = null)
    {
        return JsonConvert.DeserializeObject<T>(json, (jsonSerializerOptions ?? GetSerializerOptions()) as JsonSerializerSettings);
    }

    /// <summary>
    /// 反序列化字符串
    /// </summary>
    /// <param name="json"></param>
    /// <param name="returnType"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public object Deserialize(string json, Type returnType, object jsonSerializerOptions = null)
    {
        return JsonConvert.DeserializeObject(json, returnType, (jsonSerializerOptions ?? GetSerializerOptions()) as JsonSerializerSettings);
    }

    /// <summary>
    /// 返回读取全局配置的 JSON 选项
    /// </summary>
    /// <returns></returns>
    public object GetSerializerOptions()
    {
        return App.GetOptions<MvcNewtonsoftJsonOptions>()?.SerializerSettings;
    }
}