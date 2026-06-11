// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统枚举服务接口
/// </summary>
public interface ISysEnumService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取所有枚举类型 🔖
    /// </summary>
    List<EnumTypeOutput> GetEnumTypeList();

    /// <summary>
    /// 通过枚举类型获取枚举值集合
    /// </summary>
    List<EnumEntity> GetEnumDataList([FromQuery] EnumInput input);

    /// <summary>
    /// 通过实体的字段名获取相关枚举值集合（目前仅支持枚举类型） 🔖
    /// </summary>
    List<EnumEntity> GetEnumDataListByField(QueryEnumDataInput input);
}