// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 缓存管理.
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// 申请分布式锁 🔖
    /// </summary>
    /// <param name="key">要锁定的key</param>
    /// <param name="msTimeout">申请锁等待的时间，单位毫秒</param>
    /// <param name="msExpire">锁过期时间，超过该时间没有主动是放则自动是放，必须整数秒，单位毫秒</param>
    /// <param name="throwOnFailure">失败时是否抛出异常,如不抛出异常，可通过判断返回null得知申请锁失败</param>
    IDisposable? BeginCacheLock(string key, int msTimeout = 500, int msExpire = 10000, bool throwOnFailure = true);

    /// <summary>
    /// 获取缓存键名集合 🔖
    /// </summary>
    List<string> GetKeyList();

    /// <summary>
    /// 增加缓存
    /// </summary>
    bool Set(string key, object value);

    /// <summary>
    /// 增加缓存并设置过期时间
    /// </summary>
    bool Set(string key, object value, TimeSpan expire);

    Task<TR> AdGetAsync<TR>(String cacheName, Func<Task<TR>> del, TimeSpan? expiry = default) where TR : class;

    Task<TR> AdGetAsync<TR, T1>(String cacheName, Func<T1, Task<TR>> del, T1 t1, TimeSpan? expiry = default) where TR : class;

    Task<TR> AdGetAsync<TR, T1, T2>(String cacheName, Func<T1, T2, Task<TR>> del, T1 t1, T2 t2, TimeSpan? expiry = default) where TR : class;

    Task<TR> AdGetAsync<TR, T1, T2, T3>(String cacheName, Func<T1, T2, T3, Task<TR>> del, T1 t1, T2 t2, T3 t3, TimeSpan? expiry = default) where TR : class;

    T Get<T>(String cacheName, object t1);

    T Get<T>(String cacheName, object t1, object t2);

    T Get<T>(String cacheName, object t1, object t2, object t3);

    /// <summary>
    /// 获取缓存的剩余生存时间
    /// </summary>
    TimeSpan GetExpire(string key);

    /// <summary>
    /// 获取缓存
    /// </summary>
    T Get<T>(string key);

    /// <summary>
    /// 删除缓存
    /// </summary>
    int Remove(string key);

    /// <summary>
    /// 清空所有缓存
    /// </summary>
    void Clear();

    /// <summary>
    /// 检查缓存是否存在
    /// </summary>
    /// <param name="key">键</param>
    bool ExistKey(string key);

    /// <summary>
    /// 根据键名前缀删除缓存
    /// </summary>
    /// <param name="prefixKey">键名前缀</param>
    int RemoveByPrefixKey(string prefixKey);

    /// <summary>
    /// 根据键名前缀获取键名集合
    /// </summary>
    /// <param name="prefixKey">键名前缀</param>
    List<string> GetKeysByPrefixKey(string prefixKey);

    /// <summary>
    /// 获取缓存值
    /// </summary>
    object GetValue(string key);

    /// <summary>
    /// 获取或添加缓存（在数据不存在时执行委托请求数据）
    /// </summary>
    T GetOrAdd<T>(string key, Func<string, T> callback, int expire = -1);

    /// <summary>
    /// Hash匹配
    /// </summary>
    IDictionary<String, T> GetHashMap<T>(string key);

    /// <summary>
    /// 批量添加HASH
    /// </summary>
    bool HashSet<T>(string key, Dictionary<string, T> dic);

    /// <summary>
    /// 添加一条HASH
    /// </summary>
    void HashAdd<T>(string key, string hashKey, T value);

    /// <summary>
    /// 添加或更新一条HASH
    /// </summary>
    void HashAddOrUpdate<T>(string key, string hashKey, T value);

    /// <summary>
    /// 获取多条HASH
    /// </summary>
    List<T> HashGet<T>(string key, params string[] fields);

    /// <summary>
    /// 获取一条HASH
    /// </summary>
    T HashGetOne<T>(string key, string field);

    /// <summary>
    /// 根据KEY获取所有HASH
    /// </summary>
    IDictionary<string, T> HashGetAll<T>(string key);

    /// <summary>
    /// 删除HASH
    /// </summary>
    int HashDel<T>(string key, params string[] fields);
}