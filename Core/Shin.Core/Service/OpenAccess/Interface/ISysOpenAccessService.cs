// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 开放接口身份服务接口
/// </summary>
public interface ISysOpenAccessService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 生成签名
    /// </summary>
    string GenerateSignature(GenerateSignatureInput input);

    /// <summary>
    /// 获取开放接口身份分页列表
    /// </summary>
    Task<SqlSugarPagedList<OpenAccessOutput>> Page(OpenAccessInput input);

    /// <summary>
    /// 增加开放接口身份
    /// </summary>
    Task AddOpenAccess(AddOpenAccessInput input);

    /// <summary>
    /// 更新开放接口身份
    /// </summary>
    Task UpdateOpenAccess(UpdateOpenAccessInput input);

    /// <summary>
    /// 删除开放接口身份
    /// </summary>
    Task DeleteOpenAccess(DeleteOpenAccessInput input);

    /// <summary>
    /// 创建密钥
    /// </summary>
    Task<string> CreateSecret();

    /// <summary>
    /// 根据 Key 获取对象
    /// </summary>
    Task<SysOpenAccess> GetByKey(string accessKey);
}