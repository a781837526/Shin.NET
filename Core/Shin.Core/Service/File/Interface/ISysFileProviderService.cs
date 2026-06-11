// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统文件存储提供者服务接口
/// </summary>
public interface ISysFileProviderService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取文件存储提供者分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysFileProvider>> GetFileProviderPage([FromQuery] PageFileProviderInput input);

    /// <summary>
    /// 获取文件存储提供者列表
    /// </summary>
    Task<List<SysFileProvider>> GetFileProviderList();

    /// <summary>
    /// 增加文件存储提供者
    /// </summary>
    Task AddFileProvider(AddFileProviderInput input);

    /// <summary>
    /// 批量启用/禁用存储提供者
    /// </summary>
    Task BatchEnableProvider(BatchEnableProviderInput input);

    /// <summary>
    /// 更新文件存储提供者
    /// </summary>
    Task UpdateFileProvider(UpdateFileProviderInput input);

    /// <summary>
    /// 删除文件存储提供者
    /// </summary>
    Task DeleteFileProvider(DeleteFileProviderInput input);

    /// <summary>
    /// 获取文件存储提供者详情
    /// </summary>
    Task<SysFileProvider> GetFileProvider([FromQuery] QueryFileProviderInput input);

    /// <summary>
    /// 根据提供者和存储桶获取配置
    /// </summary>
    Task<SysFileProvider?> GetFileProviderByBucket(string provider, string bucketName);

    /// <summary>
    /// 根据ID获取配置
    /// </summary>
    Task<SysFileProvider?> GetFileProviderById(long id);

    /// <summary>
    /// 根据存储桶名称获取存储提供者
    /// </summary>
    /// <param name="bucketName">存储桶名称</param>
    Task<SysFileProvider?> GetProviderByBucketName(string bucketName);

    /// <summary>
    /// 获取默认存储提供者
    /// </summary>
    Task<SysFileProvider?> GetDefaultProvider();

    /// <summary>
    /// 获取默认存储提供者信息
    /// </summary>
    Task<SysFileProvider?> GetDefaultProviderInfo();

    /// <summary>
    /// 设置默认存储提供者 🔖
    /// </summary>
    Task SetDefaultProvider(SetDefaultProviderInput input);

    /// <summary>
    /// 获取存储提供者统计信息
    /// </summary>
    Task<object> GetProviderStatistics();

    /// <summary>
    /// 获取缓存的文件提供者列表
    /// </summary>
    Task<List<SysFileProvider>> GetCachedFileProviders();

    /// <summary>
    /// 清除缓存
    /// </summary>
    Task ClearCache();

    /// <summary>
    /// 获取所有可用的存储桶列表
    /// </summary>
    Task<List<string>> GetAvailableBuckets();

    /// <summary>
    /// 获取存储桶和提供者的映射关系
    /// </summary>
    Task<Dictionary<string, List<SysFileProvider>>> GetBucketProviderMapping();
}