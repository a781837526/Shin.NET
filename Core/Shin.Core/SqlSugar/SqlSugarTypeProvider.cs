// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using System.Linq.Dynamic.Core.CustomTypeProviders;

namespace Shin.Core;

/// <summary>
/// 扩展支持 SqlFunc，不支持 Subqueryable
/// </summary>
public class SqlSugarTypeProvider : DefaultDynamicLinqCustomTypeProvider
{
    public SqlSugarTypeProvider(bool cacheCustomTypes = true) : base(ParsingConfig.Default, cacheCustomTypes)
    {
    }

    public override HashSet<Type> GetCustomTypes()
    {
        var customTypes = base.GetCustomTypes();
        customTypes.Add(typeof(SqlFunc));
        return customTypes;
    }
}